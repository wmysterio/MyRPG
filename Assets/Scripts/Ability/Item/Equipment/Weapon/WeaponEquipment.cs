/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using UnityEngine;

namespace MyRPG {

    public abstract class WeaponEquipment : EquipmentItem, IDamage {

        private float delayTimer;

        protected float minDamage, minRange, maxRange;

        public float MinDamage { get { return minDamage; } }
        public float MinRange { get { return minRange; } }
        public float MaxRange { get { return maxRange; } }

        public TypeOfWeapon Type { get; private set; }
        public string TypeName { get; private set; }

        public float Delay { get; protected set; }

        protected WeaponEquipment( int id, int nameId, int level, Sprites sprite, TypeOfItemRarity rarity, int descriptionId, int modelId, TypeOfWeapon type, int count = 1 ) : base( id, nameId, level, sprite, rarity, descriptionId, modelId, MaterialOfEquipment.Other, PartOfEquipment.Weapon, count ) {
            Type = type;
            Delay = 4f;
            delayTimer = 0f;
            minDamage = 1f;
            minRange = 1f;
            maxRange = 2f;
            price = price + getPriceByTypeOfWeapon( type );
            TypeName = Localization.Current.WeaponEquipmentTypeNames[ ( int ) type ];
        }
        
        public bool ReadyToUse() { return Timer == 0f; }

        public override void Update() {
            base.Update();
            if( Delay > delayTimer ) {
                delayTimer += 1f * Time.deltaTime;
                if( delayTimer > Delay )
                    delayTimer = Delay;
            }
            Timer = Mathf.Round( Delay - delayTimer );
        }

        public override void Use( Personage target = null ) {
            base.Use( target );
            delayTimer = 0f;
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