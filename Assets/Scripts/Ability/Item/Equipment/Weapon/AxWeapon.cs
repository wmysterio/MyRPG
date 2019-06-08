/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG.Items {

    public sealed class AxWeapon : WeaponEquipment {

        public AxWeapon( int id, int nameId, int level, Sprites sprite, TypeOfItemRarity rarity, int modelId, int count = 1 ) : base( id, nameId, level, sprite, rarity, 26, modelId, TypeOfWeapon.Ax,count ) {
            baseCharacteristic.PhysicalAttackPower = 50;
        }
        
    }

}