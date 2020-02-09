/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using MyRPG.Configuration;
using UnityEngine;
using UnityEngine.UI;

namespace MyRPG.Sync {

	public sealed class BagWindowSync : MonoBehaviour {

        public static BagWindowSync Current { get; private set; }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public GameObject TitleTextObject, QuestionButtonObject, CloseButtonObject, ButtonsObject;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private Button[] buttons;
        private Text[] counters;
        private Outline[] borders;
        private BagSlotSync[] slots;
        private bool isNotReady = true;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private void Awake() { Current = this; }
        private void Update() {
            if( isNotReady )
                return;
            if( !Player.Exist() ) {
                gameObject.SetActive( false );
                return;
            }
            if( Player.Current.NoLongerNeeded ) {
                gameObject.SetActive( false );
                return;
            }
            if( !Player.Current.IsActive || Player.Current.IsDead ) {
                gameObject.SetActive( false );
                return;
            }
        }
        private void OnEnable() {
            if( isNotReady ) {
                buttons = ButtonsObject.GetComponentsInChildren<Button>();
                counters = ButtonsObject.GetComponentsInChildren<Text>();
                borders = ButtonsObject.GetComponentsInChildren<Outline>();
                slots = ButtonsObject.GetComponentsInChildren<BagSlotSync>();
                TitleTextObject.GetComponent<Text>().text = Localization.Current.WindowNames[ 1 ].ToUpper();
                CloseButtonObject.GetComponent<Button>().onClick.AddListener( delegate { gameObject.SetActive( false ); } );
                QuestionButtonObject.GetComponent<Button>().onClick.AddListener( delegate {
                    // to do
                } );
                isNotReady = false;
                RefreshItems();
            }

        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public void RefreshItems() {
            int iterator = Player.Current.Loot.Each( ( item, i ) => {
                buttons[ i ].image.sprite = item.SpriteIcon;
                counters[ i ].text = item.Count.ToString();
                if( Player.Current.Equipments.HasItem( item.Id ) ) {
                    borders[ i ].effectDistance = new Vector2( 3f, 3f );
                } else {
                    borders[ i ].effectDistance = new Vector2( 2f, 2f );
                }
                borders[ i ].effectColor = Colors.BaseColor( item.Rarity );
                slots[ i ].Number = i;
                slots[ i ].Id = item.Id;
            } );
            for( ; iterator < Bag.SLOT_COUNT; iterator++ ) {
                buttons[ iterator ].image.sprite = null;
                counters[ iterator ].text = string.Empty;
                borders[ iterator ].effectColor = Colors.BaseColor( Colors.Mode.WINDOW_BORDER_COLOR );
                slots[ iterator ].Number = -1;
                slots[ iterator ].Id = -1;
            }
        }

    }

}