/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

    public abstract class NormalItem : Item {

        public NormalItem( int level, TypeOfItemRarity rarity ) : base( level, ClassOfItem.Normal, rarity ) {
            nameId = 12;
        }

    }

}