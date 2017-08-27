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

    public abstract partial class Entity : IDescription {

        private static EntityUpdator updator = new EntityUpdator();

        private RaycastHit hit;

        protected int iconID;
        protected GameObject gameObject;
        protected Collider collider;
        protected Rigidbody rigidbody;
        protected Targeting targetingScript;

        public Texture2D Icon {
            get {
                return Player.Interface.Icons[ iconID ];
            }
        }

        public int ModelID { get; private set; }
        public bool NoLongerNeeded { get; private set; }

        public string Name {
            get { return gameObject.name; }
            protected set { gameObject.name = value; }
        }
        public string Description { get; protected set; }

        public Vector3 Position {
            get { return gameObject.transform.localPosition; }
            set { gameObject.transform.localPosition = value; }
        }

        public Entity( int modelID, Vector3 position ) {
            iconID = 0;
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
            rigidbody = gameObject.AddComponent<Rigidbody>();
            targetingScript = gameObject.AddComponent<Targeting>();
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

        public override string ToString() { return gameObject.name; }

    }

}