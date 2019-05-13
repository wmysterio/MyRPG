/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

	public abstract class ReproductionSpell : Spell {

        public ReproductionSpell( int minLevel, float castTime ) : base( minLevel, TypeOfSpell.Reproduction, castTime ) {
            nameId = 36;
        }

    }

}