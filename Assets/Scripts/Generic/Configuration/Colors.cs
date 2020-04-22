/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
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
            { Mode.WINDOW_BACKGROUND_COLOR,         "#FFFFFF" },  // new Color32( 255, 255, 255, 255 ) },     
            { Mode.WINDOW_BORDER_COLOR,             "#DADCDF" },  // new Color32( 218, 220, 223, 255 ) },     
            { Mode.WINDOW_TEXT_COLOR,               "#1D2129" },  // new Color32( 29, 33, 41, 255 ) },        
            { Mode.WINDOW_TITLE_BACKGROUND_COLOR,   "#F5F6F7" },  // new Color32( 245, 246, 247, 255 ) },     
            { Mode.WINDOW_TITLE_TEXT_COLOR,         "#4B4F56" },  // new Color32( 75, 79, 86, 255 ) },   
            { Mode.PANEL_BACKGROUND_COLOR,          "#E9EBEE" },  // new Color32( 233, 235, 238, 255 ) },     
            { Mode.PANEL_BORDER_COLOR,              "#DADCDF" },  // new Color32( 218, 220, 223, 255 ) },   
            { Mode.TOOLTIP_BACKGROUND_COLOR,        "#F5F5F5" },  // new Color32( 245 ,245, 245, 255 )
            { Mode.TOOLTIP_BORDER_COLOR,            "#DADCDF" },  // new Color32( 218, 220, 223, 255 )
            { Mode.TOOLTIP_TEXT_COLOR,              "#5C6065" },  // new Color32( 92, 96, 101, 255 )  
            { Mode.MESSAGEBOX_BACKGROUND_COLOR,     "#F5F5F5" },  // new Color32( 245 ,245, 245, 255 )
            { Mode.MESSAGEBOX_BORDER_COLOR,         "#DADCDF" },  // new Color32( 218, 220, 223, 255 )
            { Mode.MESSAGEBOX_TEXT_COLOR,           "#5C6065" },  // new Color32( 92, 96, 101, 255 )  
            { Mode.CONSOLE_BACKGROUND_COLOR,        "#F5F5F5" },  // new Color32( 245 ,245, 245, 255 )
            { Mode.CONSOLE_BORDER_COLOR,            "#DADCDF" },  // new Color32( 218, 220, 223, 255 )
            { Mode.CONSOLE_TEXT_COLOR,              "#5C6065" },  // new Color32( 92, 96, 101, 255 )  
            { Mode.SCROLLBAR_BACKGROUND_COLOR,      "#FFFFFF" },  // new Color32( 255, 255, 255, 255 ) },     
            { Mode.SCROLLBAR_COLOR,                 "#DADCDF" },  // new Color32( 218, 220, 223, 255 ) },     
            { Mode.SLIDER_BACKGROUND_COLOR,         "#FFFFFF" },  // new Color32( 255, 255, 255, 255 ) 
            { Mode.SLIDER_COLOR,                    "#333740" },  // new Color32( 51, 55, 64, 255 ) 
            { Mode.BAG_ITEM_BACKGROUND_COLOR,       "#FFFFFF" },  // new Color32( 255, 255, 255, 255 ) },     
            { Mode.BAG_ITEM_BORDER_COLOR,           "#DADCDF" },  // new Color32( 218, 220, 223, 255 ) },     
            { Mode.BAG_ITEM_AMOUNT_BACKGROUND_COLOR,"#DADCDF" },  // new Color32( 218, 220, 223, 255 ) },       
            { Mode.BAG_ITEM_EQUIP_BORDER_COLOR,     "#000000" },  // new Color32( 0, 0, 0, 255 ) },     
            { Mode.RESOURCE_HEALTH_LIGHT,           "#B4191D" },  // new Color32( 180, 25, 29, 255 ) },       
            { Mode.RESOURCE_HEALTH_DARK,            "#5A0C0E" },  // new Color32( 90, 12, 14, 255 ) },        
            { Mode.RESOURCE_MANA_LIGHT,             "#52B3D9" },  // new Color32( 82, 179, 217, 255 ) },      
            { Mode.RESOURCE_MANA_DARK,              "#013243" },  // new Color32( 1, 50, 67, 255 ) },         
            { Mode.RESOURCE_ENERGY_LIGHT,           "#FFFF7E" },  // new Color32( 255, 255, 126, 255 ) },     
            { Mode.RESOURCE_ENERGY_DARK,            "#F7CA18" },  // new Color32( 247, 202, 24, 255 ) },      
            { Mode.POSITIV_VALUE,                   "#03A762" },  // new Color32( 3, 167, 98, 255 ) },        
            { Mode.NEGATIVE_VALUE,                  "#EE1C25" }   // new Color32( 255, 160, 122, 255 ) }  
        };

        private static Dictionary<TypeOfItemRarity, string> richColorsByRarity = new Dictionary<TypeOfItemRarity, string>() {
            { TypeOfItemRarity.Junk,                "#5C6065" },  // new Color32( 92, 96, 101, 255 )  
            { TypeOfItemRarity.Normal,              "#5C6065" },  // new Color32( 92, 96, 101, 255 )  
            { TypeOfItemRarity.Unusual,             "#008E5B" },  // new Color32( 0, 142, 91, 255 )   
            { TypeOfItemRarity.Rare,                "#2A71B0" },  // new Color32( 42, 113, 176, 255 ) 
            { TypeOfItemRarity.Epic,                "#6D398B" },  // new Color32( 109, 57, 139, 255 ) 
            { TypeOfItemRarity.Legendary,           "#EA621F" }   // new Color32( 234, 98, 31, 255 )  

        };

        private static Dictionary<RelationshipOfPersonage, string> richColorsByRelationship = new Dictionary<RelationshipOfPersonage, string>() {
            { RelationshipOfPersonage.Friendly,     "#26A65B" },  //  new Color32( 38, 166, 91, 255 ) 
            { RelationshipOfPersonage.Neutral,      "#F5AB35" },  //  new Color32( 245, 171, 53, 255 )
            { RelationshipOfPersonage.Enemy,        "#D91E18" }   //  new Color32( 217, 30, 24, 255 ) 
        };

        /* ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- */

        private static Dictionary<Mode, Color> baseColorsByMode = new Dictionary<Mode, Color>() {
            { Mode.WINDOW_BACKGROUND_COLOR,         new Color32( 255, 255, 255, 255 ) },    // #FFFFFF
            { Mode.WINDOW_BORDER_COLOR,             new Color32( 218, 220, 223, 255 ) },    // #DADCDF
            { Mode.WINDOW_TEXT_COLOR,               new Color32( 29, 33, 41, 255 ) },       // #1D2129
            { Mode.WINDOW_TITLE_BACKGROUND_COLOR,   new Color32( 245, 246, 247, 255 ) },    // #F5F6F7
            { Mode.WINDOW_TITLE_TEXT_COLOR,         new Color32( 75, 79, 86, 255 ) },       // #4B4F56
            { Mode.PANEL_BACKGROUND_COLOR,          new Color32( 233, 235, 238, 255 ) },    // #E9EBEE
            { Mode.PANEL_BORDER_COLOR,              new Color32( 218, 220, 223, 255 ) },    // #DADCDF
            { Mode.TOOLTIP_BACKGROUND_COLOR,        new Color32( 245 ,245, 245, 255 ) },    // #F5F5F5
            { Mode.TOOLTIP_BORDER_COLOR,            new Color32( 218, 220, 223, 255 ) },    // #DADCDF
            { Mode.TOOLTIP_TEXT_COLOR,              new Color32( 92, 96, 101, 255 ) },      // #5C6065
            { Mode.MESSAGEBOX_BACKGROUND_COLOR,     new Color32( 245 ,245, 245, 255 ) },    // #F5F5F5
            { Mode.MESSAGEBOX_BORDER_COLOR,         new Color32( 218, 220, 223, 255 ) },    // #DADCDF
            { Mode.MESSAGEBOX_TEXT_COLOR,           new Color32( 92, 96, 101, 255 ) },      // #5C6065
            { Mode.CONSOLE_BACKGROUND_COLOR,        new Color32( 245 ,245, 245, 255 ) },    // #F5F5F5
            { Mode.CONSOLE_BORDER_COLOR,            new Color32( 218, 220, 223, 255 ) },    // #DADCDF
            { Mode.CONSOLE_TEXT_COLOR,              new Color32( 92, 96, 101, 255 ) },      // #5C6065
            { Mode.SCROLLBAR_BACKGROUND_COLOR,      new Color32( 255, 255, 255, 255 ) },    // #FFFFFF
            { Mode.SCROLLBAR_COLOR,                 new Color32( 218, 220, 223, 255 ) },    // #DADCDF
            { Mode.SLIDER_BACKGROUND_COLOR,         new Color32( 255, 255, 255, 255 ) },    // #FFFFFF
            { Mode.SLIDER_COLOR,                    new Color32( 51, 55, 64, 255 ) },       // #333740
            { Mode.BAG_ITEM_BACKGROUND_COLOR,       new Color32( 255, 255, 255, 255 ) },    // #FFFFFF
            { Mode.BAG_ITEM_BORDER_COLOR,           new Color32( 218, 220, 223, 255 ) },    // #DADCDF
            { Mode.BAG_ITEM_AMOUNT_BACKGROUND_COLOR,new Color32( 218, 220, 223, 255 ) },    // #DADCDF
            { Mode.BAG_ITEM_EQUIP_BORDER_COLOR,     new Color32( 0, 0, 0, 255 ) },          // #000000
            { Mode.RESOURCE_HEALTH_LIGHT,           new Color32( 180, 25, 29, 255 ) },      // #B4191D
            { Mode.RESOURCE_HEALTH_DARK,            new Color32( 90, 12, 14, 255 ) },       // #5A0C0E
            { Mode.RESOURCE_MANA_LIGHT,             new Color32( 82, 179, 217, 255 ) },     // #52B3D9
            { Mode.RESOURCE_MANA_DARK,              new Color32( 1, 50, 67, 255 ) },        // #013243
            { Mode.RESOURCE_ENERGY_LIGHT,           new Color32( 255, 255, 126, 255 ) },    // #FFFF7E
            { Mode.RESOURCE_ENERGY_DARK,            new Color32( 247, 202, 24, 255 ) },     // #F7CA18
            { Mode.POSITIV_VALUE,                   new Color32( 3, 167, 98, 255 ) },       // #03A762
            { Mode.NEGATIVE_VALUE,                  new Color32( 255, 160, 122, 255 ) }     // #EE1C25
        };

        private static Dictionary<TypeOfItemRarity, Color> baseColorsByRarity = new Dictionary<TypeOfItemRarity, Color>() {
            { TypeOfItemRarity.Junk,                new Color32( 92, 96, 101, 255 ) },      // #5C6065
            { TypeOfItemRarity.Normal,              new Color32( 92, 96, 101, 255 ) },      // #5C6065
            { TypeOfItemRarity.Unusual,             new Color32( 0, 142, 91, 255 ) },       // #008E5B
            { TypeOfItemRarity.Rare,                new Color32( 42, 113, 176, 255 ) },     // #2A71B0
            { TypeOfItemRarity.Epic,                new Color32( 109, 57, 139, 255 ) },     // #6D398B
            { TypeOfItemRarity.Legendary,           new Color32( 234, 98, 31, 255 ) }       // #EA621F
        };

        private static Dictionary<RelationshipOfPersonage, Color> baseColorsByRelationship = new Dictionary<RelationshipOfPersonage, Color>() {
            { RelationshipOfPersonage.Friendly,     new Color32( 38, 166, 91, 255 ) },      // #26A65B
            { RelationshipOfPersonage.Neutral,      new Color32( 245, 171, 53, 255 ) },     // #F5AB35
            { RelationshipOfPersonage.Enemy,        new Color32( 217, 30, 24, 255 ) }       // #D91E18
        };

        public enum Mode {
            WINDOW_BACKGROUND_COLOR,
            WINDOW_BORDER_COLOR,
            WINDOW_TEXT_COLOR,
            WINDOW_TITLE_BACKGROUND_COLOR,
            WINDOW_TITLE_TEXT_COLOR,
            CONSOLE_BACKGROUND_COLOR,
            CONSOLE_BORDER_COLOR,
            CONSOLE_TEXT_COLOR,
            TOOLTIP_BACKGROUND_COLOR,
            TOOLTIP_BORDER_COLOR,
            TOOLTIP_TEXT_COLOR,
            MESSAGEBOX_BACKGROUND_COLOR,
            MESSAGEBOX_BORDER_COLOR,
            MESSAGEBOX_TEXT_COLOR,
            SCROLLBAR_BACKGROUND_COLOR,
            SCROLLBAR_COLOR,
            SLIDER_BACKGROUND_COLOR,
            SLIDER_COLOR,
            BAG_ITEM_BACKGROUND_COLOR,
            BAG_ITEM_BORDER_COLOR,
            BAG_ITEM_AMOUNT_BACKGROUND_COLOR,
            BAG_ITEM_EQUIP_BORDER_COLOR,
            PANEL_BACKGROUND_COLOR,
            PANEL_BORDER_COLOR,
            RESOURCE_HEALTH_LIGHT,
            RESOURCE_HEALTH_DARK,
            RESOURCE_MANA_LIGHT,
            RESOURCE_MANA_DARK,
            RESOURCE_ENERGY_LIGHT,
            RESOURCE_ENERGY_DARK,
            POSITIV_VALUE,
            NEGATIVE_VALUE
        }

    }

}