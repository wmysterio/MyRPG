using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG {

    public partial class Entity {

        public class EntityUpdator {

            private const int MAX_SIZE = 192;
            private const int LOOP_SIZE = 4;

            private Entity[] entitys;
            private int startFindIndex, findIndexIterator, updateStartIndex, updateStopIndex, updateIterator, fixedUpdateStartIndex, fixedUpdateStopIndex, fixedUpdateIterator;

            public int Lenght { get; private set; }

            public EntityUpdator() {
                entitys = new Entity[ MAX_SIZE ];
                Lenght = 0;
                startFindIndex = 0;
                findIndexIterator = 0;
                resetUpdateIndex();
                resetFixedUpdateIndex();
            }

            private void resetUpdateIndex() {
                updateStartIndex = 0;
                updateStopIndex = LOOP_SIZE;
            }
            private void resetFixedUpdateIndex() {
                fixedUpdateStartIndex = 0;
                fixedUpdateStopIndex = LOOP_SIZE;
            }
            private int findFreeIndex() {
                for( findIndexIterator = startFindIndex; findIndexIterator < MAX_SIZE; findIndexIterator++ ) {
                    if( entitys[ findIndexIterator ] == null ) {
                        startFindIndex = findIndexIterator;
                        break;
                    }
                }
                return startFindIndex;
            }

            public void Add( Entity entity ) {
                Lenght += 1;
                if( Lenght > MAX_SIZE )
                    throw new Exception( "Кількість ігрових об'єктів перевищив максимум!" );
                entitys[ findFreeIndex() ] = entity;
            }

            public void Update() {
                for( updateIterator = updateStartIndex; updateIterator < updateStopIndex; updateIterator++ ) {
                    if( entitys[ updateIterator ] != null )
                        entitys[ updateIterator ].update();  
                }
                updateStartIndex = updateStopIndex;
                updateStopIndex += LOOP_SIZE;
                if( updateStartIndex == MAX_SIZE )
                    resetUpdateIndex();
            }

            public void FixedUpdate() {
                for( fixedUpdateIterator = fixedUpdateStartIndex; fixedUpdateIterator < fixedUpdateStopIndex; fixedUpdateIterator++ ) {
                    if( entitys[ fixedUpdateIterator ] != null )
                        entitys[ fixedUpdateIterator ].physics();
                }
                fixedUpdateStartIndex = fixedUpdateStopIndex;
                fixedUpdateStopIndex += LOOP_SIZE;
                if( fixedUpdateStartIndex == MAX_SIZE )
                    resetFixedUpdateIndex();
            }

        }

    }

}