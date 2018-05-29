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

    public abstract class EquipmentItem : Item {

        private float strength;

        protected Characteristic baseCharacteristic;

        public PartOfEquipment EquipmentPart { get; private set; }
        public MaterialOfEquipment Material { get; private set; }
        public int ModelID { get; private set; }

        public Characteristic CurrentCharacteristic { get; private set; }

        public float Strength {
            get { return strength; }
            set {
                if( 0 > value )
                    value = 0;
                if( value > 100 )
                    value = 100;
                strength = value;
            }
        }

        public EquipmentItem( int level, TypeOfItemRarity rarity, MaterialOfEquipment material, PartOfEquipment part, int modelId ) : base( level, ClassOfItem.Equipment, rarity ) {
            nameId = 13;
            baseCharacteristic = Characteristic.CreateEmpty();
            CurrentCharacteristic = Characteristic.CreateEmpty();
            Strength = 100;
            EquipmentPart = part;
            Material = material;
            ModelID = modelId;
            price += 5 * ( 1 + ( int ) part ) + ( int ) material;
        }

        public override void Update() {
            base.Update();
            CurrentCharacteristic = ( CurrentCharacteristic.Clear() + baseCharacteristic ) / ( 100f - Strength );
        }

    }

    public enum PartOfEquipment : int {
        Hands = 0,      // Зап'ястя
        Neck = 1,       // Шия
        Ring = 2,       // Палець
        Belt = 3,       // Пояс
        Spine = 4,      // Спина
        Accessory = 5,	// Аксесуар
        Footwear = 6,   // Взуття
        Shoulders = 7,  // Плечі
        Feet = 8,       // Ноги
        Torso = 9,      // Груди
        Head = 10,      // Голова
        Weapon = 11     // Зброя
    }

    public enum MaterialOfEquipment : int {
        Cloth = 12,			// Тканина
        Leather = 6,		// Шкіра
        Hauberk = 4,		// Кольчуга
        PlateArmour = 3,    // Лати 
        Other = 12   		// Різне
    }

}