/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/

namespace MyRPG {

	public abstract class InstantSpell : Spell {

        public InstantSpell( int minLevel ) : base( minLevel, TypeOfSpell.Instant, 0f ) {
            nameId = 37;
            EnableCastInAir = true;
            EnableCastInRun = true;
        }

    }

}