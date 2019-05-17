/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using UnityEngine;

namespace MyRPG {

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
                return 20;
                case PartOfEquipment.Spine: // Спина
                return 13;
                case PartOfEquipment.Head: // Голова
                return 10;
                case PartOfEquipment.Torso: // Груди
                return 10;
                case PartOfEquipment.Shoulders: // Плечі
                return 10;
                case PartOfEquipment.Feet: // Ноги
                return 10;
                case PartOfEquipment.Footwear: // Взуття
                return 10;
                case PartOfEquipment.Belt: // Пояс
                return 6;
                case PartOfEquipment.Hands: // Зап'ястя
                return 6;
                case PartOfEquipment.Neck: // Шия
                return 4;
                case PartOfEquipment.Ring: // Палець
                return 4;
            }
            return 1; // Аксесуар
        }

        private Sprites spriteId;
        protected readonly int algorithmUseId = -1;

        protected int price = 0;

        public int Count { get; set; }

        public TypeOfAbility AbilityType { get { return TypeOfAbility.Item; } }
        public int Price { get { return price; } }
        public int TotalPrice { get { return price * Count; } }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Sprite SpriteIcon { get { return Player.Interface.GetSprite( spriteId ); } }
        public ClassOfItem Class { get; private set; }
        public TypeOfItemRarity Rarity { get; private set; }
        public int Level { get; private set; }
        public string ClassName { get; private set; }
        public string RarityName { get; private set; }
        public bool ForSelling { get; private set; }

        public float Timer { get; protected set; }

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