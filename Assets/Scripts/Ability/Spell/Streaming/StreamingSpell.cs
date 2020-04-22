/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/

namespace MyRPG {

	public abstract class StreamingSpell : Spell {
	
		public StreamingSpell( int minLevel, float castTime ) : base( minLevel, TypeOfSpell.Streaming, castTime ) {
            nameId = 35;
        }

    }

}