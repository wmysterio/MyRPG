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

    public class Init : MonoBehaviour {

        private bool chg = false, hasError = false;
        private string errorMessage = string.Empty;
        private Texture2D whitePixel, redPixel, blackPixel;
        private Rect boxRect, wrapperRect, errorRect;
        private int CurrentIndex = 0, totalIndexes = 5;
        private float speed = 0f, range = 0.15f;

        void Awake() {

            boxRect = new Rect( 0, 0, 10, 4 );
            wrapperRect = new Rect();
            errorRect = new Rect( 0, 0, 150, 36 );

            blackPixel = new Texture2D( 1, 1, TextureFormat.RGBA32, false );
            blackPixel.SetPixel( 0, 0, new Color( 30f / 255f, 30f / 255f, 30f / 255f, 1f ) );
            blackPixel.Apply();

            whitePixel = new Texture2D( 1, 1, TextureFormat.RGBA32, false );
            whitePixel.SetPixel( 0, 0, Color.white );
            whitePixel.Apply();

            redPixel = new Texture2D( 1, 1, TextureFormat.RGBA32, false );
            redPixel.SetPixel( 0, 0, new Color( 224f / 255f, 1f / 255f, 1f / 255f, 1f ) );
            redPixel.Apply();


            InputManager.Init();

            Config conf = null;
            var xml = XMLFile<Config>.Create( "config" );

            if( !Config.HasFile() ) {
                conf = Config.Default();
                if( !xml.Save( conf ) )
                    Debug.LogWarning( "Файл 'config' не збережено!" );
            } else {
                if( !xml.Load( out conf ) ) {
                    conf = Config.Default();
                    if( !xml.Save( conf, true ) )
                        Debug.LogWarning( "Файл 'config' не збережено!" );
                } else {
                    InputManager.SetDataBinding( conf.BindingKeys );
                }
            }
            Config.Intance = conf;

            if( !Localization.Init() ) {
                addError( "" );
                return;
            }

            if( !Localization.SwitchLanguage( string.Empty, Config.Intance.CurrentLanguage.ToUpper() ) ) {
                addError( "" );
                return;
            }

            var go = GameObject.Find( "EntityList" );
            go.AddComponent<Entity.EntityList>();
            go.AddComponent<Room>();

        }

        IEnumerator Start() {
            yield return Player.Interface.Init();
            Player.Interface.Enable = true;
        }

        void Update() {
            if( hasError ) {
                if( InputManager.AnyKeyDown() )
                    Application.Quit();
                return;
            } else {
                if( chg ) {
                    Room.Load( Room.All.SelectProfile );
                    return;
                }
                speed += Time.deltaTime;
                if( speed > range ) {
                    speed = 0f;
                    CurrentIndex += 1;
                    if( CurrentIndex >= totalIndexes )
                        CurrentIndex = 0;
                }

            }
        }

        void OnGUI() {
            wrapperRect.width = Screen.width;
            wrapperRect.height = Screen.height;
            GUI.DrawTexture( wrapperRect, blackPixel );

            if( hasError ) {
                errorRect.x = Screen.width / 2 - errorRect.width / 2;
                errorRect.y = Screen.height - errorRect.height - boxRect.height * 2;
                GUI.Label( errorRect, errorMessage, GUI.skin.button );
                return;
            }

            boxRect.y = Screen.height - boxRect.height * 4f;
            boxRect.x = Screen.width / 2 - ( ( boxRect.width * 2 ) * totalIndexes ) / 2;
            for( int i = 0; i < totalIndexes; i++ ) {
                GUI.DrawTexture( boxRect, i == CurrentIndex ? redPixel : whitePixel );
                boxRect.x += boxRect.width * 2;
            }

            if( !chg ) {
                if( !Player.Interface.IsInit )
                    return;
                Player.Interface.InitStyles();

                GUI.skin.settings.cursorColor = new Color( 0.44f, 0.48f, 0.58f, 1f );
                GUI.skin.settings.selectionColor = new Color( 0.44f, 0.48f, 0.58f, 1f );
                GUI.skin.settings.cursorFlashSpeed = 1f;
                chg = true;
            }
        }

        private void addError( string text ) {
            hasError = true;
            errorMessage = text;
        }

    }

}