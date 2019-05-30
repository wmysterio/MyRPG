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
            { Mode.WINDOW_BACKGROUND, "#deedfcff" },
            { Mode.WINDOW_BORDER, "#a8b4c6ff" },
            { Mode.PANEL_BACKGROUND, "#bfd3e9ff" },
            { Mode.PANEL_BORDER, "#95a2b4ff" },
            { Mode.MAIN_TEXT, "#03091bff" },
            { Mode.RED_LIGHT, "#b4191dff" },
            { Mode.RED_DARK, "#5a0c0eff" },
            { Mode.BLUE_LIGHT, "#52b3d9ff" },
            { Mode.BLUE_DARK, "#013243ff" },
            { Mode.YELLOW_LIGHT, "#ffff7eff" },
            { Mode.YELLOW_DARK, "#f7ca18ff" }
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
            { Mode.WINDOW_BACKGROUND, new Color32( 222, 237, 252, 255 ) },
            { Mode.WINDOW_BORDER, new Color32( 168, 180, 198, 255 ) },
            { Mode.PANEL_BACKGROUND, new Color32( 191, 211, 233, 255 ) },
            { Mode.PANEL_BORDER, new Color32( 149, 162, 180, 255 ) },
            { Mode.MAIN_TEXT, new Color32( 3, 9, 27, 255 ) },
            { Mode.RED_LIGHT, new Color32( 180, 25, 29, 255 ) },
            { Mode.RED_DARK, new Color32( 90, 12, 14, 255 ) },
            { Mode.BLUE_LIGHT, new Color32( 82, 179, 217, 255 ) },
            { Mode.BLUE_DARK, new Color32( 1, 50, 67, 255 ) },
            { Mode.YELLOW_LIGHT, new Color32( 255, 255, 126, 255 ) },
            { Mode.YELLOW_DARK, new Color32( 247, 202, 24, 255 ) }
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

        /* ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- */

        public enum Mode {
            WINDOW_BACKGROUND,
            WINDOW_BORDER,
            PANEL_BACKGROUND,
            PANEL_BORDER,
            MAIN_TEXT,
            RED_LIGHT,
            RED_DARK,
            BLUE_LIGHT,
            BLUE_DARK,
            YELLOW_LIGHT,
            YELLOW_DARK
        }

    }

}