/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

    public sealed class QuestItem : Item {

        public QuestItem( int id, int nameId, Sprites sprite, int count = 1 ) : base( id, nameId, Personage.MIN_LEVEL, sprite, TypeOfItemRarity.Normal, 9, ClassOfItem.Quest, count, -1 ) { }
        
    }

}