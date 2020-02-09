/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System;
using System.Collections;
using UnityEngine;

namespace MyRPG {

    public static class Coroutines {

        private static MonoBehaviour behaviour = null;
        private static bool isInit;

        public static void Init( MonoBehaviour behaviour ) {
            if( isInit )
                return;
            Coroutines.behaviour = behaviour;
            isInit = true;
        }

        public static Coroutine Start( IEnumerator routine ) { return behaviour.StartCoroutine( routine ); }
        public static void Stop( IEnumerator routine ) { behaviour.StopCoroutine( routine ); }
        public static void Stop( Coroutine routine ) { behaviour.StopCoroutine( routine ); }
        public static void StopAll() { behaviour.StopAllCoroutines(); }
        public static Coroutine Repeat( Action func, float time, float repeatRate ) { return behaviour.StartCoroutine( repeatCoroutine( func, time, repeatRate ) ); }

        private static IEnumerator repeatCoroutine( Action func, float time, float repeatRate ) {
            float timer = 0f;
            while( timer < time ) {
                yield return null;
                timer += Time.unscaledDeltaTime;
            }
            timer = 0f;
            while( true ) {
                while( timer < repeatRate ) {
                    yield return null;
                    timer += Time.unscaledDeltaTime;
                }
                timer = 0f;
                func.Invoke();
            }
        }

    }

}