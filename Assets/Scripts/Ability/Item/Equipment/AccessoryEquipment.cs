/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/

namespace MyRPG.Items {

    public sealed class AccessoryEquipment : EquipmentItem {

        public AccessoryEquipment( int id, int nameId, int level, Sprites sprite, TypeOfItemRarity rarity, int modelId, int count = 1 ) : base( id, nameId, level, sprite, rarity, 14, modelId, MaterialOfEquipment.Other, PartOfEquipment.Accessory, count ) { }

    }

}