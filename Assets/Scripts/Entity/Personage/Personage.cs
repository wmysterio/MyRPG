using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG {

    public enum TypeOfPersonage {
        Undead,     // зобмі
        Mechanism,  // механізм
        Daemon,     // демони, чорти
        Elemental,  // елементаль
        Animal,     // тварина
        Humanoid    // людиноподібна істота
    }

    public class Personage : Entity {
        
        public TypeOfPersonage Type { get; private set; }

        public bool IsDead { get; private set; }
        public bool CanMove { get; set; }
        public bool IsStopped { get; private set; }

        public Personage( TypeOfPersonage type, int modelId, Vector3 position ) : base( modelId, position ) {
            Name = "Personage";
            IsDead = false;
            CanMove = true;
            IsStopped = false;
        }

        public void Die() {
            IsDead = true;
        }

        public void Reanimate() {
            IsDead = false;
        }

    }

}