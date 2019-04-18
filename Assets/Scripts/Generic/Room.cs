/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG {

    public static class Room {

        private static int unloadIndex = -1, currentIndex = -1;
        private static Coroutine coroutine;

        public static bool Loading { get; private set; }

        public static void Switch( int sceneID ) {
            if( currentIndex == sceneID )
                return;
            unloadIndex = currentIndex;
            currentIndex = sceneID;
            coroutine = Coroutines.Start( load_level( unloadIndex, currentIndex ) );
        }
        public static void Switch( Special sceneID ) { Switch( ( int ) sceneID ); }
        public static void Switch( Levels sceneID ) { Switch( ( int ) sceneID ); }
        
        private static IEnumerator load_level( int lastID, int currentID ) {
            Loading = true;
            if( lastID != -1 )
                yield return SceneManager.UnloadSceneAsync( lastID );
            if( currentID != -1 )
                yield return SceneManager.LoadSceneAsync( currentID );
            Coroutines.Stop( coroutine );
            Loading = false;
        }

        public enum Special : int {
            SelectProfile = 1,
            CreateProfile = 2,
            MainMenu = 3
        }

        public enum Levels : int {
            TrainingLevel = 4,
            Level_1 = 5
        }

    }

}