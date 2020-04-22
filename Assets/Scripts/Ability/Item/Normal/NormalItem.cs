/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/

namespace MyRPG.Items {

    public abstract class NormalItem : Item {

        protected NormalItem( int id, int nameId, int level, Sprites sprite, TypeOfItemRarity rarity, int descriptionId, int count = 1, int algorithmUseId = -1 ) : base( id, nameId, level, sprite, rarity, descriptionId, ClassOfItem.Normal, count, algorithmUseId ) { }

    }

}