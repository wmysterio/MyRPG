/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG.Items {

    public sealed class FeetEquipment : EquipmentItem {

        public FeetEquipment( int id, int nameId, int level, Sprites sprite, TypeOfItemRarity rarity, int modelId, MaterialOfEquipment material, int count = 1 ) : base( id, nameId, level, sprite, rarity, 16, modelId, material, PartOfEquipment.Feet, count ) { }

    }

}