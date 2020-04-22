/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/
using UnityEngine;

namespace MyRPG {

    public class PickUp : Object {

        protected const float MIN_RANGE = 1f;
        protected const float RESTORE_TIME = 5f;

        private const float TURN_SPEED = 100f;

        private float restoreTimer;
        public bool isVisible = true;
        public TypeOfPickUp type;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public bool IsVisible { get { return isVisible; } }
        public TypeOfPickUp Type { get { return type; } }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        protected bool enableRotation = true;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public bool EnableRotation { get { return enableRotation; } }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public PickUp( TypeOfPickUp type, int modelId, Vector3 position, int nameId = 2 ) : base( modelId, position, nameId ) {
            mainRigidbody.useGravity = false;
            mainRigidbody.mass = float.MaxValue;

            EnableCollision( false );
            this.type = type;
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        protected override void update() {
            base.update();

            if( type == TypeOfPickUp.Disposable ) {
                if( !IsVisible ) {
                    if( !NoLongerNeeded )
                        Destroy();
                    return;
                }
            } else {
                if( !isVisible ) {
                    restoreTimer += 1f * Time.deltaTime;
                    if( restoreTimer > RESTORE_TIME ) {
                        restoreTimer = 0f;
                        isVisible = true;
                        gameObject.SetActive( true );
                    }
                    return;
                }
            }

            if( enableRotation && isVisible )
                gameObject.transform.Rotate( 0f, TURN_SPEED * Time.deltaTime, 0f );

            #region ?
            if( !Player.Exist() )
                return;

            if( Near( Player.Current, MIN_RANGE ) ) {
                action();
                return;
            }
            #endregion

        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public bool PickedUp() { return !isVisible; }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        protected virtual void action() {
            isVisible = false;
            gameObject.SetActive( false );
        }

    }

    public enum TypeOfPickUp {
        Disposable, // одноразовий
        Reusable    // багаторазовий
    }

}