using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG {

    public enum TypeOfPickUp {
        Disposable, // одноразовий
        Reusable    // багаторазовий
    }

    public class PickUp : Object {

        protected const float MIN_RANGE = 1f;
        protected const float RESTORE_TIME = 5f;

        public bool EnableRotation { get; set; }
        public bool IsVisible { get; private set; }
        public TypeOfPickUp Type { get; private set; }

        private float restoreTimer;

        public PickUp( TypeOfPickUp type, int modelId, Vector3 position ) : base( modelId, position ) {
            Name = "PickUp";
            EnableRotation = true;
            IsVisible = true;
            Type = type;
            EnableCollision( false );

            restoreTimer = 0f;
        }

        protected override void update() {
            base.update();

            if( Type == TypeOfPickUp.Disposable ) {
                if( !IsVisible ) {
                    if( !NoLongerNeeded )
                        Destroy();
                    return;
                }
            } else {
                if( !IsVisible ) {
                    restoreTimer += 1f * Time.deltaTime;
                    if( restoreTimer > RESTORE_TIME ) {
                        restoreTimer = 0f;
                        IsVisible = true;
                        gameObject.SetActive( true );
                    }
                    return;
                }
           }

            if( EnableRotation && IsVisible )
                gameObject.transform.Rotate( 0f, 100f * Time.deltaTime, 0f );

            if( !Player.Exist() )
                return;

            if( Near( Player.Current, MIN_RANGE ) ) {
                action();
                return;
            }

        }

        public bool PickedUp() { return !IsVisible; }
        
        protected virtual void action() {
            IsVisible = false;
            gameObject.SetActive( false );
        }

    }

}