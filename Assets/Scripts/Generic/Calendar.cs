/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/
using UnityEngine;
using MyRPG.Configuration;
using System.Timers;

namespace MyRPG {

    public static class Calendar {

        static Calendar() {
            timer = new Timer( 1000 );
            timer.Elapsed += update;
        }

        private static void update( object sender, ElapsedEventArgs e ) {
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

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private static Timer timer;
        private static byte day;
        private static byte minute;
        private static byte hour;
        private static uint totalDays;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

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
        public static bool Enable {
            get { return timer.Enabled; }
            set { timer.Enabled = value; }
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public static void NextDay() {
            day += 1;
            if( day > 6 )
                day = 0;
            totalDays += 1;
        }
        public static string GetDayName() { return Localization.Current.DayNames[ day ]; }
        public static string GetDayName( Weekday weekday ) { return Localization.Current.DayNames[ ( int ) weekday ]; }
        public static string GetCalendarInfo() { return $"{GetDayName()}, {( hour > 9 ? string.Empty : "0" )}{hour}:{( minute > 9 ? string.Empty : "0" )}{minute}"; }

    }

    /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

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