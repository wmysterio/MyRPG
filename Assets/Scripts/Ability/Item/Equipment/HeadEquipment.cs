/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

    public sealed class HeadEquipment : EquipmentItem {

        public HeadEquipment( int id, int nameId, int level, Sprites sprite, TypeOfItemRarity rarity, int modelId, MaterialOfEquipment material, int count = 1 ) : base( id, nameId, level, sprite, rarity, 19, modelId, material, PartOfEquipment.Head, count ) { }

    }

}