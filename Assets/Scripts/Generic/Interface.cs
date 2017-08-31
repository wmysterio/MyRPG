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

            private static bool isInitStyles = false, isInit = false, enable = false, enableСinematicView, fadeOn;
            private static ResourceRequest request;
            private static float fadeStep, fadeAlpha, messageBoxDuration, messageBoxTimer, messageBoxWidth = DEFAULT_MESSAGE_BOX_WIDTH, subtitlesTimer, subtitlesDuration;
            private static Texture2D messageBoxBackground, subtitlesBlackPixel, fadeTexture, imageHP, imageMP, imageEP, imageHP_bg, imageMP_bg, imageEP_bg;
            private static AudioClip messageBoxPlay;
            private static Rect messageBoxRect, subtitlesUpRect, subtitlesBottomRect, fadeRect, hudRect, hudBorderRect, hudNameRect;
            private static GUIContent messageBoxContent, subtitlesContent;
            private static GUIStyle messageBoxStyle, messageBoxLabelStyle, subtitlesStyle, hudNameStyle;
            private static Color fadeColor = Color.black;

            public static Texture2D[] Icons { get; private set; }
            public static bool SubtitlesDisplayed { get; private set; }
            public static bool MessageBoxDisplayed { get; private set; }
            public static bool IsInit {
                get { return isInit; }
                private set { isInit = value; }
            }
            public static bool Enable {
                get { return enable; }
                set { enable = value; }
            }
            public static bool EnableСinematicView {
                get { return enableСinematicView; }
                set {
                    if( value ) {
                        HideMessageBox();
                    } else {
                        ClearSubtitles();
                    }
                    enableСinematicView = value;
                }
            }
            public static bool Fadind { get; private set; }

            public static void ClearSubtitles() {
                subtitlesDuration = 0f;
                subtitlesTimer = 0f;
                subtitlesContent.text = string.Empty;
                SubtitlesDisplayed = false;
            }
            public static void PrintSubtitles( float duration, string text ) {
                subtitlesTimer = 0f;
                subtitlesDuration = duration;
                subtitlesContent.text = text;
            }
            public static void PrintSubtitles( float duration, string format, params object[] args ) {
                PrintSubtitles( duration, string.Format( format, args ) );
            }
            public static void SetMessageBoxWidth( float width ) { messageBoxWidth = width; }
            public static void HideMessageBox() {
                messageBoxTimer = 0f;
                messageBoxDuration = 0f;
                messageBoxContent.text = string.Empty;
                MessageBoxDisplayed = false;
            }
            public static void ShowMessageBox( float duration, string text ) {
                if( EnableСinematicView || Fadind || Console.Enable )
                    return;
                messageBoxTimer = 0f;
                messageBoxDuration = duration;
                messageBoxContent.text = text;
                playSound( messageBoxPlay );
            }
            public static void ShowMessageBox( float duration, string format, params object[] args ) {
                ShowMessageBox( duration, string.Format( format, args ) );
            }
            public static void Fade( FadeMode mode, float fadeSpeed = 0.01f ) {
                if( mode == FadeMode.In ) {
                    fadeStep = -fadeSpeed;
                    fadeAlpha = 0f;
                } else {
                    fadeStep = fadeSpeed;
                    fadeAlpha = 1f;
                }
                fadeOn = true;
                Fadind = true;
            }


            public static IEnumerator Init() {
                if( IsInit )
                    yield return null;

                messageBoxRect = new Rect( 10f, 10f, 0f, 0f );
                messageBoxContent = new GUIContent( string.Empty );
                HideMessageBox();

                subtitlesUpRect = new Rect( 0, 0, Screen.width, 76 );
                subtitlesBottomRect = new Rect( 0, Screen.height - 76, Screen.width, 76 );
                subtitlesContent = new GUIContent( string.Empty );
                subtitlesBlackPixel = new Texture2D( 1, 1, TextureFormat.RGBA32, false );
                subtitlesBlackPixel.alphaIsTransparency = true;
                subtitlesBlackPixel.SetPixel( 0, 0, Color.black );
                subtitlesBlackPixel.Apply();
                EnableСinematicView = false;

                fadeRect = new Rect( 0, 0, Screen.width, Screen.height );
                Fadind = false;
                fadeOn = true;
                fadeTexture = new Texture2D( 1, 1, TextureFormat.RGBA32, false );
                fadeTexture.alphaIsTransparency = true;
                fadeTexture.SetPixel( 0, 0, fadeColor );
                fadeTexture.Apply();

                hudRect = new Rect( 0f, 0f, 200f, 8f );
                hudBorderRect = new Rect( 0f, 0f, 204f, 57f );
                hudNameRect = new Rect( 0f, 0f, 200f, 32f );


                imageHP = new Texture2D( 1, 1, TextureFormat.RGBA32, false );
                imageHP.alphaIsTransparency = true;
                imageHP.SetPixel( 0, 0, new Color( 180f / 255f, 25f / 255f, 29f / 255f, 1f ) );
                imageHP.Apply();

                imageHP_bg = new Texture2D( 1, 1, TextureFormat.RGBA32, false );
                imageHP_bg.alphaIsTransparency = true;
                imageHP_bg.SetPixel( 0, 0, new Color( 90f / 255f, 12f / 255f, 14f / 255f, 1f ) );
                imageHP_bg.Apply();

                imageMP = new Texture2D( 1, 1, TextureFormat.RGBA32, false );
                imageMP.alphaIsTransparency = true;
                imageMP.SetPixel( 0, 0, new Color( 172f / 255f, 203f / 255f, 241f / 255f, 1f ) );
                imageMP.Apply();

                imageMP_bg = new Texture2D( 1, 1, TextureFormat.RGBA32, false );
                imageMP_bg.alphaIsTransparency = true;
                imageMP_bg.SetPixel( 0, 0, new Color( 86f / 255f, 101f / 255f, 120f / 255f, 1f ) );
                imageMP_bg.Apply();

                imageEP = new Texture2D( 1, 1, TextureFormat.RGBA32, false );
                imageEP.alphaIsTransparency = true;
                imageEP.SetPixel( 0, 0, new Color( 83f / 255f, 153f / 255f, 51f / 255f, 1f ) );
                imageEP.Apply();

                imageEP_bg = new Texture2D( 1, 1, TextureFormat.RGBA32, false );
                imageEP_bg.alphaIsTransparency = true;
                imageEP_bg.SetPixel( 0, 0, new Color( 20f / 255f, 38f / 255f, 12f / 255f, 1f ) );
                imageEP_bg.Apply();




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
                if( Fadind ) {
                    fadeAlpha = fadeColor.a + fadeStep;
                    if( 0f > fadeAlpha ) {
                        fadeAlpha = 0f;
                        Fadind = false;
                        fadeOn = false;
                    }
                    if( fadeAlpha > 1f ) {
                        fadeAlpha = 1f;
                        Fadind = false;
                    }
                    fadeColor.a = fadeAlpha;
                    fadeRect.width = Screen.width;
                    fadeRect.height = Screen.height;
                    fadeTexture.SetPixel( 0, 0, fadeColor );
                    fadeTexture.Apply();
                }
                if( EnableСinematicView ) {
                    if( subtitlesDuration > subtitlesTimer ) {
                        SubtitlesDisplayed = true;
                        subtitlesTimer += 1 * Time.deltaTime;
                        if( subtitlesTimer > subtitlesDuration )
                            ClearSubtitles();
                    }
                    return;
                }
                if( InputManager.IsKeyDown( KeyName.CONSOLE ) )
                    Console.Enable = !Console.Enable;
                if( messageBoxDuration > messageBoxTimer ) {
                    MessageBoxDisplayed = true;
                    messageBoxTimer += 1f * Time.deltaTime;
                    if( messageBoxTimer > messageBoxDuration )
                        HideMessageBox();
                }
            }

            public static void Draw() {
                if( IsInit && Enable ) {
                    if( fadeOn )
                        GUI.DrawTexture( fadeRect, fadeTexture, ScaleMode.StretchToFill );
                    if( EnableСinematicView ) {
                        subtitlesUpRect.width = Screen.width;
                        subtitlesBottomRect.width = Screen.width;
                        subtitlesBottomRect.y = Screen.height - subtitlesBottomRect.height;
                        GUI.DrawTexture( subtitlesUpRect, subtitlesBlackPixel, ScaleMode.StretchToFill );
                        GUI.DrawTexture( subtitlesBottomRect, subtitlesBlackPixel, ScaleMode.StretchToFill );
                        if( SubtitlesDisplayed )
                            GUI.Label( subtitlesBottomRect, subtitlesContent, subtitlesStyle );
                    }
                    if( MessageBoxDisplayed && !EnableСinematicView ) {
                        messageBoxRect.width = messageBoxWidth;
                        messageBoxRect.height = messageBoxLabelStyle.CalcHeight( messageBoxContent, messageBoxWidth );
                        GUI.Box( messageBoxRect, string.Empty, messageBoxStyle );
                        GUI.Label( messageBoxRect, messageBoxContent, messageBoxLabelStyle );
                    }
                }
                if( !fadeOn && !EnableСinematicView ) {
                    Console.Draw();
                    if( !Console.Enable ) {
                        if( !MessageBoxDisplayed )
                            displayHUD( 10f, 10f, Current );
                        if( Current.Target != null )
                            displayHUD( Screen.width - 214f, 10f, Current.Target, true );
                    }
                }
            }

            public static void InitStyles() {
                if( isInitStyles )
                    return;
                if( messageBoxStyle == null ) {
                    messageBoxStyle = new GUIStyle( GUI.skin.box );
                    messageBoxStyle.normal.background = messageBoxBackground;
                    messageBoxStyle.active.background = messageBoxBackground;
                    messageBoxStyle.focused.background = messageBoxBackground;
                    messageBoxStyle.hover.background = messageBoxBackground;
                }
                if( messageBoxLabelStyle == null ) {
                    messageBoxLabelStyle = new GUIStyle( GUI.skin.label );
                    messageBoxLabelStyle.alignment = TextAnchor.UpperLeft;
                    messageBoxLabelStyle.padding = new RectOffset( DEFAULT_PADDING, DEFAULT_PADDING, DEFAULT_PADDING, DEFAULT_PADDING );
                    messageBoxLabelStyle.normal.textColor = Color.black;
                    messageBoxLabelStyle.active.textColor = Color.black;
                    messageBoxLabelStyle.focused.textColor = Color.black;
                    messageBoxLabelStyle.hover.textColor = Color.black;
                }
                if( subtitlesStyle == null ) {
                    subtitlesStyle = new GUIStyle( messageBoxLabelStyle );
                    subtitlesStyle.alignment = TextAnchor.MiddleCenter;
                    subtitlesStyle.normal.textColor = Color.white;
                    subtitlesStyle.active.textColor = Color.white;
                    subtitlesStyle.focused.textColor = Color.white;
                    subtitlesStyle.hover.textColor = Color.white;
                }
                if( hudNameStyle == null ) {
                    hudNameStyle = new GUIStyle( GUI.skin.label );
                    hudNameStyle.normal.textColor = Color.white;
                    hudNameStyle.active.textColor = Color.white;
                    hudNameStyle.focused.textColor = Color.white;
                    hudNameStyle.hover.textColor = Color.white;
                    hudNameStyle.fontSize = 16;
                    hudNameStyle.fontStyle = FontStyle.Bold;
                }
                isInitStyles = true;
            }

            private static void displayHUD( float x, float y, Personage personage, bool target = false ) {
                hudBorderRect.x = x;
                hudBorderRect.y = y;
                hudRect.x = x + 2;
                hudRect.y = y + 2;
                hudRect.width = 200f;

                GUI.DrawTexture( hudBorderRect, subtitlesBlackPixel, ScaleMode.StretchToFill );

                hudNameRect.y = y;
                if( target ) {
                    hudNameRect.x = x - 5f;
                    hudNameStyle.alignment = TextAnchor.UpperRight;
                } else {
                    hudNameRect.x = x + 5f;
                    hudNameStyle.alignment = TextAnchor.UpperLeft;
                }
                GUI.Label( hudNameRect, personage.Name, hudNameStyle );


                hudRect.y += 25;
                GUI.DrawTexture( hudRect, imageHP_bg, ScaleMode.StretchToFill );
                hudRect.width = Mathf.Round( 2f * Characteristic.GetPercentageOfValue( personage.CurrentCharacteristic.MaxHealth, personage.CurrentHealth ) );
                GUI.DrawTexture( hudRect, imageHP, ScaleMode.StretchToFill );
                hudRect.y += 10;
                hudRect.width = 200f;
                GUI.DrawTexture( hudRect, imageMP_bg, ScaleMode.StretchToFill );
                hudRect.width = Mathf.Round( 2f * Characteristic.GetPercentageOfValue( personage.CurrentCharacteristic.MaxMana, personage.CurrentMana ) );
                GUI.DrawTexture( hudRect, imageMP, ScaleMode.StretchToFill );
                hudRect.y += 10;
                hudRect.width = 200f;
                GUI.DrawTexture( hudRect, imageEP_bg, ScaleMode.StretchToFill );
                hudRect.width = Mathf.Round( 2f * Characteristic.GetPercentageOfValue( personage.CurrentCharacteristic.MaxEnergy, personage.CurrentEnergy ) );
                GUI.DrawTexture( hudRect, imageEP, ScaleMode.StretchToFill );
            }

            private static void playSound( AudioClip clip ) { AudioSource.PlayClipAtPoint( clip, Camera.main.transform.position ); }

        }

    }

    public enum FadeMode { In, Out }

}