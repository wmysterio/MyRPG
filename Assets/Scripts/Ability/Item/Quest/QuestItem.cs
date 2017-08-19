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

    public abstract class QuestItem : Item {

        public QuestItem( int level, TypeOfItemRarity rarity ) : base( level, ClassOfItem.Quest, rarity ) { }

    }

}