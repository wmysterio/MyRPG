/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System.Collections;
using UnityEngine;
using MyRPG.Sync;
using MyRPG.Configuration;

namespace MyRPG {

    public sealed class Init : MonoBehaviour {

        private UnityEngine.UI.Text languageTextComponent = null;
        private UnityEngine.UI.Image[] lights = null;
        private Color passiveCircleColor, activeCircleColor;
        private InitState state = InitState.Stop;
        private XMLFile<Config> config;
        private string[] languages = null;
        private float timer = 0f;
        private int currentLanguagesIndex = -1, totalLanguagesIndex = -1, currentLightCircleIndex = 0;
        private Coroutine tempCoroutine = null;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public GameObject selectLanguagePanel, languageTextObject, lightsPanel;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private void Awake() {
            languageTextComponent = languageTextObject.GetComponent<UnityEngine.UI.Text>();
            lights = lightsPanel.GetComponentsInChildren<UnityEngine.UI.Image>();
            activeCircleColor = lights[ 0 ].color;
            passiveCircleColor = lights[ 1 ].color;
        }

        private IEnumerator Start() {
            yield return Audio.Init();
            yield return Player.Interface.Init();
            if( !Player.Interface.IsInit ) {
                state = InitState.Stop;
                Application.Quit();
                yield return null;
            }
            Player.Interface.Enable = true;
            state = InitState.InitInputManager;
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
                var go = new GameObject( "GameSync" );
                var gs = go.AddComponent<GameSync>();
                if( !gs.ConnectionReady ) {
                    state = InitState.Stop;
                    Application.Quit();
                    return;
                }
                Camera.Init();
                Audio.SyncCamara();
                state = InitState.Stop;
                Room.Switch( Room.Levels.TrainingLevel );
                break;

            }

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

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private enum InitState {
            InitInputManager,
            InitConfig,
            InitLocalization,
            SelectLanguage,
            SwitchLanguage,
            InitEntityListAndRooms,
            Stop
        }

    }

}