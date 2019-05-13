/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

    public abstract class AccessoryEquipment : EquipmentItem {

        public AccessoryEquipment( int level, TypeOfItemRarity rarity, int modelID ) : base( level, rarity, MaterialOfEquipment.Other, PartOfEquipment.Accessory, modelID ) {
            nameId = 14;
        }

    }

}