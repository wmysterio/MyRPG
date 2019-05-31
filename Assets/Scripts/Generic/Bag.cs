/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MyRPG.Items;

namespace MyRPG {

    public sealed class Bag {

        public delegate void BagHandler( Item item, int index );

        public const int MAX_ITEMS_COUNT = 100;

        [System.Obsolete( "DELETE" )]
        public static readonly int ItemsInRow = ( int ) Mathf.Sqrt( MAX_ITEMS_COUNT );

        private Dictionary<int, Item> items = new Dictionary<int, Item>();
        private int iterator = 0;

        [System.Obsolete( "DELETE" )]
        public Item this[ int slot ] {
            get {
                if( 0 > slot || slot >= items.Count )
                    return null;
                return items.ElementAt( slot ).Value;
            }
        }

        public int Count { get { return items.Count; } }
        public bool IsOnPlayer { get; private set; }

        public Bag( bool isOnPlayer = false ) { IsOnPlayer = isOnPlayer; }

        public void Clear() { items.Clear(); }
        public bool Add( Item item, bool playSound = false ) {
            if( item == null )
                return false;
            if( 1 > item.Count )
                return false;
            if( item.Id == 0 && IsOnPlayer ) {
                item.Use();
                // play sound
                return true;
            }
            if( items.ContainsKey( item.Id ) ) {
                items[ item.Id ].Count += item.Count;
                // play sound
                return true;
            }
            if( items.Count == MAX_ITEMS_COUNT )
                return false;
            items.Add( item.Id, item );
            // play sound
            return true;
        }

        public bool Add( Bag bag, bool playSound = false ) {
            if( bag == this )
                return false;
            foreach( var item in bag.items.Values )
                Add( item );
            // play sound
            return true;
        }

        public void ForEach( BagHandler callback ) {
            if( callback == null )
                return;
            int counter = 0;
            foreach( var item in items.Values ) {
                callback.Invoke( item, counter );
                counter += 1;
            }
        }
        public bool Remove( Item item, bool playSound = false ) {
            if( item == null )
                return false;
            if( !items.ContainsKey( item.Id ) )
                return false;
            items.Remove( item.Id );
            // play sound
            return true;
        }
        public bool Remove( Item item, int count, bool playSound = false ) {
            if( item == null )
                return false;
            if( !items.ContainsKey( item.Id ) )
                return false;
            if( 1 > count )
                return false;
            if( count > items[ item.Id ].Count )
                count = items[ item.Id ].Count;
            items[ item.Id ].Count -= count;
            if( items[ item.Id ].Count == 0 )
                items.Remove( item.Id );
            // play sound
            return true;
        }
        public bool HasItem( Item item ) {
            if( item == null )
                return false;
            return items.ContainsKey( item.Id );
        }
        public bool HasItem( int id ) { return items.ContainsKey( id ); }
        public void UpdateItems() {
            for( iterator = items.Count - 1; iterator >= 0; iterator-- ) {
                var item = items.ElementAt( iterator ).Value;
                if( 1 > item.Count ) {
                    items.Remove( item.Id );
                    continue;
                }
                item.Update();
            }
        }

        /*
        public T Get<T>() where T : Item {
            if( !items.ContainsKey( typeof( T ) ) )
                return null;
            return ( T ) items[ typeof( T ) ];
        }
        */

    }

}