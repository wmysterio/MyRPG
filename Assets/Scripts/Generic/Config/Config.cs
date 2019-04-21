﻿/*
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
using System.IO;

namespace MyRPG {

    public sealed class Config {

        public static Config Intance { get; set; }
        public static Config Default() {
            return new Config() {
                LastSelectedLanguage = Localization.DEFAULT_LANGUAGE,
                FullScreen = Screen.fullScreen,
                ScreenWidth = Screen.width,
                ScreenHeight = Screen.height,
                BindingKeys = InputManager.GetDataBinding()
            };
        }

        public static bool HasFile() { return File.Exists( string.Format( "{0}/{1}.xml", Application.dataPath, "config" ) ); }

        public string LastSelectedLanguage { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public bool FullScreen { get; set; }
        public int[] BindingKeys { get; set; }

    }

}