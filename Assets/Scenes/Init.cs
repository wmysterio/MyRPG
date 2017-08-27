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

    public class Jack : Humanoid {

        public Jack() : base( 1, RankOfPersonage.Normal, 0, new Vector3( -2f, 0.5f, -4f ) ) {
            Name = "Jack";
        }

    }

    public class Init : MonoBehaviour {

        bool chg = false;

        void Awake() {
            var go = GameObject.Find( "EntityList" );
            go.AddComponent<Entity.EntityList>();
        }

        Player player;
        Jack jack;

        IEnumerator Start() {
            yield return Player.Interface.Init();
            Player.Interface.Enable = true;

            Model.Request( 0 );
            Model.LoadRequestedNow();

            player = new Player( 1, 0, new Vector3( 0f, 1f, 0f ) );
            jack = new Jack();

            Model.Unload();

            Player.Interface.Fade( FadeMode.In, 0.007f );

        }





        void Update() {
            if( Player.Exist() ) {
                //print( player.Target );
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