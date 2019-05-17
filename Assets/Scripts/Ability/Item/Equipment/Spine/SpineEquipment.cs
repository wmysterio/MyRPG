/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

    public abstract class SpineEquipment : EquipmentItem {

        public SpineMode Mode { get; private set; }
        public string ModeName { get; private set; }

        protected SpineEquipment( int id, int nameId, int level, Sprites sprite, TypeOfItemRarity rarity, int descriptionId, int modelId, SpineMode mode, int count = 1 ) : base( id, nameId, level, sprite, rarity, descriptionId, modelId, MaterialOfEquipment.Other, PartOfEquipment.Spine, count, -1 ) {
            Mode = mode;
            ModeName = Localization.Current.SpineEquipmentModeNames[ ( int ) mode ];
        }
        
        public enum SpineMode : int {
            Shield = 0,
            Cloak = 1
        }

    }

}