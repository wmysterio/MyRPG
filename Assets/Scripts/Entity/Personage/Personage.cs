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

    public abstract class Personage : Entity {

        public const int MIN_LEVEL = 1;
        public const int MAX_LEVEL = 100;

        private Characteristic baseCharacteristic;
        private RelationshipOfPersonage relationship = RelationshipOfPersonage.Friendly;

        public TypeOfPersonage Type { get; private set; }
        public RankOfPersonage Rank { get; private set; }
        public bool IsDead { get; private set; }
        public bool IsStopped { get; private set; }
        public float CurrentHealth { get; private set; }
        public float CurrentMana { get; private set; }
        public float CurrentEnergy { get; private set; }
        public EffectList Effects { get; private set; }
        public Bag Loot { get; private set; }
        public EquipmentList Equipments { get; private set; }

        public int Level { get; protected set; }
        public Characteristic CurrentCharacteristic { get; protected set; }
        
        public bool CanMove { get; set; }
        public RelationshipOfPersonage Relationship {
            get { return relationship; }
            set {
                if( this is Player )
                    return;
                relationship = value;
            }
        }

        public Personage Target { get; set; }


        public Personage( int level, RankOfPersonage rank, TypeOfPersonage type, int modelId, Vector3 position ) : base( modelId, position ) {
            Name = "Personage";
            IsDead = false;
            CanMove = true;
            IsStopped = false;
            Level = 0;
            Rank = rank;
            Relationship = RelationshipOfPersonage.Neutral;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            Effects = new EffectList();
            Loot = new Bag();
            Equipments = new EquipmentList();
            Target = null;

            // !!! Ініціалізацію об'єктів здійснювати до методу LevelUp
            LevelUp( level );
        }

        private void calculateCharacteristic() {
            baseCharacteristic = Characteristic.CreateBase( Level, Rank );
            CurrentCharacteristic = Characteristic.CreateEmpty();
            updateCharacteristic();
        }

        private void updateCharacteristic() {
            CurrentCharacteristic = ( ( ( CurrentCharacteristic.Clear() + baseCharacteristic ) + Equipments.CurrentCharacteristic ) + Effects.Update() );
        }

        public bool IsFriendlyOf( Personage personage ) { return relationship != personage.relationship; }

        protected override void update() {
            base.update();
            Loot.UpdateItems();
            updateCharacteristic();

            if( targetingScript.MouseHover ) {
                if( Player.Exist() && Input.GetMouseButtonDown( 0 ) ) {
                    if( !Player.Current.NoLongerNeeded && !Player.Current.IsDead )
                        Player.Current.Target = this;
                }
            }

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
            if( IsDead )
                return;
            CurrentHealth = CurrentCharacteristic.MaxHealth;
            CurrentMana = CurrentCharacteristic.MaxMana;
            CurrentEnergy = CurrentCharacteristic.MaxEnergy;
        }

        public void LevelUp( int amount = 1 ) {
            Level += amount;
            if( MIN_LEVEL > Level )
                Level = MIN_LEVEL;
            if( Level > MAX_LEVEL )
                Level = MAX_LEVEL;
            calculateCharacteristic();
            Restore();
        }

        public virtual void Die() {
            if( IsDead )
                return;
            Effects.ClearAll();
            IsDead = true;
            CurrentHealth = 0f;
            CurrentMana = 0f;
            CurrentEnergy = 0f;
        }

        public void Reanimate() {
            if( !IsDead )
                return;
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

    public enum RelationshipOfPersonage {
        Friendly,   // Привітний
        Neutral,    // Нейтральний
        Enemy       // Ворог
    }

}