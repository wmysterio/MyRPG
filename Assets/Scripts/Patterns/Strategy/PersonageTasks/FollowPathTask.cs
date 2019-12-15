/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using UnityEngine;

namespace MyRPG.Patterns.Strategy.PersonageTasks {

    public sealed class FollowPathTask : IPersonageTask {
        
        private Path currentPath;
        private Path.Node currentNode;

        private FollowPathTask() { }
        public FollowPathTask( Path path ) { currentPath = path; }

        public bool Execute( Personage personage ) {
            if( currentPath == null )
                return false;
            if( currentNode == null ) {
                if( !currentPath.NextNode( out currentNode ) )
                    return false;
            }
            if( personage.Near( currentNode ) ) {
                currentNode = null;
                return true;
            }
            var tempVector = currentNode.Point - personage.GetGameObject().transform.position;
            tempVector.y = 0;
            if( tempVector != Vector3.zero )
                personage.GetGameObject().transform.rotation = Quaternion.Slerp( personage.GetGameObject().transform.rotation, Quaternion.LookRotation( tempVector ), Time.deltaTime * 10f );
            //personage.MoveForward();
            personage.AnimationGroup.MoveForwardBack = 1f;
            return true;
        }
        
    }

}