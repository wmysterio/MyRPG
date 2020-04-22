/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/

namespace MyRPG {

	public abstract class ReproductionSpell : Spell {

        public ReproductionSpell( int minLevel, float castTime ) : base( minLevel, TypeOfSpell.Reproduction, castTime ) {
            nameId = 36;
        }

    }

}