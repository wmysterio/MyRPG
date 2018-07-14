﻿/*
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

        public const float TURN_SPEED = 100f;

        public static Player Current { get; private set; }
        public static bool Exist() { return Current != null; }

        public int Money { get; private set; }
        public int CurrentExperience { get; private set; }
        public int TotalExperience { get; private set; }
        public int ExperienceToLevel { get { return TotalExperience - CurrentExperience; } }
        public RingForKeys Keys { get; private set; }

        public Player( string name, int level, int modelId, Vector3 position ) : base( level, RankOfPersonage.Normal, modelId, position ) {
            nameId = -1;
            Name = name;
            if( Current != null )
                throw new System.Exception( "Гравець може бути тільки один!" );
            Current = this;
            Money = 0;
            CurrentExperience = 0;
            TotalExperience = calculateMaxExperience();
            Keys = new RingForKeys();
        }

        protected override void update() {
            base.update();
        }

        protected override void onCast( CastResult result, TypeOfResources resource = TypeOfResources.Nothing ) {
            base.onCast( result, resource );

        }

        public void AddMoney( int amount ) {
            try {
                var pMoney = Money;
                checked { pMoney += amount; }
                if( pMoney > 0 )
                    Money = pMoney;
            } catch {
                if( amount > 0 )
                    Money = int.MaxValue;
            }
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
            if( Immortal )
                return;
            Camera.Detach();
            Interface.ToggleWindow( Window.None );
        }
        public override void Destroy() {
            base.Destroy();
            Camera.Detach();
            Interface.ToggleWindow( Window.None );
            Current = null;
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

            if( !InputManager.GetMouse( MouseKeyName.Right ) ) {
                if( InputManager.GetKey( KeyName.TURN_LEFT ) ) {
                    Turn( -TURN_SPEED );
                } else if( InputManager.GetKey( KeyName.TURN_RIGHT ) ) {
                    Turn( TURN_SPEED );
                }
                if( InputManager.GetKey( KeyName.LEFT ) ) {
                    MoveLeft();
                } else if( InputManager.GetKey( KeyName.RIGHT ) ) {
                    MoveRight();
                }
            }

            if( InputManager.GetKey( KeyName.FORWARD ) ) {
                MoveForward();
            } else if( InputManager.GetKey( KeyName.BACK ) ) {
                MoveBack();
            }
            if( InputManager.IsKeyDown( KeyName.JUMP ) ) {
                Jump();
            }

        }

    }

}