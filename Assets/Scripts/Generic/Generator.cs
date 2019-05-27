/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System.Collections.Generic;
using MyRPG.Items;

namespace MyRPG {

    public static class Generator {

/*
0   1     2      3       4       5      6         7              8                       
id, name, level, sprite, rarity, model, material, algorithm_use, algorithm_create
*/

        public static OtherItem CreateMoney( int count ) { return new OtherItem( 0, 0, Sprites.ITEM_GOLD, count, 0 ); }
        public static Item CreateItem( int id, int count = 1 ) {
            //
            return null;
        }


        private delegate Item CreateAlgorithmHandler( int count, params int[] db_values );
        private static Dictionary<int, CreateAlgorithmHandler> createAlgorithms = new Dictionary<int, CreateAlgorithmHandler>() {
            { 1, ( count, db ) => new TrashItem( db[ 0 ], db[ 1 ], ( Sprites ) db[ 3 ], count ) },
            { 2, ( count, db ) => new ReagentItem( db[ 0 ], db[ 1 ], ( Sprites ) db[ 3 ], count ) },
            { 3, ( count, db ) => new QuestItem( db[ 0 ], db[ 1 ], ( Sprites ) db[ 3 ], count ) },
            { 4, ( count, db ) => new OtherItem( db[ 0 ], db[ 1 ], ( Sprites ) db[ 3 ], count, db[ 7 ] ) },
            { 5, ( count, db ) => new AccessoryEquipment( db[ 0 ], db[ 1 ], db[ 2 ], ( Sprites ) db[ 3 ], ( TypeOfItemRarity ) db[ 4 ], db[ 5 ], count ) },
            { 6, ( count, db ) => new NeckEquipment( db[ 0 ], db[ 1 ], db[ 2 ], ( Sprites ) db[ 3 ], ( TypeOfItemRarity ) db[ 4 ], db[ 5 ], count ) },
            { 7, ( count, db ) => new RingEquipment( db[ 0 ], db[ 1 ], db[ 2 ], ( Sprites ) db[ 3 ], ( TypeOfItemRarity ) db[ 4 ], db[ 5 ], count ) },
            { 8, ( count, db ) => new ShieldSpine( db[ 0 ], db[ 1 ], db[ 2 ], ( Sprites ) db[ 3 ], ( TypeOfItemRarity ) db[ 4 ], db[ 5 ], count ) },
            { 9, ( count, db ) => new CloakSpine( db[ 0 ], db[ 1 ], db[ 2 ], ( Sprites ) db[ 3 ], ( TypeOfItemRarity ) db[ 4 ], db[ 5 ], count ) },
            { 10, ( count, db ) => new AxWeapon( db[ 0 ], db[ 1 ], db[ 2 ], ( Sprites ) db[ 3 ], ( TypeOfItemRarity ) db[ 4 ], db[ 5 ], count ) },
            { 11, ( count, db ) => new BowWeapon( db[ 0 ], db[ 1 ], db[ 2 ], ( Sprites ) db[ 3 ], ( TypeOfItemRarity ) db[ 4 ], db[ 5 ], count ) },
            { 12, ( count, db ) => new BrassKnucklesWeapon( db[ 0 ], db[ 1 ], db[ 2 ], ( Sprites ) db[ 3 ], ( TypeOfItemRarity ) db[ 4 ], db[ 5 ], count ) },
            { 13, ( count, db ) => new DaggerWeapon( db[ 0 ], db[ 1 ], db[ 2 ], ( Sprites ) db[ 3 ], ( TypeOfItemRarity ) db[ 4 ], db[ 5 ], count ) },
            { 14, ( count, db ) => new HammerWeapon( db[ 0 ], db[ 1 ], db[ 2 ], ( Sprites ) db[ 3 ], ( TypeOfItemRarity ) db[ 4 ], db[ 5 ], count ) },
            { 15, ( count, db ) => new StaffWeapon( db[ 0 ], db[ 1 ], db[ 2 ], ( Sprites ) db[ 3 ], ( TypeOfItemRarity ) db[ 4 ], db[ 5 ], count ) },
            { 16, ( count, db ) => new SwordWeapon( db[ 0 ], db[ 1 ], db[ 2 ], ( Sprites ) db[ 3 ], ( TypeOfItemRarity ) db[ 4 ], db[ 5 ], count ) },
            { 17, ( count, db ) => new WandWeapon( db[ 0 ], db[ 1 ], db[ 2 ], ( Sprites ) db[ 3 ], ( TypeOfItemRarity ) db[ 4 ], db[ 5 ], count ) },
            { 18, ( count, db ) => new BeltEquipment( db[ 0 ], db[ 1 ], db[ 2 ], ( Sprites ) db[ 3 ], ( TypeOfItemRarity ) db[ 4 ], db[ 5 ], ( MaterialOfEquipment ) db[ 6 ], count ) },
            { 19, ( count, db ) => new FeetEquipment( db[ 0 ], db[ 1 ], db[ 2 ], ( Sprites ) db[ 3 ], ( TypeOfItemRarity ) db[ 4 ], db[ 5 ], ( MaterialOfEquipment ) db[ 6 ], count ) },
            { 20, ( count, db ) => new FootwearEquipment( db[ 0 ], db[ 1 ], db[ 2 ], ( Sprites ) db[ 3 ], ( TypeOfItemRarity ) db[ 4 ], db[ 5 ], ( MaterialOfEquipment ) db[ 6 ], count ) },
            { 21, ( count, db ) => new HandsEquipment( db[ 0 ], db[ 1 ], db[ 2 ], ( Sprites ) db[ 3 ], ( TypeOfItemRarity ) db[ 4 ], db[ 5 ], ( MaterialOfEquipment ) db[ 6 ], count ) },
            { 22, ( count, db ) => new HeadEquipment( db[ 0 ], db[ 1 ], db[ 2 ], ( Sprites ) db[ 3 ], ( TypeOfItemRarity ) db[ 4 ], db[ 5 ], ( MaterialOfEquipment ) db[ 6 ], count ) },
            { 23, ( count, db ) => new ShouldersEquipment( db[ 0 ], db[ 1 ], db[ 2 ], ( Sprites ) db[ 3 ], ( TypeOfItemRarity ) db[ 4 ], db[ 5 ], ( MaterialOfEquipment ) db[ 6 ], count ) },
            { 24, ( count, db ) => new TorsoEquipment( db[ 0 ], db[ 1 ], db[ 2 ], ( Sprites ) db[ 3 ], ( TypeOfItemRarity ) db[ 4 ], db[ 5 ], ( MaterialOfEquipment ) db[ 6 ], count ) }

            // NORMAL ITEM

        };
        
    }

}