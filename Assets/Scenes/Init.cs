using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG {

	public class Init : MonoBehaviour {

		void Awake() {
            var go = GameObject.Find( "EntityList" );
            go.AddComponent<Entity.EntityList>();
		}

        void Start() {
            //Model.Request( 0 );
            //Model.LoadRequestedNow();

            //var obj1 = new Object( 0, new Vector3( -3f, 1f, 3f ) );
            //obj1.EnableCollision( true );
            //var obj2 = new Object( 0, new Vector3( 3f, 1f, 3f ) );


            //var pick1 = new PickUp( TypeOfPickUp.Disposable, 0, new Vector3( -3f, 1f, -3f ) );
            //var pick2 = new PickUp( TypeOfPickUp.Disposable, 0, new Vector3( 3f, 1f, -3f ) );
            //var pick3 = new PickUp( TypeOfPickUp.Disposable, 0, new Vector3( 0f, 1f, -0f ) );
            //pick2.EnableRotation = false;
            //pick3.Destroy();

            //Model.Unload();
        }
		
		void Update() {
            
		}
		
		void FixedUpdate() {
			
		}
		
		void OnGUI() {
			
		}
		
	}

}