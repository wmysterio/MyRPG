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

        Player player;

        void Start() {
            Model.Request( 0 );
            Model.LoadRequestedNow();

            player = new Player( 10, 0, new Vector3( 0f, 1f, 0f ) );



            Model.Unload();
        }

        void Update() {

        }

        void FixedUpdate() {

        }

        void OnGUI() {

        }

    }

}