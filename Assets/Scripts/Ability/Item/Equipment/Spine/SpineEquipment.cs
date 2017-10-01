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

    public abstract class SpineEquipment : EquipmentItem {

        public SpineEquipment( int level, TypeOfItemRarity rarity, int modelID ) : base( level, rarity, MaterialOfEquipment.Other, PartOfEquipment.Spine, modelID ) {
            nameId = 23;
        }

    }

}