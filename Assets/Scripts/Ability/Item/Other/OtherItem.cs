/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

    public sealed class OtherItem : Item {

        public OtherItem( int id, int nameId, Sprites sprite, int count = 1, int algorithmUseId = -1 ) : base( id, nameId, Personage.MIN_LEVEL, sprite, TypeOfItemRarity.Normal, 11, ClassOfItem.Other, count, algorithmUseId ) { }
        
    }

}