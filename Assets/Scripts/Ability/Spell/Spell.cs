/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using UnityEngine;

namespace MyRPG {

    public abstract class Spell : IAbility, IDamage {

        private float delayTimer;

        protected int iconID, nameId;
        protected float minDamage, minRange, maxRange;

        public TypeOfAbility AbilityType { get { return TypeOfAbility.Spell; } }
        public string Name { get { return Localization.Current.EntityDescriptions[ nameId ]; } }
        public virtual string Description { get { return string.Empty; } }
        public Texture2D Icon { get { return Player.Interface.Icons[ iconID ]; } }
        public float MinDamage { get { return minDamage; } }
        public float MinRange { get { return minRange; } }
        public float MaxRange { get { return maxRange; } }

        public TypeOfResources DamageResources { get; protected set; }
        public TypeOfResources TakeResources { get; protected set; }
        public SchoolOfDamage School { get; protected set; }
        public ModeOfCast Mode { get; protected set; }
        public float Timer { get; protected set; }
        public int Count { get; protected set; }
        public int MaxCount { get; protected set; }
        public float Delay { get; protected set; }
        public int TakeResourcesAmount { get; protected set; }
        public bool EnableCastInAir { get; protected set; }
        public bool EnableCastInRun { get; protected set; }
        public bool CastOnlyInSpine { get; protected set; }
        public bool EnableCastInDeadTarget { get; protected set; }

        public int MinLevel { get; private set; }
        public float CastTime { get; private set; }
        public TypeOfSpell Type { get; private set; }

        public Spell( int minLevel, TypeOfSpell type, float castTime ) {
            nameId = 34;
            iconID = 0;
            Timer = 0f;
            Count = 0;
            MaxCount = 0;
            minDamage = 1f;
            minRange = 0f;
            maxRange = 2f;
            Type = type;
            MinLevel = Mathf.Clamp( minLevel, Personage.MIN_LEVEL, Personage.MAX_LEVEL );
            CastTime = castTime;
            Delay = 0f;
            delayTimer = 0f;
            TakeResourcesAmount = 0;
            EnableCastInAir = false;
            EnableCastInRun = false;
            CastOnlyInSpine = false;
            EnableCastInDeadTarget = false;
            DamageResources = TypeOfResources.Nothing;
            TakeResources = TypeOfResources.Nothing;
            School = SchoolOfDamage.Other;
            Mode = ModeOfCast.OnlyNotFriendly;
        }

        public virtual void Continue() { }
        public virtual void Stop() { }

        public bool ReadyToUse() { return Timer == 0f; }

        public virtual void Update() {
            if( Delay > delayTimer ) {
                delayTimer += 1f * Time.deltaTime;
                if( delayTimer > Delay ) {
                    delayTimer = Delay;
                    Count = MaxCount;
                }
            }
            Timer = Mathf.Round( Delay - delayTimer );
        }

        public virtual void Use( Personage sender = null ) {
            if( Count > 0 ) {
                Count--;
                return;
            }
            if( Delay > 0 )
                delayTimer = 0f;
        }

        public override string ToString() { return Name; }

    }

    public enum TypeOfSpell : byte {
        Streaming,      // потокове
        Reproduction,   // відтворюване
        Instant         // миттєве
    }

    public enum TypeOfResources : byte {
        Health,
        Mana,
        Energy,
        Nothing
    }

    public enum SchoolOfDamage : byte {
        Physic,
        Nature,
        Fire,
        Water,
        Air,
        Earth,
        Darkness,
        Light,
        Other
    }

    public enum ModeOfCast : byte {
        OnlySender,
        OnlyFriendly,
        OnlyNotFriendly
    }

}