/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

    public sealed class AccessoryEquipment : EquipmentItem {

        public AccessoryEquipment( int id, int nameId, int level, Sprites sprite, TypeOfItemRarity rarity, int modelId, int count = 1 ) : base( id, nameId, level, sprite, rarity, 14, modelId, MaterialOfEquipment.Other, PartOfEquipment.Accessory, count, -1 ) { }

    }

}