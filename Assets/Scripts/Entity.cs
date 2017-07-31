using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG {

	public abstract partial class Entity : IDescription {

        private static EntityUpdator updator = new EntityUpdator();

        private Texture2D icon;
        protected string description;

        public Texture2D Icon {
            get {
                return icon;
            }
            set {
                icon = value;
            }
        }
        public string Name {
            get { return gameObject.name; }
            set { gameObject.name = value; }
        }
        public string Description {
            get { return description; }
            set { description = value; }
        }


        public Entity() {
            gameObject = new GameObject( "Entity" );
            gameObject.transform.parent = EntityList.Contrainer.transform;

            updator.Add( this );
        }








        protected GameObject gameObject;


        protected virtual void update() { }

        protected virtual void physics() { }

        protected virtual void draw() { }
		



	}

}