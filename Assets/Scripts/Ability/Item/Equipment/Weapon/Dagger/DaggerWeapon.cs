/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

    public abstract class DaggerWeapon : WeaponEquipment {

        public DaggerWeapon( int level, TypeOfItemRarity rarity, int modelID ) : base( level, TypeOfWeapon.Dagger, rarity, modelID ) {
            nameId = 29;
        }

    }

}