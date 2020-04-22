/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/
using UnityEngine;
using MyRPG.Configuration;

namespace MyRPG {

    public abstract partial class Entity : IDescription {

        private RaycastHit hit;
        private bool mouseHover;
        private int modelID;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        protected GameObject gameObject;
        protected Collider mainCollider;
        protected Rigidbody mainRigidbody;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public bool MouseHover { get { return mouseHover; } }
        public int ModelID { get { return modelID; } }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public string Name {
            get { return gameObject.name; }
            protected set { gameObject.name = value; }
        }
        public Vector3 Position {
            get { return gameObject.transform.position; }
            set { gameObject.transform.position = value; }
        }
        public bool IsActive {
            get { return gameObject.activeSelf; }
            set { gameObject.SetActive( value ); }
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        #region ?

        [System.Obsolete( "Буде видалено!" )]
        protected int iconID, nameId;
        [System.Obsolete( "Буде видалено!" )]
        public Texture2D Icon { get { return Player.Interface.Icons[ iconID ]; } }
        [System.Obsolete( "Буде видалено!" )]
        public virtual string Description { get { return string.Empty; } }
        [System.Obsolete( "Буде видалено!" )]
        public bool NoLongerNeeded { get; private set; }

        #endregion

		private Entity() {}
        public Entity( int modelID, Vector3 position, int nameId = 0 ) {
            this.modelID = modelID;
            gameObject = GameObject.Instantiate<GameObject>( Model.Find( modelID ).Prefab, position, Quaternion.identity );
            if( nameId > -1 )
                Name = Localization.Current.EntityDescriptions[ nameId ];
            mainCollider = gameObject.GetComponent<Collider>();
            mainRigidbody = gameObject.GetComponent<Rigidbody>();
            gameObject.AddComponent<EntityUpdator>().AddReference( this );
        }

        #region ?
        public bool IsColliderExist() { return mainCollider != null; }
        public void EnableCollision( bool state ) {
            if( !IsColliderExist() )
                return;
            mainCollider.enabled = state;
        }
        public bool IsColliderActive() {
            if( !IsColliderExist() )
                return false;
            return mainCollider.enabled;
        }
        #endregion

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public void Destroy() { GameObject.Destroy( gameObject ); }

        public GameObject GetGameObject() { return gameObject; }

        public float DistanceTo( Vector3 position ) { return Vector3.Distance( Position, position ); }
        public float DistanceTo( float x, float y, float z ) { return DistanceTo( new Vector3( x, y, z ) ); }
        public float DistanceTo( Entity entity ) { return DistanceTo( entity.Position ); }
        public float DistanceTo( Path.Node node ) { return DistanceTo( node.Point ); }

        public bool Near( Vector3 position, float radius ) { return radius >= Vector3.Distance( Position, position ); }
        public bool Near( float x, float y, float z, float radius ) { return Near( new Vector3( x, y, z ), radius ); }
        public bool Near( Entity entity, float radius ) { return Near( entity.Position, radius ); }
        public bool Near( Path.Node node ) { return Near( node.Point, node.Radius ); }

        public Vector3 GetDirection( Vector3 position ) { return ( position - Position ).normalized; }
        public Vector3 GetDirection( float x, float y, float z ) { return GetDirection( new Vector3( x, y, z ) ); }
        public Vector3 GetDirection( Entity entity ) { return GetDirection( entity.Position ); }
        public Vector3 GetDirection( Path.Node node ) { return GetDirection( node.Point ); }

        public Vector3 GetPositionWithOffset( Vector3 offset ) { return gameObject.transform.TransformPoint( offset ); }
        public Vector3 GetPositionWithOffset( float offsetX, float offsetY, float offsetZ ) { return GetPositionWithOffset( new Vector3( offsetX, offsetY, offsetZ ) ); }

        public float DistanceToGround() { return Physics.Raycast( Position, Vector3.down, out hit, 100f ) ? hit.distance - ( gameObject.transform.lossyScale.y / 2.01f ) : 9999f; }
        public bool IsFreeDistanceTo( Entity entity ) {
            if( entity == this )
                return false;
            if( Physics.Raycast( Position, GetDirection( entity ), out hit, DistanceTo( entity ) + 1f ) ) {
                return entity.gameObject.transform == hit.transform;
            }
            return true;
        }
        public bool IsInAir() {
            return false; // треба знайти адекватний спосіб перевірки
            return DistanceToGround() > 0.02f;
        }
        public float RayCastDistance( Vector3 position, float maxDistance ) { return Physics.Raycast( Position - gameObject.transform.forward, GetDirection( position ), out hit, maxDistance ) ? Mathf.Abs( hit.distance ) : 9999f; }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        protected virtual void onEnable() { }
        protected virtual void onDisable() { }
        protected virtual void onDestroy() { }

        protected virtual void update() { }
        protected virtual void physics() { }

        protected virtual void onMouseEnter() { mouseHover = true; }
        protected virtual void onMouseExit() { mouseHover = false; }
        protected virtual void onMouseOver() { }

        public override string ToString() { return gameObject.name; }

    }

}