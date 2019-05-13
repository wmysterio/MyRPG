/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

    public abstract class TorsoEquipment : EquipmentItem {

        public TorsoEquipment( int level, TypeOfItemRarity rarity, MaterialOfEquipment material, int modelID ) : base( level, rarity, material, PartOfEquipment.Torso, modelID ) {
            nameId = 24;
        }

    }

}