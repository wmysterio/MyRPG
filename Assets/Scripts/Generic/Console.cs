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

    public partial class Player {

        public static class Console {

            private const int MAX_COMMAND_LENGTH = 30;

            private delegate void Command( string[] args );

            private static Dictionary<string, Command> listCommands = new Dictionary<string, Command>() {
                { "CLOSE", close }, // сам метод буде написано нижче
                { "PLAYER_PLACE", player_place } // сам метод буде написано нижче
            };


            private static AudioClip soundSuccess = null, soundError = null;
            private static Texture2D boxBackground = null, areaBackground = null;
            private static GUIStyle boxStyle = null, areaStyle = null;
            private static Rect rectBox = new Rect( 10, 10, Screen.width - 20, 40 );
            private static Rect rectArea = new Rect( 15, 15, Screen.width - 30, 30 );
            private static ResourceRequest request = null;
            private static string command = string.Empty, commandName = string.Empty;
            private static char tmpChar = ' ';
            private static char[] normalChar = new char[] { '.', ',', ':', '-', '>' };
            private static string[] split = null, commandArgs = null;
            private static bool isInit = false, isInitStyles = false, enable = false;

            public static bool IsInit {
                get { return isInit; }
                private set { isInit = value; }
            }
            public static bool Enable {
                get { return enable; }
                set {
                    if( value )
                        Interface.HideMessageBox();
                    enable = value;
                }
            }


            public static IEnumerator Init() {
                if( IsInit )
                    yield return null;

                request = Resources.LoadAsync<Texture2D>( "UI/Console/images/areaBackground" );
                yield return request;
                areaBackground = request.asset as Texture2D;

                request = Resources.LoadAsync<Texture2D>( "UI/Console/images/boxBackground" );
                yield return request;
                boxBackground = request.asset as Texture2D;

                request = Resources.LoadAsync<AudioClip>( "UI/Console/sounds/consoleSuccess" );
                yield return request;
                soundSuccess = request.asset as AudioClip;

                request = Resources.LoadAsync<AudioClip>( "UI/Console/sounds/consoleError" );
                yield return request;
                soundError = request.asset as AudioClip;

                IsInit = true;
            }
            public static void InitStyles() {
                if( isInitStyles )
                    return;

                boxStyle = new GUIStyle( GUI.skin.box );
                boxStyle.normal.background = boxBackground;
                boxStyle.active.background = boxBackground;
                boxStyle.focused.background = boxBackground;
                boxStyle.hover.background = boxBackground;

                areaStyle = new GUIStyle( GUI.skin.textArea );
                areaStyle.normal.background = areaBackground;
                areaStyle.active.background = areaBackground;
                areaStyle.focused.background = areaBackground;
                areaStyle.hover.background = areaBackground;
                areaStyle.normal.textColor = Color.black;
                areaStyle.active.textColor = Color.black;
                areaStyle.focused.textColor = Color.black;
                areaStyle.hover.textColor = Color.black;
                areaStyle.fontSize = 18;
                areaStyle.wordWrap = false;

                isInitStyles = true;
            }


            public static void Draw() {
                if( !IsInit || !Enable )
                    return;
                rectBox.width = Screen.width - 20;
                rectArea.width = Screen.width - 30;
                GUI.Box( rectBox, "", boxStyle );

                command = command.Trim().ToUpper();
                if( command.Length > 0 ) {
                    tmpChar = command[ command.Length - 1 ];
                    if( ( 'A' > tmpChar || tmpChar > 'Z' ) && !char.IsNumber( tmpChar ) && !normalChar.Contains( tmpChar ) ) {
                        command = command.Remove( command.Length - 1, 1 );
                    }
                    if( command.Length > MAX_COMMAND_LENGTH ) {
                        command = command.Substring( 0, MAX_COMMAND_LENGTH );
                    }
                }

                command = GUI.TextArea( rectArea, command, areaStyle );

                if( command.Contains( '\n' ) ) {
                    command = command.Replace( ">", "_" ).Replace( "\n", "" );
                    split = command.Split( ':' );
                    if( split.Length > 2 ) {
                        playSound( soundError );
                    } else {
                        commandName = split[ 0 ];
                        if( split.Length == 2 ) {
                            commandArgs = split[ 1 ].Split( ',' );
                        } else {
                            commandArgs = null;
                        }
                    }
                    if( listCommands.ContainsKey( commandName ) ) {
                        listCommands[ commandName ].Invoke( commandArgs );
                    } else {
                        playSound( soundError );
                    }
                    command = string.Empty;
                }
            }

            private static void playSound( AudioClip clip ) { AudioSource.PlayClipAtPoint( clip, Camera.main.transform.position ); }

            /* СПИСОК КОМАНД: */

            private static void close( string[] args ) { Enable = false; }

            private static void player_place( string[] args ) {
                if( args == null ) {
                    playSound( soundError );
                    return;
                }
                if( args.Length != 3 ) {
                    playSound( soundError );
                    return;
                }
                float x = 0f, y = 0f, z = 0f;
                if( !float.TryParse( args[ 0 ], out x ) ) {
                    playSound( soundError );
                    return;
                }
                if( !float.TryParse( args[ 1 ], out y ) ) {
                    playSound( soundError );
                    return;
                }
                if( !float.TryParse( args[ 2 ], out z ) ) {
                    playSound( soundError );
                    return;
                }

                Current.Position = new Vector3( x, y, z );
                playSound( soundSuccess );
            }



        }

    }

}