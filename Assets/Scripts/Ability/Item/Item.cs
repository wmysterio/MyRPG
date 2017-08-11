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

        private Texture2D icon;

        public TypeOfAbility Type {
            get { return TypeOfAbility.Item; }
        }

        protected string description;
        protected string name;

        public float TimeToRestore { get; protected set; }
        public int Count { get; protected set; }

        public Texture2D Icon {
            get { return icon; }
            set { icon = value; }
        }
        public string Name {
            get { return name; }
            set { name = value; }
        }
        public string Description {
            get { return description; }
            set { description = value; }
        }

        public Item() { }

        public void Use( Personage traget = null ) { }

    }

}