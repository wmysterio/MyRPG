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

        private UnityEngine.UI.Text languageTextComponent = null;
        private UnityEngine.UI.Image[] lights = null;
        private Color passiveCircleColor, activeCircleColor;
        private InitState state = InitState.Stop;
        private XMLFile<Config> config;
        private string[] languages = null;
        private float timer = 0f;
        private int currentLanguagesIndex = -1, totalLanguagesIndex = -1, currentLightCircleIndex = 0;

        public GameObject selectLanguagePanel, languageTextObject, lightsPanel;

        private void Awake() {
            languageTextComponent = languageTextObject.GetComponent<UnityEngine.UI.Text>();
            lights = lightsPanel.GetComponentsInChildren<UnityEngine.UI.Image>();
            activeCircleColor = lights[ 0 ].color;
            passiveCircleColor = lights[ 1 ].color;
        }

        private IEnumerator Start() {
            yield return Player.Interface.Init();
            if( !Player.Interface.IsInit ) {
                state = InitState.Stop;
                Application.Quit();
                yield return null;
            }
            Player.Interface.Enable = true;
            state = InitState.InitStyles;
        }

        private void Update() {
            timer += Time.deltaTime;
            if( timer > 0.07f ) {
                highlightNextCircle();
                timer = 0f;
            }
            if( Input.GetKeyUp( KeyCode.Escape ) ) {
                state = InitState.Stop;
                Application.Quit();
                return;
            }

            switch( state ) {

                case InitState.InitInputManager:
                InputManager.Init();
                state = InitState.InitConfig;
                break;

                case InitState.InitConfig:
                Config conf = null;
                config = XMLFile<Config>.Create( "config" );
                if( !Config.HasFile() ) {
                    conf = Config.Default();
                    if( !config.Save( conf ) ) {
                        state = InitState.Stop;
                        Application.Quit();
                        return;
                    }
                } else {
                    if( !config.Load( out conf ) ) {
                        conf = Config.Default();
                        if( !config.Save( conf, true ) ) {
                            state = InitState.Stop;
                            Application.Quit();
                            return;
                        }
                    } else {
                        InputManager.SetDataBinding( conf.BindingKeys );
                    }
                }
                Config.Intance = conf;
                state = InitState.InitLocalization;
                break;

                case InitState.InitLocalization:
                if( !Localization.Init() ) {
                    state = InitState.Stop;
                    Application.Quit();
                    return;
                }
                selectLanguagePanel.SetActive( true );
                lightsPanel.SetActive( false );
                languages = Localization.GetLanguages();
                totalLanguagesIndex = Localization.GetTotalLanguages();
                if( Localization.HasLanguage( Config.Intance.LastSelectedLanguage.ToUpper() ) )
                    currentLanguagesIndex = Localization.GetLanguageIndex( Config.Intance.LastSelectedLanguage.ToUpper() );
                languageTextComponent.text = languages[ currentLanguagesIndex ];
                state = InitState.SelectLanguage;
                break;

                case InitState.SelectLanguage:
                if( Input.GetKeyUp( KeyCode.Return ) || Input.GetKeyUp( KeyCode.Space ) ) {
                    Config.Intance.LastSelectedLanguage = languages[ currentLanguagesIndex ];
                    state = InitState.SwitchLanguage;
                    return;
                }
                if( Input.GetKeyUp( KeyCode.A ) || Input.GetKeyUp( KeyCode.LeftArrow ) )
                    PrevLanguage();
                if( Input.GetKeyUp( KeyCode.D ) || Input.GetKeyUp( KeyCode.RightArrow ) )
                    NextLanguage();
                break;

                case InitState.SwitchLanguage:
                selectLanguagePanel.SetActive( false );
                lightsPanel.SetActive( true );
                if( !Localization.SwitchLanguage( Localization.DEFAULT_LANGUAGE, Config.Intance.LastSelectedLanguage.ToUpper() ) ) {
                    state = InitState.Stop;
                    Application.Quit();
                    return;
                }
                if( !config.Save( Config.Intance, true ) ) {
                    state = InitState.Stop;
                    Application.Quit();
                    return;
                }
                state = InitState.InitEntityListAndRooms;
                break;


                case InitState.InitEntityListAndRooms:
                var go = GameObject.Find( "EntityList" );
                go.AddComponent<Entity.EntityList>();
                go.AddComponent<Room>();
                state = InitState.InitCamera;
                break;

                case InitState.InitCamera:
                Camera.Init();
                // ----------------------------------------


                var plane = GameObject.CreatePrimitive( PrimitiveType.Cube );
                plane.transform.position = Vector3.zero;
                plane.transform.rotation = Quaternion.identity;
                plane.transform.localScale = new Vector3( 25f, 0.001f, 25f );
                var tmpGo = new GameObject( "Light" );
                tmpGo.transform.parent = plane.transform;
                tmpGo.transform.localEulerAngles = new Vector3( 90f, 0f, 0f );
                tmpGo.transform.localPosition = new Vector3( 0f, 150f, 0f );
                var light = tmpGo.AddComponent<Light>();
                light.type = LightType.Directional;
                GameObject.DontDestroyOnLoad( plane );


                

                Model.Request( Chest.GetModelByType( TypeOfChest.Quest ) );
                Model.Request( 0 );
                Model.LoadRequestedNow();



                new Player( "My Player", Personage.MIN_LEVEL, 0, Vector3.up );
                var chest = new Chest( TypeOfChest.Quest, new Vector3( 0f, 0.5f, -5f ) );




                Model.Unload();
                Camera.AttachToPlayer();



                // ----------------------------------------
                state = InitState.Stop;
                Room.Load( Room.All.SelectProfile );
                break;
            }

        }

        private void OnGUI() {
            if( state != InitState.InitStyles )
                return;

            Player.Interface.InitStyles();

            GUI.skin.settings.cursorColor = new Color( 0.44f, 0.48f, 0.58f, 1f );
            GUI.skin.settings.selectionColor = new Color( 0.44f, 0.48f, 0.58f, 1f );
            GUI.skin.settings.cursorFlashSpeed = 1f;
            state = InitState.InitInputManager;
        }

        public void PrevLanguage() {
            currentLanguagesIndex -= 1;
            if( 0 > currentLanguagesIndex )
                currentLanguagesIndex = totalLanguagesIndex - 1;
            languageTextComponent.text = languages[ currentLanguagesIndex ];
        }

        public void NextLanguage() {
            currentLanguagesIndex += 1;
            if( currentLanguagesIndex >= totalLanguagesIndex )
                currentLanguagesIndex = 0;
            languageTextComponent.text = languages[ currentLanguagesIndex ];
        }

        private void highlightNextCircle() {
            currentLightCircleIndex += 1;
            if( currentLightCircleIndex >= lights.Length )
                currentLightCircleIndex = 0;
            for( int i = 0; i < lights.Length; i++ )
                lights[ i ].color = i == currentLightCircleIndex ? activeCircleColor : passiveCircleColor;
        }

        private enum InitState {
            InitStyles,
            InitInputManager,
            InitConfig,
            InitLocalization,
            SelectLanguage,
            SwitchLanguage,
            InitEntityListAndRooms,
            InitCamera,
            Stop
        }

    }

}