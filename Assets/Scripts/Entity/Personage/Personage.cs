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

    public abstract partial class Personage : Entity {

        public const int MIN_LEVEL = 1;
        public const int MAX_LEVEL = 100;

        private Characteristic baseCharacteristic;
        private RelationshipOfPersonage relationship = RelationshipOfPersonage.Friendly;
        private Vector3 velocity, tempVector;
        private Path currentPath;
        private Path.Node currentNode;
        private bool moveFlag;

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
        public Task CurrentTask { get; private set; }

        public int Level { get; protected set; }
        public Characteristic CurrentCharacteristic { get; protected set; }

        public bool Immortal { get; set; }
        public bool EnableJumping { get; set; }
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
            nameId = 3;
            IsDead = false;
            CanMove = true;
            IsStopped = false;
            Immortal = false;
            Level = 0;
            Rank = rank;
            Relationship = RelationshipOfPersonage.Neutral;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            Effects = new EffectList();
            Loot = new Bag();
            Equipments = new EquipmentList();
            Target = null;
            EnableJumping = true;
            moveFlag = false;
            ClearTask();
            velocity = Vector3.zero;
            tempVector = Vector3.zero;

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


        protected override void update() {
            base.update();
            Loot.UpdateItems();
            updateCharacteristic();
            moveFlag = false;
            if( targetingScript.MouseHover ) {
                if( Player.Exist() && InputManager.IsMouseDown( MouseKeyName.Left ) ) {
                    if( !Player.Current.NoLongerNeeded && !Player.Current.IsDead )
                        Player.Current.Target = this;
                }
            }
            if( !IsDead ) {
                if( CanMove )
                    taskManager();
                IsStopped = moveFlag;
                if( 0 >= CurrentHealth && !Immortal ) {
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

        protected override void physics() {
            base.physics();
            rigidbody.velocity += velocity;
            velocity = Vector3.zero;
        }

        protected virtual void move() { }

        public bool IsFriendlyOf( Personage personage ) { return relationship != personage.relationship; }
        public void MoveForward() {
            gameObject.transform.Translate( Vector3.forward * CurrentCharacteristic.MoveSpeed * Time.deltaTime );
            moveFlag = true;
        }
        public void MoveBack() {
            gameObject.transform.Translate( Vector3.back * CurrentCharacteristic.MoveSpeed * Time.deltaTime );
            moveFlag = true;
        }
        public void MoveLeft() {
            gameObject.transform.Translate( Vector3.left * CurrentCharacteristic.MoveSpeed * Time.deltaTime );
            moveFlag = true;
        }
        public void MoveRight() {
            gameObject.transform.Translate( Vector3.right * CurrentCharacteristic.MoveSpeed * Time.deltaTime );
            moveFlag = false;
        }
        public void Turn( float speed ) {
            gameObject.transform.Rotate( 0f, speed * Time.deltaTime, 0f );
            moveFlag = true;
        }
        public void Jump() {
            if( NoLongerNeeded || IsDead || !EnableJumping || DistanceToGround() > 0.02f )
                return;
            moveFlag = true;
            velocity.y += 4f;
        }
        public void Restore() {
            if( IsDead )
                return;
            CurrentHealth = CurrentCharacteristic.MaxHealth;
            CurrentMana = CurrentCharacteristic.MaxMana;
            CurrentEnergy = CurrentCharacteristic.MaxEnergy;
        }
        public void LevelUp( int amount = 1 ) {
            Level = Mathf.Clamp( Level += amount, MIN_LEVEL, MAX_LEVEL );
            calculateCharacteristic();
            Restore();
        }
        public virtual void Die() {
            if( IsDead || Immortal )
                return;
            Effects.ClearAll();
            ClearTask();
            Target = null;
            IsDead = true;
            CurrentHealth = 0f;
            CurrentMana = 0f;
            CurrentEnergy = 0f;
        }
        public void Reanimate( float percent = 20f ) {
            if( !IsDead )
                return;
            IsDead = false;
            if( percent > 0f && 99f > percent ) {
                CurrentHealth = Characteristic.GetValueOfPercentage( CurrentCharacteristic.MaxHealth, percent );
                CurrentMana = Characteristic.GetValueOfPercentage( CurrentCharacteristic.MaxMana, percent );
                return;
            }
            Restore();
        }
        public void ClearTask() {
            CurrentTask = Task.STAY_IDLE;
            currentNode = null;
            currentPath = null;
        }
        public void WalkToPoint( Path.Node node ) {
            CurrentTask = Task.WALK_TO_NODE;
            currentNode = node;
        }
        public void AssignToPath( Path path ) {
            currentPath = path;
            if( !currentPath.NextNode( out currentNode ) ) {
                currentNode = null;
                currentPath = null;
                return;
            }
            WalkToPoint( currentNode );
        }


        partial void taskManager();

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