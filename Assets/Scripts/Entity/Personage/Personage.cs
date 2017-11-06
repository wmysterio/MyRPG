/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://www.unity3d.tk/
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG {

    public abstract partial class Personage : Entity {

        public const int MIN_LEVEL = 1;
        public const int MAX_LEVEL = 100;

        private Characteristic baseCharacteristic;
        private RelationshipOfPersonage relationship = RelationshipOfPersonage.Friendly;
        private Vector3 velocity, tempVector;
        private Path currentPath;
        private Path.Node currentNode;
        private bool moveFlag;

        private Personage castTarget;
        private float coolDownTimer;

        public TypeOfPersonage Type { get; private set; }
        public RankOfPersonage Rank { get; private set; }
        public bool IsDead { get; private set; }
        public bool IsStopped { get; private set; }
        public float CurrentHealth { get; private set; }
        public float CurrentMana { get; private set; }
        public float CurrentEnergy { get; private set; }
        public EffectList Effects { get; private set; }
        public Bag Loot { get; private set; }
        public EquipmentList Equipments { get; private set; }
        public SkillBook SpellBook { get; private set; }
        public Task CurrentTask { get; private set; }
        public int Level { get; private set; }
        public Characteristic CurrentCharacteristic { get; private set; }
        public float CurrentCastTime { get; private set; }
        public float MaxCastTime { get; private set; }
        public Spell CurrentCastSpell { get; private set; }

        public float GlobalCoolDown { get; set; }
        public bool Immortal { get; set; }
        public bool EnableJumping { get; set; }
        public bool CanMove { get; set; }
        public bool Targetable { get; set; }
        public bool EnableWalking { get; set; }
        public Personage Target { get; set; }

        public RelationshipOfPersonage Relationship {
            get { return relationship; }
            set {
                if( this is Player )
                    return;
                relationship = value;
            }
        }


        public Personage( int level, RankOfPersonage rank, TypeOfPersonage type, int modelId, Vector3 position ) : base( modelId, position ) {
            nameId = 3;
            Name = Localization.Current.EntityNames[ nameId ];
            IsDead = false;
            IsStopped = false;
            Immortal = false;
            CanMove = true;
            EnableJumping = true;
            Targetable = true;
            EnableWalking = true;
            moveFlag = true;
            Level = 0;
            Rank = rank;
            coolDownTimer = 0f;
            Relationship = RelationshipOfPersonage.Neutral;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            Effects = new EffectList();
            SpellBook = new SkillBook();
            Loot = new Bag();
            Equipments = new EquipmentList();
            Target = null;
            ClearTask();
            velocity = Vector3.zero;
            tempVector = Vector3.zero;
            StopCast();

            // !!! Ініціалізацію об'єктів здійснювати до методу LevelUp
            LevelUp( level );
        }

        private void calculateCharacteristic() {
            baseCharacteristic = Characteristic.CreateBase( Level, Rank );
            CurrentCharacteristic = Characteristic.CreateEmpty();
            updateCharacteristic();
        }
        private void updateCharacteristic() {
            Loot.UpdateItems();
            CurrentCharacteristic = ( ( ( CurrentCharacteristic.Clear() + baseCharacteristic ) + Equipments.CurrentCharacteristic ) + Effects.Update() );
            SpellBook.Update();
        }

        protected virtual void onCast( CastResult result, TypeOfResources resource = TypeOfResources.Nothing ) { }

        protected override void update() {
            base.update();
            updateCharacteristic();
            moveFlag = true;
            if( eventSystemScript.MouseHover ) {
                if( Player.Exist() && InputManager.IsMouseDown( MouseKeyName.Left ) ) {
                    if( !Player.Current.NoLongerNeeded && !Player.Current.IsDead )
                        Player.Current.Target = this;
                }
            }
            if( Target != null ) {
                if( !Target.Targetable )
                    Target = null;
            }
            if( GlobalCoolDown > 0f ) {
                coolDownTimer += Time.deltaTime;
                if( coolDownTimer > GlobalCoolDown ) {
                    GlobalCoolDown = 0f;
                    coolDownTimer = 0f;
                }
            }
            if( 0 > CurrentCharacteristic.MoveSpeed )
                CurrentCharacteristic.MoveSpeed = 0f;
            if( !IsDead ) {
                if( CanMove )
                    taskManager();
                IsStopped = moveFlag;
                if( 0 >= CurrentHealth && !Immortal ) {
                    Die();
                    return;
                }
                CurrentHealth += CurrentCharacteristic.HealthRegeneration;
                CurrentMana += CurrentCharacteristic.ManaRegeneration;
                CurrentEnergy += CurrentCharacteristic.EnergyRegeneration;
                if( CurrentHealth > CurrentCharacteristic.MaxHealth )
                    CurrentHealth = CurrentCharacteristic.MaxHealth;
                if( CurrentMana > CurrentCharacteristic.MaxMana )
                    CurrentMana = CurrentCharacteristic.MaxMana;
                if( CurrentEnergy > CurrentCharacteristic.MaxEnergy )
                    CurrentEnergy = CurrentCharacteristic.MaxEnergy;
                if( CurrentCastSpell != null ) {
                    if( castTarget != Target ) {
                        StopCast();
                        return;
                    }
                    CurrentCastTime += CurrentCharacteristic.CastSpeed * Time.deltaTime;
                    var takeAmount = CurrentCastSpell.TakeResourcesAmount;
                    if( CurrentCastSpell.Type == TypeOfSpell.Reproduction && CurrentCastSpell.TakeResources != TypeOfResources.Nothing && CurrentCastSpell.TakeResourcesAmount > 0 ) {
                        switch( CurrentCastSpell.TakeResources ) {
                            case TypeOfResources.Health:
                            takeAmount *= Level;
                            if( takeAmount > CurrentHealth ) {
                                StopCast();
                                return;
                            }
                            break;
                            case TypeOfResources.Mana:
                            takeAmount *= Level;
                            if( takeAmount > CurrentMana ) {
                                StopCast();
                                return;
                            }
                            break;
                            case TypeOfResources.Energy:
                            if( CurrentCastSpell.TakeResourcesAmount > CurrentEnergy ) {
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
                        if( NoLongerNeeded || IsDead ) {
                            StopCast();
                            return;
                        }
                    } else {
                        if( Target.NoLongerNeeded ) {
                            StopCast();
                            return;
                        }
                        if( !CurrentCastSpell.EnableCastInDeadTarget && Target.IsDead ) {
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
            } else {
                if( 0f > CurrentHealth )
                    CurrentHealth = 0f;
                if( 0f > CurrentMana )
                    CurrentMana = 0f;
                if( 0f > CurrentEnergy )
                    CurrentEnergy = 0f;
            }
        }

        protected override void physics() {
            base.physics();
            rigidbody.velocity += velocity;
            velocity = Vector3.zero;
        }

        protected virtual void move() { }

        public bool IsFriendlyOf( Personage personage ) { return relationship == personage.relationship; }
        public void MoveForward() {
            if( !EnableWalking )
                return;
            gameObject.transform.Translate( Vector3.forward * CurrentCharacteristic.MoveSpeed * Time.deltaTime );
            moveFlag = false;
        }
        public void MoveBack() {
            if( !EnableWalking )
                return;
            gameObject.transform.Translate( Vector3.back * CurrentCharacteristic.MoveSpeed * Time.deltaTime );
            moveFlag = false;
        }
        public void MoveLeft() {
            if( !EnableWalking )
                return;
            gameObject.transform.Translate( Vector3.left * CurrentCharacteristic.MoveSpeed * Time.deltaTime );
            moveFlag = false;
        }
        public void MoveRight() {
            if( !EnableWalking )
                return;
            gameObject.transform.Translate( Vector3.right * CurrentCharacteristic.MoveSpeed * Time.deltaTime );
            moveFlag = false;
        }
        public void Turn( float speed ) {
            gameObject.transform.Rotate( 0f, speed * Time.deltaTime, 0f );
        }
        public void Jump() {
            if( !EnableJumping || IsInAir() )
                return;
            moveFlag = false;
            velocity.y += 4f;
        }
        public void Restore() {
            if( IsDead )
                return;
            CurrentHealth = CurrentCharacteristic.MaxHealth;
            CurrentMana = CurrentCharacteristic.MaxMana;
            CurrentEnergy = CurrentCharacteristic.MaxEnergy;
        }
        public void LevelUp( int amount = 1 ) {
            Level = Mathf.Clamp( Level + amount, MIN_LEVEL, MAX_LEVEL );
            calculateCharacteristic();
            Restore();
        }
        public virtual void Die() {
            if( IsDead || Immortal )
                return;
            Effects.ClearAll();
            ClearTask();
            Target = null;
            IsDead = true;
            StopCast();
            CurrentHealth = 0f;
            CurrentMana = 0f;
            CurrentEnergy = 0f;
        }
        public override void Destroy() {
            base.Destroy();
            Effects.ClearAll( false );
            ClearTask();
            Loot.Clear();
            SpellBook.Clear();
            Target = null;
            IsDead = true;
            StopCast();
        }
        public void Reanimate( float percent = 20f ) {
            if( !IsDead )
                return;
            IsDead = false;
            if( percent > 0f && 100f > percent ) {
                CurrentHealth = Characteristic.GetValueOfPercentage( CurrentCharacteristic.MaxHealth, percent );
                CurrentMana = Characteristic.GetValueOfPercentage( CurrentCharacteristic.MaxMana, percent );
                return;
            }
            Restore();
        }
        public void ClearTask() {
            CurrentTask = Task.STAY_IDLE;
            currentNode = null;
            currentPath = null;
        }
        public void WalkToPoint( Path.Node node ) {
            CurrentTask = Task.WALK_TO_NODE;
            currentNode = node;
        }
        public void AssignToPath( Path path ) {
            currentPath = path;
            if( !currentPath.NextNode( out currentNode ) ) {
                currentNode = null;
                currentPath = null;
                return;
            }
            WalkToPoint( currentNode );
        }
        public bool IsTargetItYourself() {
            if( Target == null )
                return true;
            return Target == this;
        }
        public bool IsTurnedFaceTo( Vector3 position ) { return Vector3.Distance( GetPositionWithOffset( Vector3.back ), position ) - Vector3.Distance( GetPositionWithOffset( Vector3.forward ), position ) > 0f; }
        public bool IsTurnedFaceTo( Entity entity ) {
            if( entity == this )
                return false;
            return Vector3.Distance( GetPositionWithOffset( Vector3.back ), entity.Position ) - Vector3.Distance( GetPositionWithOffset( Vector3.forward ), entity.Position ) > 0f;
        }
        public DamageResult AddDamage( TypeOfResources resource, float damage, SchoolOfDamage school = SchoolOfDamage.Other ) {
            if( damage == 0f || resource == TypeOfResources.Nothing )
                return DamageResult.NoDamage;
            switch( resource ) {
                case TypeOfResources.Energy:
                CurrentEnergy -= damage;
                return damage > 0 ? DamageResult.AddEnergy : DamageResult.SubEnergy;
                case TypeOfResources.Mana:
                CurrentMana -= damage;
                return damage > 0 ? DamageResult.AddMana : DamageResult.SubMana;
                case TypeOfResources.Health:
                if( damage > 0 ) {
                    var finalDamage = 0f;
                    switch( school ) {
                        case SchoolOfDamage.Air:
                        finalDamage = damage - CurrentCharacteristic.MagicDefenceOfAir;
                        break;
                        case SchoolOfDamage.Darkness:
                        finalDamage = damage - CurrentCharacteristic.MagicDefenceOfDarkness;
                        break;
                        case SchoolOfDamage.Earth:
                        finalDamage = damage - CurrentCharacteristic.MagicDefenceOfEarth;
                        break;
                        case SchoolOfDamage.Fire:
                        finalDamage = damage - CurrentCharacteristic.MagicDefenceOfFire;
                        break;
                        case SchoolOfDamage.Light:
                        finalDamage = damage - CurrentCharacteristic.MagicDefenceOfLight;
                        break;
                        case SchoolOfDamage.Nature:
                        finalDamage = damage - CurrentCharacteristic.MagicDefenceOfNature;
                        break;
                        case SchoolOfDamage.Water:
                        finalDamage = damage - CurrentCharacteristic.MagicDefenceOfWater;
                        break;
                        case SchoolOfDamage.Physic:
                        if( CurrentCharacteristic.ChanceOfParrying > UnityEngine.Random.Range( 1, 100 ) )
                            return DamageResult.Parrying;
                        if( CurrentCharacteristic.ChanceOfBlocking > UnityEngine.Random.Range( 1, 100 ) )
                            return DamageResult.Blocked;
                        finalDamage = damage - CurrentCharacteristic.PhysicalDefencePower;
                        break;
                        default:
                        finalDamage = damage;
                        break;
                    }
                    if( finalDamage > 0f ) {
                        CurrentHealth -= finalDamage;
                        return DamageResult.SubHealth;
                    }
                    return DamageResult.Absorb;
                } else { CurrentHealth -= damage; }
                break;
            }
            return DamageResult.AddHealth;
        }
        public float GetRandomDamage( float minDamage, SchoolOfDamage school = SchoolOfDamage.Other ) {
            minDamage *= Level;
            switch( school ) {
                case SchoolOfDamage.Air:
                minDamage += CurrentCharacteristic.MagicPowerOfAir;
                break;
                case SchoolOfDamage.Darkness:
                minDamage += CurrentCharacteristic.MagicPowerOfDarkness;
                break;
                case SchoolOfDamage.Earth:
                minDamage += CurrentCharacteristic.MagicPowerOfEarth;
                break;
                case SchoolOfDamage.Fire:
                minDamage += CurrentCharacteristic.MagicPowerOfFire;
                break;
                case SchoolOfDamage.Light:
                minDamage += CurrentCharacteristic.MagicPowerOfLight;
                break;
                case SchoolOfDamage.Nature:
                minDamage += CurrentCharacteristic.MagicPowerOfNature;
                break;
                case SchoolOfDamage.Water:
                minDamage += CurrentCharacteristic.MagicPowerOfWater;
                break;
                case SchoolOfDamage.Physic:
                minDamage += CurrentCharacteristic.PhysicalAttackPower;
                break;
            }
            return minDamage * UnityEngine.Random.Range( 1 + CurrentCharacteristic.CriticalChance, 100 + CurrentCharacteristic.CriticalEffect );
        }
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
            if( !spell.ReadyToUse() || NoLongerNeeded || IsDead ) {
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
                    takeAmount *= Level;
                    if( takeAmount > CurrentHealth ) {
                        onCast( CastResult.LackOfHealth, TypeOfResources.Health );
                        return;
                    }
                    break;
                    case TypeOfResources.Mana:
                    takeAmount *= Level;
                    if( takeAmount > CurrentMana ) {
                        onCast( CastResult.LackOfMana, TypeOfResources.Mana );
                        return;
                    }
                    break;
                    case TypeOfResources.Energy:
                    if( spell.TakeResourcesAmount > CurrentEnergy ) {
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
                if( !spell.EnableCastInDeadTarget && Target.IsDead ) {
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

        partial void taskManager();


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