/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG.Items {

    public sealed class TrashItem : Item {
        
        public TrashItem( int id, int nameId, Sprites sprite, int count = 1 ) : base( id, nameId, Personage.MIN_LEVEL, sprite, TypeOfItemRarity.Junk, 8, ClassOfItem.Trash, count, -1 ) { }

    }

}