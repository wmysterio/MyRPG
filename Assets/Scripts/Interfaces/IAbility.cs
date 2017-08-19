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

    public enum TypeOfAbility { Effect, Item, Spell }

    public interface IAbility : IDescription {
        
        TypeOfAbility AbilityType { get; }
        float Timer { get; }
        int Count { get; }
        void Use( Personage target = null );

	}

}