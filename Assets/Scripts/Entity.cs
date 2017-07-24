using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG {

    public class EntityTest : Entity { }

	public abstract partial class Entity : IDescription {

        private static List<Entity> all = new List<Entity>();

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


            all.Add( this );
        }








        protected GameObject gameObject;


        protected virtual void update() { Debug.Log( "update" ); }

        protected virtual void physics() { Debug.Log( "physics" ); }

        protected virtual void draw() { Debug.Log( "draw" );

            GUI.Label( new Rect( 50, 50, 100, 32 ), "Text" );

        }
		



	}

}