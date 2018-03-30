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

	public sealed class Chest : Object {

        public static int GetModelByType( TypeOfChest type ) { return ( int ) type; }

        public TypeOfChest Type { get; private set; }
        public KeysForChest Key { get; private set; }
        public Bag Loot { get; private set; }
        public bool IsLocked { get; private set; }

        private byte totalItems;

        public Chest( TypeOfChest type, Vector3 position, KeysForChest key = KeysForChest.Universal, byte maxItems = 8 ) : base( GetModelByType( type ), position ) {
            Freeze( true );
            nameId = 38;
            Name = Localization.Current.EntityNames[ nameId ];
            Type = type;
            Key = key;
            totalItems = maxItems;
            IsLocked = key != KeysForChest.Universal;
            Loot = new Bag();
            if( !IsLocked )
                generateLoot();
        }

        protected override void update() {
            base.update();
            if( !IsActive )
                return;
            if( !Player.Exist() )
                return;
            if( Player.Current.NoLongerNeeded )
                return;
            if( !Player.Current.IsActive )
                return;
            if( !Player.Current.CanMove || !Player.Current.IsStopped )
                return;
            if( !eventSystemScript.MouseHover || !InputManager.IsMouseUp( MouseKeyName.Right ) )
                return;
            if( !Near( Player.Current, 2f ) )
                return;
            if( !Player.Current.IsTurnedFaceTo( this ) )
                return;
            if( Player.Current.IsInAir() )
                return;
            if( IsLocked ) {
                if( !Player.Current.Keys.HasKey( Key ) )
                    return;
                if( !Unlock() )
                    return;
                Player.Current.Keys.Remove( Key );
            }
        }

        public bool Unlock() {
            if( NoLongerNeeded )
                return false;
            if( !IsLocked )
                return true;
            generateLoot();
            IsLocked = false;
            return true;
        }

        private void generateLoot() {
            //for( byte i = 0; i < totalItems; i++ ) {
            //    Loot.Add( ? item );
            //}
        }

        public override void Destroy() {
            base.Destroy();
            Loot.Clear();
        }

    }

    public enum TypeOfChest : sbyte {
        Quest = -1,                 // Квестові скрині
        FoodAndDrink = -2,          // Їжа та напої
        PoisonsAndReagents = -3,    // Зілля та реагенти
        WeaponsAndEquipment = -4,   // Зброя та обладунки
        Other = -5                  // Різні предмети
    }

}