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

    public partial class Player : Humanoid {

        public static Player Current { get; private set; }
        public static bool Exist() { return Current != null; }

        public int CurrentExperience { get; private set; }
        public int TotalExperience { get; private set; }
        public int ExperienceToLevel {
            get { return TotalExperience - CurrentExperience; }
        }

        public Player( int level, int modelId, Vector3 position ) : base( level, RankOfPersonage.Normal, modelId, position ) {
            Name = "Player";
            Current = this;
            CurrentExperience = 0;
            TotalExperience = calculateMaxExperience();
        }

        protected override void update() {
            base.update();
            Interface.Update();
        }

        public void AddExperience( int amount ) {
            if( MAX_LEVEL == Level )
                return;
            CurrentExperience += amount;
            if( CurrentExperience > TotalExperience ) {
                var newExp = Mathf.Abs( ExperienceToLevel );
                LevelUp();
                if( Level == MAX_LEVEL ) {
                    CurrentExperience = 0;
                    TotalExperience = 0;
                } else {
                    CurrentExperience = newExp;
                    TotalExperience = calculateMaxExperience();
                }
            }
        }

        public override void Die() {
            base.Die();
            Target = null;
        }

        private int calculateMaxExperience() { return ( int ) ( 795 + 250 * Mathf.Pow( 1.08f, Level ) ); }
        

        public void Draw() { Interface.Draw(); }


    }

}