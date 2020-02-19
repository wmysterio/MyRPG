/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using UnityEngine;

namespace MyRPG {

	public sealed class Chest : Object {

        public static int GetModelByType( TypeOfChest type ) { return ( int ) type; }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private byte totalItems;
        private TypeOfChest type;
        private KeysForChest key;
        private Bag loot = new Bag();
        private bool isLocked;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public TypeOfChest Type { get { return type; } }
        public KeysForChest Key { get { return key; } }
        public Bag Loot { get { return loot; } }
        public bool IsLocked { get { return isLocked; } }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public Chest( TypeOfChest type, Vector3 position, KeysForChest key = KeysForChest.Universal, byte maxItems = 8, int nameId = 38 ) : base( GetModelByType( type ), position, nameId ) {
            this.type = type;
            this.key = key;
            totalItems = maxItems;
            isLocked = key != KeysForChest.Universal;
            Freeze( true );
            if( !isLocked )
                generateLoot();
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        protected override void update() {
            base.update();
            if( !IsActive )
                return;
            if( !Player.Exist() )
                return;
            if( Player.Current.NoLongerNeeded )
                return;
            if( !Player.Current.IsActive )
                return;
            if( !Player.Current.IsControllable || !Player.Current.IsStopped )
                return;
            if( !MouseHover || !InputManager.IsMouseUp( MouseKeyName.Right ) )
                return;
            if( !Near( Player.Current, 2f ) )
                return;
            if( !Player.Current.IsTurnedFaceTo( this ) )
                return;
            if( Player.Current.IsInAir() )
                return;
            if( isLocked ) {
                if( !Player.Current.Keys.HasKey( Key ) )
                    return;
                if( !Unlock() )
                    return;
                Player.Current.Keys.Remove( Key );
            }
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public bool Unlock() {
            if( NoLongerNeeded )
                return false;
            if( !isLocked )
                return true;
            generateLoot();
            isLocked = false;
            return true;
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private void generateLoot() {
            //for( byte i = 0; i < totalItems; i++ ) {
            //    Loot.Add( ? item );
            //}
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        protected override void onDestroy() {
            base.onDestroy();
            loot.Clear();
        }

    }

    public enum TypeOfChest : sbyte {
        Quest = -1,                 // Квестові скрині
        FoodAndDrink = -2,          // Їжа та напої
        PoisonsAndReagents = -3,    // Зілля та реагенти
        WeaponsAndEquipment = -4,   // Зброя та обладунки
        Other = -5                  // Різні предмети
    }

}