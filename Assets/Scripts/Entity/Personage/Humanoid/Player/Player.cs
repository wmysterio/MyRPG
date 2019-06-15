/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using UnityEngine;

namespace MyRPG {

    public partial class Player : Humanoid {

        public const float TURN_SPEED = 100f;
        public const string TAG = "Player";

        public static Player Current { get; private set; }
        public static bool Exist() { return Current != null; }

        private float strength;

        public float Strength {
            get { return strength; }
            set { strength = Mathf.Clamp( value, 0f, 100f ); }
        }

        public EquipmentList Equipments { get; private set; }
        public int Money { get; private set; }
        public int CurrentExperience { get; private set; }
        public int TotalExperience { get; private set; }
        public int ExperienceToLevel { get { return TotalExperience - CurrentExperience; } }
        public RingForKeys Keys { get; private set; }

        public Player( string name, int level, int modelId, Vector3 position ) : base( level, RankOfPersonage.Normal, modelId, position ) {
            nameId = -1;
            Name = name;
            if( Current != null )
                throw new System.Exception( "Гравець може бути тільки один!" );
            gameObject.tag = TAG;
            strength = 100f;
            Current = this;
            Money = 0;
            CurrentExperience = 0;
            TotalExperience = calculateMaxExperience();
            Keys = new RingForKeys();
            Equipments = new EquipmentList();


            //
            Loot.Add( new Items.TrashItem( 1, 0, Sprites.ITEM_GOLD, 1 ) );
            Loot.Add( new Items.ReagentItem( 2, 0, Sprites.ITEM_GOLD, 1 ) );
            Loot.Add( new Items.QuestItem( 3, 0, Sprites.ITEM_GOLD, 1 ) );
            Loot.Add( new Items.OtherItem( 4, 0, Sprites.ITEM_GOLD, 1, -1 ) );
            Loot.Add( new Items.AccessoryEquipment( 5, 0, 100, Sprites.ITEM_GOLD, Items.TypeOfItemRarity.Junk, 0, 1 ) );
            Loot.Add( new Items.NeckEquipment( 6, 0, 100, Sprites.ITEM_GOLD, Items.TypeOfItemRarity.Normal, 0, 1 ) );
            Loot.Add( new Items.RingEquipment( 7, 0, 100, Sprites.ITEM_GOLD, Items.TypeOfItemRarity.Unusual, 0, 1 ) );
            Loot.Add( new Items.BeltEquipment( 8, 0, 100, Sprites.ITEM_GOLD, Items.TypeOfItemRarity.Rare, 0, Items.MaterialOfEquipment.Cloth, 1 ) );
            Loot.Add( new Items.FeetEquipment( 9, 0, 100, Sprites.ITEM_GOLD, Items.TypeOfItemRarity.Epic, 0, Items.MaterialOfEquipment.Hauberk, 1 ) );
            Loot.Add( new Items.FootwearEquipment( 10, 0, 100, Sprites.ITEM_GOLD, Items.TypeOfItemRarity.Legendary, 0, Items.MaterialOfEquipment.Leather, 1 ) );
            Loot.Add( new Items.HandsEquipment( 11, 0, 100, Sprites.ITEM_GOLD, Items.TypeOfItemRarity.Junk, 0, Items.MaterialOfEquipment.PlateArmour, 1 ) );
            Loot.Add( new Items.HeadEquipment( 12, 0, 100, Sprites.ITEM_GOLD, Items.TypeOfItemRarity.Normal, 0, Items.MaterialOfEquipment.Cloth, 1 ) );
            Loot.Add( new Items.ShouldersEquipment( 13, 0, 100, Sprites.ITEM_GOLD, Items.TypeOfItemRarity.Unusual, 0, Items.MaterialOfEquipment.Hauberk, 1 ) );
            Loot.Add( new Items.TorsoEquipment( 14, 0, 100, Sprites.ITEM_GOLD, Items.TypeOfItemRarity.Rare, 0, Items.MaterialOfEquipment.Leather, 1 ) );
            Loot.Add( new Items.ShieldSpine( 15, 0, 100, Sprites.ITEM_GOLD, Items.TypeOfItemRarity.Epic, 0, 1 ) );
            Loot.Add( new Items.CloakSpine( 16, 0, 100, Sprites.ITEM_GOLD, Items.TypeOfItemRarity.Legendary, 0, 1 ) );
            Loot.Add( new Items.AxWeapon( 17, 0, 100, Sprites.ITEM_GOLD, Items.TypeOfItemRarity.Junk, 0, 1 ) );
            Loot.Add( new Items.BowWeapon( 18, 0, 100, Sprites.ITEM_GOLD, Items.TypeOfItemRarity.Normal, 0, 1 ) );
            Loot.Add( new Items.BrassKnucklesWeapon( 19, 0, 100, Sprites.ITEM_GOLD, Items.TypeOfItemRarity.Unusual, 0, 1 ) );
            Loot.Add( new Items.DaggerWeapon( 20, 0, 100, Sprites.ITEM_GOLD, Items.TypeOfItemRarity.Rare, 0, 10 ) );
            Loot.Add( new Items.HammerWeapon( 21, 0, 100, Sprites.ITEM_GOLD, Items.TypeOfItemRarity.Epic, 0, 1 ) );
            Loot.Add( new Items.StaffWeapon( 22, 0, 100, Sprites.ITEM_GOLD, Items.TypeOfItemRarity.Legendary, 0, 1 ) );
            Loot.Add( new Items.SwordWeapon( 23, 0, 100, Sprites.ITEM_GOLD, Items.TypeOfItemRarity.Junk, 0, 1 ) );
            Loot.Add( new Items.WandWeapon( 24, 0, 100, Sprites.ITEM_GOLD, Items.TypeOfItemRarity.Normal, 0, 1 ) );

            Equipments.Set( Loot[ 20 ] );
            Equipments.Set( Loot[ 16 ] );
            Equipments.Set( Loot[ 7 ] );
            Equipments.Set( Loot[ 10 ] );

            //

        }

        protected override void updateCharacteristic() {
            base.updateCharacteristic();
            if( Equipments != null )
                CurrentCharacteristic += Equipments.CurrentCharacteristic / ( 100f - Strength );
        }

        protected override void update() {
            base.update();
        }

        protected override void onCast( CastResult result, TypeOfResources resource = TypeOfResources.Nothing ) {
            base.onCast( result, resource );

        }

        public void AddMoney( int amount ) {
            try {
                var pMoney = Money;
                checked { pMoney += amount; }
                if( pMoney > 0 )
                    Money = pMoney;
            } catch {
                if( amount > 0 )
                    Money = int.MaxValue;
            }
        }
        public void AddExperience( int amount ) {
            if( MAX_LEVEL == Level )
                return;
            CurrentExperience += amount;
            if( CurrentExperience > TotalExperience ) {
                var newExp = Mathf.Abs( ExperienceToLevel );
                LevelUp();
                if( Level == MAX_LEVEL ) {
                    CurrentExperience = 0;
                    TotalExperience = 0;
                } else {
                    CurrentExperience = newExp;
                    TotalExperience = calculateMaxExperience();
                }
            }
        }

        public override void Die() {
            base.Die();
            if( Immortal )
                return;
            Camera.Detach();
            Interface.ToggleWindow( Window.None );
        }
        public override void Destroy() {
            base.Destroy();
            Camera.Detach();
            Interface.ToggleWindow( Window.None );
            Current = null;
        }

        private int calculateMaxExperience() { return ( int ) ( 795 + 250 * Mathf.Pow( 1.08f, Level ) ); }

    }

}