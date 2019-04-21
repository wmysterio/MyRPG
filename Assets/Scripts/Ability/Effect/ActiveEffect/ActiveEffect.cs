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

	public abstract class ActiveEffect : Effect {

        private float duration, timer = 0f, tickTimer = 0f;

        protected float tickTimerLimit = 2f;

        public int MaxCount { get; protected set; }
        public bool HasTick { get; protected set; }

        public ActiveEffect( float duration, Personage sender, Personage target= null ) : base( false, sender, target ) {
            nameId = 6;
            MaxCount = 0;
            HasTick = false;
            this.duration = duration;
        }

        public override bool Update() {
            if( !base.Update() )
                return false;
            timer += 1f * Time.deltaTime;
            Timer = Mathf.Round( duration - timer );
            if( timer > duration ) {
                end();
                Destroy();
                return true;
            }
            if( HasTick ) {
                tickTimer += Sender.CurrentCharacteristic.CastSpeed * Time.deltaTime;
                if( tickTimer > tickTimerLimit ) {
                    if( MaxCount > 0 && MaxCount > Count )
                        Count++;
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