﻿/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/
using MyRPG.Configuration;

namespace MyRPG.Items {

    public abstract class EquipmentItem : Item {

        public Characteristic CurrentCharacteristic { get; private set; }
        public PartOfEquipment EquipmentPart { get; private set; }
        public MaterialOfEquipment Material { get; private set; }
        public int ModelID { get; private set; }
        public string MaterialName { get; private set; }
        public string EquipmentPartName { get; private set; }

        protected EquipmentItem( int id, int nameId, int level, Sprites sprite, TypeOfItemRarity rarity, int descriptionId, int modelId, MaterialOfEquipment material, PartOfEquipment part, int count = 1 ) : base( id, nameId, level, sprite, rarity, descriptionId, ClassOfItem.Equipment, count, -1 ) {
            EquipmentPart = part;
            Material = material;
            ModelID = modelId;
            price = price + getPriceByMaterial( material ) + getPriceByPartOfEquipment( part );
            CurrentCharacteristic = Characteristic.CreateEmpty();
            MaterialName = Localization.Current.EquipmentMaterialNames[ ( int ) material ];
            EquipmentPartName = Localization.Current.EquipmentPartNames[ ( int ) part ];
        }
        
    }

    public enum PartOfEquipment : int {
        Hands = 0,       // Зап'ястя
        Neck = 1,        // Шия
        Ring = 2,        // Палець
        Belt = 3,        // Пояс
        Spine = 4,       // Спина
        Accessory = 5,	 // Аксесуар
        Footwear = 6,    // Взуття
        Shoulders = 7,   // Плечі
        Feet = 8,        // Ноги
        Torso = 9,       // Груди
        Head = 10,       // Голова
        Weapon = 11      // Зброя
    }

    public enum MaterialOfEquipment : int {
        Cloth = 0,		 // Тканина
        Leather = 1,	 // Шкіра
        Hauberk = 2,	 // Кольчуга
        PlateArmour = 3, // Лати 
        Other = 4   	 // Різне
    }

}