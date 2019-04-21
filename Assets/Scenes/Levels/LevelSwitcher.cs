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

	public sealed class LevelSwitcher : MonoBehaviour {

        public Room.Levels Level = Room.Levels.TrainingLevel;
        public bool OverridePosition = false;
        public Vector3 OverrideStartPoint = Vector3.zero;

        private void OnCollisionEnter( Collision collision ) {
            if( collision.gameObject.CompareTag( Player.TAG ) ) {
                Player.Current.Immortal = true;
                Player.Current.CanMove = false;
                Player.Interface.Fade( FadeMode.In );
                if( OverridePosition )
                    BaseLevelStarter.OverrideStartPoint( OverrideStartPoint );
                Room.Switch( Level );
            }
        }

    }

}