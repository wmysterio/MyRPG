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

    public partial class Personage {

        partial void taskManager() {
            switch( CurrentTask ) {
                case Task.WALK_AROUND:
                break;
                case Task.WALK_TO_NODE:
                if( currentPath != null && currentNode != null ) {
                   if( Near( currentNode ) ) {
                        if( !currentPath.NextNode( out currentNode ) ) {
                            ClearTask();
                        } else {
                            WalkToPoint( currentNode );
                        }
                    }
                } else {
                    ClearTask();
                }
                if( currentNode != null ) {
                    tempVector = currentNode.Point - gameObject.transform.position;
                    tempVector.y = 0;
                    if( tempVector != Vector3.zero )
                        gameObject.transform.rotation = Quaternion.Slerp( gameObject.transform.rotation, Quaternion.LookRotation( tempVector ), Time.deltaTime * 10f );
                    MoveForward();
                }
                break;
                default:
                move();
                break;
            }
        }

    }

    public enum Task {
        STAY_IDLE,
        WALK_AROUND,
        WALK_TO_NODE
    }

}