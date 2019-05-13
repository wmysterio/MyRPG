/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
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