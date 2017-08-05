﻿using System;
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
                //for( int i = 0; i < all.Count; i++ ) {
                //    all[ i ].draw();
                //}
            }

        }



    }

}