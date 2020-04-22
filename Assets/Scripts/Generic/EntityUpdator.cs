/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/
using System.Collections.Generic;
using UnityEngine;

namespace MyRPG {

    public partial class Entity {

        public sealed class EntityUpdator : MonoBehaviour {

            public static Dictionary<Entity, Entity> ActiveEntities = new Dictionary<Entity, Entity>();
            public static Dictionary<Entity, Entity> PassiveEntities = new Dictionary<Entity, Entity>();

            private Entity targetUpdate = null;

            public void AddReference( Entity entity ) {
                targetUpdate = entity;
                ActiveEntities.Add( entity, entity );
                enabled = true;
            }

            private void Awake() { enabled = false; }
            private void Update() { targetUpdate.update(); }
            private void FixedUpdate() { targetUpdate.physics(); }

            private void OnEnable() {
                if( targetUpdate == null )
                    return;
                targetUpdate.onEnable();
                if( PassiveEntities.ContainsKey( targetUpdate ) )
                    PassiveEntities.Remove( targetUpdate );
                if( !ActiveEntities.ContainsKey( targetUpdate ) )
                    ActiveEntities.Add( targetUpdate, targetUpdate );
            }
            private void OnDisable() {
                if( targetUpdate == null )
                    return;
                targetUpdate.onDisable();
                if( ActiveEntities.ContainsKey( targetUpdate ) )
                    ActiveEntities.Remove( targetUpdate );
                if( !PassiveEntities.ContainsKey( targetUpdate ) )
                    PassiveEntities.Add( targetUpdate, targetUpdate );
            }
            private void OnDestroy() {
                if( targetUpdate == null )
                    return;
                targetUpdate.onDestroy();
                if( PassiveEntities.ContainsKey( targetUpdate ) )
                    PassiveEntities.Remove( targetUpdate );
                if( ActiveEntities.ContainsKey( targetUpdate ) )
                    ActiveEntities.Remove( targetUpdate );
                targetUpdate = null;
            }

            private void OnMouseEnter() { targetUpdate.onMouseEnter(); }
            private void OnMouseOver() { targetUpdate.onMouseOver(); }
            private void OnMouseExit() { targetUpdate.onMouseExit(); }

        }

    }

}