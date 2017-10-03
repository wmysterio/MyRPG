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
            nameId = -1;
            Name = "";
            Current = this;
            CurrentExperience = 0;
            TotalExperience = calculateMaxExperience();
        }

        protected override void update() {
            base.update();
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
            Interface.ToggleWindow( Window.None );
        }

        private int calculateMaxExperience() { return ( int ) ( 795 + 250 * Mathf.Pow( 1.08f, Level ) ); }

        protected override void move() {
            base.move();

            if( InputManager.IsKeyDown( KeyName.VIEW_BAG ) )
                Interface.ToggleWindow( Window.Bag );
            if( InputManager.IsKeyDown( KeyName.VIEW_EFFECTS ) )
                Interface.ToggleWindow( Window.Effects );
            if( InputManager.IsKeyDown( KeyName.VIEW_PERSONAGE ) )
                Interface.ToggleWindow( Window.Personage );
            if( InputManager.IsKeyDown( KeyName.VIEW_QUESTS ) )
                Interface.ToggleWindow( Window.Quests );
            if( InputManager.IsKeyDown( KeyName.VIEW_SPELLS ) )
                Interface.ToggleWindow( Window.Spells );

            if( InputManager.GetKey( KeyName.TURN_LEFT ) ) {
                Turn( -100f );
            } else if( InputManager.GetKey( KeyName.TURN_RIGHT ) ) {
                Turn( 100f );
            }
            if( InputManager.GetKey( KeyName.FORWARD ) ) {
                MoveForward();
            } else if( InputManager.GetKey( KeyName.BACK ) ) {
                MoveBack();
            }
            if( InputManager.GetKey( KeyName.LEFT ) ) {
                MoveLeft();
            } else if( InputManager.GetKey( KeyName.RIGHT ) ) {
                MoveRight();
            }
            if( InputManager.IsKeyDown( KeyName.JUMP ) ) { 
                Jump();
            }

        }

    }

}