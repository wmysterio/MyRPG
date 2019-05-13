/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using UnityEngine;

namespace MyRPG {

	public abstract class Item : IAbility {

        #region V2.0

        private static int getPriceByRarity( TypeOfItemRarity rarity ) {
            switch( rarity ) {
                case TypeOfItemRarity.Legendary:
                return 25;
                case TypeOfItemRarity.Epic:
                return 18;
                case TypeOfItemRarity.Rare:
                return 12;
                case TypeOfItemRarity.Unusual:
                return 7;
            }
            return 1;
        }
        private static int getPriceByClass( ClassOfItem itemClass ) {
            switch( itemClass ) {
                case ClassOfItem.Equipment:
                return 10;
                case ClassOfItem.Reagent:
                return 2;
            }
            return 1;
        }

        public TypeOfAbility AbilityType { get { return TypeOfAbility.Item; } }
        public int Price { get { return price; } }
        public int TotalPrice { get { return price * Count; } }

        public int Count { get; set; }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Sprite SpriteIcon { get; private set; }
        public ClassOfItem Class { get; private set; }
        public TypeOfItemRarity Rarity { get; private set; }
        public int Level { get; private set; }
        public string ClassName { get; private set; }
        public string RarityName { get; private set; }

        public bool ForSelling { get; protected set; }

        public float Timer { get; protected set; }

        private int algorithmCreateId = -1, algorithmUsedId = -1;

        private Item() { }
        public Item( int id, int nameId, int level, Sprites spite, TypeOfItemRarity rarity, ClassOfItem itemClass, int count = 1, int algorithmCreateId = -1, int algorithmUsedId = -1 ) {
            Id = id;
            Name = Localization.Current.ItemNames[ nameId ];
            Description = Localization.Current.EntityDescriptions[ 8 ];
            SpriteIcon = Player.Interface.GetSprite( spite );
            Count = Mathf.Clamp( count, 1, int.MaxValue );
            Class = itemClass;
            Rarity = rarity;
            Level = Mathf.Clamp( level, Personage.MIN_LEVEL, Personage.MAX_LEVEL );
            price = Level * getPriceByClass( itemClass ) * getPriceByRarity( rarity );
            ForSelling = !( itemClass == ClassOfItem.Quest || itemClass == ClassOfItem.Other );
            ClassName = Localization.Current.ItemClassNames[ ( int ) itemClass ];
            RarityName = Localization.Current.ItemClassNames[ ( int ) rarity ];
            Timer = 0f;
            this.algorithmCreateId = algorithmCreateId;
            this.algorithmUsedId = algorithmUsedId;
        }


        public virtual void Use( Personage target = null ) { }
        public virtual void Update() { }

        public override string ToString() { return Name; }
        #endregion

        protected int iconID, price, nameId;
        public Texture2D Icon { get { return Player.Interface.Icons[ iconID ]; } }



        




        private Item( int level, ClassOfItem itemClass, TypeOfItemRarity rarity ) {
            nameId = 8;
            iconID = 0;
            Timer = 0f;
            Count = 1;
            Class = itemClass;
            Rarity = rarity;
            Level = Mathf.Clamp( level, Personage.MIN_LEVEL, Personage.MAX_LEVEL );
            price = Level * ( int ) itemClass * ( int ) rarity;
            ForSelling = itemClass != ClassOfItem.Quest;
        }


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