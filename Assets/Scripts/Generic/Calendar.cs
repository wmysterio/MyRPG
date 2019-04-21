/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG {

    public static class Calendar {

        private static byte day = 0;
        private static byte minute = 0;
        private static byte hour = 0;
        private static uint totalDays = 0;

        public static uint TotalDays {
            get { return totalDays; }
            set { totalDays = value; }
        }
        public static int Minute {
            get { return minute; }
            set { minute = ( byte ) Mathf.Clamp( value, 0, 59 ); }
        }
        public static int Hour {
            get { return hour; }
            set { hour = ( byte ) Mathf.Clamp( value, 0, 23 ); }
        }
        public static Weekday Day {
            get { return ( Weekday ) day; }
            set { day = ( byte ) value; }
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
                NextDay();
            }
        }

        public static void NextDay() {
            day += 1;
            if( day > 6 )
                day = 0;
            totalDays += 1;
        }

        public static string GetDayName() { return Localization.Current.DayNames[ day ]; }
        public static string GetDayName( Weekday weekday ) { return Localization.Current.DayNames[ ( int ) weekday ]; }
        public static string GetCalendarInfo() { return string.Format( "{0}, {1}{2}:{3}{4}", GetDayName(), hour > 9 ? string.Empty : "0", hour, minute > 9 ? string.Empty : "0", minute ); }

    }

    public enum Weekday : byte {
        Monday = 0,     // Понеділок
        Tuesday = 1,    // Вівторок
        Wednesday = 2,  // Середа
        Thursday = 3,   // Четвер
        Friday = 4,     // П'ятниця
        Saturday = 5,   // Субота
        Sunday = 6      // Неділя
    }

}