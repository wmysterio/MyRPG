/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

    public abstract class TrashItem : Item {

        public TrashItem( int level, TypeOfItemRarity rarity ) : base( level, ClassOfItem.Trash, rarity ) {
            nameId = 9;
        }

    }

}