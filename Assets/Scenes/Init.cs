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

            ////new Object( 0, new Vector3( -2f, 1f, 2f ) );
            ////new Object( 0, new Vector3( 2f, 1f, 2f ) );
            ////new Object( 0, new Vector3( -2f, 1f, -2f ) );
            ////new Object( 0, new Vector3( 2f, 1f, -2f ) );


            ////new PickUp( TypeOfPickUp.Disposable, 0, new Vector3( 0f, 1f, 0f ) );
            ////var p = new PickUp( TypeOfPickUp.Disposable, 0, new Vector3( 0f, 3.5f, 0f ) );
            ////p.EnableRotation = false;

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