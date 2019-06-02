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

        public GameObject TitleTextObject, QuestionButtonObject, CloseButtonObject, ButtonsObject;

        private Button[] buttons;
        private Text[] counters;
        private Outline[] borders;
        private BagSlotSync[] slots;
        private bool isNotReady = true;

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
                TitleTextObject.GetComponent<Text>().text = Localization.Current.WindowNames[ 1 ];
                CloseButtonObject.GetComponent<Button>().onClick.AddListener( delegate { gameObject.SetActive( false ); } );
                QuestionButtonObject.GetComponent<Button>().onClick.AddListener( delegate {
                    // to do
                } );
                isNotReady = false;
            }
            int iterator = Player.Current.Loot.Each( ( item, i ) => {
                buttons[ i ].image.sprite = item.SpriteIcon;
                counters[ i ].text = item.Count.ToString();
                borders[ i ].effectColor = Colors.BaseColor( item.Rarity );
                slots[ i ].Number = i;
                slots[ i ].Id = item.Id;
            } );
            for( ; iterator < Bag.SLOT_COUNT; iterator++ )
                Hide( iterator );
        }

        public void Hide( int slot ) {
            buttons[ slot ].image.sprite = null;
            counters[ slot ].text = string.Empty;
            borders[ slot ].effectColor = Colors.BaseColor( Colors.Mode.WINDOW_BORDER );
            slots[ slot ].Number = -1;
            slots[ slot ].Id = -1;
        }

    }

}