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

    //public class DemoSword : SwordWeapon {

    //    public DemoSword() : base( "Мега меч", 1, TypeOfItemRarity.Normal, -1 ) { }

    //}

    //public class DemoReagent : ReagentItem {

    //    public DemoReagent() : base( 1, TypeOfItemRarity.Normal ) { }

    //}

    public class Init : MonoBehaviour {

        bool chg = false;

        void Awake() {
            var go = GameObject.Find( "EntityList" );
            go.AddComponent<Entity.EntityList>();
        }
        
        Player player;

        void Start() {
            StartCoroutine( loadUI() );



            Model.Request( 0, 1, 2, 3, 4, 5, 6, 7 );
            Model.LoadRequestedNow();

            player = new Player( 1, 3, new Vector3( 0f, 1f, 0f ) );

            //for( int i = 0; i < 72; i++ ) {
            //    player.Loot.Add( new DemoSword() );
            //    player.Loot.Add( new DemoReagent() );
            //}

            //print( string.Format( "Total items: {0}", player.Loot.Count ) );

            //// #, Name, Count, Total Price
            //for( int i = 0; i < player.Loot.Count; i++ ) {
            //    print( string.Format(
            //        "{0}. Name: {1}. Count: {2}. Price: {3}",
            //        i,
            //        player.Loot[ i ].Name,
            //        player.Loot[ i ].Count,
            //        player.Loot[ i ].TotalPrice
            //                         )
            //    );
            //}






            var obj = new Object( 0, new Vector3( -1f, 0.5f, -1f ) );
            obj.Freeze( true );

            var pick = new PickUp( TypeOfPickUp.Reusable, 0, new Vector3( 1, 0.5f, 2 ) );


            Model.Unload();
        }





        void Update() {
            if( Player.Exist() ) {

            }
        }

        void FixedUpdate() { }

        private IEnumerator loadUI() { yield return Player.Interface.Init(); }

        void OnGUI() {
            if( !chg ) {
                GUI.skin.settings.cursorColor = new Color( 0.44f, 0.48f, 0.58f, 1f );
                GUI.skin.settings.selectionColor = new Color( 0.44f, 0.48f, 0.58f, 1f );
                GUI.skin.settings.cursorFlashSpeed = 1f;
                chg = true;
            }
        }

    }

}