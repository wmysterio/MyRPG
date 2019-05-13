/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

    public abstract class FeetEquipment : EquipmentItem {

        public FeetEquipment( int level, TypeOfItemRarity rarity, MaterialOfEquipment material, int modelID ) : base( level, rarity, material, PartOfEquipment.Feet, modelID ) {
            nameId = 16;
        }

    }

}