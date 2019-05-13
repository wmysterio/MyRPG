/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

    public abstract class ReagentItem : Item {

        public ReagentItem( int level, TypeOfItemRarity rarity ) : base( level, ClassOfItem.Reagent, rarity ) {
            nameId = 10;
        }

    }

}