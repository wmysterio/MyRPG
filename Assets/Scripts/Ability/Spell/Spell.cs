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

	public abstract class Spell : IAbility {

        private int iconID;

        public TypeOfAbility AbilityType {
            get { return TypeOfAbility.Spell; }
        }

        public float Timer { get; protected set; }
        public int Count { get; protected set; }

        public Texture2D Icon {
            get { return Player.Interface.Icons[ iconID ]; }
        }
        public string Name { get; protected set; }
        public string Description { get; protected set; }


        public Spell( int iconID ) {
            Name = "Spell";
            Description = string.Empty;
            this.iconID = iconID;
            Timer = 0f;
            Count = 0;


        }






        public void Use( Personage target = null ) { }


        public override string ToString() { return Name; }

    }

}