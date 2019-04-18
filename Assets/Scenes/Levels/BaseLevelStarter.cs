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

namespace MyRPG.Levels {

	public abstract class BaseLevelStarter : MonoBehaviour {

        public Audio.BackgroundID Background;
        public bool BackgroundLoop;
        
        IEnumerator Start() {

            Model.Request( 0 );
            Model.Request( GetUsedModels() );
            yield return Model.LoadRequestedAsync();

            if( Player.Exist() ) {
                Player.Current.Position = transform.position;
            } else {
                new Player( "Player", Player.MAX_LEVEL, 0, transform.position );
                Camera.AttachToPlayer();
            }

            SpawnMobs();

            Audio.PlayBackground( Background, BackgroundLoop );
            Player.Interface.Fade( FadeMode.Out );

		}

        protected abstract int[] GetUsedModels();
        protected abstract void SpawnMobs();

    }

}