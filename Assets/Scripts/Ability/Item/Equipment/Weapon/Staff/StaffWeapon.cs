﻿/*
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

    public abstract class StaffWeapon : WeaponEquipment {

        public StaffWeapon( int level, TypeOfItemRarity rarity, int modelID ) : base( level, TypeOfWeapon.Staff, rarity, modelID ) {
            nameId = 31;
        }

    }

}