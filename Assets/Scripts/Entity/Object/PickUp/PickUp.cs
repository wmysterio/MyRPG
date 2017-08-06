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

        public bool EnableRotation { get; set; }
        public bool Visible { get; protected set; }
        public TypeOfPickUp Type { get; private set; }


        public PickUp( TypeOfPickUp type, int modelId, Vector3 position ) : base( modelId, position ) {
            Name = "PickUp";
            EnableRotation = true;
            Visible = true;
            Type = type;
            EnableCollision( false );
        }
        



        protected override void update() {
            base.update();
            if( EnableRotation )
                gameObject.transform.Rotate( 0f, 100f * Time.deltaTime, 0f );
        }

        public bool PickedUp() { return !Visible; }



    }

}