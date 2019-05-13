/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Text.RegularExpressions;
 
namespace MyRPG {
 
	public sealed class ConsoleSync : MonoBehaviour {

        private delegate void Command( string[] args );

        private Dictionary<string, Command> allCommands = new Dictionary<string, Command>();
        private InputField commandInput = null;
        private char[] commandSeparators = new char[] { ':' };
        private char[] argumentSeparators = new char[] { ' ' };

        private void Awake() {
            allCommands.Add( "PUT_AT", PUT_AT );




            commandInput = gameObject.GetComponent<InputField>();
            commandInput.onValueChanged.AddListener( text => { commandInput.text = Regex.Replace( commandInput.text, @"[^a-z0-9 \.\:\-_,]+", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled ).ToUpper(); } );
            commandInput.onEndEdit.AddListener( text => {
                clear();
                var split = text.Split( commandSeparators, StringSplitOptions.RemoveEmptyEntries );
                if( split.Length == 0 || split.Length > 2 ) {
                    playErrorTune();
                    return;
                }
                if( !allCommands.ContainsKey( split[ 0 ] ) ) {
                    playErrorTune();
                    return;
                }
                if( split.Length == 1 ) {
                    allCommands[ split[ 0 ] ].Invoke( null );
                    return;
                }
                allCommands[ split[ 0 ] ].Invoke( split[ 1 ].Split( argumentSeparators, StringSplitOptions.RemoveEmptyEntries ) );
            } );
        }
		
		private void Update() {
            if( !Player.Exist() || InputManager.IsKeyDown( KeyName.CONSOLE ) )
                gameObject.SetActive( false );
		}

        private void OnEnable() { clear(); playOpenTune(); }

        private void clear() { commandInput.text = string.Empty; commandInput.ActivateInputField(); }

        private void playOpenTune() { Audio.PlayTune( Audio.TuneID.CONSOLE_OPEN ); }
        private void playErrorTune() { Audio.PlayTune( Audio.TuneID.CONSOLE_ERROR ); }
        private void playSuccessTune() { Audio.PlayTune( Audio.TuneID.CONSOLE_SUCCESS ); }

        #region СПИСОК КОМАНД
        private void PUT_AT( string[] args ) {
            if( args == null ) {
                playErrorTune();
                return;
            }
            if( args.Length != 3 ) {
                playErrorTune();
                return;
            }
            float x = 0f, y = 0f, z = 0f;
            if( !float.TryParse( args[ 0 ], out x ) ) {
                playErrorTune();
                return;
            }
            if( !float.TryParse( args[ 1 ], out y ) ) {
                playErrorTune();
                return;
            }
            if( !float.TryParse( args[ 2 ], out z ) ) {
                playErrorTune();
                return;
            }
            Player.Current.Position = new Vector3( x, y, z );
            playSuccessTune();
        }


        #endregion

    }

}