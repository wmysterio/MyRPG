using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG {

	public class Humanoid : Personage {

        public Humanoid( int level, RankOfPersonage rank, int modelId, Vector3 position ) : base( level, rank, TypeOfPersonage.Humanoid, modelId, position ) {
            Name = "Humanoid";
        }

    }

}