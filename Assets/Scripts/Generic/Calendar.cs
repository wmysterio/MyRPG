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

	public static class Calendar {

        private static byte dayId = 0;
        private static byte minute = 0;
        private static byte hour = 0;
        private static uint totalDays = 0;

        public static uint TotalDays {
            get { return totalDays; }
            set { totalDays = value; }
        }


        public static int Minute {
            get { return minute; }
            set {
                value = Mathf.Clamp( value, 0, 60 );
                if( value == 60 )
                    value = 0;
                minute = ( byte ) value;
            }
        }
        public static int Hour {
            get { return hour; }
            set {
                value = Mathf.Clamp( value, 0, 24 );
                if( value == 24 )
                    value = 0;
                hour = ( byte ) value;
            }
        }
        public static Weekday Day {
            get { return ( Weekday ) dayId; }
            set { dayId = ( byte ) value; }
        }
        public static bool Stop { get; set; }

        public static void Update() {
            if( Stop )
                return;
            minute += 1;
            if( minute > 59 ) {
                minute = 0;
                hour += 1;
            }
            if( hour > 23 ) {
                hour = 0;
                dayId += 1;
            }
            if( dayId > 6 ) {
                dayId = 0;
                totalDays += 1;
            }
            Debug.Log( GetCalendarInfo() );
        }

        public static void NextDay() {
            dayId += 1;
            if( dayId > 6 )
                dayId = 0;
            totalDays += 1;
        }

        public static string GetDayName() { return Localization.Current.DayNames[ dayId ]; }
        public static string GetDayName( Weekday day ) { return Localization.Current.DayNames[ ( int ) day ]; }
        public static string GetCalendarInfo() { return string.Format( "{0}, {1}:{2}", GetDayName(), hour, minute ); }


    }

    public enum Weekday : byte {
        Monday = 0,     // Понеділок
        Tuesday = 1,    // Вівторок
        Wednesday = 2,  // Середа
        Thursday = 3,   // Четвер
        Friday = 4,     // П'ятниця
        Saturday = 5,    // Субота
        Sunday = 6      // Неділя
    }

}