/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

    public abstract class SwordWeapon : WeaponEquipment {

        public SwordWeapon( int level, TypeOfItemRarity rarity, int modelID ) : base( level, TypeOfWeapon.Sword, rarity, modelID ) {
            nameId = 32;
        }

    }

}