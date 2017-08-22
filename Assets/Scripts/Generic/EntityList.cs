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

            void Awake() {
                Container = gameObject;
                DontDestroyOnLoad( this );
            }

            void Start() {

            }

            void Update() {
                updator.Update();
            }
            
            void FixedUpdate() {
                updator.FixedUpdate();
            }

            void OnGUI() {
                
                if( Player.Exist() && Player.Interface.IsInit ) {
                    Player.Current.Draw();
                }




            }

        }



    }

}