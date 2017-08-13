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

	public abstract class ActiveEffect : Effect {

        private float duration, timer = 0f, tickTimer = 0f;

        protected float tickTimerLimit = 2f;

        public int MaxCount { get; protected set; }

        public ActiveEffect( float duration, Personage sender, Personage target= null ) : base( false, sender, target ) {
            Name = "Active Effect";
            MaxCount = 0;
            this.duration = duration;
        }

        public override bool Update() {
            if( !base.Update() )
                return false;
            timer += 1f * Time.deltaTime;
            TimeToRestore = Mathf.Round( duration - timer );
            if( timer > duration ) {
                end();
                Destroy();
                return true;
            }
            if( MaxCount > 0 ) {
                tickTimer += Sender.CurrentCharacteristic.CastSpeed * Time.deltaTime;
                if( tickTimer > tickTimerLimit ) {
                    Count++;
                    if( Count > MaxCount )
                        Count = Count;
                    Use();
                    tickTimer = 0f;
                }
            }
            return true;
        }

        public override void Recreate() {
            base.Recreate();
            timer = 0f;
        }

    }

}