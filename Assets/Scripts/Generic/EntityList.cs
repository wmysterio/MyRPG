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

    public partial class Entity {

        public class EntityList : MonoBehaviour {

            public static GameObject Container { get; private set; }

            private void Awake() {
                Container = gameObject;
                Coroutines.Init( this );
                Coroutines.Repeat( Calendar.Update, 0f, 1f );
                DontDestroyOnLoad( this );
            }

            private void Start() { }

            private void Update() {
                updator.Update();
                if( Player.Interface.IsInit )
                    Player.Interface.Update();
                Camera.Update();
            }

            private void FixedUpdate() {
                updator.FixedUpdate();
            }

            private void OnGUI() {
                if( Player.Interface.IsInit ) {
                    Player.Interface.Draw();
                }
            }

        }

    }

}