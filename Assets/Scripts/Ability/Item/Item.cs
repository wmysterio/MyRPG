/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyRPG {

	public abstract class Item : IAbility {

        protected int iconID, price, nameId;

        public TypeOfAbility AbilityType { get { return TypeOfAbility.Item; } }
        public string Name { get { return Localization.Current.EntityNames[ nameId ]; } }
        public virtual string Description { get { return string.Empty; } }
        public Texture2D Icon { get { return Player.Interface.Icons[ iconID ]; } }
        public int Price { get { return price; } }
        public int TotalPrice { get { return price * Count; } }

        public float Timer { get; protected set; }
        public bool ForSelling { get; protected set; }

        public ClassOfItem Class { get; private set; }
        public TypeOfItemRarity Rarity { get; private set; }
        public int Level { get; private set; }

        public int Count { get; set; }

        public Item( int level, ClassOfItem itemClass, TypeOfItemRarity rarity ) {
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

        public virtual void Use( Personage target = null ) { }

        public virtual void Update() { }

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