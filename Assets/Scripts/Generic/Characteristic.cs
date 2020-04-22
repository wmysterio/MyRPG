/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/
using System;
using UnityEngine;
using System.Text;
using MyRPG.Configuration;

namespace MyRPG {

    public sealed class Characteristic {

        public const int SLOT_COUNT = 28;

        public delegate void CharacteristicCallbackHandler( int slot, float value, string slotName );

        public static Characteristic CreateEmpty() { return new Characteristic(); }
        public static Characteristic CreateBase( int level, RankOfPersonage rank ) {
            return new Characteristic() {
                MaxHealth = 1000f * level * ( int ) rank,
                MaxMana = 1000f * level * ( int ) rank,
                MaxEnergy = 100f * ( int ) rank,
                HealthRegeneration = 0.09f * level,
                ManaRegeneration = 0.09f * level,
                EnergyRegeneration = 0.1f,
                MoveSpeed = 100f,
                Speed = 1f
            };
        }

        public static float GetPercentageOfValue( float maxValue, float curValue ) {
            if( maxValue == 0f )
                return 0f;
            return ( 100f * curValue ) / maxValue;
        }
        public static float GetValueOfPercentage( float maxValue, float percent ) { return ( percent * maxValue ) / 100f; }

        private int iterator = 0;
        private StringBuilder stringBuilder = new StringBuilder();
        private float[] slots = new float[ SLOT_COUNT ];

        /* характеристики атаки */
        public float PhysicalAttackPower { get { return slots[ 0 ]; } set { slots[ 0 ] = value; } }
        public float MagicPowerOfNature { get { return slots[ 1 ]; } set { slots[ 1 ] = value; } }
        public float MagicPowerOfFire { get { return slots[ 2 ]; } set { slots[ 2 ] = value; } }
        public float MagicPowerOfWater { get { return slots[ 3 ]; } set { slots[ 3 ] = value; } }
        public float MagicPowerOfAir { get { return slots[ 4 ]; } set { slots[ 4 ] = value; } }
        public float MagicPowerOfEarth { get { return slots[ 5 ]; } set { slots[ 5 ] = value; } }
        public float MagicPowerOfDarkness { get { return slots[ 6 ]; } set { slots[ 6 ] = value; } }
        public float MagicPowerOfLight { get { return slots[ 7 ]; } set { slots[ 7 ] = value; } }

        /* характеристики захисту */
        public float PhysicalDefencePower { get { return slots[ 8 ]; } set { slots[ 8 ] = value; } }
        public float MagicDefenceOfNature { get { return slots[ 9 ]; } set { slots[ 9 ] = value; } }
        public float MagicDefenceOfFire { get { return slots[ 10 ]; } set { slots[ 10 ] = value; } }
        public float MagicDefenceOfWater { get { return slots[ 11 ]; } set { slots[ 11 ] = value; } }
        public float MagicDefenceOfAir { get { return slots[ 12 ]; } set { slots[ 12 ] = value; } }
        public float MagicDefenceOfEarth { get { return slots[ 13 ]; } set { slots[ 13 ] = value; } }
        public float MagicDefenceOfDarkness { get { return slots[ 14 ]; } set { slots[ 14 ] = value; } }
        public float MagicDefenceOfLight { get { return slots[ 15 ]; } set { slots[ 15 ] = value; } }

        /* характеристики ресурсів */
        public float MaxHealth { get { return slots[ 16 ]; } set { slots[ 16 ] = value; } }
        public float MaxMana { get { return slots[ 17 ]; } set { slots[ 17 ] = value; } }
        public float MaxEnergy { get { return slots[ 18 ]; } set { slots[ 18 ] = value; } }
        public float HealthRegeneration { get { return slots[ 19 ]; } set { slots[ 19 ] = value; } }
        public float ManaRegeneration { get { return slots[ 20 ]; } set { slots[ 20 ] = value; } }
        public float EnergyRegeneration { get { return slots[ 21 ]; } set { slots[ 21 ] = value; } }

        /* фізичні характеристики */
        public float MoveSpeed { get { return slots[ 22 ]; } set { slots[ 22 ] = value; } }

        /* додаткові характеристики */
        public float CriticalChance { get { return slots[ 23 ]; } set { slots[ 23 ] = value; } }
        public float CriticalEffect { get { return slots[ 24 ]; } set { slots[ 24 ] = value; } }
        public float Speed { get { return slots[ 25 ]; } set { slots[ 25 ] = value; } }
        public float ChanceOfParrying { get { return slots[ 26 ]; } set { slots[ 26 ] = value; } }
        public float ChanceOfBlocking { get { return slots[ 27 ]; } set { slots[ 27 ] = value; } }

        private Characteristic() { }

        public Characteristic Clear() { for( int i = 0; i < SLOT_COUNT; i++ ) slots[ i ] = 0f; return this; }

        public string GetSlotName( int slot ) { return Localization.Current.CharacteristicNames[ slot ]; }

        public string GetNames() {
            stringBuilder.Clear();
            for( iterator = 0; iterator < SLOT_COUNT; iterator++ )
                stringBuilder.AppendLine( Localization.Current.CharacteristicNames[ iterator ] );
            return stringBuilder.ToString();
        }

        public string GetValues() {
            stringBuilder.Clear();
            for( iterator = 0; iterator < SLOT_COUNT; iterator++ )
                stringBuilder.AppendLine( Math.Round( slots[ iterator ], 2 ).ToString() );
            return stringBuilder.ToString();
        }

        public void Each( CharacteristicCallbackHandler callback ) {
            if( callback == null )
                return;
            for( iterator = 0; iterator < SLOT_COUNT; iterator++ )
                callback.Invoke( iterator, slots[ iterator ], Localization.Current.CharacteristicNames[ iterator ] );
        }

        public Characteristic Compare( Characteristic other ) {
            var c = new Characteristic();
            for( iterator = 0; iterator < SLOT_COUNT; iterator++ )
                c.slots[ iterator ] = slots[ iterator ] - other.slots[ iterator ];
            return c;
        }

        public static Characteristic operator +( Characteristic c1, Characteristic c2 ) {
            for( int i = 0; i < SLOT_COUNT; i++ )
                c1.slots[ i ] += c2.slots[ i ];
            return c1;
        }
        public static Characteristic operator -( Characteristic c1, Characteristic c2 ) {
            for( int i = 0; i < SLOT_COUNT; i++ )
                c1.slots[ i ] -= c2.slots[ i ];
            return c1;
        }

        public static Characteristic operator /( Characteristic c1, float percent ) {
            percent = Mathf.Clamp( percent, 0f, 100f );
            if( percent == 0f )
                return c1;
            for( int i = 0; i < SLOT_COUNT; i++ )
                c1.slots[ i ] = GetValueOfPercentage( c1.slots[ i ], percent );
            return c1;
        }

    }

}