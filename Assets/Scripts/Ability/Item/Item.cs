/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://www.unity3d.tk/
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG {

	public abstract class Item : IAbility {

        protected int iconID, price;

        public TypeOfAbility AbilityType {
            get { return TypeOfAbility.Item; }
        }

        public float Timer { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public bool ForSelling { get; protected set; }

        public int Price { get { return price; } }
        public int TotalPrice { get { return price * Count; } }
        public Texture2D Icon {
            get { return Player.Interface.Icons[ iconID ]; }
        }

        public ClassOfItem Class { get; private set; }
        public TypeOfItemRarity Rarity { get; private set; }
        public int Level { get; private set; }

        public int Count { get; set; }

        public Item( int level, ClassOfItem itemClass, TypeOfItemRarity rarity ) {
            Name = "Item";
            Description = string.Empty;
            iconID = 0;
            Timer = 0f;
            Count = 1;
            Class = itemClass;
            Rarity = rarity;
            if( Personage.MIN_LEVEL > level )
                level = Personage.MIN_LEVEL;
            if( level > Personage.MAX_LEVEL )
                level = Personage.MAX_LEVEL;
            Level = level;
            price = level * ( int ) itemClass * ( int ) rarity;
            ForSelling = itemClass != ClassOfItem.Quest;
        }

        public virtual void Use( Personage target = null ) { }

        public override string ToString() { return Name; }

    }

    public enum ClassOfItem : int {
        Quest = 0,      // Квестовий
        Trash = 1,      // Сміття
        Normal = 3,     // Звичайний
        Reagent = 5,    // Реагент
        Equipment = 10  // Обладунок
    }

    public enum TypeOfItemRarity : int {
        Junk = 1,        // Мотлох
        Normal = 3,      // Звичайний
        Unusual = 7,     // Незвичайний
        Rare = 12,       // Рідкісний
        Epic = 18,       // Епічний
        Legendary = 25   // Легендарний
    }

}