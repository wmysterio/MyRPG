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

	public abstract class InstantSpell : Spell {

        public InstantSpell( int minLevel ) : base( minLevel, TypeOfSpell.Streaming, 0f ) {
            nameId = 37;
        }

    }

}