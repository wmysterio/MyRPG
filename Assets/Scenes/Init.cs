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

    public class TempHuman : Humanoid {

        public TempHuman( string name, Vector3 position ) : base( 1, RankOfPersonage.Normal, 0, position ) {
            Name = name;
        }

    }

    public class Item1 : NormalItem {

        public Item1() : base( 1, TypeOfItemRarity.Epic ) { }

    }

    public class Item2 : NormalItem {

        public Item2() : base( 1, TypeOfItemRarity.Legendary ) { }

    }

    public class Item3 : NormalItem {

        public Item3() : base( 1, TypeOfItemRarity.Rare ) {
            Count = 20;
        }

    }

    public class Item4 : NormalItem {

        public Item4() : base( 1, TypeOfItemRarity.Unusual ) { }

    }

    public class Item5 : NormalItem {

        public Item5() : base( 1, TypeOfItemRarity.Junk ) { }

    }

    public class Item6 : SwordWeapon {

        public Item6() : base( 1, TypeOfItemRarity.Normal, 0 ) {
            baseCharacteristic.MaxHealth = 5000;
            baseCharacteristic.PhysicalAttackPower = 300;
            baseCharacteristic.HealthRegeneration = 5;
            Delay = 60;
        }

    }

    public class Init : MonoBehaviour {

        bool chg = false, hasError = false;
        string errorMessage = string.Empty;

        private void addError( string text ) {
            hasError = true;
            errorMessage = text;
        }

        void Awake() {
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


        }

        Player player;
        TempHuman jack, mike;

        IEnumerator Start() {
            if( !hasError ) {

                yield return Player.Interface.Init();
                Player.Interface.Enable = true;

                Model.Request( 0 );
                yield return Model.LoadRequestedNowAsync();

                player = new Player( 1, 0, new Vector3( 0f, 1f, 0f ) );

                player.Loot.Add( new Item1() );
                player.Loot.Add( new Item2() );
                player.Loot.Add( new Item3() );
                player.Loot.Add( new Item4() );
                player.Loot.Add( new Item5() );
                player.Loot.Add( new Item6() );


                jack = new TempHuman( "Jack", new Vector3( -2f, 0.5f, -4f ) );

                mike = new TempHuman( "Mike", new Vector3( 2f, 0.5f, -4f ) );
                mike.Die();

                Model.Unload();

                Player.Interface.Fade( FadeMode.In );

                var path = Path.Create( true );
                path.AddNode( -6f, 0.5f, -6f );
                path.AddNode( 6f, 0.5f, -6f );
                path.AddNode( 6f, 0.5f, 6f );
                path.AddNode( -6f, 0.5f, 6f );

                jack.AssignToPath( path );

            }
        }

        void Update() {
            if( hasError )
                return;


            if( Player.Exist() ) {
                Debug.Log( player.Loot[ 0 ].Name );
                if( Input.GetKeyDown( KeyCode.F ) )
                    //mike.Reanimate();
                    Localization.SwitchLanguage( Config.Intance.CurrentLanguage, Localization.DEFAULT_LANGUAGE );
            }
        }

        void FixedUpdate() { }

        void OnGUI() {
            if( hasError )
                return;



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

    }

}