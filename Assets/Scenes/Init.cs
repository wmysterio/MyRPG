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

        Player player;

        IEnumerator Start() {
            StartCoroutine( loadUI() );

            Model.Request( 0 );
            Model.LoadRequestedNow();

            player = new Player( 1, 0, new Vector3( 0f, 1f, 0f ) );
            Player.Interface.Enable = true;
            Player.Interface.EnableСinematicView = true;


            Model.Unload();

            yield return new WaitForSeconds( 2 );
            
            Player.Interface.PrintSubtitles( 10f, "Знайти {0} предметів і потім віднеси їх старцю на край печери! В тебе буде всього {1} секунд, щоб дістатись туди. Інакше він забуде про завдання! Давай, виручай, друже!", 20, 5 );


            yield return new WaitForSeconds( 11 );

            Player.Interface.PrintSubtitles( 5f, "Привіт, світ!" );

            yield return new WaitForSeconds( 6 );

            Player.Interface.EnableСinematicView = false;
        }





        void Update() {
            if( Player.Exist() ) {

            }
        }

        void FixedUpdate() { }

        private IEnumerator loadUI() { yield return Player.Interface.Init(); }

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