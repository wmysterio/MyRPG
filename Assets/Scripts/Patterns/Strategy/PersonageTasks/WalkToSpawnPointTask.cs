/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/
using UnityEngine;

namespace MyRPG.Patterns.Strategy.PersonageTasks {

    public sealed class WalkToSpawnPointTask : IPersonageTask {

        public bool Execute( Personage personage ) {
            if( personage.IsPlayer )
                return false;

            return true;
        }

    }

}