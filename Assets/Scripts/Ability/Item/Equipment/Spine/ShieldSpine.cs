﻿/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
namespace MyRPG.Items {

    public sealed class ShieldSpine : SpineEquipment {

        public ShieldSpine( int id, int nameId, int level, Sprites sprite, TypeOfItemRarity rarity, int modelId, int count = 1 ) : base( id, nameId, level, sprite, rarity, 12, modelId, SpineMode.Shield, count ) {
            price += 16;
        }

    }

}