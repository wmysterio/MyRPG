/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

    public abstract class BrassKnucklesWeapon : WeaponEquipment {

        public BrassKnucklesWeapon( int level, TypeOfItemRarity rarity, int modelID ) : base( level, TypeOfWeapon.BrassKnuckles, rarity, modelID ) {
            nameId = 28;
        }

    }

}