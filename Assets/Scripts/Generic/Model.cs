﻿/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyRPG {

	public sealed class Model {

        private static Dictionary<int, Model> allModels = new Dictionary<int, Model>();
        private static List<int> loadModels = new List<int>();
        private static ResourceRequest request;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public static bool Exist( int id ) { return allModels.ContainsKey( id ); }
        public static Model Find( int id ) { return Exist( id ) ? allModels[ id ] : null; }
        public static void Request( int id ) {
            if( !Exist( id ) )
                loadModels.Add( id );
        }
        public static void Request( params int[] idS ) {
            for( int i = 0; i < idS.Length; i++ )
                Request( idS[ i ] );
        }
        public static void LoadRequestedNow() {
            if( loadModels.Count == 0 )
                return;
            for( int i = 0; i < loadModels.Count; i++ ) {
                var prefab = Resources.Load<GameObject>( $"Models/{loadModels[ i ]}" );
                allModels.Add( loadModels[ i ], new Model( loadModels[ i ], prefab ) );
            }
            loadModels.Clear();
        }
        public static IEnumerator LoadRequestedAsync() {
            if( loadModels.Count == 0 )
                yield return null;
            for( int i = 0; i < loadModels.Count; i++ ) {
                request = Resources.LoadAsync<GameObject>( $"Models/{loadModels[ i ]}" );
                yield return request;
                allModels.Add( loadModels[ i ], new Model( loadModels[ i ], ( GameObject ) request.asset ) );
            }
            loadModels.Clear();
        }
        public static void Unload() { allModels.Clear(); }
        public static void Unload( int id ) { if( Exist( id ) ) allModels.Remove( id ); }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private int id;
        private GameObject prefab;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public int ID { get { return id; } }
        public GameObject Prefab { get { return prefab; } }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private Model( int id, GameObject prefab ) { this.id = id; this.prefab = prefab; }

	}

}