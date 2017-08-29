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

    public class TempHuman : Humanoid {

        public TempHuman( string name, Vector3 position ) : base( 1, RankOfPersonage.Normal, 0, position ) {
            Name = name;
        }

    }

    public class Init : MonoBehaviour {

        bool chg = false;

        void Awake() {
            InputManager.Init();
            var go = GameObject.Find( "EntityList" );
            go.AddComponent<Entity.EntityList>();
        }

        Player player;
        TempHuman jack, mike;

        IEnumerator Start() {
            yield return Player.Interface.Init();
            Player.Interface.Enable = true;

            Model.Request( 0 );
            Model.LoadRequestedNow();

            player = new Player( 1, 0, new Vector3( 0f, 1f, 0f ) );
            jack = new TempHuman( "Jack", new Vector3( -2f, 0.5f, -4f ) );
            mike = new TempHuman( "Mike", new Vector3( 2f, 0.5f, -4f ) );
            mike.Die();

            Model.Unload();

            Player.Interface.Fade( FadeMode.In );

            yield return new WaitForSeconds( 4f );
            Player.Interface.ShowMessageBox( 4f, "Hello, world!" );
        }





        void Update() {
            if( Player.Exist() ) {
                if( Input.GetKeyDown( KeyCode.F ) )
                    mike.Reanimate();
            }
        }

        void FixedUpdate() { }

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