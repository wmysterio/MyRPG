using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG {

    public abstract partial class Entity {

        public class EntityList : MonoBehaviour {

            public static GameObject Contrainer { get; private set; }

            void Awake() {
                Contrainer = gameObject; // new GameObject( "EntityList" )
                DontDestroyOnLoad( this ); //Contrainer
            }

            void Start() { }

            void Update() {
                StartCoroutine( update() );
            }

            void FixedUpdate() {
                StartCoroutine( physics() );
            }

            void OnGUI() {
                
                StartCoroutine( draw() );
            }





            IEnumerator update() {
                for( int i = 0; i < all.Count; i++ ) {
                    all[ i ].update();
                    yield return all[ i ];
                }
            }

            IEnumerator physics() {
                for( int i = 0; i < all.Count; i++ ) {
                    yield return all[ i ];
                    all[ i ].physics();
                }
            }

            IEnumerator draw() {
                for( int i = 0; i < all.Count; i++ ) {
                    yield return all[ i ];
                    all[ i ].draw();
                }
            }


        }


    }
}