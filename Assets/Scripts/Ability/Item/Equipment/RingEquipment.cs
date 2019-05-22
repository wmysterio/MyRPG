/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

    public sealed class RingEquipment : EquipmentItem {

        public RingEquipment( int id, int nameId, int level, Sprites sprite, TypeOfItemRarity rarity, int modelId, int count = 1 ) : base( id, nameId, level, sprite, rarity, 21, modelId, MaterialOfEquipment.Other, PartOfEquipment.Ring, count ) { }

    }

}