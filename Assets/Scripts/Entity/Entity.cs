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
        private RaycastHit hit;

        protected string description;
        protected GameObject gameObject;

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
        public bool NoLongerNeeded { get; private set; }
        public Vector3 Position {
            get { return gameObject.transform.localPosition; }
            set { gameObject.transform.localPosition = value; }
        }

        protected Collider collider;

        public Entity( int modelID, Vector3 position ) {
            Icon = null;
            Description = string.Empty;
            NoLongerNeeded = false;
            gameObject = new GameObject( "Entity" );
            gameObject.transform.parent = EntityList.Container.transform;
            ModelID = modelID;
            var model = Model.Find( modelID );
            var obj = GameObject.Instantiate<GameObject>( model.Prefab );
            obj.name = "Model";
            obj.transform.parent = gameObject.transform;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
            Position = position;
            collider = gameObject.GetComponentInChildren<Collider>();




            updator.Add( this );
        }

        public bool IsColliderExist() { return collider != null; }
        public void EnableCollision( bool state ) {
            if( !IsColliderExist() )
                return;
            collider.enabled = state;
        }
        public bool IsColliderActive() {
            if( !IsColliderExist() )
                return false;
            return collider.enabled;
        }
        public void Destroy() { NoLongerNeeded = true; }
        public float DistanceTo( float x, float y, float z ) { return Vector3.Distance( Position, new Vector3( x, y, z ) ); }
        public float DistanceTo( Vector3 position ) { return Vector3.Distance( Position, position ); }
        public float DistanceTo( Entity entity ) { return Vector3.Distance( Position, entity.Position ); }
        public bool Near( float x, float y, float z, float radius ) { return radius >= Vector3.Distance( Position, new Vector3( x, y, z ) ); }
        public bool Near( Vector3 position, float radius ) { return radius >= Vector3.Distance( Position, position ); }
        public bool Near( Entity entity, float radius ) { return radius >= Vector3.Distance( Position, entity.Position ); }
        public float DistanceToGround() { return Physics.Raycast( Position, Vector3.down, out hit, 100f ) ? hit.distance - ( gameObject.transform.localScale.y / 2.01f ) : 9999f; }


        protected virtual void update() { }

        protected virtual void physics() { }

        protected virtual void draw() { }

        public override string ToString() { return gameObject.name; }

    }

}