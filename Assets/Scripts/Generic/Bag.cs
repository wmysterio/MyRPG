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

    public sealed class Bag {

        public const int MAX_ITEM_COUNT = 144;

        private Dictionary<System.Type, Item> items;
        private int iterator;

        public int Count { get { return items.Count; } }
        public Item this[ int index ] {
            get {
                if( 0 > index || index > items.Count )
                    return null;
                return items.ElementAt( index ).Value;
            }
        }

        public Bag() {
            items = new Dictionary<Type, Item>();
            iterator = 0;
        }

        public bool Add<T>( T item ) where T : Item {
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
            for( iterator = 0; iterator < bag.items.Count; iterator++ ) {
                if( !Add( bag.items.ElementAt( iterator ).Value ) )
                    return false;
            }
            return true;
        }
        public void Clear() { items.Clear(); }
        public void Remove<T>( T item = null ) where T : Item {
            if( items.ContainsKey( typeof( T ) ) ) {
                items.Remove( typeof( T ) );
            }
        }
        public T Get<T>() where T : Item {
            if( !items.ContainsKey( typeof( T ) ) )
                return null;
            return ( T ) items[ typeof( T ) ];
        }
        public void UpdateItems() {
            for( iterator = items.Count - 1; iterator >= 0; iterator-- ) {
                items.ElementAt( iterator ).Value.Update();
            }
        }

    }

}