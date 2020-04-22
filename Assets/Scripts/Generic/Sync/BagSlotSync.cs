/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/
using UnityEngine;
using UnityEngine.EventSystems;
using MyRPG.Items;

namespace MyRPG.Sync {

    public sealed class BagSlotSync : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

        [HideInInspector] public int Number = -1;
        [HideInInspector] public int Id = -1;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public void OnPointerClick( PointerEventData eventData ) {
            if( Number == -1 || Id == -1 )
                return;
            switch( eventData.button ) {

                case PointerEventData.InputButton.Left:
                Debug.Log( "move" );
                break;

                case PointerEventData.InputButton.Middle:
                if( Player.Current.Loot[ Id ].Class == ClassOfItem.Quest )
                    return;
                if( !Player.Current.Equipments.HasItem( Id ) ) {
                    Player.Current.Loot.Remove( Id, true );
                    Player.Interface.HideTooltip();
                    BagWindowSync.Current.RefreshItems();
                    return;
                }
                if( 2 > Player.Current.Loot[ Id ].Count )
                    return;
                Player.Current.Loot.Remove( Id, Player.Current.Loot[ Id ].Count - 1, true );
                Player.Interface.HideTooltip();
                BagWindowSync.Current.RefreshItems();
                break;

                case PointerEventData.InputButton.Right:
                if( Player.Current.Loot[ Id ].Class == ClassOfItem.Equipment ) {
                    Player.Current.Equipments.Set( Player.Current.Loot[ Id ], true );
                } else { Player.Current.Loot[ Id ].Use(); }
                Player.Interface.HideTooltip();
                BagWindowSync.Current.RefreshItems();
                break;

            }
        }
        public void OnPointerEnter( PointerEventData eventData ) {
            if( Number == -1 || Id == -1 )
                return;
            var item = Player.Current.Loot[ Id ];
            Player.Interface.ShowTooltip( item.GetTooltipText() );
        }
        public void OnPointerExit( PointerEventData eventData ) {
            if( Number == -1 || Id == -1 )
                return;
            Player.Interface.HideTooltip();
        }

    }

}