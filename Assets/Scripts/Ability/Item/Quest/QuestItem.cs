/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

    public abstract class QuestItem : Item {

        public QuestItem( int level, TypeOfItemRarity rarity ) : base( level, ClassOfItem.Quest, rarity ) {
            nameId = 11;
        }

    }

}