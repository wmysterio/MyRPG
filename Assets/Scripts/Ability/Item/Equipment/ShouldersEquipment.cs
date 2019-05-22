/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG.Items {

    public sealed class ShouldersEquipment : EquipmentItem {

        public ShouldersEquipment( int id, int nameId, int level, Sprites sprite, TypeOfItemRarity rarity, int modelId, MaterialOfEquipment material, int count = 1 ) : base( id, nameId, level, sprite, rarity, 22, modelId, material, PartOfEquipment.Shoulders, count ) { }

    }

}