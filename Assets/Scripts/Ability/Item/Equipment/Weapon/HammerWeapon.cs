﻿/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/

namespace MyRPG.Items {

    public sealed class HammerWeapon : WeaponEquipment {

        public HammerWeapon( int id, int nameId, int level, Sprites sprite, TypeOfItemRarity rarity, int modelId, int count = 1 ) : base( id, nameId, level, sprite, rarity, 30, modelId, TypeOfWeapon.Hammer, count ) { }
        
    }

}