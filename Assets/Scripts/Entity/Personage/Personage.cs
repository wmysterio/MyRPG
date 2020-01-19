/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using UnityEngine;
using MyRPG.AnimGroups;
using MyRPG.Patterns.Strategy.PersonageTasks;

namespace MyRPG {

    public abstract class Personage : Entity {

        public const int MIN_LEVEL = 1;
        public const int MAX_LEVEL = 100;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private RelationshipOfPersonage relationship = RelationshipOfPersonage.Friendly;
        private EffectList effects = new EffectList();
        private SkillBook spellBook = new SkillBook();
        private IPersonageTask currentTask, defaultTask;
        private GroupOfAnimation animationGroup;
        private Characteristic baseCharacteristic;
        private TypeOfPersonage type;
        private RankOfPersonage rank;
        private Bag loot;
        private int level;
        private bool isPlayer, isDead;
        private float currentHealth, currentMana, currentEnergy;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        protected Characteristic currentCharacteristic;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public TypeOfPersonage Type { get { return type; } }
        public RankOfPersonage Rank { get { return rank; } }
        public GroupOfAnimation AnimationGroup { get { return animationGroup; } }
        public Bag Loot { get { return loot; } }
        public EffectList Effects { get { return effects; } }
        public SkillBook SpellBook { get { return spellBook; } }
        public Characteristic CurrentCharacteristic { get { return currentCharacteristic; } }
        public bool IsPlayer { get { return isPlayer; } }
        public bool IsDead { get { return isDead; } }
        public int Level { get { return level; } }
        public float CurrentHealth { get { return currentHealth; } }
        public float CurrentMana { get { return currentMana; } }
        public float CurrentEnergy { get { return currentEnergy; } }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public RelationshipOfPersonage Relationship {
            get { return relationship; }
            set {
                if( isPlayer )
                    return;
                relationship = value;
            }
        }
        public Personage Target { get; set; }
        public bool Immortal { get; set; }
        public bool Targetable { get; set; } = true;
        public bool IsContollable { get; set; } = true;
        public bool EnableWalking { get; set; } = true;
        public bool EnableJumping { get; set; } = true;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        #region ?
        private bool moveFlag = true;
        private Personage castTarget;
        private float coolDownTimer = 0f;

        public bool IsStopped { get; private set; } = false; // ?
        public float GlobalCoolDown { get; set; }
        public float CurrentCastTime { get; private set; }
        public float MaxCastTime { get; private set; }
        public Spell CurrentCastSpell { get; private set; }
        #endregion

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public Personage( int level, RankOfPersonage rank, TypeOfPersonage type, int modelId, Vector3 position, int nameId = 3 ) : base( modelId, position, nameId ) {
            isPlayer = this is Player;
            this.type = type;
            this.rank = rank;
            this.level = level;
            animationGroup = GroupOfAnimation.Create( this );
            Relationship = RelationshipOfPersonage.Neutral;
            loot = new Bag( isPlayer );
            if( isPlayer ) {
                defaultTask = new PlayerMovementTask();
            } else {
                defaultTask = new WalkToSpawnPointTask();
            }
            currentTask = defaultTask;

            // !!! Ініціалізацію об'єктів здійснювати до методу LevelUp
            LevelUp( level );
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public void Restore() {
            if( isDead )
                return;
            currentHealth = currentCharacteristic.MaxHealth;
            currentMana = currentCharacteristic.MaxMana;
            currentEnergy = currentCharacteristic.MaxEnergy;
        }
        public void LevelUp( int amount = 1 ) {
            level = Mathf.Clamp( level + amount, MIN_LEVEL, MAX_LEVEL );
            calculateCharacteristic();
            Restore();
        }
        public void Reborn( float percent = 20f ) {
            if( !isDead )
                return;
            isDead = false;
            if( percent > 0f && 100f > percent ) {
                currentHealth = Characteristic.GetValueOfPercentage( currentCharacteristic.MaxHealth, percent );
                currentMana = Characteristic.GetValueOfPercentage( currentCharacteristic.MaxMana, percent );
                return;
            }
            Restore();
        }
        public void ClearTask() { currentTask = defaultTask; }
        public void WalkToPoint( Vector3 position, float radius = 1f ) { currentTask = new WalkToPointTask( position, radius ); }
        public void WalkToPoint( float x, float y, float z, float radius = 1f ) { currentTask = new WalkToPointTask( x, y, z, radius ); }
        public void WalkToPoint( Path.Node node ) { currentTask = new WalkToPointTask( node ); }
        public void AssignToPath( Path path ) { currentTask = new FollowPathTask( path ); }

        #region ?
        public void Cast( Spell spell ) {
            if( GlobalCoolDown != 0f ) {
                onCast( CastResult.NoReady );
                return;
            }
            if( CurrentCastSpell != null || spell == null ) {
                StopCast();
                onCast( CastResult.NoSpell );
                return;
            }
            if( !spell.ReadyToUse() || NoLongerNeeded || isDead ) {
                onCast( CastResult.NoReady );
                return;
            }
            if( spell.MinLevel > Level ) {
                onCast( CastResult.LackOfLevel );
                return;
            }
            if( !spell.EnableCastInRun && !IsStopped ) {
                onCast( CastResult.CanNotUseNow );
                return;
            }
            if( spell.MaxCount > 0 && spell.Count == 0 ) {
                onCast( CastResult.CanNotUseNow );
                return;
            }
            if( !spell.EnableCastInAir ) {
                if( IsInAir() ) {
                    onCast( CastResult.CanNotUseNow );
                    return;
                }
            }
            var takeAmount = spell.TakeResourcesAmount;
            if( spell.TakeResources != TypeOfResources.Nothing && spell.TakeResourcesAmount > 0 ) {
                switch( spell.TakeResources ) {
                    case TypeOfResources.Health:
                    takeAmount *= level;
                    if( takeAmount > currentHealth ) {
                        onCast( CastResult.LackOfHealth, TypeOfResources.Health );
                        return;
                    }
                    break;
                    case TypeOfResources.Mana:
                    takeAmount *= level;
                    if( takeAmount > currentMana ) {
                        onCast( CastResult.LackOfMana, TypeOfResources.Mana );
                        return;
                    }
                    break;
                    case TypeOfResources.Energy:
                    if( spell.TakeResourcesAmount > currentEnergy ) {
                        onCast( CastResult.LackOfEnergy, TypeOfResources.Energy );
                        return;
                    }
                    break;
                }
            }
            if( IsTargetItYourself() ) {
                if( spell.Mode != ModeOfCast.OnlySender ) {
                    onCast( CastResult.ForbiddenTarget );
                    return;
                }
                if( spell.CastOnlyInSpine ) {
                    onCast( CastResult.CanNotUseNow );
                    return;
                }
            } else {
                if( Target.NoLongerNeeded ) {
                    onCast( CastResult.CanNotUseNow );
                    return;
                }
                if( !spell.EnableCastInDeadTarget && Target.isDead ) {
                    onCast( CastResult.CanNotUseNow );
                    return;
                }
                if( !IsTurnedFaceTo( Target ) ) {
                    onCast( CastResult.CanNotUseNow );
                    return;
                }
                if( spell.CastOnlyInSpine && Target.IsTurnedFaceTo( this ) ) {
                    onCast( CastResult.CanNotUseNow );
                    return;
                }
                switch( spell.Mode ) {
                    case ModeOfCast.OnlyFriendly:
                    if( !IsFriendlyOf( Target ) ) {
                        onCast( CastResult.ForbiddenTarget );
                        return;
                    }
                    break;
                    case ModeOfCast.OnlyNotFriendly:
                    if( IsFriendlyOf( Target ) ) {
                        onCast( CastResult.ForbiddenTarget );
                        return;
                    }
                    break;
                    case ModeOfCast.OnlySender:
                    onCast( CastResult.ForbiddenTarget );
                    return;
                }
                var distance = DistanceTo( Target );
                if( spell.MinRange > distance || distance > spell.MaxRange ) {
                    onCast( CastResult.OutRange );
                    return;
                }
                if( !IsFreeDistanceTo( Target ) ) {
                    onCast( CastResult.CanNotUseNow );
                    return;
                }
            }
            if( spell.Type != TypeOfSpell.Reproduction ) {
                AddDamage( spell.TakeResources, takeAmount );
                GlobalCoolDown = 1f;
                spell.Use( this );
            }
            onCast( CastResult.Done );
            if( spell.Type == TypeOfSpell.Instant ) {
                StopCast();
                return;
            }
            castTarget = Target;
            CurrentCastTime = 0f;
            MaxCastTime = spell.CastTime;
            CurrentCastSpell = spell;
        }
        public void StopCast() {
            if( CurrentCastSpell != null )
                CurrentCastSpell.Stop();
            CurrentCastSpell = null;
            castTarget = null;
            MaxCastTime = 0f;
            CurrentCastTime = 0f;
        }
        #endregion

        public void Turn( float speed ) { gameObject.transform.Rotate( 0f, speed * Time.deltaTime, 0f ); }

        public DamageResult AddDamage( TypeOfResources resource, float damage, SchoolOfDamage school = SchoolOfDamage.Other ) {
            if( damage == 0f || resource == TypeOfResources.Nothing )
                return DamageResult.NoDamage;
            switch( resource ) {
                case TypeOfResources.Energy:
                currentEnergy -= damage;
                return damage > 0 ? DamageResult.AddEnergy : DamageResult.SubEnergy;
                case TypeOfResources.Mana:
                currentMana -= damage;
                return damage > 0 ? DamageResult.AddMana : DamageResult.SubMana;
                case TypeOfResources.Health:
                if( damage > 0 ) {
                    var finalDamage = 0f;
                    switch( school ) {
                        case SchoolOfDamage.Air:
                        finalDamage = damage - currentCharacteristic.MagicDefenceOfAir;
                        break;
                        case SchoolOfDamage.Darkness:
                        finalDamage = damage - currentCharacteristic.MagicDefenceOfDarkness;
                        break;
                        case SchoolOfDamage.Earth:
                        finalDamage = damage - currentCharacteristic.MagicDefenceOfEarth;
                        break;
                        case SchoolOfDamage.Fire:
                        finalDamage = damage - currentCharacteristic.MagicDefenceOfFire;
                        break;
                        case SchoolOfDamage.Light:
                        finalDamage = damage - currentCharacteristic.MagicDefenceOfLight;
                        break;
                        case SchoolOfDamage.Nature:
                        finalDamage = damage - currentCharacteristic.MagicDefenceOfNature;
                        break;
                        case SchoolOfDamage.Water:
                        finalDamage = damage - currentCharacteristic.MagicDefenceOfWater;
                        break;
                        case SchoolOfDamage.Physic:
                        if( currentCharacteristic.ChanceOfParrying > UnityEngine.Random.Range( 1, 100 ) )
                            return DamageResult.Parrying;
                        if( currentCharacteristic.ChanceOfBlocking > UnityEngine.Random.Range( 1, 100 ) )
                            return DamageResult.Blocked;
                        finalDamage = damage - currentCharacteristic.PhysicalDefencePower;
                        break;
                        default:
                        finalDamage = damage;
                        break;
                    }
                    if( finalDamage > 0f ) {
                        currentHealth -= finalDamage;
                        return DamageResult.SubHealth;
                    }
                    return DamageResult.Absorb;
                } else { currentHealth -= damage; }
                break;
            }
            return DamageResult.AddHealth;
        }
        public float GetRandomDamage( float minDamage, SchoolOfDamage school = SchoolOfDamage.Other ) {
            minDamage *= level;
            switch( school ) {
                case SchoolOfDamage.Air:
                minDamage += currentCharacteristic.MagicPowerOfAir;
                break;
                case SchoolOfDamage.Darkness:
                minDamage += currentCharacteristic.MagicPowerOfDarkness;
                break;
                case SchoolOfDamage.Earth:
                minDamage += currentCharacteristic.MagicPowerOfEarth;
                break;
                case SchoolOfDamage.Fire:
                minDamage += currentCharacteristic.MagicPowerOfFire;
                break;
                case SchoolOfDamage.Light:
                minDamage += currentCharacteristic.MagicPowerOfLight;
                break;
                case SchoolOfDamage.Nature:
                minDamage += currentCharacteristic.MagicPowerOfNature;
                break;
                case SchoolOfDamage.Water:
                minDamage += currentCharacteristic.MagicPowerOfWater;
                break;
                case SchoolOfDamage.Physic:
                minDamage += currentCharacteristic.PhysicalAttackPower;
                break;
            }
            return minDamage * UnityEngine.Random.Range( 1 + currentCharacteristic.CriticalChance, 100 + currentCharacteristic.CriticalEffect );
        }

        public bool IsTargetItYourself() {
            if( Target == null )
                return true;
            return Target == this;
        }
        public bool IsTurnedFaceTo( Vector3 position ) { return Vector3.Distance( GetPositionWithOffset( Vector3.back ), position ) - Vector3.Distance( GetPositionWithOffset( Vector3.forward ), position ) > 0f; }
        public bool IsTurnedFaceTo( float x, float y, float z ) { return IsTurnedFaceTo( new Vector3( x, y, z ) ); }
        public bool IsTurnedFaceTo( Entity entity ) {
            if( entity == this )
                return false;
            return IsTurnedFaceTo( entity.Position );
        }
        public bool IsFriendlyOf( Personage personage ) { return relationship == personage.relationship; }

        protected override void onDestroy() {
            base.onDestroy();
            GlobalCoolDown = 0f;
            coolDownTimer = 0f;
            Target = null;
            StopCast();
            ClearTask();
            effects.ClearAll( false );
            loot.Clear();
            spellBook.Clear();
        }

        public virtual bool Die() { // + bool
            if( isDead || Immortal )
                return false;
            if( !isPlayer )
                generateLoot();
            isDead = true;
            Target = null;
            StopCast();
            ClearTask();
            effects.ClearAll();
            currentHealth = 0f;
            currentMana = 0f;
            currentEnergy = 0f;
            GlobalCoolDown = 0f;
            coolDownTimer = 0f;
            return true;
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private void calculateCharacteristic() {
            baseCharacteristic = Characteristic.CreateBase( level, rank );
            currentCharacteristic = Characteristic.CreateEmpty();
            updateCharacteristic();
        }
        protected virtual void updateCharacteristic() {
            if( isPlayer )
                loot.UpdateItems();
            currentCharacteristic = ( ( currentCharacteristic.Clear() + baseCharacteristic ) + effects.Update() );
            spellBook.Update();
        }

        protected virtual void generateLoot() { }
        protected virtual void onCast( CastResult result, TypeOfResources resource = TypeOfResources.Nothing ) { }

        protected override void update() {
            base.update();
            moveFlag = true; // ?

            if( isDead ) {
                // ...
            } else {

                // далі оновлюємо характеристики
                updateCharacteristic();

                // регенерація буде здійснюватись, якщо значення більше за 0
                if( currentCharacteristic.HealthRegeneration > 0f )
                    currentHealth += currentCharacteristic.HealthRegeneration;
                if( currentCharacteristic.ManaRegeneration > 0f )
                    currentMana += currentCharacteristic.ManaRegeneration;
                if( currentCharacteristic.EnergyRegeneration > 0f )
                    currentEnergy += currentCharacteristic.EnergyRegeneration;

                // не дозволяємо ресурсам бути більшими за максимальне значення
                if( currentHealth > currentCharacteristic.MaxHealth )
                    currentHealth = currentCharacteristic.MaxHealth;
                if( currentMana > currentCharacteristic.MaxMana )
                    currentMana = currentCharacteristic.MaxMana;
                if( currentEnergy > currentCharacteristic.MaxEnergy )
                    currentEnergy = currentCharacteristic.MaxEnergy;

                // логіка смерті персонажа
                if( 0f >= currentHealth ) {
                    if( Immortal ) {
                        currentHealth = 1f;
                    } else {
                        Die();
                        return;
                    }
                }

                // оновлюємо швидкість анімації з характристик
                animationGroup.MoveSpeed = currentCharacteristic.MoveSpeed;

                // задаємо анімацію падіння, якщо персонаж в повітрі
                animationGroup.IsInAir = IsInAir();

                // логіка виконання поточного завдання персонажа
                if( IsContollable ) {
                    if( !currentTask.Execute( this ) )
                        ClearTask();
                }

                // логіка зміни цілі гравця
                if( Targetable && MouseHover && Player.Exist() && Player.Current.IsActive && !Player.Current.isDead && InputManager.IsMouseDown( MouseKeyName.Left ) )
                    Player.Current.Target = this;

                // логіка відкріплення цілі гравця
                if( Target != null ) {
                    if( !Target.IsActive || !Target.Targetable )
                        Target = null;
                }

                IsStopped = moveFlag; // ?

                // логіка глобальної затримки (буде змінено)
                if( GlobalCoolDown > 0f ) {
                    coolDownTimer += Time.deltaTime;
                    if( coolDownTimer > GlobalCoolDown ) {
                        GlobalCoolDown = 0f;
                        coolDownTimer = 0f;
                    }
                }

                // логіка відтворення заклинання (буде змінено)
                if( CurrentCastSpell != null ) {
                    if( castTarget != Target ) {
                        StopCast();
                        return;
                    }
                    CurrentCastTime += currentCharacteristic.Speed * Time.deltaTime;
                    var takeAmount = CurrentCastSpell.TakeResourcesAmount;
                    if( CurrentCastSpell.Type == TypeOfSpell.Reproduction && CurrentCastSpell.TakeResources != TypeOfResources.Nothing && CurrentCastSpell.TakeResourcesAmount > 0 ) {
                        switch( CurrentCastSpell.TakeResources ) {
                            case TypeOfResources.Health:
                            takeAmount *= level;
                            if( takeAmount > currentHealth ) {
                                StopCast();
                                return;
                            }
                            break;
                            case TypeOfResources.Mana:
                            takeAmount *= level;
                            if( takeAmount > currentMana ) {
                                StopCast();
                                return;
                            }
                            break;
                            case TypeOfResources.Energy:
                            if( CurrentCastSpell.TakeResourcesAmount > currentEnergy ) {
                                StopCast();
                                return;
                            }
                            break;
                        }
                    }
                    if( !CurrentCastSpell.EnableCastInRun && !IsStopped ) {
                        StopCast();
                        return;
                    }
                    if( IsTargetItYourself() ) {
                        if( NoLongerNeeded || isDead ) {
                            StopCast();
                            return;
                        }
                    } else {
                        if( Target.NoLongerNeeded ) {
                            StopCast();
                            return;
                        }
                        if( !CurrentCastSpell.EnableCastInDeadTarget && Target.isDead ) {
                            StopCast();
                            return;
                        }
                        if( !IsTurnedFaceTo( Target ) ) {
                            StopCast();
                            return;
                        }
                        if( CurrentCastSpell.CastOnlyInSpine && Target.IsTurnedFaceTo( this ) ) {
                            StopCast();
                            return;
                        }
                        if( CurrentCastSpell.Type != TypeOfSpell.Streaming ) {
                            var dist = DistanceTo( Target );
                            if( CurrentCastSpell.MinRange > dist || dist > CurrentCastSpell.MaxRange ) {
                                StopCast();
                                return;
                            }
                            if( !IsFreeDistanceTo( Target ) ) {
                                StopCast();
                                return;
                            }
                        }
                    }
                    if( CurrentCastTime > MaxCastTime ) {
                        if( CurrentCastSpell.Type != TypeOfSpell.Streaming ) {
                            CurrentCastSpell.Use( this );
                            AddDamage( CurrentCastSpell.TakeResources, takeAmount );
                            GlobalCoolDown = 1f;
                            onCast( CastResult.Done );
                        }
                        StopCast();
                    } else {
                        if( CurrentCastSpell.Type == TypeOfSpell.Streaming )
                            CurrentCastSpell.Continue();
                    }
                }

            }

        }

    }

    public enum RankOfPersonage : int {
        Normal = 1, // звичайний персонаж, підсилення x1
        Strong = 2, // сильний персонаж, підсилення x2
        Elite = 5,  // елітний персонаж, підсилення x5
        Boss = 25   // бос, підсилення x25
    }

    public enum TypeOfPersonage {
        Undead,     // зобмі
        Mechanism,  // механізм
        Daemon,     // демони, чорти
        Elemental,  // елементаль
        Animal,     // тварина
        Humanoid    // людиноподібна істота
    }

    public enum RelationshipOfPersonage {
        Friendly,   // Привітний
        Neutral,    // Нейтральний
        Enemy       // Ворог
    }

    public enum DamageResult {
        NoDamage,
        AddHealth,
        SubHealth,
        AddMana,
        SubMana,
        AddEnergy,
        SubEnergy,
        Parrying,
        Blocked,
        Absorb
    }

    public enum CastResult : byte {
        NoReady,
        NoSpell,
        LackOfLevel,
        CanNotUseNow,
        LackOfHealth,
        LackOfMana,
        LackOfEnergy,
        ForbiddenTarget,
        OutRange,
        Done
    }

}