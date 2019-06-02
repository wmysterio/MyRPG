/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyRPG.Sync {

    public sealed class BagSlotSync : MonoBehaviour, IPointerClickHandler {

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
                BagWindowSync.Current.Hide( Number );
                break;
                case PointerEventData.InputButton.Right:
                Debug.Log( "use" );
                break;
            }
        }

    }

}