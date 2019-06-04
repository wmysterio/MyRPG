/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using MyRPG.Configuration;

#if UNITY_EDITOR
namespace MyRPG.Tools {

    public static class Styling {

        [MenuItem( "MyRPG/Styling/Switch colors" )]
        public static void SwitchColors() {

            int TitleFontSize = 28;

            Color32 windowBackgroundColor = Colors.BaseColor( Colors.Mode.WINDOW_BACKGROUND_COLOR );
            Color32 windowBorderColor = Colors.BaseColor( Colors.Mode.WINDOW_BORDER_COLOR );
            Color32 windowTextColor = Colors.BaseColor( Colors.Mode.WINDOW_TEXT_COLOR );
            Color32 windowTitleTextColor = Colors.BaseColor( Colors.Mode.WINDOW_TITLE_TEXT_COLOR );
            Color32 windowTitleBackgroundColor = windowBorderColor;
            Color32 scrollBarCaretColor = windowTitleTextColor;
            Color32 bagSlotBorderColor = windowTitleTextColor;
            Color32 bagSlotBackgroundColor = windowTitleTextColor;
            Color32 scrollBarBackgroundColor = windowBackgroundColor;
            Color32 bagSlotTextColor = windowTextColor;
            Color32 panelBackgroundColor = windowTitleTextColor;

            var playerUI = GameObject.Find( "PlayerUI" );
            if( playerUI == null )
                return;
            var rootObject = playerUI.transform.GetChild( 0 );
            if( rootObject == null )
                return;
            var peronageWindow = rootObject.transform.GetChild( 0 );
            if( peronageWindow == null )
                return;
            var bagWindow = rootObject.transform.GetChild( 1 );
            if( bagWindow == null )
                return;
            var tooltipBox = rootObject.transform.GetChild( 2 );
            if( tooltipBox == null )
                return;
            var messageBox = rootObject.transform.GetChild( 3 );
            if( messageBox == null )
                return;
            var consoleWindow = rootObject.transform.GetChild( 4 );
            if( consoleWindow == null )
                return;
            var cinematicView = rootObject.transform.GetChild( 5 );
            if( cinematicView == null )
                return;

            List<Image> windowBackgrounds = new List<Image>();
            List<Outline> windowBorders = new List<Outline>();
            List<Image> windowTitleBackgrounds = new List<Image>();
            List<Text> windowTitleTexts = new List<Text>();

            List<Button> bagButtons = new List<Button>();
            List<Text> bagTexts = new List<Text>();
            List<Outline> bagBorders = new List<Outline>();
            List<Image> bagTextImages = new List<Image>();

            List<Text> allTexts = new List<Text>();
            List<Image> allScrollBarBackgrounds = new List<Image>();
            List<Scrollbar> allScrollBarColors = new List<Scrollbar>();
            List<Slider> allSliders = new List<Slider>();

            var uiSliders = playerUI.GetComponentsInChildren<Slider>( true );
            for( int i = 0; i < uiSliders.Length; i++ )
                allSliders.Add( uiSliders[ i ] );

            var uiText = playerUI.GetComponentsInChildren<Text>( true );
            for( int i = 0; i < uiText.Length; i++ )
                allTexts.Add( uiText[ i ] );

            #region PERSONAGE WINDOW SETTING
            windowBackgrounds.Add( peronageWindow.GetComponent<Image>() );
            windowBorders.Add( peronageWindow.GetComponent<Outline>() );
            var personageTitle = peronageWindow.transform.GetChild( 0 );
            windowTitleBackgrounds.Add( personageTitle.GetComponent<Image>() );
            windowTitleTexts.Add( personageTitle.GetChild( 1 ).GetComponent<Text>() );
            var сharacteristicTitle = peronageWindow.transform.GetChild( 1 );
            var сharacteristicScrollbarVertical = сharacteristicTitle.transform.GetChild( 2 );
            allScrollBarBackgrounds.Add( сharacteristicScrollbarVertical.GetComponent<Image>() );
            allScrollBarColors.Add( сharacteristicScrollbarVertical.GetComponent<Scrollbar>() );
            var StatsTitle = peronageWindow.transform.GetChild( 2 );
            windowBorders.Add( StatsTitle.GetComponent<Outline>() );
            #endregion

            #region BAG SETTING
            windowBackgrounds.Add( bagWindow.GetComponent<Image>() );
            windowBorders.Add( bagWindow.GetComponent<Outline>() );
            var bagTitle = bagWindow.transform.GetChild( 0 );
            windowTitleBackgrounds.Add( bagTitle.GetComponent<Image>() );
            windowTitleTexts.Add( bagTitle.GetChild( 2 ).GetComponent<Text>() );
            var bagButtonsTransform = bagWindow.transform.GetChild( 1 );
            var bagButtonComponents = bagButtonsTransform.GetComponentsInChildren<Button>( true );
            var bagTextComponents = bagButtonsTransform.GetComponentsInChildren<Text>( true );
            var bagBorderComponents = bagButtonsTransform.GetComponentsInChildren<Outline>( true );
            for( int i = 0; i < bagButtonComponents.Length; i++ )
                bagButtons.Add( bagButtonComponents[ i ] );
            for( int i = 0; i < bagBorderComponents.Length; i++ )
                bagBorders.Add( bagBorderComponents[ i ] );
            for( int i = 0; i < bagTextComponents.Length; i++ ) {
                bagTexts.Add( bagTextComponents[ i ] );
                bagTextImages.Add( bagTextComponents[ i ].transform.parent.GetComponent<Image>() );
            }
            #endregion

            #region CONSOLE SETTING
            windowBackgrounds.Add( consoleWindow.GetComponent<Image>() );
            windowBorders.Add( consoleWindow.GetComponent<Outline>() );
            var consoleInputField = consoleWindow.GetComponent<InputField>();
            var consoleText = consoleWindow.GetComponentsInChildren<Text>( true );
            #endregion

            // ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- -----

            #region SETUP STYLES
            сharacteristicTitle.GetComponent<Image>().color = windowBackgroundColor;
            StatsTitle.GetComponent<Image>().color = panelBackgroundColor;
            consoleInputField.selectionColor = windowBorderColor;
            consoleInputField.caretColor = windowBorderColor;

            for( int i = 0; i < allTexts.Count; i++ ) {
                allTexts[ i ].color = windowTextColor;
                allTexts[ i ].text = string.Empty;
            }

            for( int i = 0; i < windowBackgrounds.Count; i++ )
                windowBackgrounds[ i ].color = windowBackgroundColor;

            for( int i = 0; i < windowBorders.Count; i++ )
                windowBorders[ i ].effectColor = windowBorderColor;

            for( int i = 0; i < windowTitleBackgrounds.Count; i++ )
                windowTitleBackgrounds[ i ].color = windowTitleBackgroundColor;

            for( int i = 0; i < windowTitleTexts.Count; i++ ) {
                windowTitleTexts[ i ].color = windowTitleTextColor;
                windowTitleTexts[ i ].alignByGeometry = true;
                windowTitleTexts[ i ].resizeTextForBestFit = false;
                windowTitleTexts[ i ].fontSize = TitleFontSize;
                windowTitleTexts[ i ].horizontalOverflow = HorizontalWrapMode.Overflow;
            }

            for( int i = 0; i < consoleText.Length; i++ ) {
                consoleText[ i ].fontSize = TitleFontSize;
                consoleText[ i ].alignByGeometry = false;
                consoleText[ i ].color = windowTextColor;
            }

            messageBox.GetComponent<Image>().color = new Color32( 0, 0, 0, 191 );
            messageBox.GetComponent<Outline>().effectColor = new Color32( 255, 255, 255, 191 );
            messageBox.GetComponentInChildren<Text>().color = new Color32( 255, 255, 255, 255 );
            tooltipBox.GetComponent<Image>().color = new Color32( 0, 0, 0, 191 );
            tooltipBox.GetComponent<Outline>().effectColor = new Color32( 255, 255, 255, 191 );
            tooltipBox.GetComponentInChildren<Text>().color = new Color32( 255, 255, 255, 255 );
            cinematicView.GetComponentInChildren<Text>().color = Color.white;

            for( int i = 0; i < allScrollBarBackgrounds.Count; i++ )
                allScrollBarBackgrounds[ i ].color = scrollBarBackgroundColor;

            for( int i = 0; i < allScrollBarColors.Count; i++ ) {
                ColorBlock block = allScrollBarColors[ i ].colors;
                block.normalColor = scrollBarCaretColor;
                block.highlightedColor = scrollBarCaretColor;
                block.pressedColor = scrollBarCaretColor;
                block.selectedColor = scrollBarCaretColor;
                block.disabledColor = scrollBarCaretColor;
                allScrollBarColors[ i ].colors = block;
            }

            for( int i = 0; i < allSliders.Count; i++ ) {
                allSliders[ i ].value = allSliders[ i ].minValue;
                var sliderImage = allSliders[ i ].transform.GetChild( 0 ).GetComponent<Image>();
                var block = allSliders[ i ].colors;
                var sliderFillImage = allSliders[ i ].transform.GetChild( 1 ).GetChild( 0 ).GetComponent<Image>();
                var sliderRectTransform = allSliders[ i ].transform.GetChild( 0 ).GetComponent<RectTransform>();
                sliderRectTransform.anchorMin.Set( 0f, 0f );
                sliderRectTransform.anchorMax.Set( 1f, 1f );
                sliderRectTransform.offsetMin = new Vector2( 0f, 0f );
                sliderRectTransform.offsetMax = new Vector2( 0f, 0f );
                sliderRectTransform.pivot = new Vector2( 0.5f, 0.5f );
                var sliderComponentTransform = allSliders[ i ].transform.GetChild( 2 ).GetChild( 0 );
                var sliderHandleImage = sliderComponentTransform.GetComponent<Image>();
                sliderHandleImage.sprite = null;
                sliderFillImage.sprite = null;
                sliderImage.sprite = null;
                sliderImage.color = Color.white;
                sliderHandleImage.color = windowBorderColor;
                sliderFillImage.color = windowBorderColor;
                block.normalColor = windowBorderColor;
                block.highlightedColor = windowBorderColor;
                block.pressedColor = windowBorderColor;
                block.selectedColor = windowBorderColor;
                block.disabledColor = windowBorderColor;
                allSliders[ i ].colors = block;
            }

            for( int i = 0; i < bagButtons.Count; i++ ) {
                bagButtons[ i ].image.sprite = null;
                bagButtons[ i ].image.color = Color.white;
            }

            for( int i = 0; i < bagTexts.Count; i++ )
                bagTexts[ i ].color = bagSlotTextColor;

            for( int i = 0; i < bagBorders.Count; i++ )
                bagBorders[ i ].effectColor = bagSlotBorderColor;

            for( int i = 0; i < bagTextImages.Count; i++ )
                bagTextImages[ i ].color = bagSlotBackgroundColor;

            #endregion

            Debug.Log( "Кольори змінено!" );
        }



    }

}
#endif