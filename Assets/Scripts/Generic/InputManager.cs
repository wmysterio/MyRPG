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

    public static class InputManager {

        private static Dictionary<KeyName, KeyCode> keys;
        private static Dictionary<KeyCode, string> keyNames;
        private static int iterator;

        public static KeyCode GetKeyCode( KeyName name ) { return keys[ name ]; }

        public static void Init() {
            if( keys != null )
                return;
            keys = new Dictionary<KeyName, KeyCode>();
            var values = ( KeyName[] ) Enum.GetValues( typeof( KeyName ) );
            for( iterator = 0; iterator < values.Length; iterator++ )
                keys.Add( values[ iterator ], KeyCode.None );
            keyNames = new Dictionary<KeyCode, string>() {
                { KeyCode.Alpha0, "0" },
                { KeyCode.Alpha1, "1" },
                { KeyCode.Alpha2, "2" },
                { KeyCode.Alpha3, "3" },
                { KeyCode.Alpha4, "4" },
                { KeyCode.Alpha5, "5" },
                { KeyCode.Alpha6, "6" },
                { KeyCode.Alpha7, "7" },
                { KeyCode.Alpha8, "8" },
                { KeyCode.Alpha9, "9" },
                { KeyCode.A, "A" },
                { KeyCode.B, "B" },
                { KeyCode.C, "C" },
                { KeyCode.D, "D" },
                { KeyCode.E, "E" },
                { KeyCode.F, "F" },
                { KeyCode.G, "G" },
                { KeyCode.H, "H" },
                { KeyCode.I, "I" },
                { KeyCode.J, "J" },
                { KeyCode.K, "K" },
                { KeyCode.L, "L" },
                { KeyCode.M, "M" },
                { KeyCode.N, "N" },
                { KeyCode.O, "O" },
                { KeyCode.P, "P" },
                { KeyCode.Q, "Q" },
                { KeyCode.R, "R" },
                { KeyCode.S, "S" },
                { KeyCode.T, "T" },
                { KeyCode.U, "U" },
                { KeyCode.V, "V" },
                { KeyCode.W, "W" },
                { KeyCode.X, "X" },
                { KeyCode.Y, "Y" },
                { KeyCode.Z, "Z" },
                { KeyCode.F1, "F1" },
                { KeyCode.F2, "F2" },
                { KeyCode.F3, "F3" },
                { KeyCode.F4, "F4" },
                { KeyCode.F5, "F5" },
                { KeyCode.F6, "F6" },
                { KeyCode.F7, "F7" },
                { KeyCode.F8, "F8" },
                { KeyCode.F9, "F9" },
                { KeyCode.F10, "F10" },
                { KeyCode.F11, "F11" },
                { KeyCode.F12, "F12" },
                { KeyCode.Minus, "-" },
                { KeyCode.Equals, "=" },
                { KeyCode.Tab, "Tab" },
                { KeyCode.LeftControl, "LC" },
                { KeyCode.RightControl, "RC" },
                { KeyCode.LeftShift, "LS" },
                { KeyCode.RightShift, "RS" },
                { KeyCode.LeftAlt, "LA" },
                { KeyCode.RightAlt, "RA" },
                { KeyCode.CapsLock, "CPS" },
                { KeyCode.BackQuote, "~" },
                { KeyCode.Return, "Enter" },
                { KeyCode.Escape, "Esc" },
                { KeyCode.Backspace, "Backspace" },
                { KeyCode.Space, "Space" },
                { KeyCode.LeftBracket, "[" },
                { KeyCode.RightBracket, "]" },
                { KeyCode.Backslash, "\\" },
                { KeyCode.Delete, "Del" },
                { KeyCode.Insert, "Ins" },
                { KeyCode.None, string.Empty },
                { KeyCode.LeftArrow, "←" },
                { KeyCode.RightArrow, "→" },
                { KeyCode.UpArrow, "↑" },
                { KeyCode.DownArrow, "↓" }
            };
            ToDefault();
        }

        public static string GetKeyName( KeyCode code ) {
            if( !keyNames.ContainsKey( code ) )
                return string.Empty;
            return keyNames[ code ];
        }
        public static string GetKeyName( KeyName name ) {
            if( !keyNames.ContainsKey( keys[ name ] ) )
                return string.Empty;
            return keyNames[ keys[ name ] ];
        }
        public static string GetKeyName( int index ) {
            if( !keyNames.ContainsKey( keys[ ( KeyName ) index ] ) )
                return string.Empty;
            return keyNames[ keys[ ( KeyName ) index ] ];
        }

        public static int[] GetDataBinding() { return ( from k in keys select ( int ) k.Value ).ToArray(); }
        public static void SetDataBinding( int[] data ) {
            try {
                var query = ( from b in data select ( KeyCode ) b ).ToArray();
                for( iterator = 0; iterator < keys.Count; iterator++ ) {
                    var k = keys.Keys.ElementAt( iterator );
                    BindKey( k, query[ iterator ] );
                }
            } catch { return; }
        }

        public static void ToDefault() {
            for( int i = 0; i < 9; i++ )
                keys[ ( KeyName ) i ] = ( KeyCode ) ( 49 + i );

            keys[ KeyName.PANEL_1_KEY_10 ] = KeyCode.Alpha0;
            keys[ KeyName.PANEL_1_KEY_11 ] = KeyCode.Minus;
            keys[ KeyName.PANEL_1_KEY_12 ] = KeyCode.Equals;

            keys[ KeyName.FORWARD ] = KeyCode.W;
            keys[ KeyName.BACK ] = KeyCode.S;
            keys[ KeyName.LEFT ] = KeyCode.Q;
            keys[ KeyName.RIGHT ] = KeyCode.E;
            keys[ KeyName.TURN_LEFT ] = KeyCode.A;
            keys[ KeyName.TURN_RIGHT ] = KeyCode.D;
            keys[ KeyName.JUMP ] = KeyCode.Space;
            keys[ KeyName.NEXT_TARGET ] = KeyCode.Tab;

            for( int i = 50; i < 56; i++ )
                keys[ ( KeyName ) i ] = ( KeyCode ) ( 232 + i );

            keys[ KeyName.VIEW_BAG ] = KeyCode.I;
            keys[ KeyName.VIEW_PERSONAGE ] = KeyCode.P;
            keys[ KeyName.VIEW_SPELLS ] = KeyCode.B;

            keys[ KeyName.ACTION ] = KeyCode.Return;
            keys[ KeyName.CONSOLE ] = KeyCode.BackQuote;
            keys[ KeyName.CANCEL ] = KeyCode.Backspace;
            keys[ KeyName.MENU ] = KeyCode.Escape;
        }
        public static bool BindKey( KeyName name, KeyCode code ) {
            if( name == KeyName.ACTION || name == KeyName.CONSOLE || name == KeyName.MENU || name == KeyName.CANCEL )
                return false;
            if( code == KeyCode.Return || code == KeyCode.BackQuote || code == KeyCode.Escape || code == KeyCode.Backspace )
                return false;
            if( !keyNames.ContainsKey( code ) )
                return false;
            var query = ( from k in keys where k.Value == code select k.Key ).ToArray();
            if( query.Length != 0 )
                keys[ query[ 0 ] ] = KeyCode.None;
            keys[ name ] = code;
            return true;
        }
        public static bool GetKey( KeyName name ) { return Input.GetKey( keys[ name ] ); }
        public static bool IsKeyDown( KeyName name ) { return Input.GetKeyDown( keys[ name ] ); }
        public static bool IsKeyUp( KeyName name ) { return Input.GetKeyUp( keys[ name ] ); }
        public static bool AnyKeyDown() { return Input.anyKeyDown; }
        public static bool GetMouse( MouseKeyName name ) { return Input.GetMouseButton( ( int ) name ); }
        public static bool IsMouseDown( MouseKeyName name ) { return Input.GetMouseButtonDown( ( int ) name ); }
        public static bool IsMouseUp( MouseKeyName name ) { return Input.GetMouseButtonUp( ( int ) name ); }
        public static bool IsMouseScrollUp() { return Input.GetAxis( "Mouse ScrollWheel" ) > 0f; }
        public static bool IsMouseScrollDown() { return 0f > Input.GetAxis( "Mouse ScrollWheel" ); }
        public static bool IsMouseMoveLeft() { return 0f > Input.GetAxis( "Mouse X" ); }
        public static bool IsMouseMoveRight() { return Input.GetAxis( "Mouse X" ) > 0f; }
        public static bool IsMouseMoveDown() { return 0f > Input.GetAxis( "Mouse Y" ); }
        public static bool IsMouseMoveUp() { return Input.GetAxis( "Mouse Y" ) > 0f; }

    }

    public enum MouseKeyName : int {
        Left = 0,
        Right = 1,
        Middle = 2
    }

    public enum KeyName : byte {

        // кнопки для панелі #1
        PANEL_1_KEY_1 = 0,
        PANEL_1_KEY_2 = 1,
        PANEL_1_KEY_3 = 2,
        PANEL_1_KEY_4 = 3,
        PANEL_1_KEY_5 = 4,
        PANEL_1_KEY_6 = 5,
        PANEL_1_KEY_7 = 6,
        PANEL_1_KEY_8 = 7,
        PANEL_1_KEY_9 = 8,
        PANEL_1_KEY_10 = 9,
        PANEL_1_KEY_11 = 10,
        PANEL_1_KEY_12 = 11,

        // кнопки для панелі #2
        PANEL_2_KEY_1 = 12,
        PANEL_2_KEY_2 = 13,
        PANEL_2_KEY_3 = 14,
        PANEL_2_KEY_4 = 15,
        PANEL_2_KEY_5 = 16,
        PANEL_2_KEY_6 = 17,
        PANEL_2_KEY_7 = 18,
        PANEL_2_KEY_8 = 19,
        PANEL_2_KEY_9 = 20,
        PANEL_2_KEY_10 = 21,
        PANEL_2_KEY_11 = 22,
        PANEL_2_KEY_12 = 23,

        // кнопки панелі слуги
        PET_STAY_IDLE = 50,
        PET_FOLLOW_MASTER = 51,
        PET_DEFENSE_MASTER = 52,
        PET_CAST_SPELL_1 = 53,
        PET_CAST_SPELL_2 = 54,
        PET_CAST_SPELL_3 = 55,

        // клавіші руху
        FORWARD = 100,
        BACK = 101,
        LEFT = 102,
        RIGHT = 103,
        TURN_LEFT = 104,
        TURN_RIGHT = 105,
        JUMP = 106,
        NEXT_TARGET = 107,

        // клавіші для вікон
        VIEW_PERSONAGE = 200,
        VIEW_BAG = 201,
        VIEW_EFFECTS = 202,
        VIEW_SPELLS = 203,
        VIEW_QUESTS = 204,

        // спеціальні клавіші
        CANCEL = 252,
        ACTION = 253,
        CONSOLE = 254,
        MENU = 255
    }

}