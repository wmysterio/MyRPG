using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG {


    public class Personage : Entity {

        public const int MIN_LEVEL = 1;
        public const int MAX_LEVEL = 100;

        private Characteristic baseCharacteristic;

        public TypeOfPersonage Type { get; private set; }
        public RankOfPersonage Rank { get; private set; }
        public bool IsDead { get; private set; }
        public bool IsStopped { get; private set; }
        public float CurrentHealth { get; private set; }
        public float CurrentMana { get; private set; }
        public float CurrentEnergy { get; private set; }

        public int Level { get; protected set; }
        public Characteristic CurrentCharacteristic { get; protected set; }

        public bool CanMove { get; set; }

        public Personage( int level, RankOfPersonage rank, TypeOfPersonage type, int modelId, Vector3 position ) : base( modelId, position ) {
            Name = "Personage";
            IsDead = false;
            CanMove = true;
            IsStopped = false;
            Level = 0;
            Rank = rank;
            LevelUp( level );
        }

        protected override void update() {
            base.update();
            updateCharacteristic();

            if( !IsDead ) {
                if( 0 >= CurrentHealth ) {
                    Die();
                    return;
                }
                CurrentHealth += CurrentCharacteristic.HealthRegeneration;
                CurrentMana += CurrentCharacteristic.ManaRegeneration;
                CurrentEnergy += CurrentCharacteristic.EnergyRegeneration;
                if( CurrentHealth > CurrentCharacteristic.MaxHealth )
                    CurrentHealth = CurrentCharacteristic.MaxHealth;
                if( CurrentMana > CurrentCharacteristic.MaxMana )
                    CurrentMana = CurrentCharacteristic.MaxMana;
                if( CurrentEnergy > CurrentCharacteristic.MaxEnergy )
                    CurrentEnergy = CurrentCharacteristic.MaxEnergy;
            }

        }




        public void Restore() {
            if( IsDead )    // <--- додати у відео
                return;     // <--- додати у відео
            CurrentHealth = CurrentCharacteristic.MaxHealth;
            CurrentMana = CurrentCharacteristic.MaxMana;
            CurrentEnergy = CurrentCharacteristic.MaxEnergy;
        }

        public void LevelUp( int amount = 1 ) {
            if( IsDead )    // <--- додати у відео
                return;     // <--- додати у відео
            Level += amount;
            if( MIN_LEVEL > Level )
                Level = MIN_LEVEL;
            if( Level > MAX_LEVEL )
                Level = MAX_LEVEL;
            calculateCharacteristic();
            Restore(); // <--- додати у відео
        }

        private void calculateCharacteristic() {
            baseCharacteristic = Characteristic.CreateBase( Level, Rank );
            CurrentCharacteristic = Characteristic.CreateEmpty();
            updateCharacteristic();
        }


        private void updateCharacteristic() {
            CurrentCharacteristic = ( CurrentCharacteristic.Clear() ) + baseCharacteristic;
        }


        public void Die() {
            IsDead = true;
            CurrentHealth = 0f;
            CurrentMana = 0f;
            CurrentEnergy = 0f;
        }

        public void Reanimate() {
            IsDead = false;
            Restore();
        }

    }

    public enum RankOfPersonage : int {
        Normal = 1, // звичайний персонаж, підсилення x1
        Strong = 2, // сильний персонаж, підсилення x2
        Elite = 5,  // елітний персонаж, підсилення x5
        Boss = 25   // бос, підсилення x25
    }

    public enum TypeOfPersonage {
        Undead,     // зобмі
        Mechanism,  // механізм
        Daemon,     // демони, чорти
        Elemental,  // елементаль
        Animal,     // тварина
        Humanoid    // людиноподібна істота
    }

}