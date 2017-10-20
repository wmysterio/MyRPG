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
        private GameObject model;

        protected int iconID, nameId;
        protected GameObject gameObject;
        protected Collider collider;
        protected Rigidbody rigidbody;
        protected EventSystem eventSystemScript;

        public Texture2D Icon { get { return Player.Interface.Icons[ iconID ]; } }
        public virtual string Description { get { return string.Empty; } }

        public int ModelID { get; private set; }
        public bool NoLongerNeeded { get; private set; }

        public string Name {
            get { return gameObject.name; }
            protected set { gameObject.name = value; }
        }
        public Vector3 Position {
            get { return gameObject.transform.localPosition; }
            set { gameObject.transform.localPosition = value; }
        }

        public Entity( int modelID, Vector3 position ) {
            iconID = 0;
            NoLongerNeeded = false;
            nameId = 0;
            gameObject = new GameObject( Localization.Current.EntityNames[ nameId ] );
            gameObject.transform.parent = EntityList.Container.transform;
            ModelID = modelID;
            model = GameObject.Instantiate<GameObject>( Model.Find( modelID ).Prefab );
            model.name = "Model";
            model.transform.parent = gameObject.transform;
            model.transform.localPosition = Vector3.zero;
            model.transform.localRotation = Quaternion.identity;
            model.transform.localScale = Vector3.one;
            Position = position;
            collider = gameObject.GetComponentInChildren<Collider>();
            rigidbody = gameObject.AddComponent<Rigidbody>();
            eventSystemScript = gameObject.AddComponent<EventSystem>();
            Localization.LanguageChanged += Localization_LanguageChanged;
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
        public virtual void Destroy() {
            if( !NoLongerNeeded ) {
                Localization.LanguageChanged -= Localization_LanguageChanged;
            }
            NoLongerNeeded = true;
        }
        public float DistanceTo( float x, float y, float z ) { return Vector3.Distance( Position, new Vector3( x, y, z ) ); }
        public float DistanceTo( Vector3 position ) { return Vector3.Distance( Position, position ); }
        public float DistanceTo( Entity entity ) { return Vector3.Distance( Position, entity.Position ); }
        public bool Near( float x, float y, float z, float radius ) { return radius >= Vector3.Distance( Position, new Vector3( x, y, z ) ); }
        public bool Near( Vector3 position, float radius ) { return radius >= Vector3.Distance( Position, position ); }
        public bool Near( Entity entity, float radius ) { return radius >= Vector3.Distance( Position, entity.Position ); }
        public bool Near( Path.Node node ) { return node.Radius >= Vector3.Distance( Position, node.Point ); }
        public float DistanceToGround() { return Physics.Raycast( Position, Vector3.down, out hit, 100f ) ? hit.distance - ( gameObject.transform.localScale.y / 2.01f ) : 9999f; }
        public GameObject GetGameObject() { return gameObject; }
        public float RayCastDistance( Vector3 position, float maxDistance ) { return Physics.Raycast( Position - gameObject.transform.forward, GetDirection( position ), out hit, maxDistance ) ? Mathf.Abs( hit.distance ) : 9999f; }
        public GameObject GetModel() { return model; }
        public Vector3 GetDirection( Vector3 position ) { return ( position - Position ).normalized; }
        public Vector3 GetDirection( Entity entity ) { return ( entity.Position - Position ).normalized; }
        public Vector3 GetPositionWithOffset( Vector3 offset ) { return gameObject.transform.TransformPoint( offset ); }
        public bool IsFreeDistanceTo( Entity entity ) {
            if( entity == this )
                return false;
            if( Physics.Raycast( Position, GetDirection( entity ), out hit, DistanceTo( entity ) + 1f ) ) {
                return entity.gameObject.transform == hit.transform;
            }
            return true;
        }
        public bool IsInAir() { return DistanceToGround() > 0.02f; }

        protected virtual void update() { }
        protected virtual void physics() { }

        public override string ToString() { return gameObject.name; }

        private void Localization_LanguageChanged( string path ) { if( nameId != -1 ) Name = Localization.Current.EntityNames[ nameId ]; }

    }

}