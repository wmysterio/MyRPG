using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG {

	public class Init : MonoBehaviour {

		void Awake() {
            var go = GameObject.Find( "EntityList" );
            go.AddComponent<Entity.EntityList>();
		}

		void Start() {

        }
		
		void Update() {
			
		}
		
		void FixedUpdate() {
			
		}
		
		void OnGUI() {
			
		}
		
	}

}