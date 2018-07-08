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
using System.IO;

namespace MyRPG {

    public sealed class Localization {
        
        public const string DEFAULT_LANGUAGE = "ENGLISH";
        
        private static Dictionary<string, string> all = new Dictionary<string, string>();
        private static bool isInit = false;
        
        public static Localization Current { get; private set; }
        public static string[] GetLanguages() { return all.Keys.ToArray(); }
        public static int GetTotalLanguages() { return all.Count; }
        public static bool HasLanguage( string language ) { return all.ContainsKey( language ); }
        public static bool SwitchLanguage( string oldLanguage, string newLanguage ) {
            if( !all.ContainsKey( newLanguage ) )
                newLanguage = DEFAULT_LANGUAGE;
            if( oldLanguage == newLanguage )
                return true;
            Localization loc = null;
            var xml = XMLFile<Localization>.Create( string.Format( "{0}/{1}", all[ newLanguage ], "main" ) );
            if( !xml.Load( out loc ) )
                return false;
            Config.Intance.LastSelectedLanguage = newLanguage;
            Current = loc;
            return true;
        }
        public static bool Init() {
            if( isInit )
                return true;
            var dirFormat = string.Format( "{0}/{1}", Application.dataPath, "Localization" );
            if( !Directory.Exists( dirFormat ) )
                return false;
            var directories = Directory.GetDirectories( dirFormat );
            if( directories.Length == 0 )
                return false;
            for( int i = 0; i < directories.Length; i++ ) {
                var split = directories[ i ].Split( '\\' );
                var lang = split[ split.Length - 1 ];
                all.Add( lang.ToUpper(), string.Format( "Localization/{0}", lang ) );
            }
            if( !all.ContainsKey( DEFAULT_LANGUAGE ) )
                return false;
            isInit = true;
            return true;
        }
        public static int GetLanguageIndex( string language ) { return all.Keys.ToList().IndexOf( language ); }

        public string[] WindowNames { get; set; }
        public string[] EntityNames { get; set; }
        public string[] DayNames { get; set; }
        public string[] KeyNames { get; set; }
        public string[] CharacteristicNames { get; set; }
        public string[] StatNames { get; set; }

    }

}