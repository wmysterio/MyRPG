/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/
using MyRPG.Configuration;

namespace MyRPG.Items {

    public abstract class WeaponEquipment : EquipmentItem, IDamage {

        protected float minDamage, minRange, maxRange;

        public float MinDamage { get { return minDamage; } }
        public float MinRange { get { return minRange; } }
        public float MaxRange { get { return maxRange; } }

        public TypeOfWeapon Type { get; private set; }
        public string TypeName { get; private set; }

        protected WeaponEquipment( int id, int nameId, int level, Sprites sprite, TypeOfItemRarity rarity, int descriptionId, int modelId, TypeOfWeapon type, int count = 1 ) : base( id, nameId, level, sprite, rarity, descriptionId, modelId, MaterialOfEquipment.Other, PartOfEquipment.Weapon, count ) {
            Type = type;
            Timer = 4f;
            minDamage = 1f;
            minRange = 1f;
            maxRange = 2f;
            price = price + getPriceByTypeOfWeapon( type );
            TypeName = Localization.Current.WeaponEquipmentTypeNames[ ( int ) type ];
        }

    }

    public enum TypeOfWeapon : int {
        Staff = 0,          // Посох
        Wand = 1,			// Жезл
        Dagger = 2,         // Кинджал
        Sword = 3,          // Меч
        Bow = 4,            // Лук
        Hammer = 5,         // Молот
        Ax = 6,             // Сокира
        BrassKnuckles = 7   // Кастет
    }

}