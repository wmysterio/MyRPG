/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using UnityEngine;
using MyRPG.Configuration;

namespace MyRPG {

    public class PickUp : Object {

        //private float restoreTimer;
        //public bool isVisible = true;
        //public TypeOfPickUp type;
        //public bool IsVisible { get { return isVisible; } }
        //public TypeOfPickUp Type { get { return type; } }

        private const float TURN_SPEED = 100f; // +

        protected const float MIN_RANGE = 1f;
        protected const float RESTORE_TIME = 5f;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        protected bool enableRotation = true;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public bool EnableRotation { get { return enableRotation; } }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        // TypeOfPickUp type, 
        public PickUp( int modelId, Vector3 position, int nameId = 2 ) : base( modelId, position, nameId ) {
            mainRigidbody.useGravity = false;
            mainRigidbody.mass = float.MaxValue; // +

            //EnableCollision( false );
            //this.type = type;
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        protected override void update() {
            base.update();

           // if( type == TypeOfPickUp.Disposable ) {
           //     if( !IsVisible ) {
           //         if( !NoLongerNeeded )
           //             Destroy();
           //         return;
           //     }
           // } else {
           //     if( !isVisible ) {
           //         restoreTimer += 1f * Time.deltaTime;
           //         if( restoreTimer > RESTORE_TIME ) {
           //             restoreTimer = 0f;
           //             isVisible = true;
           //             gameObject.SetActive( true );
           //         }
           //         return;
           //     }
           //}

            if( enableRotation ) //  && isVisible
                gameObject.transform.Rotate( 0f, TURN_SPEED * Time.deltaTime, 0f ); // 100f

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

        //public bool PickedUp() { return !isVisible; }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        protected virtual void action() {
            //isVisible = false;
            gameObject.SetActive( false );
        }

    }

    //public enum TypeOfPickUp {
    //    Disposable, // одноразовий
    //    Reusable    // багаторазовий
    //}

}