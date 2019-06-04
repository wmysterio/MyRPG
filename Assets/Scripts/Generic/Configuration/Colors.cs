/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System.Collections.Generic;
using UnityEngine;
using MyRPG.Items;

namespace MyRPG.Configuration {

    public static class Colors {

        public static string RichColor( Mode mode ) { return richColorsByMode[ mode ]; }
        public static string RichColor( TypeOfItemRarity rarity ) { return richColorsByRarity[ rarity ]; }
        public static string RichColor( RelationshipOfPersonage relationship ) { return richColorsByRelationship[ relationship ]; }

        public static Color BaseColor( Mode mode ) { return baseColorsByMode[ mode ]; }
        public static Color BaseColor( TypeOfItemRarity rarity ) { return baseColorsByRarity[ rarity ]; }
        public static Color BaseColor( RelationshipOfPersonage relationship ) { return baseColorsByRelationship[ relationship ]; }

        public static string WrapString( string text, Mode color ) { return $"<color={richColorsByMode[ color ]}>{text}</color>"; }
        public static string WrapString( string text, TypeOfItemRarity color ) { return $"<color={richColorsByRarity[ color ]}>{text}</color>"; }
        public static string WrapString( string text, RelationshipOfPersonage color ) { return $"<color={richColorsByRelationship[ color ]}>{text}</color>"; }

        /* ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- */

        private static Dictionary<Mode, string> richColorsByMode = new Dictionary<Mode, string>() {
            { Mode.WINDOW_BACKGROUND_COLOR, "#ffffffff" },
            { Mode.WINDOW_BORDER_COLOR, "#1bd88eff" },
            { Mode.WINDOW_TEXT_COLOR, "#000000ff" },
            { Mode.WINDOW_TITLE_TEXT_COLOR, "#daede5ff" },
            { Mode.RESOURCE_HEALTH_LIGHT, "#b4191dff" },
            { Mode.RESOURCE_HEALTH_DARK, "#5a0c0eff" },
            { Mode.RESOURCE_MANA_LIGHT, "#52b3d9ff" },
            { Mode.RESOURCE_MANA_DARK, "#013243ff" },
            { Mode.RESOURCE_ENERGY_LIGHT, "#ffff7eff" },
            { Mode.RESOURCE_ENERGY_DARK, "#f7ca18ff" }
        };

        private static Dictionary<TypeOfItemRarity, string> richColorsByRarity = new Dictionary<TypeOfItemRarity, string>() {
            { TypeOfItemRarity.Junk, "#9d9d9dff" },
            { TypeOfItemRarity.Normal, "#95a2b4ff" },
            { TypeOfItemRarity.Unusual, "#2ecc71ff" },
            { TypeOfItemRarity.Rare, "#0070ddff" },
            { TypeOfItemRarity.Epic, "#9345ffff" },
            { TypeOfItemRarity.Legendary, "#ff8000ff" }
        };

        private static Dictionary<RelationshipOfPersonage, string> richColorsByRelationship = new Dictionary<RelationshipOfPersonage, string>() {
            { RelationshipOfPersonage.Friendly, "#26a65bff" },
            { RelationshipOfPersonage.Neutral, "#f5ab35ff" },
            { RelationshipOfPersonage.Enemy, "#d91e18ff" },
        };

        /* ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- */

        private static Dictionary<Mode, Color> baseColorsByMode = new Dictionary<Mode, Color>() {
            { Mode.WINDOW_BACKGROUND_COLOR, new Color32( 255, 255, 255, 255 ) },
            { Mode.WINDOW_BORDER_COLOR, new Color32( 111, 185, 156, 255 ) },
            { Mode.WINDOW_TEXT_COLOR, new Color32( 0, 0, 0, 255 ) },
            { Mode.WINDOW_TITLE_TEXT_COLOR, new Color32( 218, 237, 229, 255 ) },
            { Mode.RESOURCE_HEALTH_LIGHT, new Color32( 180, 25, 29, 255 ) },
            { Mode.RESOURCE_HEALTH_DARK, new Color32( 90, 12, 14, 255 ) },
            { Mode.RESOURCE_MANA_LIGHT, new Color32( 82, 179, 217, 255 ) },
            { Mode.RESOURCE_MANA_DARK, new Color32( 1, 50, 67, 255 ) },
            { Mode.RESOURCE_ENERGY_LIGHT, new Color32( 255, 255, 126, 255 ) },
            { Mode.RESOURCE_ENERGY_DARK, new Color32( 247, 202, 24, 255 ) }
        };

        private static Dictionary<TypeOfItemRarity, Color> baseColorsByRarity = new Dictionary<TypeOfItemRarity, Color>() {
            { TypeOfItemRarity.Junk, new Color32( 157, 157, 157, 255 ) },
            { TypeOfItemRarity.Normal, new Color32( 149, 162, 180, 255 ) },
            { TypeOfItemRarity.Unusual, new Color32( 46, 204, 113, 255 ) },
            { TypeOfItemRarity.Rare, new Color32( 0, 112, 221, 255 ) },
            { TypeOfItemRarity.Epic, new Color32( 147, 69, 255, 255 ) },
            { TypeOfItemRarity.Legendary, new Color32( 255, 128, 0, 255 ) }
        };

        private static Dictionary<RelationshipOfPersonage, Color> baseColorsByRelationship = new Dictionary<RelationshipOfPersonage, Color>() {
            { RelationshipOfPersonage.Friendly, new Color32( 38, 166, 91, 255 ) },
            { RelationshipOfPersonage.Neutral, new Color32( 245, 171, 53, 255 ) },
            { RelationshipOfPersonage.Enemy, new Color32( 217, 30, 24, 255 ) }
        };

        public enum Mode {
            WINDOW_BACKGROUND_COLOR,
            WINDOW_BORDER_COLOR,
            WINDOW_TEXT_COLOR,
            WINDOW_TITLE_TEXT_COLOR,
            RESOURCE_HEALTH_LIGHT,
            RESOURCE_HEALTH_DARK,
            RESOURCE_MANA_LIGHT,
            RESOURCE_MANA_DARK,
            RESOURCE_ENERGY_LIGHT,
            RESOURCE_ENERGY_DARK
        }

    }

}