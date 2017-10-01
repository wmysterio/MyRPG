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

    public abstract class ShouldersEquipment : EquipmentItem {

        public ShouldersEquipment( int level, TypeOfItemRarity rarity, MaterialOfEquipment material, int modelID ) : base( level, rarity, material, PartOfEquipment.Shoulders, modelID ) {
            nameId = 22;
        }

    }

}