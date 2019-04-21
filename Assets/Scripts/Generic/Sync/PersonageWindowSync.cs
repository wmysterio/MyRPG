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
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
namespace MyRPG {
 
	public sealed class PersonageWindowSync : MonoBehaviour {

        private static GameObject instance = null;
        public static GameObject GetWindowGameObject() { return instance; }

        public GameObject CharacteristicKeysObject, CharacteristicValuesObject, WindowTitleObject, StatsObject, CloseButtonObject;

        private Text characteristicValuesText, statPlayerName, statPlayerMoney, statCalendarDate;
        private Slider expSlider, levelSlider;
        private bool isNotReady = true;

        private void Update() {
            if( isNotReady )
                return;
            characteristicValuesText.text = Player.Current.CurrentCharacteristic.GetValues();
            statCalendarDate.text = Calendar.GetCalendarInfo();
            if( Personage.MAX_LEVEL == Player.Current.Level ) {
                levelSlider.minValue = -2f;
                levelSlider.maxValue = -1f;
                levelSlider.value = -1f;
                expSlider.minValue = -2f;
                expSlider.maxValue = -1f;
                expSlider.value = -1f;
            } else {
                levelSlider.minValue = 0f;
                levelSlider.maxValue = Personage.MAX_LEVEL;
                levelSlider.value = Player.Current.Level;
                expSlider.minValue = 0f;
                expSlider.maxValue = Player.Current.TotalExperience;
                expSlider.value = Player.Current.CurrentExperience;
            }
        }

        private void OnEnable() {
            if( isNotReady ) {
                instance = gameObject;
                characteristicValuesText = CharacteristicValuesObject.GetComponent<Text>();
                WindowTitleObject.GetComponent<Text>().text = Localization.Current.WindowNames[ 3 ].ToUpper();
                CharacteristicKeysObject.GetComponent<Text>().text = Player.Current.CurrentCharacteristic.GetNames();
                var statsTexts = StatsObject.GetComponentsInChildren<Text>();
                var sliders = StatsObject.GetComponentsInChildren<Slider>();
                expSlider = sliders[ 0 ];
                levelSlider = sliders[ 1 ];
                for( int i = 0; i < 5; i++ )
                    statsTexts[ i ].text = Localization.Current.StatNames[ i ];
                statPlayerName = statsTexts[ 5 ];
                statCalendarDate = statsTexts[ 6 ];
                statPlayerMoney = statsTexts[ 7 ];
                CloseButtonObject.GetComponent<Button>().onClick.AddListener( delegate {
                    gameObject.SetActive( false );
                } );
                isNotReady = false;
            }
            statPlayerName.text = Player.Current.Name;
            statPlayerMoney.text = Player.Current.Money.ToString();
        }

    }
 
}