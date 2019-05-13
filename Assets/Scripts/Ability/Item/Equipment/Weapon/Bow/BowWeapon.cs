/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

    public abstract class BowWeapon : WeaponEquipment {

        public BowWeapon( int level, TypeOfItemRarity rarity, int modelID ) : base( level, TypeOfWeapon.Bow, rarity, modelID ) {
            nameId = 27;
        }

    }

}