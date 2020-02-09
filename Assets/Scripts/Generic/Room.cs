/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG {

    public static class Room {

        private static int currentIndex = -1;
        private static Coroutine coroutine;
        private static bool loading;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public static bool Loading { get { return loading; } }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public static void Switch( int sceneID ) {
            if( currentIndex == sceneID || loading )
                return;
            currentIndex = sceneID;
            coroutine = Coroutines.Start( load_level( currentIndex ) );
        }
        public static void Switch( Special sceneID ) { Switch( ( int ) sceneID ); }
        public static void Switch( Levels sceneID ) { Switch( ( int ) sceneID ); }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private static IEnumerator load_level( int currentID ) {
            loading = true;
            if( currentID != -1 )
                yield return SceneManager.LoadSceneAsync( currentID );
            Coroutines.Stop( coroutine );
            loading = false;
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public enum Special : int {
            SelectProfile = 1,
            CreateProfile = 2,
            MainMenu = 3
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public enum Levels : int {
            TrainingLevel = 4,
            Level_1 = 5
        }

    }

}