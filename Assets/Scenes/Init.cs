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

    public class Init : MonoBehaviour {

        bool chg = false;



        void Awake() {
            var go = GameObject.Find( "EntityList" );
            go.AddComponent<Entity.EntityList>();
        }





        void Start() {
            StartCoroutine( loadConsole() );


            Model.Request( 0, 1, 2, 3, 4, 5, 6, 7 );
            Model.LoadRequestedNow();

            var player = new Player( 1, 3, new Vector3( 0f, 1f, 0f ) );

            var obj = new Object( 0, new Vector3( -1f, 0.5f, -1f ) );
            obj.Freeze( true );

            var pick = new PickUp( TypeOfPickUp.Reusable, 0, new Vector3( 1, 0.5f, 2 ) );


            Model.Unload();
        }





        void Update() { }

        void FixedUpdate() { }

        private IEnumerator loadConsole() { yield return Player.Console.Init(); }

        void OnGUI() {
            if( !chg ) {
                GUI.skin.settings.cursorColor = new Color( 0.44f, 0.48f, 0.58f, 1f );
                GUI.skin.settings.selectionColor = new Color( 0.44f, 0.48f, 0.58f, 1f );
                GUI.skin.settings.cursorFlashSpeed = 1f;
                chg = true;
            }
        }

    }

}