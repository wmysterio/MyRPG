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

        void Awake() {
            var go = GameObject.Find( "EntityList" );
            go.AddComponent<Entity.EntityList>();
        }

        Player player;

        void Start() {
            Model.Request( 0 );
            Model.LoadRequestedNow();

            player = new Player( 1, 0, new Vector3( 0f, 1f, 0f ) );
            Player.Console.Enable = true;

            Model.Unload();

            StartCoroutine( loadConsole() );

        }

        void Update() {

        }

        void FixedUpdate() {

        }

        private IEnumerator loadConsole() {
            yield return Player.Console.Init();
        }


        bool chg = false;

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