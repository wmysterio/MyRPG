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

	public abstract class InstantSpell : Spell {

        public InstantSpell( int minLevel ) : base( minLevel, TypeOfSpell.Instant, 0f ) {
            nameId = 37;
            EnableCastInAir = true;
            EnableCastInRun = true;
        }

    }

}