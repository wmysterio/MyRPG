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

    public abstract class TrashItem : Item {

        public TrashItem( int level, TypeOfItemRarity rarity ) : base( level, ClassOfItem.Trash, rarity ) {
            nameId = 9;
        }

    }

}