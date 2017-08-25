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

        public static class Interface {

            private const int DEFAULT_PADDING = 10;

            public const int MAX_ICON_COUNT = 10;
            public const float DEFAULT_MESSAGE_BOX_WIDTH = 310f;

            private static bool isInit = false, enable = false;
            private static ResourceRequest request;
            private static float messageBoxDuration, messageBoxTimer, messageBoxWidth = DEFAULT_MESSAGE_BOX_WIDTH;
            private static Texture2D messageBoxBackground;
            private static AudioClip messageBoxPlay;
            private static Rect messageBoxRect;
            private static GUIContent messageBoxContent;
            private static GUIStyle messageBoxStyle, messageBoxLabelStyle;

            public static Texture2D[] Icons { get; private set; }
            public static bool MessageBoxDisplayed { get; private set; }
            public static bool IsInit {
                get { return isInit; }
                private set { isInit = value; }
            }
            public static bool Enable {
                get { return enable; }
                set { enable = value; }
            }

            
            public static void SetMessageBoxWidth( float width ) { messageBoxWidth = width; }
            public static void HideMessageBox() {
                messageBoxTimer = 0f;
                messageBoxDuration = 0f;
                messageBoxContent.text = string.Empty;
                MessageBoxDisplayed = false;
            }
            public static void ShowMessageBox( float duration, string text ) {
                messageBoxTimer = 0f;
                messageBoxDuration = duration;
                messageBoxContent.text = text;
                playSound( messageBoxPlay );
            }
            public static void ShowMessageBox( float duration, string format, params object[] args ) {
                ShowMessageBox( duration, string.Format( format, args ) );
            }

            public static IEnumerator Init() {
                if( IsInit )
                    yield return null;

                messageBoxRect = new Rect( 10f, 10f, 0f, 0f );
                messageBoxContent = new GUIContent( string.Empty );
                HideMessageBox();

                yield return Console.Init();

                Icons = new Texture2D[ MAX_ICON_COUNT ];
                for( int i = 0; i < MAX_ICON_COUNT; i++ ) {
                    request = Resources.LoadAsync<Texture2D>( string.Format( "UI/Interface/images/{0}", i ) );
                    yield return request;
                    Icons[ i ] = request.asset as Texture2D;
                }

                request = Resources.LoadAsync<Texture2D>( "UI/Interface/images/messageBoxBackground" );
                yield return request;
                messageBoxBackground = request.asset as Texture2D;

                request = Resources.LoadAsync<AudioClip>( "UI/Interface/sounds/messageBoxSound" );
                yield return request;
                messageBoxPlay = request.asset as AudioClip;

                IsInit = true;
            }

            public static void Update() {
                if( !IsInit || !Enable )
                    return;
                if( messageBoxDuration > messageBoxTimer ) {
                    MessageBoxDisplayed = true;
                    messageBoxTimer += 1f * Time.deltaTime;
                    if( messageBoxTimer > messageBoxDuration )
                        HideMessageBox();
                }
            }

            public static void Draw() {
                if( IsInit && Enable ) {
                    if( messageBoxStyle == null ) {
                        messageBoxStyle = new GUIStyle( GUI.skin.box );
                        messageBoxStyle.normal.background = messageBoxBackground;
                        messageBoxStyle.active.background = messageBoxBackground;
                        messageBoxStyle.focused.background = messageBoxBackground;
                        messageBoxStyle.hover.background = messageBoxBackground;
                    }
                    if( messageBoxLabelStyle == null ) {
                        messageBoxLabelStyle = new GUIStyle( GUI.skin.label );
                        messageBoxLabelStyle.fontSize = 16;
                        messageBoxLabelStyle.alignment = TextAnchor.UpperLeft;
                        messageBoxLabelStyle.padding = new RectOffset( DEFAULT_PADDING, DEFAULT_PADDING, DEFAULT_PADDING, DEFAULT_PADDING );
                        messageBoxLabelStyle.normal.textColor = Color.black;
                        messageBoxLabelStyle.active.textColor = Color.black;
                        messageBoxLabelStyle.focused.textColor = Color.black;
                        messageBoxLabelStyle.hover.textColor = Color.black;
                    }
                    if( MessageBoxDisplayed ) {
                        messageBoxRect.width = messageBoxWidth;
                        messageBoxRect.height = messageBoxLabelStyle.CalcHeight( messageBoxContent, messageBoxWidth );
                        GUI.Box( messageBoxRect, string.Empty, messageBoxStyle );
                        GUI.Label( messageBoxRect, messageBoxContent, messageBoxLabelStyle );
                    }
                }
                Console.Draw();
            }

            private static void playSound( AudioClip clip ) { AudioSource.PlayClipAtPoint( clip, Camera.main.transform.position ); }

        }

    }

}