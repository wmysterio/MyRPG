/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MyRPG.Items;

namespace MyRPG {

    public sealed class Bag {

        [System.Obsolete( "DELETE" )]
        public static readonly int ItemsInRow = ( int ) Mathf.Sqrt( SLOT_COUNT );

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */






        public const int SLOT_COUNT = 100;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public delegate void BagCallbackHandler( Item item, int index );

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private Dictionary<int, Item> items = new Dictionary<int, Item>();
        private int iterator = 0;
        private bool isOnPlayer;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public Item this[ int itemID ] {
            get {
                if( !items.ContainsKey( itemID ) )
                    return null;
                return items[ itemID ];
            }
        }
        public int Count { get { return items.Count; } }
        public bool IsOnPlayer { get { return isOnPlayer; } }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public Bag( bool isOnPlayer = false ) { this.isOnPlayer = isOnPlayer; }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public void Clear() { items.Clear(); }
        public bool Add( Item item, bool playSound = false ) {
            if( item == null )
                return false;
            if( 1 > item.Count )
                return false;
            if( item.Id == 0 && isOnPlayer ) {
                item.Use();
                // play sound
                return true;
            }
            if( items.ContainsKey( item.Id ) ) {
                items[ item.Id ].Count += item.Count;
                // play sound
                return true;
            }
            if( items.Count == SLOT_COUNT )
                return false;
            items.Add( item.Id, item );
            // play sound
            return true;
        }
        public bool Add( Bag bag, bool playSound = false ) {
            if( bag == null || bag == this )
                return false;
            foreach( var item in bag.items.Values )
                Add( item );
            // play sound
            return true;
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
            if( 1 > count )
                return false;
            if( !items.ContainsKey( item.Id ) )
                return false;
            if( count > items[ item.Id ].Count )
                count = items[ item.Id ].Count;
            items[ item.Id ].Count -= count;
            if( items[ item.Id ].Count == 0 )
                items.Remove( item.Id );
            // play sound
            return true;
        }
        public bool Remove( int itemID, bool playSound = false ) {
            if( !items.ContainsKey( itemID ) )
                return false;
            items.Remove( itemID );
            // play sound
            return true;
        }
        public bool Remove( int itemID, int count, bool playSound = false ) {
            if( 1 > count )
                return false;
            if( !items.ContainsKey( itemID ) )
                return false;
            if( count > items[ itemID ].Count )
                count = items[ itemID ].Count;
            items[ itemID ].Count -= count;
            if( items[ itemID ].Count == 0 )
                items.Remove( itemID );
            // play sound
            return true;
        }
        public bool HasItem( Item item ) {
            if( item == null )
                return false;
            return items.ContainsKey( item.Id );
        }
        public bool HasItem( int itemID ) { return items.ContainsKey( itemID ); }
        public int Each( BagCallbackHandler callback ) {
            if( callback == null )
                return 0;
            int counter = -1;
            foreach( var item in items.Values ) {
                counter += 1;
                callback.Invoke( item, counter );
            }
            return counter + 1;
        }
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
        public Item Get( int slot ) {
            if( 0 > slot || slot >= items.Count )
                return null;
            return items.ElementAt( slot ).Value;
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