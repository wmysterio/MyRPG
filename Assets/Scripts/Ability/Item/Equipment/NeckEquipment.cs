/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/

namespace MyRPG.Items {

    public sealed class NeckEquipment : EquipmentItem {

        public NeckEquipment( int id, int nameId, int level, Sprites sprite, TypeOfItemRarity rarity, int modelId, int count = 1 ) : base( id, nameId, level, sprite, rarity, 20, modelId, MaterialOfEquipment.Other, PartOfEquipment.Neck, count ) { }

    }

}