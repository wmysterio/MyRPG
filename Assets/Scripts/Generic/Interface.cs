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

            private static bool mouseLeft = false, mouseRight = false, isInitStyles = false, isInit = false, enable = false, enableСinematicView, fadeOn;
            private static ResourceRequest request;
            private static float fadeStep, fadeAlpha, messageBoxDuration, messageBoxTimer, messageBoxWidth = DEFAULT_MESSAGE_BOX_WIDTH, subtitlesTimer, subtitlesDuration;
            private static Texture2D activeEquipmentImage, windowBackground, windowTitleBackground, messageBoxBackground, subtitlesBlackPixel, fadeTexture, imageHP, imageMP, imageEP, imageHP_bg, imageMP_bg, imageEP_bg;
            private static AudioClip messageBoxPlay;
            private static Rect selectedAbilityRect, abilityRect,abilityButtonRect, windowRect, windowTitleRect, windowCloseButtonRect, messageBoxRect, subtitlesUpRect, subtitlesBottomRect, fadeRect, hudRect, hudBorderRect, hudNameRect;
            private static GUIContent messageBoxContent, subtitlesContent;
            private static GUIStyle selectedAbilityStyle, abilityStyle, activeEquipmentStyle, windowStyle, windowTitleStyle, windowCloseButtonStyle, messageBoxStyle, messageBoxLabelStyle, subtitlesStyle, hudNameStyle;
            private static Color windowTitleColor, fadeColor;
            private static Dictionary<Window, Action> windowFunc;
            private static Dictionary<TypeOfItemRarity, Texture2D> borderImages;
            private static Dictionary<TypeOfItemRarity, GUIStyle> borderStyles;
            private static int iterator, iterator2;
            private static Item currentItem, tempItem;
            private static EquipmentItem currentEquipment;
            private static IAbility[] abilitys;
            private static IAbility selectedAbility;


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
            public static Window CurrentWindow { get; private set; }

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

                fadeColor = Color.black;

                selectedAbilityRect = new Rect( 0, 0, 32, 32 );
                deselectAbility();

                windowFunc = new Dictionary<Window, Action>() {
                    { Window.Bag, drawBagWindow },
                    { Window.Effects, drawEffectsWindow },
                    { Window.Personage, drawPersonageWindow },
                    { Window.Spells, drawSpellsWindow },
                    { Window.Quests, drawQuestsWindow }
                };

                abilityRect = new Rect( 0, 0, 36 * 12 + 4, 76 );
                abilitys = ( IAbility[] ) Array.CreateInstance( typeof( IAbility ), 24 );

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

                windowRect = new Rect( 0, 0, 400, 300 );
                windowTitleRect = new Rect( 1, 1, 0, 35 );
                windowCloseButtonRect = new Rect( 0, 0, 35, 35 );
                CurrentWindow = Window.None;

                windowTitleColor = new Color( 58f / 225f, 85f / 225f, 125f / 225f, 1f );

                windowTitleBackground = new Texture2D( 1, 1, TextureFormat.RGBA32, false );
                windowTitleBackground.SetPixel( 0, 0, new Color( 222f / 255f, 237f / 255f, 252f / 255f, 1f ) );
                windowTitleBackground.Apply();

                borderImages = new Dictionary<TypeOfItemRarity, Texture2D>();
                borderStyles = new Dictionary<TypeOfItemRarity, GUIStyle>();

                abilityButtonRect = new Rect( 0, 0, 32, 32 );

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

                request = Resources.LoadAsync<Texture2D>( "UI/Interface/images/windowBackgroundImage" );
                yield return request;
                windowBackground = request.asset as Texture2D;

                var bordersValues = ( TypeOfItemRarity[] ) Enum.GetValues( typeof( TypeOfItemRarity ) );
                for( int i = 0; i < bordersValues.Length; i++ ) {
                    request = Resources.LoadAsync<Texture2D>( string.Format( "UI/Interface/images/borders/{0}", ( int ) bordersValues[ i ] ) );
                    yield return request;
                    borderImages.Add( bordersValues[ i ], request.asset as Texture2D );
                    borderStyles.Add( bordersValues[ i ], null );
                }

                request = Resources.LoadAsync<Texture2D>( "UI/Interface/images/activeEquipment" );
                yield return request;
                activeEquipmentImage = request.asset as Texture2D;

                IsInit = true;
            }
            public static void InitStyles() {
                if( isInitStyles )
                    return;

                Console.InitStyles();

                messageBoxStyle = new GUIStyle( GUI.skin.box );
                messageBoxStyle.normal.background = messageBoxBackground;
                messageBoxStyle.active.background = messageBoxBackground;
                messageBoxStyle.focused.background = messageBoxBackground;
                messageBoxStyle.hover.background = messageBoxBackground;

                messageBoxLabelStyle = new GUIStyle( GUI.skin.label );
                messageBoxLabelStyle.alignment = TextAnchor.UpperLeft;
                messageBoxLabelStyle.padding = new RectOffset( DEFAULT_PADDING, DEFAULT_PADDING, DEFAULT_PADDING, DEFAULT_PADDING );
                messageBoxLabelStyle.normal.textColor = Color.black;
                messageBoxLabelStyle.active.textColor = Color.black;
                messageBoxLabelStyle.focused.textColor = Color.black;
                messageBoxLabelStyle.hover.textColor = Color.black;

                subtitlesStyle = new GUIStyle( messageBoxLabelStyle );
                subtitlesStyle.alignment = TextAnchor.MiddleCenter;
                subtitlesStyle.normal.textColor = Color.white;
                subtitlesStyle.active.textColor = Color.white;
                subtitlesStyle.focused.textColor = Color.white;
                subtitlesStyle.hover.textColor = Color.white;

                hudNameStyle = new GUIStyle( GUI.skin.label );
                hudNameStyle.normal.textColor = Color.white;
                hudNameStyle.active.textColor = Color.white;
                hudNameStyle.focused.textColor = Color.white;
                hudNameStyle.hover.textColor = Color.white;
                hudNameStyle.fontSize = 16;
                hudNameStyle.fontStyle = FontStyle.Bold;

                foreach( var item in borderImages ) {
                    borderStyles[ item.Key ] = new GUIStyle( GUI.skin.button );
                    borderStyles[ item.Key ].normal.background = borderImages[ item.Key ];
                    borderStyles[ item.Key ].hover.background = borderImages[ item.Key ];
                    borderStyles[ item.Key ].active.background = borderImages[ item.Key ];
                    borderStyles[ item.Key ].focused.background = borderImages[ item.Key ];
                    borderStyles[ item.Key ].normal.textColor = Color.yellow;
                    borderStyles[ item.Key ].hover.textColor = Color.yellow;
                    borderStyles[ item.Key ].active.textColor = Color.yellow;
                    borderStyles[ item.Key ].focused.textColor = Color.yellow;
                    borderStyles[ item.Key ].fontStyle = FontStyle.Bold;
                }

                activeEquipmentStyle = new GUIStyle( GUI.skin.button );
                activeEquipmentStyle.normal.background = activeEquipmentImage;
                activeEquipmentStyle.hover.background = activeEquipmentImage;
                activeEquipmentStyle.active.background = activeEquipmentImage;
                activeEquipmentStyle.focused.background = activeEquipmentImage;
                activeEquipmentStyle.normal.textColor = Color.white;
                activeEquipmentStyle.hover.textColor = Color.white;
                activeEquipmentStyle.active.textColor = Color.white;
                activeEquipmentStyle.focused.textColor = Color.white;
                activeEquipmentStyle.fontStyle = FontStyle.Bold;

                windowStyle = new GUIStyle( GUI.skin.window );
                windowStyle.normal.background = windowBackground;
                windowStyle.active.background = windowBackground;
                windowStyle.focused.background = windowBackground;
                windowStyle.hover.background = windowBackground;
                windowStyle.onNormal.background = windowBackground;
                windowStyle.onActive.background = windowBackground;
                windowStyle.onFocused.background = windowBackground;
                windowStyle.onHover.background = windowBackground;

                windowCloseButtonStyle = new GUIStyle( GUI.skin.button );
                windowCloseButtonStyle.normal.background = null;
                windowCloseButtonStyle.active.background = null;
                windowCloseButtonStyle.focused.background = null;
                windowCloseButtonStyle.hover.background = null;
                windowCloseButtonStyle.normal.textColor = windowTitleColor;
                windowCloseButtonStyle.active.textColor = windowTitleColor;
                windowCloseButtonStyle.focused.textColor = windowTitleColor;
                windowCloseButtonStyle.hover.textColor = windowTitleColor;

                windowTitleStyle = new GUIStyle( GUI.skin.label );
                windowTitleStyle.padding = new RectOffset( DEFAULT_PADDING, DEFAULT_PADDING, DEFAULT_PADDING, DEFAULT_PADDING );
                windowTitleStyle.wordWrap = false;
                windowTitleStyle.normal.background = windowTitleBackground;
                windowTitleStyle.active.background = windowTitleBackground;
                windowTitleStyle.focused.background = windowTitleBackground;
                windowTitleStyle.hover.background = windowTitleBackground;
                windowTitleStyle.normal.textColor = windowTitleColor;
                windowTitleStyle.active.textColor = windowTitleColor;
                windowTitleStyle.focused.textColor = windowTitleColor;
                windowTitleStyle.hover.textColor = windowTitleColor;

                abilityStyle = new GUIStyle( windowStyle );

                isInitStyles = true;
            }

            public static void Update() {
                if( !IsInit || !Enable )
                    return;
                mouseLeft = InputManager.GetMouse( MouseKeyName.Left );
                mouseRight = InputManager.GetMouse( MouseKeyName.Right );

                if( InputManager.GetKey( KeyName.MENU ) || Console.Enable || ( selectedAbility != null && mouseRight ) )
                    deselectAbility();

                for( iterator= 0; iterator < 24; iterator++ ) {
                    if( abilitys[ iterator ] != null && InputManager.IsKeyDown( ( KeyName ) iterator ) )
                        Debug.Log( "click" );
                }

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
                if( Console.Enable && InputManager.IsKeyDown( KeyName.MENU ) )
                    Console.Enable = false;
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

                        drawAbilityPanel();
                        
                        if( CurrentWindow != Window.None )
                            windowRect = drawWindow( CurrentWindow.ToString(), windowRect );

                        if( selectedAbility != null ) {
                            selectedAbilityRect.x = Event.current.mousePosition.x - 16;
                            selectedAbilityRect.y = Event.current.mousePosition.y - 16;
                            GUI.Box( selectedAbilityRect, selectedAbility.Icon, selectedAbilityStyle );
                            GUI.UnfocusWindow();
                        }

                    }
                }
            }



            public static void ToggleWindow( Window window ) {
                if( window != Window.None ) {
                    if( CurrentWindow == window ) {
                        CurrentWindow = Window.None;
                        return;
                    }
                    switch( window ) {
                        case Window.Bag:
                        windowRect.width = 36f * Bag.ItemsInRow + 4f;
                        windowRect.height = 36f * Bag.ItemsInRow + 40f;
                        break;
                        case Window.Spells:
                        // windowRect.width = ?
                        // windowRect.height = ?
                        break;
                        case Window.Personage:
                        // windowRect.width = ?
                        // windowRect.height = ?
                        break;
                        case Window.Effects:
                        // windowRect.width = ?
                        // windowRect.height = ?
                        break;
                        case Window.Quests:
                        // windowRect.width = ?
                        // windowRect.height = ?
                        break;
                        default:
                        // windowRect.width = ?
                        // windowRect.height = ?
                        break;
                    }
                    windowRect.x = Screen.width / 2f - windowRect.width / 2f;
                    windowRect.y = Screen.height / 2f - windowRect.height / 2f;
                    windowTitleRect.width = windowRect.width - 2;
                }
                CurrentWindow = window;
            }

            private static Rect drawWindow( string title, Rect windowRect, bool dragable = true ) {
                windowRect.x = Mathf.Clamp( windowRect.x, 0f, Screen.width - windowRect.width );
                windowRect.y = Mathf.Clamp( windowRect.y, 0f, Screen.height - windowRect.height );
                return GUI.Window( 0, windowRect, id => {
                    GUI.Label( windowTitleRect, title, windowTitleStyle );
                    windowCloseButtonRect.x = windowRect.width - 35;
                    if( GUI.Button( windowCloseButtonRect, "X", windowCloseButtonStyle ) ) {
                        CurrentWindow = Window.None;
                        return;
                    }
                    windowFunc[ CurrentWindow ].Invoke();

                    if( selectedAbility != null ) {
                        selectedAbilityRect.x = Event.current.mousePosition.x - 16;
                        selectedAbilityRect.y = Event.current.mousePosition.y - 16;
                        GUI.Box( selectedAbilityRect, selectedAbility.Icon, selectedAbilityStyle );
                    }

                    if( dragable )
                        GUI.DragWindow();
                }, string.Empty, windowStyle );
            }

            private static void drawBagWindow() {
                abilityButtonRect.x = 4f;
                abilityButtonRect.y = 4f;

                for( iterator = 0; iterator < Current.Loot.Count; iterator++ ) {
                    if( iterator % Bag.ItemsInRow == 0 ) {
                        abilityButtonRect.x = 4f;
                        abilityButtonRect.y += 36f;
                    }
                    currentItem = Current.Loot[ iterator ];
                    if( currentItem.Class == ClassOfItem.Equipment ) {
                        currentEquipment = ( EquipmentItem ) currentItem;
                        GUI.DrawTexture( abilityButtonRect, currentEquipment.Icon );
                        if( Current.Equipments[ currentEquipment.EquipmentPart ] != null ) {
                            if( GUI.Button( abilityButtonRect, currentEquipment.Timer > 0f ? currentEquipment.Timer.ToString() : currentItem.Count > 2 ? currentItem.Count.ToString() : string.Empty, activeEquipmentStyle ) ) {
                                if( mouseLeft )
                                    selectAbility( Current.Loot[ iterator ] );
                                if( mouseRight )
                                    Current.Equipments.Set( currentEquipment );
                            }
                        } else {
                            if( GUI.Button( abilityButtonRect, currentEquipment.Timer > 0f ? currentEquipment.Timer.ToString() : currentItem.Count > 2 ? currentItem.Count.ToString() : string.Empty, borderStyles[ currentItem.Rarity ] ) ) {
                                if( mouseLeft )
                                    selectAbility( Current.Loot[ iterator ] );
                                if( mouseRight )
                                    Current.Equipments.Set( currentEquipment );
                            }
                        }
                    } else {
                        GUI.DrawTexture( abilityButtonRect, currentItem.Icon );
                        if( currentItem.Timer > 0f ) {
                            if( GUI.Button( abilityButtonRect, currentItem.Timer.ToString(), borderStyles[ currentItem.Rarity ] ) ) {
                                if( mouseLeft )
                                    selectAbility( Current.Loot[ iterator ] );
                            }
                        } else {
                            if( GUI.Button( abilityButtonRect, currentItem.Count > 2 ? currentItem.Count.ToString() : string.Empty, borderStyles[ currentItem.Rarity ] ) ) {
                                if( mouseLeft )
                                    selectAbility( Current.Loot[ iterator ] );
                                if( mouseRight )
                                    currentItem.Use( Current );
                            }
                        }
                    }
                    abilityButtonRect.x += 36f;
                }
                for( ; iterator < Bag.MAX_ITEM_COUNT; iterator++ ) {
                    if( iterator % Bag.ItemsInRow == 0 ) {
                        abilityButtonRect.x = 4f;
                        abilityButtonRect.y += 36f;
                    }
                    GUI.DrawTexture( abilityButtonRect, borderImages[ TypeOfItemRarity.Normal ] );
                    abilityButtonRect.x += 36f;
                }
            }

            private static void drawSpellsWindow() { }
            private static void drawPersonageWindow() { }
            private static void drawEffectsWindow() { }
            private static void drawQuestsWindow() { }

            private static void drawAbilityPanel() {
                abilityRect.x = ( Screen.width / 2 ) - ( 32 * 12 ) / 2 - 26;
                abilityRect.y = Screen.height - 76;
                GUI.Box( abilityRect, string.Empty, abilityStyle );

                abilityButtonRect.x = abilityRect.x + 4;
                abilityButtonRect.y = abilityRect.y + 4;
                for( iterator2 = 0, iterator = 12; iterator2 < 12; iterator2++, iterator++ ) {
                    if( abilitys[ iterator ] == null ) {
                        if( GUI.Button( abilityButtonRect, string.Empty, borderStyles[ TypeOfItemRarity.Normal ] ) ) {
                            if( mouseLeft && selectedAbility != null ) {
                                abilitys[ iterator ] = selectedAbility;
                                deselectAbility();
                            }
                        }
                    } else {
                        GUI.DrawTexture( abilityButtonRect, abilitys[ iterator ].Icon );
                        currentItem = abilitys[ iterator ] == null ? null : ( Item ) abilitys[ iterator ];
                        if( GUI.Button( abilityButtonRect, string.Empty, borderStyles [ currentItem == null ? TypeOfItemRarity.Normal : currentItem.Rarity ] ) ) {
                            if( mouseLeft ) {
                                if( selectedAbility == null ) {
                                    selectAbility( abilitys[ iterator ] );
                                    abilitys[ iterator ] = null;
                                } else {
                                    var tmp = abilitys[ iterator ];
                                    abilitys[ iterator ] = selectedAbility;
                                    selectAbility( tmp );
                                }
                            }
                            if( mouseRight )
                                Debug.Log( "click" );
                        }
                    }
                    abilityButtonRect.x += 36f;
                }
                abilityButtonRect.x = abilityRect.x + 4;
                abilityButtonRect.y = abilityRect.y + 40;
                abilityRect.x = Screen.width / 2 - abilityRect.width + 4;
                for( iterator = 0; iterator < 12; iterator++) {
                    if( abilitys[ iterator ] == null ) {
                        if( GUI.Button( abilityButtonRect, string.Empty, borderStyles[ TypeOfItemRarity.Normal ] ) ) {
                            if( mouseLeft && selectedAbility != null ) {
                                abilitys[ iterator ] = selectedAbility;
                                deselectAbility();
                            }
                        }
                    } else {
                        GUI.DrawTexture( abilityButtonRect, abilitys[ iterator ].Icon );
                        currentItem = abilitys[ iterator ] == null ? null : ( Item ) abilitys[ iterator ];
                        if( GUI.Button( abilityButtonRect, string.Empty, borderStyles[ currentItem == null ? TypeOfItemRarity.Normal : currentItem.Rarity ] ) ) {
                            if( mouseLeft ) {
                                if( selectedAbility == null ) {
                                    selectAbility( abilitys[ iterator ] );
                                    abilitys[ iterator ] = null;
                                } else {
                                    var tmp = abilitys[ iterator ];
                                    abilitys[ iterator ] = selectedAbility;
                                    selectAbility( tmp );
                                }
                            }
                            if( mouseRight )
                                Debug.Log( "click" );
                        }
                    }
                    abilityButtonRect.x += 36f;
                }
            }

            private static void selectAbility( IAbility ability ) {
                selectedAbility = ability;
                tempItem = ability.AbilityType == TypeOfAbility.Item ? ( Item ) ability : null;
                selectedAbilityStyle = borderStyles[ tempItem == null ? TypeOfItemRarity.Normal : tempItem.Rarity ];
            }
            private static void deselectAbility() {
                tempItem = null;
                selectedAbilityStyle = null;
                selectedAbility = null;
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

    public enum Window {
        None,
        Bag,
        Spells,
        Personage,
        Effects,
        Quests
    }

    public enum FadeMode { In, Out }

}