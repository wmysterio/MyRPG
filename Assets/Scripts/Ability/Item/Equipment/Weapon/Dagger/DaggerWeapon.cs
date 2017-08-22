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

    public class DaggerWeapon : WeaponEquipment {

        public DaggerWeapon( string name, int level, TypeOfItemRarity rarity, int modelID ) : base( level, TypeOfWeapon.Dagger, rarity, modelID ) {
            Name = name;
        }

    }

}