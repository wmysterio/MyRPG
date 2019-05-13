﻿/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System.Collections;
using UnityEngine;

namespace MyRPG.Levels {

    public abstract class BaseLevelStarter : MonoBehaviour {

        private static bool overridePosition = false;
        private static Vector3 overrideStartPoint = Vector3.zero;
        public static void OverrideStartPoint( Vector3 newStartPoint ) {
            overridePosition = true;
            overrideStartPoint = newStartPoint;
        }

        public Audio.BackgroundID Background = Audio.BackgroundID.OFF;
        public bool BackgroundLoop = false;

        IEnumerator Start() {

            while( Room.Loading ) { yield return null; }

            Model.Request( 0 );
            Model.Request( GetUsedModels() );
            yield return Model.LoadRequestedAsync();

            if( Player.Exist() ) {
                Player.Current.Position = overridePosition ? overrideStartPoint : transform.position;
            } else {
                new Player( "Player", Player.MAX_LEVEL, 0, transform.position );
                Camera.AttachToPlayer();
            }
            overridePosition = false;

            SpawnEntities();

            Audio.PlayBackground( Background, BackgroundLoop );
            Player.Interface.Fade( FadeMode.Out );

            Player.Current.CanMove = true;
            Player.Current.Immortal = false;

        }

        protected abstract int[] GetUsedModels();
        protected abstract void SpawnEntities();

    }

}