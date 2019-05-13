/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

    public abstract class WandWeapon : WeaponEquipment {

        public WandWeapon( int level, TypeOfItemRarity rarity, int modelID ) : base( level, TypeOfWeapon.Wand, rarity, modelID ) {
            nameId = 33;
        }

    }

}