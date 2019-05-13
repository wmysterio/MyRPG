/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

    public abstract class NeckEquipment : EquipmentItem {

        public NeckEquipment( int level, TypeOfItemRarity rarity, int modelID ) : base( level, rarity, MaterialOfEquipment.Other, PartOfEquipment.Neck, modelID ) {
            nameId = 20;
        }

    }

}