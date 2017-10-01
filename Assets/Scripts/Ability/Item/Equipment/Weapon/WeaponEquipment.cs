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

    public abstract class WeaponEquipment : EquipmentItem, IDamage {

        private float delayTimer;

        protected float minDamage = 1f, minRange = 1f, maxRange = 2f;

        public float MinDamage {
            get { return minDamage; }
        }
        public float MinRange {
            get { return minRange; }
        }
        public float MaxRange {
            get { return maxRange; }
        }

        public TypeOfWeapon Type { get; private set; }

        public float Delay { get; protected set; }

        public WeaponEquipment( int level, TypeOfWeapon type, TypeOfItemRarity rarity, int modelID ) : base( level, rarity, MaterialOfEquipment.Other, PartOfEquipment.Weapon, modelID ) {
            nameId = 25;
            Type = type;
            Delay = 4f;
            delayTimer = 0f;
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
        Staff,          // Посох
        Wand,			// Жезл
        Dagger,         // Кинджал
        Sword,          // Меч
        Bow,            // Лук
        Hammer,         // Молот
        Ax,             // Сокира
        BrassKnuckles   // Кастет
    }

}