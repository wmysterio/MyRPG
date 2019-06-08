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
using MyRPG.Sync;

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
            var interfaceSync = playerUI.GetComponent<InterfaceSync>();

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
            windowBackgrounds.Add( interfaceSync.PersonageWindowObject.GetComponent<Image>() );
            windowBorders.Add( interfaceSync.PersonageWindowObject.GetComponent<Outline>() );
            var personageTitle = interfaceSync.PersonageWindowObject.transform.GetChild( 0 );
            windowTitleBackgrounds.Add( personageTitle.GetComponent<Image>() );
            windowTitleTexts.Add( personageTitle.GetChild( 1 ).GetComponent<Text>() );
            var сharacteristicTitle = interfaceSync.PersonageWindowObject.transform.GetChild( 1 );
            var сharacteristicScrollbarVertical = сharacteristicTitle.transform.GetChild( 2 );
            allScrollBarBackgrounds.Add( сharacteristicScrollbarVertical.GetComponent<Image>() );
            allScrollBarColors.Add( сharacteristicScrollbarVertical.GetComponent<Scrollbar>() );
            var StatsTitle = interfaceSync.PersonageWindowObject.transform.GetChild( 2 );
            windowBorders.Add( StatsTitle.GetComponent<Outline>() );
            #endregion

            #region BAG SETTING
            windowBackgrounds.Add( interfaceSync.BagWindowObject.GetComponent<Image>() );
            windowBorders.Add( interfaceSync.BagWindowObject.GetComponent<Outline>() );
            var bagTitle = interfaceSync.BagWindowObject.transform.GetChild( 0 );
            windowTitleBackgrounds.Add( bagTitle.GetComponent<Image>() );
            windowTitleTexts.Add( bagTitle.GetChild( 2 ).GetComponent<Text>() );
            var bagButtonsTransform = interfaceSync.BagWindowObject.transform.GetChild( 1 );
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
            windowBackgrounds.Add( interfaceSync.ConsoleObject.GetComponent<Image>() );
            windowBorders.Add( interfaceSync.ConsoleObject.GetComponent<Outline>() );
            var consoleInputField = interfaceSync.ConsoleObject.GetComponent<InputField>();
            var consoleText = interfaceSync.ConsoleObject.GetComponentsInChildren<Text>( true );
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

            interfaceSync.MessageBoxObject.GetComponent<Image>().color = new Color32( 0, 0, 0, 191 );
            interfaceSync.MessageBoxObject.GetComponent<Outline>().effectColor = new Color32( 255, 255, 255, 191 );
            interfaceSync.MessageBoxObject.GetComponentInChildren<Text>().color = new Color32( 255, 255, 255, 255 );
            interfaceSync.TooltipObject.GetComponent<Image>().color = new Color32( 0, 0, 0, 191 );
            interfaceSync.TooltipObject.GetComponent<Outline>().effectColor = new Color32( 255, 255, 255, 191 );
            var tooltipTexts = interfaceSync.TooltipObject.GetComponentsInChildren<Text>();
            for( int i = 0; i < tooltipTexts.Length; i++ )
                tooltipTexts[ i ].color = new Color32( 255, 255, 255, 255 );

            //interfaceSync.CinematicObject.GetComponentInChildren<Text>().color = Color.white;

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