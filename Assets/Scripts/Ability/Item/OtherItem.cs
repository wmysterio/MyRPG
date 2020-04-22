/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/

namespace MyRPG.Items {

    public sealed class OtherItem : Item {

        public OtherItem( int id, int nameId, Sprites sprite, int count = 1, int algorithmUseId = -1 ) : base( id, nameId, Personage.MIN_LEVEL, sprite, TypeOfItemRarity.Normal, 11, ClassOfItem.Other, count, algorithmUseId ) { }
        
    }

}