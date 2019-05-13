/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

    public abstract class HammerWeapon : WeaponEquipment {

        public HammerWeapon( int level, TypeOfItemRarity rarity, int modelID ) : base( level, TypeOfWeapon.Hammer, rarity, modelID ) {
            nameId = 30;
        }

    }

}