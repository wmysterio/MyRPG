/*
   Ліцензія: CC-BY
   Автор: Василь ( wmysterio )
   Сайт: http://metal-prog.zzz.com.ua/
*/
using System.IO;
using UnityEngine;

namespace MyRPG.Configuration {

    public sealed class Config {

        public const string DEFAULT_FILE_NAME = "config";

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public static Config Intance { get; set; }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public static Config Default() {
            return new Config() {
                LastSelectedLanguage = Localization.DEFAULT_LANGUAGE,
                FullScreen = Screen.fullScreen,
                ScreenWidth = Screen.width,
                ScreenHeight = Screen.height,
                BindingKeys = InputManager.GetDataBinding()
            };
        }
        public static bool HasFile() { return File.Exists( $"{Application.dataPath}/{DEFAULT_FILE_NAME}.xml" ); }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public string LastSelectedLanguage { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public bool FullScreen { get; set; }
        public int[] BindingKeys { get; set; }

    }

}