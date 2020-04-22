/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/

namespace MyRPG.Items {

    public sealed class QuestItem : Item {

        public QuestItem( int id, int nameId, Sprites sprite, int count = 1 ) : base( id, nameId, Personage.MIN_LEVEL, sprite, TypeOfItemRarity.Normal, 9, ClassOfItem.Quest, count, -1 ) { }
        
    }

}