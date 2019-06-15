/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG.Items {

    public sealed class DaggerWeapon : WeaponEquipment {

        public DaggerWeapon( int id, int nameId, int level, Sprites sprite, TypeOfItemRarity rarity, int modelId, int count = 1 ) : base( id, nameId, level, sprite, rarity, 29, modelId, TypeOfWeapon.Dagger, count ) {
            CurrentCharacteristic.ChanceOfParrying = 10;
            CurrentCharacteristic.PhysicalAttackPower = 25;
        }
        
    }

}