/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

    public sealed class ReagentItem : Item {

        public ReagentItem( int id, int nameId, Sprites sprite, int count = 1 ) : base( id, nameId, Personage.MIN_LEVEL, sprite, TypeOfItemRarity.Normal, 10, ClassOfItem.Reagent, count, -1 ) { }
        
    }

}