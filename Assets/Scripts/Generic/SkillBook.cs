/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/
using System.Collections.Generic;
using System.Linq;

namespace MyRPG {

    public sealed class SkillBook {

        private Dictionary<System.Type, Spell> spells;
        private int iterator;

        public int Count { get { return spells.Count; } }

        public Spell this[ int index ] {
            get {
                if( 0 > index || index >= spells.Count )
                    return null;
                return spells.ElementAt( index ).Value;
            }
        }

        public SkillBook() {
            spells = new Dictionary<System.Type, Spell>();
            iterator = 0;
        }

        public bool HasSpell<T>( T spell ) { return spells.ContainsKey( typeof( T ) ); }

        public bool Learn<T>( T spell ) where T : Spell {
            if( spell == null || spells.ContainsKey( typeof( T ) ) )
                return false;
            spells.Add( typeof( T ), spell );
            return true;
        }

        public bool Forget<T>( T spell ) where T : Spell {
            if( spell == null || !spells.ContainsKey( typeof( T ) ) )
                return false;
            spells.Remove( typeof( T ) );
            return true;
        }

        public void Clear() { spells.Clear(); }

        public void Update() {
            for( iterator = spells.Count - 1; iterator >= 0; iterator-- ) {
                spells.Values.ElementAt( iterator ).Update();
            }
        }

    }

}