/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/
using UnityEngine;
using System.Text;
using MyRPG.Configuration;

namespace MyRPG.Items {

	public abstract class Item : IAbility {
        
        protected static int getPriceByRarity( TypeOfItemRarity rarity ) {
            switch( rarity ) {
                case TypeOfItemRarity.Legendary: // Легендарний
                return 25;
                case TypeOfItemRarity.Epic: // Епічний
                return 18;
                case TypeOfItemRarity.Rare: // Рідкісний
                return 12;
                case TypeOfItemRarity.Unusual: // Незвичайний
                return 7;
            }
            return 1; // Мотлох і звичайний
        }
        protected static int getPriceByClass( ClassOfItem itemClass ) {
            switch( itemClass ) {
                case ClassOfItem.Equipment: // Обладунок
                return 10;
                case ClassOfItem.Reagent: // Реагент
                return 2;
            }
            return 1; // Інше, квестовий, сміття і звичайний
        }
        protected static int getPriceByMaterial( MaterialOfEquipment material ) {
            switch( material ) {
                case MaterialOfEquipment.PlateArmour: // Лати
                return 12;
                case MaterialOfEquipment.Hauberk: // Кольчуга
                return 9;
                case MaterialOfEquipment.Leather: // Шкіра
                return 4;
                case MaterialOfEquipment.Cloth: // Тканина
                return 2;
            }		
            return 1; // Різне
        }
        protected static int getPriceByPartOfEquipment( PartOfEquipment part ) {
            switch( part ) {
                case PartOfEquipment.Weapon: // Зброя
                return 35;
                case PartOfEquipment.Head: // Голова
                return 15;
                case PartOfEquipment.Torso: // Груди
                return 15;
                case PartOfEquipment.Shoulders: // Плечі
                return 15;
                case PartOfEquipment.Feet: // Ноги
                return 15;
                case PartOfEquipment.Footwear: // Взуття
                return 15;
                case PartOfEquipment.Belt: // Пояс
                return 12;
                case PartOfEquipment.Hands: // Зап'ястя
                return 12;
                case PartOfEquipment.Neck: // Шия
                return 8;
                case PartOfEquipment.Ring: // Палець
                return 8;
                case PartOfEquipment.Accessory: // Аксесуар
                return 8;
            }
            return 3; // Спина
        }
        protected static int getPriceByTypeOfWeapon( TypeOfWeapon type ) {
            switch( type ) {
                case TypeOfWeapon.Hammer: // Молот
                return 25;
                case TypeOfWeapon.Ax: // Сокира
                return 25;
                case TypeOfWeapon.Sword: // Меч
                return 25;
                case TypeOfWeapon.Dagger: // Кинджал
                return 25;
                case TypeOfWeapon.BrassKnuckles: // Кастет
                return 15;
            }
            return 12; // Лук, посох і жезл
        }


        private Sprites spriteId;
        protected readonly int algorithmUseId = -1;
        protected int price = 0;


        public TypeOfAbility AbilityType { get { return TypeOfAbility.Item; } }
        public int Price { get { return price; } }
        public int TotalPrice { get { return price * Count; } }
        public Sprite SpriteIcon { get { return Player.Interface.GetSprite( spriteId ); } }

        public float Timer { get; protected set; }

        public int Id { get; private set; }
        public int Level { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ClassOfItem Class { get; private set; }
        public TypeOfItemRarity Rarity { get; private set; }
        public string ClassName { get; private set; }
        public string RarityName { get; private set; }
        public bool ForSelling { get; private set; }
        
        public int Count { get; set; }

        private Item() { }
        protected Item( int id, int nameId, int level, Sprites sprite, TypeOfItemRarity rarity, int descriptionId, ClassOfItem itemClass, int count = 1, int algorithmUseId = -1 ) {
            Id = id;
            Name = Localization.Current.ItemNames[ nameId ];
            Description = Localization.Current.EntityDescriptions[ descriptionId ];
            spriteId = sprite;
            Count = Mathf.Clamp( count, 1, int.MaxValue );
            Class = itemClass;
            Rarity = rarity;
            Level = Mathf.Clamp( level, Personage.MIN_LEVEL, Personage.MAX_LEVEL );
            price = Level * getPriceByClass( itemClass ) * getPriceByRarity( rarity );
            ForSelling = !( itemClass == ClassOfItem.Quest || itemClass == ClassOfItem.Other );
            ClassName = Localization.Current.ItemClassNames[ ( int ) itemClass ];
            RarityName = Localization.Current.ItemRarityNames[ ( int ) rarity ];
            Timer = 0f;
            this.algorithmUseId = algorithmUseId;
        }

        public string GetTooltipText() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine( $"<size=36>{Colors.WrapString( Name, Rarity ).ToUpper()}</size>" );
            sb.AppendLine( $"{Localization.Current.StatNames[ 4 ]} {Level}" );
            if( Class == ClassOfItem.Equipment ) {
                var equipmentItem = ( EquipmentItem ) this;
                if( equipmentItem.EquipmentPart == PartOfEquipment.Weapon ) {
                    var weaponEquipment = ( WeaponEquipment ) equipmentItem;
                    sb.AppendLine( $"{weaponEquipment.EquipmentPartName} ({equipmentItem.Description})" );
                    sb.AppendLine( $"{Localization.Current.ItemInfo[ 2 ]} {weaponEquipment.MinRange}-{weaponEquipment.MaxRange}" );
                    sb.AppendFormat( Localization.Current.ItemInfo[ 3 ], weaponEquipment.MinDamage, weaponEquipment.Timer );
                    sb.Append( "\r\n" );
                } else {
                    sb.Append( $"{equipmentItem.ClassName} ({equipmentItem.Description}" );
                    if( equipmentItem.Material != MaterialOfEquipment.Other )
                        sb.Append( $", {equipmentItem.MaterialName}" );
                    sb.Append( ")\r\n" );
                }
                if( Player.Current.Equipments.IsSlotFree( equipmentItem.EquipmentPart ) ) {
                    equipmentItem.CurrentCharacteristic.Each( ( slot, value, slotName ) => {
                        if( value > 0f )
                            sb.AppendLine( Colors.WrapString( $"+{value} {slotName}", Colors.Mode.POSITIV_VALUE ) );
                    } );
                } else {
                    var playerEquipment = Player.Current.Equipments[ equipmentItem.EquipmentPart ];
                    if( equipmentItem.Id == playerEquipment.Id ) {
                        playerEquipment.CurrentCharacteristic.Each( ( slot, value, slotName ) => {
                            if( value > 0f )
                                sb.AppendLine( $"+{value} {slotName}" );
                        } );
                    } else {
                        var comparedCharacteristic = equipmentItem.CurrentCharacteristic.Compare( playerEquipment.CurrentCharacteristic );
                        comparedCharacteristic.Each( ( slot, value, slotName ) => {
                            if( value == 0f )
                                return;
                            if( value > 0f )
                                sb.AppendLine( Colors.WrapString( $"+{value} {slotName}", Colors.Mode.POSITIV_VALUE ) );
                            if( 0f > value )
                                sb.AppendLine( Colors.WrapString( $"{value} {slotName}", Colors.Mode.NEGATIVE_VALUE ) );
                        } );
                    }
                }
            } else { sb.AppendLine( Description ); }
            sb.Append( $"\r\n{( ForSelling ? $"{Localization.Current.ItemInfo[ 0 ]} {Price}" : Localization.Current.ItemInfo[ 1 ] )}" );
            return sb.ToString();
        }

        public virtual void Use( Personage target = null ) { }
        public virtual void Update() { }

        public override string ToString() { return Name; }


        #region DELETE LATER
        protected int iconID, nameId;
        public Texture2D Icon { get { return Player.Interface.Icons[ iconID ]; } }
        #endregion
    }

    public enum ClassOfItem : int {
        Other = 0,      // Інше
        Quest = 1,      // Квестовий
        Trash = 2,      // Сміття
        Normal = 3,     // Звичайний
        Reagent = 4,    // Реагент
        Equipment = 5   // Обладунок
    }

    public enum TypeOfItemRarity : int {
        Junk = 0,        // Мотлох
        Normal = 1,      // Звичайний
        Unusual = 2,     // Незвичайний
        Rare = 3,        // Рідкісний
        Epic = 4,        // Епічний
        Legendary = 5    // Легендарний
    }

}