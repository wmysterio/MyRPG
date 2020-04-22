/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/

namespace MyRPG.Items {

    public sealed class BrassKnucklesWeapon : WeaponEquipment {

        public BrassKnucklesWeapon( int id, int nameId, int level, Sprites sprite, TypeOfItemRarity rarity, int modelId, int count = 1 ) : base( id, nameId, level, sprite, rarity, 28, modelId, TypeOfWeapon.BrassKnuckles, count ) {
            CurrentCharacteristic.PhysicalAttackPower = 5;
            CurrentCharacteristic.ChanceOfParrying = 25;
            CurrentCharacteristic.Speed = 50;
        }
        
    }

}