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

	public static class Coroutines {

        private static MonoBehaviour behaviour = null;

        public static bool IsInit { get; private set; }

        public static void Init( MonoBehaviour behaviour ) {
            if( IsInit )
                return;
            Coroutines.behaviour = behaviour;
            IsInit = true;
        }

        public static Coroutine Start( IEnumerator routine ) { return behaviour.StartCoroutine( routine ); }
        public static void Stop( IEnumerator routine ) { behaviour.StopCoroutine( routine ); }
        public static void Stop( Coroutine routine ) { behaviour.StopCoroutine( routine ); }
        public static void StopAllCoroutines() { behaviour.StopAllCoroutines(); }

    }

}