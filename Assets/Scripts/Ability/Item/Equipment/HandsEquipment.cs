﻿/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/

namespace MyRPG.Items {

    public sealed class HandsEquipment : EquipmentItem {

        public HandsEquipment( int id, int nameId, int level, Sprites sprite, TypeOfItemRarity rarity, int modelId, MaterialOfEquipment material, int count = 1 ) : base( id, nameId, level, sprite, rarity, 18, modelId, material, PartOfEquipment.Hands, count ) { }

    }

}