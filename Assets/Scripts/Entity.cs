using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG {

    public class EntityText : Entity {

        public EntityText() : base( 0 ) { }



    }



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

        public int ModelID { get; private set; }


        public Entity( int modelID  ) {
            gameObject = new GameObject( "Entity" );
            gameObject.transform.parent = EntityList.Contrainer.transform;

            ModelID = modelID;
            var model = Model.Find( modelID );
            var obj = GameObject.Instantiate<GameObject>( model.Prefab );
            obj.name = "Model";
            obj.transform.parent = gameObject.transform;

            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;



            updator.Add( this );
        }




        protected GameObject gameObject;


        protected virtual void update() { }

        protected virtual void physics() { }

        protected virtual void draw() { }
		



	}

}