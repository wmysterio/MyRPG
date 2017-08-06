using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG {

	public sealed class Model {

        private static Dictionary<int, Model> allModels = new Dictionary<int, Model>();
        private static List<int> loadModels = new List<int>();

        public static bool Exist( int id ) { return allModels.ContainsKey( id ); }
        public static Model Find( int id ) { return Exist( id ) ? allModels[ id ] : null; }
        public static void Request( int id ) {
            if( !Exist( id ) )
                loadModels.Add( id );
        }
        public static void Request( params int[] idS ) {
            for( int i = 0; i < idS.Length; i++ ) {
                Request( idS[ i ] );
            }
        }
        public static void LoadRequestedNow() {
            if( loadModels.Count == 0 )
                return;
            for( int i = 0; i < loadModels.Count; i++ ) {
                allModels.Add( loadModels[ i ], new Model( loadModels[ i ] ) );
            }
            loadModels.Clear();
        }
        public static void Unload() { allModels.Clear(); }
        public static void Unload( int id ) { if( Exist( id ) ) allModels.Remove( id ); }


        public int ID { get; private set; }
        public GameObject Prefab { get; private set; }

        private Model( int id ) {
            ID = id;
            Prefab = Resources.Load<GameObject>( string.Format( "Models/{0}", id ) );
        }

	}

}