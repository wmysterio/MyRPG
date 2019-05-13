/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using UnityEngine;

namespace MyRPG.Patterns.Strategy.PersonageTasks {

    public sealed class WalkToPointTask : IPersonageTask {

        private Path.Node currentNode;

        private WalkToPointTask() { }
        public WalkToPointTask( float x, float y, float z, float radius = 1f ) { currentNode = Path.Node.Create( x, y, z, radius ); }
        public WalkToPointTask( Vector3 position, float radius = 1f ) { currentNode = Path.Node.Create( position, radius ); }
        public WalkToPointTask( Path.Node node ) { currentNode = node; }

        public bool Execute( Personage personage ) {
            if( personage.Near( currentNode ) )
                return false;
            var tempVector = currentNode.Point - personage.GetGameObject().transform.position;
            tempVector.y = 0;
            if( tempVector != Vector3.zero )
                personage.GetGameObject().transform.rotation = Quaternion.Slerp( personage.GetGameObject().transform.rotation, Quaternion.LookRotation( tempVector ), Time.deltaTime * 10f );
            personage.MoveForward();
            return true;
        }
        
    }

}