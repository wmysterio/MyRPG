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

	public abstract class Item : IAbility {

        private int iconID;

        public TypeOfAbility AlilityType {
            get { return TypeOfAbility.Item; }
        }

        public float TimeToRestore { get; protected set; }
        public int Count { get; protected set; }

        public Texture2D Icon {
            get { return Player.Interface.Icons[ iconID ]; }
        }
        public string Name { get; protected set; }
        public string Description { get; protected set; }


        public Item( int iconID ) {
            Name = "Item";
            Description = string.Empty;
            this.iconID = iconID;
            TimeToRestore = 0f;
            Count = 0;


        }






        public void Use( Personage target = null ) { }

        public override string ToString() { return Name; }
    }

}