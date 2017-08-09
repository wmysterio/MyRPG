/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://www.unity3d.tk/
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG {

	public sealed class Characteristic {

        public static Characteristic CreateEmpty() { return new Characteristic(); }
        public static Characteristic CreateBase( int level, RankOfPersonage rank ) {
            return new Characteristic() {
                MaxHealth = 10f * level * ( int ) rank,
                MaxMana = 10f * level * ( int ) rank,
                MaxEnergy = 100f * ( int ) rank,
                HealthRegeneration = 0.1f * level,
                ManaRegeneration = 0.1f * level,
                EnergyRegeneration = 0.1f,
                MoveSpeed = 1f,
                CastSpeed = 1f
            };
        }

        public static float GetPercentageOfValue( float maxValue, float curValue ) {
            if( maxValue == 0f )
                return 0f;
            return ( 100f * curValue ) / maxValue;
        }
        public static float GetValueOfPercentage( float maxValue, float percent ) {
            return ( percent * maxValue ) / 100f;
        }

        private static int length = ( typeof( Characteristic ) ).GetProperties().Length;

        private PropertyInfo[] properties = ( typeof( Characteristic ) ).GetProperties();

        /* характеристики атаки */
        public float PhysicalAttackPower { get; set; }
        public float MagicPowerOfNature { get; set; }
        public float MagicPowerOfFire { get; set; }
        public float MagicPowerOfWater { get; set; }
        public float MagicPowerOfAir { get; set; }
        public float MagicPowerOfEarth { get; set; }
        public float MagicPowerOfDarkness { get; set; }
        public float MagicPowerOfLight { get; set; }

        /* характеристики захисту */
        public float PhysicalDefencePower { get; set; }
        public float MagicDefenceOfNature { get; set; }
        public float MagicDefenceOfFire { get; set; }
        public float MagicDefenceOfWater { get; set; }
        public float MagicDefenceOfAir { get; set; }
        public float MagicDefenceOfEarth { get; set; }
        public float MagicDefenceOfDarkness { get; set; }
        public float MagicDefenceOfLight { get; set; }

        /* характеристики ресурсів */
        public float MaxHealth { get; set; }
        public float MaxMana { get; set; }
        public float MaxEnergy { get; set; }
        public float HealthRegeneration { get; set; }
        public float ManaRegeneration { get; set; }
        public float EnergyRegeneration { get; set; }

        /* фізичні характеристики */
        public float MoveSpeed { get; set; }

        /* додаткові характеристики */
        public float CriticalChance { get; set; }
        public float CriticalEffect { get; set; }
        public float CastSpeed { get; set; }

        private Characteristic() { Clear(); }

        public Characteristic Clear() {
            for( int i = 0; i < length; i++ ) {
                properties[ i ].SetValue( this, 0f, null );
            }
            return this;
        }

        public static Characteristic operator +( Characteristic c1, Characteristic c2 ) {
            for( int i = 0; i < length; i++ ) {
                var summ = ( float ) c1.properties[ i ].GetValue( c1, null ) + ( float ) c2.properties[ i ].GetValue( c2, null );
                c1.properties[ i ].SetValue( c1, summ, null );
            }
            return c1;
        }

    }

}