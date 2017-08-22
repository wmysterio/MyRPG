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

    public class AxWeapon : WeaponEquipment {

        public AxWeapon( string name, int level, TypeOfItemRarity rarity, int modelID ) : base( level, TypeOfWeapon.Ax, rarity, modelID ) {
            Name = name;
        }

    }

}