/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/
using UnityEngine;
using MyRPG.Configuration;

namespace MyRPG {

    public abstract class Effect : IAbility {

        protected int iconID, nameId;

        public TypeOfAbility AbilityType { get { return TypeOfAbility.Effect; } }
        public float Timer { get; protected set; }
        public int Count { get; protected set; }
        public Texture2D Icon { get { return Player.Interface.Icons[ iconID ]; } }

        public string Name {
            get { return Localization.Current.EntityDescriptions[ nameId ]; }
        }
        public virtual string Description { get { return string.Empty; } }

        public bool IsPassive { get; private set; }

        public Characteristic CurrentCharacteristic { get; private set; }
        public Personage Sender { get; private set; }
        public Personage Target { get; private set; }
        public bool Dispellable { get; protected set; }
        public bool NoLongerNeeded { get; private set; }

        public Effect( bool isPassive, Personage sender, Personage target = null ) {
            nameId = 5;
            iconID = 0;
            Timer = 0f;
            Count = 0;
            IsPassive = isPassive;
            Dispellable = true;
            NoLongerNeeded = false;
            CurrentCharacteristic = Characteristic.CreateEmpty();
            Sender = sender;
            Target = target;
        }

        protected virtual void end() { }

        public virtual bool Dispel( Personage helper ) {
            if( NoLongerNeeded || !Dispellable || helper == null )
                return false;
            if( helper.NoLongerNeeded || helper.IsDead )
                return false;
            return true;
        }

        public virtual bool Update() {
            if( NoLongerNeeded )
                return false;
            if( Sender == null ) {
                Destroy();
                return false;
            }
            if( Sender.NoLongerNeeded || Sender.IsDead ) {
                Destroy();
                return false;
            }
            return true;
        }

        public void Destroy() { NoLongerNeeded = true; }

        public virtual void Recreate() { }

        public virtual void Use( Personage target = null ) { }

        public override string ToString() { return Name; }

    }

}