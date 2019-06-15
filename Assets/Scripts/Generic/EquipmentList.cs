/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System;
using System.Collections.Generic;
using MyRPG.Items;

namespace MyRPG {

    public partial class Player {

        public sealed class EquipmentList {

            private delegate void EquipmentListCallbackHandler( EquipmentItem item );

            private Dictionary<int, int> slots;
            private int iterator, partLength, weaponSlot;
            private Characteristic characteristic;

            public int Count { get; private set; }
            public EquipmentItem this[ PartOfEquipment part ] {
                get {
                    var id = slots[ ( int ) part ];
                    if( id == -1 )
                        return null;
                    return ( EquipmentItem ) Current.Loot[ id ];
                }
            }
            public WeaponEquipment Weapon {
                get {
                    if( slots[ weaponSlot ] == -1 )
                        return null;
                    return ( WeaponEquipment ) Current.Loot[ slots[ weaponSlot ] ];
                }
            }
            public Characteristic CurrentCharacteristic {
                get {
                    characteristic.Clear();
                    each( item => { characteristic += item.CurrentCharacteristic; } );
                    return characteristic;
                }
            }

            public EquipmentList() {
                characteristic = Characteristic.CreateEmpty();
                partLength = Enum.GetValues( typeof( PartOfEquipment ) ).Length;
                weaponSlot = ( int ) PartOfEquipment.Weapon;
                slots = new Dictionary<int, int>();
                for( iterator = 0; iterator < partLength; iterator++ )
                    slots[ iterator ] = -1;
            }

            public bool HasWeapon() { return slots[ weaponSlot ] != -1; }
            public bool IsSlotFree( PartOfEquipment part ) { return slots[ ( int ) part ] == -1; }
            public bool HasItem( int id ) { return slots.ContainsValue( id ); }
            public bool HasItem( Item item ) { return slots.ContainsValue( item.Id ); }
            public void Set( Item item, bool playSound = false ) {
                if( item == null )
                    return;
                if( 1 > item.Count )
                    return;
                if( item.Class != ClassOfItem.Equipment )
                    return;
                if( !Current.Loot.HasItem( item.Id ) )
                    return;
                var equipmentItem = ( EquipmentItem ) item;
                var slotID = ( int ) equipmentItem.EquipmentPart;
                if( slots[ slotID ] == -1 ) {
                    slots[ slotID ] = item.Id;
                    Count += 1;
                    // прикріплюємо модель до гравця
                    if( playSound ) {
                        // відтворюємо звук прикріплення
                    }
                    return;
                }
                if( equipmentItem.Id == slots[ slotID ] ) {
                    slots[ slotID ] = -1;
                    Count -= 1;
                    // відкріплюємо модель від гравця
                    if( playSound ) {
                        // відтворюємо звук відкріплення
                    }
                    return;
                }
                slots[ slotID ] = item.Id;
                // змінюємо модель предмету
                if( playSound ) {
                    // відтворюємо звук прикріплення
                }
            }

            private void each( EquipmentListCallbackHandler callback ) {
                if( callback == null || Count == 0 )
                    return;
                for( iterator = 0; iterator < partLength; iterator++ ) {
                    if( slots[ iterator ] == -1 )
                        continue;
                    callback.Invoke( ( EquipmentItem ) Current.Loot[ slots[ iterator ] ] );
                }
            }

        }

    }

}