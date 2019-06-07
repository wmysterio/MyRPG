/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using UnityEngine;
using UnityEngine.EventSystems;
using System.Text;
using MyRPG.Configuration;
using MyRPG.Items;

namespace MyRPG.Sync {

    public sealed class BagSlotSync : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

        private static StringBuilder stringBuilder = new StringBuilder();

        [HideInInspector] public int Number = -1;
        [HideInInspector] public int Id = -1;

        public void OnPointerClick( PointerEventData eventData ) {
            if( Number == -1 || Id == -1 )
                return;
            switch( eventData.button ) {
                case PointerEventData.InputButton.Left:
                Debug.Log( "move" );
                break;
                case PointerEventData.InputButton.Middle:
                if( Player.Current.Loot.HasItem( Id ) )
                    Player.Current.Loot.Remove( Id, true );
                Player.Interface.HideTooltip();
                BagWindowSync.Current.Hide( Number );
                break;
                case PointerEventData.InputButton.Right:
                Debug.Log( "use" );
                break;
            }
        }

        public void OnPointerEnter( PointerEventData eventData ) {
            if( Number == -1 || Id == -1 )
                return;
            stringBuilder.Clear();
            var item = Player.Current.Loot[ Id ];
            stringBuilder.AppendLine( $"<size=36>{Colors.WrapString( item.Name, item.Rarity ).ToUpper()}</size>");
            stringBuilder.AppendLine( $"{Localization.Current.StatNames[ 4 ]} {item.Level}" );
            if( item.Class == ClassOfItem.Equipment ) {
                var equipmentItem = ( EquipmentItem ) item;
                if( equipmentItem.EquipmentPart == PartOfEquipment.Weapon ) {
                    var weaponEquipment = ( WeaponEquipment ) equipmentItem;
                    stringBuilder.AppendLine( $"{weaponEquipment.EquipmentPartName} ({equipmentItem.Description})" );
                    stringBuilder.AppendLine( $"{Localization.Current.ItemInfo[ 2 ]} {weaponEquipment.MinRange}-{weaponEquipment.MaxRange}" );
                    stringBuilder.AppendLine( $"{Localization.Current.ItemInfo[ 3 ]} {weaponEquipment.MinDamage}" );
                } else {
                    stringBuilder.Append( $"{equipmentItem.ClassName} ({equipmentItem.Description}" );
                    if( equipmentItem.Material != MaterialOfEquipment.Other )
                        stringBuilder.Append( $", {equipmentItem.MaterialName}" );
                    stringBuilder.Append( $")\r\n" );
                }

                // ...

            } else { stringBuilder.AppendLine( $"{item.Description}" ); }
            stringBuilder.Append( $"\r\n{( item.ForSelling ? $"{Localization.Current.ItemInfo[ 0 ]} {item.Price}" : Localization.Current.ItemInfo[ 1 ] )}" );
            Player.Interface.ShowTooltip( stringBuilder.ToString() );
        }

        public void OnPointerExit( PointerEventData eventData ) {
            if( Number == -1 || Id == -1 )
                return;
            Player.Interface.HideTooltip();
        }

    }

}