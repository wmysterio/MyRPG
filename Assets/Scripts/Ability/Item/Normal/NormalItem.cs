/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG.Items {

    public abstract class NormalItem : Item {

        protected NormalItem( int id, int nameId, int level, Sprites sprite, TypeOfItemRarity rarity, int descriptionId, int count = 1, int algorithmUseId = -1 ) : base( id, nameId, level, sprite, rarity, descriptionId, ClassOfItem.Normal, count, algorithmUseId ) { }

    }

}