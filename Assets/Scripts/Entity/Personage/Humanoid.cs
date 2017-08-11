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

    public class Humanoid : Personage {

        public GenderOfHumanoid Gender { get; private set; }
        public RaseOfHumanoid Rase { get; private set; }

        public Humanoid( int level, RankOfPersonage rank, int modelId, Vector3 position ) : base( level, rank, TypeOfPersonage.Humanoid, modelId, position ) {
            Name = "Humanoid";
            Gender = modelId % 2 == 0 ? GenderOfHumanoid.Male : GenderOfHumanoid.Female;
            Rase = ( RaseOfHumanoid ) ( modelId - ( byte ) Gender );
        }

    }

    public enum GenderOfHumanoid : byte {
        Male = 0,   // Чоловік
        Female = 1, // Жінка
    }

    public enum RaseOfHumanoid : byte {
        Human = 0,    // Людина
        Orc = 2,      // Орк
        Elf = 4,      // Ельф
        Goblіn = 6,   // Гоблін
    }
}