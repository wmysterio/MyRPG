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

    public sealed class Room : MonoBehaviour {

        private static int unloadIndex = -1, currentIndex = -1;

        public static bool Loading { get; private set; }

        public static void Load( All room ) {
            var lvl = ( int ) room;
            if( currentIndex == lvl )
                return;
            unloadIndex = currentIndex;
            currentIndex = lvl;
            Player.Interface.Fade( FadeMode.Out );
            Loading = true;
        }

        private AsyncOperation asyncOperation = null;

        private void Awake() { Loading = false; }

        private void Start() { }

        private void Update() {
            if( !Loading )
                return;
            if( Player.Interface.Fadind )
                return;
            if( unloadIndex != -1 ) {
                if( asyncOperation == null )
                    StartCoroutine( unloadLevel( unloadIndex ) );
                if( !asyncOperation.isDone )
                    return;
                asyncOperation = null;
                unloadIndex = -1;
            }
            if( asyncOperation == null )
                StartCoroutine( loadLevel( currentIndex ) );
            if( !asyncOperation.isDone )
                return;
            asyncOperation = null;
            Player.Interface.Fade( FadeMode.In );
            Loading = false;
        }


        private void OnGUI() {
            // ... 
        }

        private IEnumerator loadLevel( int index ) {
            asyncOperation = SceneManager.LoadSceneAsync( index );
            yield return asyncOperation;
        }

        private IEnumerator unloadLevel( int index ) {
            asyncOperation = SceneManager.UnloadSceneAsync( index );
            yield return asyncOperation;
        }


        public enum All : int {
            SelectProfile = 1,
            CreateProfile = 2,
            MainMenu = 3,
            Level_1 = 4
        }

    }

}