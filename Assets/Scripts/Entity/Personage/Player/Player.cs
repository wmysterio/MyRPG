using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG {

	public class Player : Humanoid {

        public static Player Current { get; private set; }
        public static bool Exist() { return Current != null;  }

        public Player( int modelId, Vector3 position ) : base( modelId, position ) {
            Name = "Player";
            Current = this;
        }

    }

}