/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

    public abstract class HeadEquipment : EquipmentItem {

        public HeadEquipment( int level, TypeOfItemRarity rarity, MaterialOfEquipment material, int modelID ) : base( level, rarity, material, PartOfEquipment.Head, modelID ) {
            nameId = 19;
        }

    }

}