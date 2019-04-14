/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG.Patterns.Strategy.PersonageTasks {

    public sealed class WalkAroundTask : IPersonageTask {

        public bool Execute( Personage personage ) {
            return true;
        }

    }

}