/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG {

	public sealed class EquipmentList {

        private static int partLength = Enum.GetValues( typeof( PartOfEquipment ) ).Length;
        public static int TotalParts { get { return partLength; } }

        private int iterator;
        private EquipmentItem[] items;
        private Characteristic characteristic;

        public EquipmentItem this[ PartOfEquipment part ] {
            get { return items[ ( int ) part ]; }
            set { items[ ( int ) part ] = value; }
        }
        public WeaponEquipment Weapon {
            get {
                if( !HasWeapon() )
                    return null;
                return ( WeaponEquipment ) this[ PartOfEquipment.Weapon ];
            }
        }
        public Characteristic CurrentCharacteristic {
            get {
                characteristic.Clear();
                for( iterator = 0; iterator < partLength; iterator++ ) {
                    if( items[ iterator ] != null )
                        characteristic += items[ iterator ].CurrentCharacteristic;
                }
                return characteristic;
            }
        }

        public EquipmentList() {
            items = ( EquipmentItem[] ) Array.CreateInstance( typeof( EquipmentItem ), partLength );
            iterator = 0;
            characteristic = Characteristic.CreateEmpty();
        }

        public bool HasWeapon() { return this[ PartOfEquipment.Weapon ] != null; }
        public void Set( EquipmentItem item ) {
            iterator = ( int ) item.EquipmentPart;
            if( items[ iterator ] == null ) {
                items[ iterator ] = item;
                return;
            }
            if( items[ iterator ] == item ) {
                items[ iterator ] = null;
                return;
            }
            items[ iterator ] = item;
        }

    }

}