/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;

namespace MyRPG.Configuration {

    public sealed class Localization {

        public const string DEFAULT_LANGUAGE = "ENGLISH";
        public const string DEFAULT_FOLDER = "Localization";

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public static Localization Current { get; private set; }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private static Dictionary<string, string> all = new Dictionary<string, string>();
        private static bool isInit;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public static bool Init() {
            if( isInit )
                return true;
            var dirPath = $"{Application.dataPath}/{DEFAULT_FOLDER}";
            if( !Directory.Exists( dirPath ) )
                return false;
            var directories = Directory.GetDirectories( dirPath );
            if( directories.Length == 0 )
                return false;

            // **
            for( int i = 0; i < directories.Length; i++ ) {
                var fileInfo = new FileInfo( directories[ i ] );
                all.Add( fileInfo.Name.ToUpper(), $"{DEFAULT_FOLDER}/{fileInfo.Name}" );
            }
            // **

            //for( int i = 0; i < directories.Length; i++ ) {
            //    var split = directories[ i ].Split( '\\' );
            //    var lang = split[ split.Length - 1 ];
            //    all.Add( lang.ToUpper(), string.Format( "Localization/{0}", lang ) );
            //}


            #region ?   
            if( !all.ContainsKey( DEFAULT_LANGUAGE ) )
                return false;
            #endregion

            isInit = true;
            return true;
        }
        public static string[] GetLanguages() { return all.Keys.ToArray(); }
        public static int GetTotalLanguages() { return all.Count; }
        public static bool HasLanguage( string language ) { return all.ContainsKey( language ); }
        public static int GetLanguageIndex( string language ) { return all.Keys.ToList().IndexOf( language ); }

        #region ?  
        public static bool SwitchLanguage( string oldLanguage, string newLanguage ) {
            if( !all.ContainsKey( newLanguage ) )
                newLanguage = DEFAULT_LANGUAGE;
            if( oldLanguage == newLanguage )
                return true;
            Localization loc = null;
            var xml = XMLFile<Localization>.Create( $"{all[ newLanguage ]}/main" );
            if( !xml.Load( out loc ) )
                return false;
            Config.Intance.LastSelectedLanguage = newLanguage;
            Current = loc;
            return true;
        }
        #endregion

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public string[] WindowNames { get; set; }
        public string[] DayNames { get; set; }
        public string[] KeyNames { get; set; }
        public string[] CharacteristicNames { get; set; }
        public string[] StatNames { get; set; }
        public string[] EntityDescriptions { get; set; }
        public string[] ItemNames { get; set; }
        public string[] ItemRarityNames { get; set; }
        public string[] ItemClassNames { get; set; }
        public string[] EquipmentPartNames { get; set; }
        public string[] EquipmentMaterialNames { get; set; }
        public string[] SpineEquipmentModeNames { get; set; }
        public string[] WeaponEquipmentTypeNames { get; set; }
        public string[] ItemInfo { get; set; }

    }

}