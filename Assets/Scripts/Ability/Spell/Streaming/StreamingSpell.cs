/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

	public abstract class StreamingSpell : Spell {
	
		public StreamingSpell( int minLevel, float castTime ) : base( minLevel, TypeOfSpell.Streaming, castTime ) {
            nameId = 35;
        }

    }

}