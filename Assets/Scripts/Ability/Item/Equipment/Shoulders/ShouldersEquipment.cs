/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

    public abstract class ShouldersEquipment : EquipmentItem {

        public ShouldersEquipment( int level, TypeOfItemRarity rarity, MaterialOfEquipment material, int modelID ) : base( level, rarity, material, PartOfEquipment.Shoulders, modelID ) {
            nameId = 22;
        }

    }

}