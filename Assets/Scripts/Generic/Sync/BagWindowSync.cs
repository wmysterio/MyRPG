/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using MyRPG.Configuration;
using UnityEngine;
using UnityEngine.UI;

namespace MyRPG {

	public sealed class BagWindowSync : MonoBehaviour {

        private static GameObject instance = null;
        public static GameObject GetWindowGameObject() { return instance; }

        public GameObject TitleTextObject, QuestionButtonObject, CloseButtonObject, ButtonsObject;

        private Button[] buttons;
        private Text[] counters;
        private Outline[] borders;
        private bool isNotReady = true;


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
                instance = gameObject;
                buttons = ButtonsObject.GetComponentsInChildren<Button>();
                counters = ButtonsObject.GetComponentsInChildren<Text>();
                borders = ButtonsObject.GetComponentsInChildren<Outline>();
                TitleTextObject.GetComponent<Text>().text = Localization.Current.WindowNames[ 1 ];
                CloseButtonObject.GetComponent<Button>().onClick.AddListener( delegate {
                    gameObject.SetActive( false );
                } );
                QuestionButtonObject.GetComponent<Button>().onClick.AddListener( delegate {
                    // to do
                } );
                isNotReady = false;
            }
            int iterator = 0;
            Player.Current.Loot.Each( ( item, i ) => {
                buttons[ i ].image.sprite = item.SpriteIcon;
                counters[ i ].text = item.Count.ToString();
                borders[ i ].effectColor = Colors.BaseColor( item.Rarity );
                iterator = i;
            } );
            iterator += 1;
            for( ; iterator < Bag.SLOT_COUNT; iterator++ ) {
                buttons[ iterator ].image.sprite = null;
                counters[ iterator ].text = string.Empty;
                borders[ iterator ].effectColor = Colors.BaseColor( Colors.Mode.WINDOW_BORDER );
            }
        }

    }

}