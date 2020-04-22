/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/
using System.Collections.Generic;
using System.Linq;

namespace MyRPG {

	public sealed class EffectList {

        private List<Effect> effects;
        private int iterator;
        private Characteristic characteristic;

        public int Count { get { return effects.Count; } }

        public Effect this[ int index ] {
            get {
                return effects[ index ];
            }
        }

        public EffectList() {
            effects = new List<Effect>();
            iterator = 0;
            characteristic = Characteristic.CreateEmpty();
        }

        private T[] find<T>( Personage sender ) where T : Effect { return ( from e in effects where e is T && e.Sender == sender select e as T ).ToArray(); }

        public void Give<T>( T effect ) where T : Effect {
            var query = find<T>( effect.Sender );
            if( query == null ) {
                effects.Add( effect );
                return;
            }
            if( query.Length == 0 ) {
                effects.Add( effect );
                return;
            }
            if( query[ 0 ].IsPassive || query[ 0 ].NoLongerNeeded )
                return;
            query[ 0 ].Recreate();
        }

        public void ClearAll( bool ignorePassive = true ) {
            if( !ignorePassive ) {
                effects.Clear();
                return;
            }
            var query = effects.Where( e => !e.IsPassive ).ToArray();
            for( iterator = 0; iterator < query.Length; iterator++ ) {
                effects.Remove( query[ iterator ] );
            }
        }

        public Characteristic Update() {
            characteristic.Clear();
            for( iterator = effects.Count - 1; iterator >= 0; iterator-- ) {
                effects[ iterator ].Update();
                if( effects[ iterator ].NoLongerNeeded ) {
                    effects.RemoveAt( iterator );
                } else {
                    characteristic += effects[ iterator ].CurrentCharacteristic;
                }
            }
            return characteristic;
        }

    }

}