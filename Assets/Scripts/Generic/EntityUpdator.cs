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

    public partial class Entity {
 
        public sealed class EntityUpdator : MonoBehaviour {

            public static readonly Dictionary<Entity, Entity> AllEntitys = new Dictionary<Entity, Entity>();
            public static GameObject Container { get; set; }

            private Entity targetUpdate = null;

            public bool MouseHover { get; private set; }

            public void AddReference( Entity entity ) {
                if( entity == null )
                    return;
                if( AllEntitys.ContainsKey( entity ) )
                    return;
                targetUpdate = entity;
                AllEntitys.Add( entity, entity );
            }

            private void Awake() { MouseHover = false; }
            private void Update() {
                if( targetUpdate == null )
                    return;
                if( targetUpdate.NoLongerNeeded ) {
                    Destroy( targetUpdate.gameObject );
                    return;
                }
                targetUpdate.update();
            }
            private void FixedUpdate() {
                if( targetUpdate == null )
                    return;
                targetUpdate.physics();
            }
            private void OnDestroy() {
                MouseHover = false;
                if( !AllEntitys.ContainsKey( targetUpdate ) )
                    return;
                AllEntitys.Remove( targetUpdate );
                targetUpdate = null;
            }
            private void OnMouseEnter() { MouseHover = false; }
            private void OnMouseOver() { MouseHover = true; }
            private void OnMouseExit() { MouseHover = false; }

        }

    }

}