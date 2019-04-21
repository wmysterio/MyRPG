/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG {

    public sealed class Bag {

        public const int MAX_ITEM_COUNT = 100;

        public static readonly int ItemsInRow = ( int ) Mathf.Sqrt( MAX_ITEM_COUNT );

        private Dictionary<System.Type, Item> items;
        private int iterator;

        public int Count { get { return items.Count; } }
        public Item this[ int index ] {
            get {
                if( 0 > index || index >= items.Count )
                    return null;
                return items.ElementAt( index ).Value;
            }
        }

        public bool IsOnPlayer { get; private set; }

        public Bag( bool isOnPlayer = false ) {
            items = new Dictionary<System.Type, Item>();
            iterator = 0;
            IsOnPlayer = isOnPlayer;
        }

        public bool Add<T>( T item ) where T : Item {
            if( IsOnPlayer && item is Money ) {
                item.Use();
                return true;
            }
            if( items.ContainsKey( typeof( T ) ) ) {
                items[ typeof( T ) ].Count += item.Count;
                return true;
            }
            if( items.Count + 1 > MAX_ITEM_COUNT )
                return false;
            items.Add( typeof( T ), item );
            return true;
        }
        public bool Add( Bag bag ) {
            for( iterator = bag.items.Count - 1; iterator >= 0 ; iterator-- ) {
                if( !Add( bag.items.ElementAt( iterator ).Value ) )
                    return false;
                bag.Remove( iterator );
            }
            return true;
        }
        public void Clear() { items.Clear(); }
        public void Remove( int index ) {
            if( this[ index ] != null )
                items.Remove( this[ index ].GetType() );
        }
        public T Get<T>() where T : Item {
            if( !items.ContainsKey( typeof( T ) ) )
                return null;
            return ( T ) items[ typeof( T ) ];
        }
        public void UpdateItems() {
            for( iterator = items.Count - 1; iterator >= 0; iterator-- ) {
                if( items.ElementAt( iterator ).Value.Count == 0 ) {
                    Remove( iterator );
                    return;
                }
                items.ElementAt( iterator ).Value.Update();
            }
        }

    }

}