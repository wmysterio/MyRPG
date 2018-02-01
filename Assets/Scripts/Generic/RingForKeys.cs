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

    public sealed class RingForKeys {

        private Dictionary<KeysForChest, byte> keys;

        public RingForKeys() {
            keys = new Dictionary<KeysForChest, byte>();
            var enumKeys = ( KeysForChest[] ) Enum.GetValues( typeof( KeysForChest ) );
            for( int i = 0; i < enumKeys.Length; i++ )
                keys.Add( enumKeys[ i ], 0 );
        }

        public byte GetKeyAmount( KeysForChest key ) { return keys[ key ]; }
        public string GetKeyName( KeysForChest key ) { return Localization.Current.KeyNames[ ( int ) key ]; }
        public void Add( KeysForChest key ) {
            if( key == KeysForChest.Universal )
                return;
            if( keys[ key ] == byte.MaxValue )
                return;
            keys[ key ] += 1;
        }
        public void Remove( KeysForChest key ) {
            if( key == KeysForChest.Universal )
                return;
            if( keys[ key ] == byte.MinValue )
                return;
            keys[ key ] -= 1;
        }
        public bool HasKey( KeysForChest key ) {
            if( key == KeysForChest.Universal )
                return true;
            return keys[ key ] > 0;
        }

    }

    public enum KeysForChest : byte {
        Universal = 0
    }

}