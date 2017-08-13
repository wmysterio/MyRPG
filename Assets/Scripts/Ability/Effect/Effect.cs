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

    public abstract class Effect : IAbility {

        protected int iconID;

        public TypeOfAbility AbilityType { get { return TypeOfAbility.Effect; } }
        public float TimeToRestore { get; protected set; }
        public int Count { get; protected set; }
        public Texture2D Icon { get { return Player.Interface.Icons[ iconID ]; } }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public bool IsPassive { get; private set; }


        public Characteristic CurrentCharacteristic { get; private set; }
        public Personage Sender { get; private set; }
        public Personage Target { get; private set; }
        public bool Dispellable { get; protected set; }
        public bool NoLongerNeeded { get; private set; }

        public Effect( bool isPassive, Personage sender, Personage target = null ) {
            Name = "Effect";
            Description = string.Empty;
            iconID = 0;
            TimeToRestore = 0f;
            Count = 0;
            IsPassive = isPassive;
            Dispellable = true;
            NoLongerNeeded = false;
            CurrentCharacteristic = Characteristic.CreateEmpty();
            Sender = sender;
            Target = target;
        }

        protected virtual void tick() { }
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

        public void Use( Personage target = null ) { tick(); }

        public override string ToString() { return Name; }

    }

}