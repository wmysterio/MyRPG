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
            var playerUI = GameObject.Find( "PlayerUI" );
            if( playerUI == null )
                return;
            int TitleFontSize = 28;
            var interfaceSync = playerUI.GetComponent<InterfaceSync>();

            #region BACKGROUND COLOR
            interfaceSync.PersonageWindowObject.GetComponent<Image>().color = Colors.BaseColor( Colors.Mode.WINDOW_BACKGROUND_COLOR );
            interfaceSync.BagWindowObject.GetComponent<Image>().color = Colors.BaseColor( Colors.Mode.WINDOW_BACKGROUND_COLOR );
            interfaceSync.ConsoleObject.GetComponent<Image>().color = Colors.BaseColor( Colors.Mode.CONSOLE_BACKGROUND_COLOR );
            interfaceSync.TooltipObject.GetComponent<Image>().color = Colors.BaseColor( Colors.Mode.TOOLTIP_BACKGROUND_COLOR );
            interfaceSync.MessageBoxObject.GetComponent<Image>().color = Colors.BaseColor( Colors.Mode.MESSAGEBOX_BACKGROUND_COLOR );
            interfaceSync.PersonageWindowObject.transform.GetChild( 2 ).GetComponent<Image>().color = Colors.BaseColor( Colors.Mode.PANEL_BACKGROUND_COLOR );
            #endregion

            #region TITLE BACKGROUND COLOR
            interfaceSync.PersonageWindowObject.transform.GetChild( 0 ).GetComponent<Image>().color = Colors.BaseColor( Colors.Mode.WINDOW_TITLE_BACKGROUND_COLOR );
            interfaceSync.BagWindowObject.transform.GetChild( 0 ).GetComponent<Image>().color = Colors.BaseColor( Colors.Mode.WINDOW_TITLE_BACKGROUND_COLOR );
            #endregion

            #region BORDER COLOR
            var allBorder = playerUI.GetComponentsInChildren<Outline>( true );
            for( int i = 0; i < allBorder.Length; i++ )
                allBorder[ i ].effectColor = Colors.BaseColor( Colors.Mode.WINDOW_BORDER_COLOR );
            #endregion

            #region TEXT COLOR
            var allText = playerUI.GetComponentsInChildren<Text>( true );
            var tooltipText = interfaceSync.TooltipObject.GetComponentsInChildren<Text>( true );
            var messageBoxText = interfaceSync.MessageBoxObject.GetComponentsInChildren<Text>( true );
            var bagText = interfaceSync.BagWindowObject.GetComponentsInChildren<Text>( true );
            var consoleText = interfaceSync.ConsoleObject.GetComponentsInChildren<Text>( true );
            List<Text> titleText = new List<Text>();
            titleText.Add( interfaceSync.PersonageWindowObject.transform.GetChild( 0 ).GetComponentInChildren<Text>( true ) );
            titleText.Add( interfaceSync.BagWindowObject.transform.GetChild( 0 ).GetComponentInChildren<Text>( true ) );
            for( int i = 0; i < allText.Length; i++ )
                allText[ i ].color = Colors.BaseColor( Colors.Mode.WINDOW_TEXT_COLOR );
            for( int i = 0; i < tooltipText.Length; i++ )
                tooltipText[ i ].color = Colors.BaseColor( Colors.Mode.TOOLTIP_TEXT_COLOR );
            for( int i = 0; i < messageBoxText.Length; i++ )
                messageBoxText[ i ].color = Colors.BaseColor( Colors.Mode.MESSAGEBOX_TEXT_COLOR );
            for( int i = 0; i < bagText.Length; i++ )
                bagText[ i ].color = Colors.BaseColor( Colors.Mode.BAG_ITEM_AMOUNT_BACKGROUND_COLOR );
            for( int i = 0; i < titleText.Count; i++ ) {
                titleText[ i ].color = Colors.BaseColor( Colors.Mode.WINDOW_TITLE_TEXT_COLOR );
                titleText[ i ].alignByGeometry = true;
                titleText[ i ].resizeTextForBestFit = false;
                titleText[ i ].fontSize = TitleFontSize;
                titleText[ i ].horizontalOverflow = HorizontalWrapMode.Overflow;
            }
            for( int i = 0; i < consoleText.Length; i++ ) {
                consoleText[ i ].fontSize = TitleFontSize;
                consoleText[ i ].alignByGeometry = false;
                consoleText[ i ].color = Colors.BaseColor( Colors.Mode.CONSOLE_TEXT_COLOR );
            }
            var consoleInputField = interfaceSync.ConsoleObject.GetComponentInChildren<InputField>();
            consoleInputField.selectionColor = Colors.BaseColor( Colors.Mode.CONSOLE_BORDER_COLOR );
            consoleInputField.caretColor = Colors.BaseColor( Colors.Mode.CONSOLE_BORDER_COLOR );
            // interfaceSync.CinematicObject.GetComponentInChildren<Text>().color = Color.white;
            #endregion

            #region BAG ITEM
            var bagRootButtons = interfaceSync.BagWindowObject.transform.GetChild( 1 );
            var bagSlotBorders = bagRootButtons.GetComponentsInChildren<Outline>( true );
            var bagSlotButtons = bagRootButtons.GetComponentsInChildren<Button>( true );
            var bagAmountTexts = bagRootButtons.GetComponentsInChildren<Text>( true );
            for( int i = 0; i < bagSlotBorders.Length; i++ )
                bagSlotBorders[ i ].effectColor = Colors.BaseColor( Colors.Mode.BAG_ITEM_BORDER_COLOR );
            for( int i = 0; i < bagAmountTexts.Length; i++ )
                bagAmountTexts[ i ].color = Colors.BaseColor( Colors.Mode.WINDOW_TEXT_COLOR );
            for( int i = 0; i < bagSlotButtons.Length; i++ ) {
                bagSlotButtons[ i ].image.sprite = null;
                bagSlotButtons[ i ].image.color = Color.white;
            }
            for( int i = 0; i < bagRootButtons.childCount; i++ ) {
                var slotTransform = bagRootButtons.GetChild( i );
                slotTransform.GetComponent<Image>().color = Color.white;
                slotTransform.GetChild( 1 ).GetComponent<Image>().color = Colors.BaseColor( Colors.Mode.BAG_ITEM_AMOUNT_BACKGROUND_COLOR );
            }
            #endregion

            #region SCROLLBARS
            var allScrollBars = playerUI.GetComponentsInChildren<Scrollbar>( true );
            for( int i = 0; i < allScrollBars.Length; i++ ) {
                allScrollBars[ i ].transform.parent.GetComponent<Image>().color = Colors.BaseColor( Colors.Mode.WINDOW_BACKGROUND_COLOR );
                allScrollBars[ i ].GetComponent<Image>().color = Colors.BaseColor( Colors.Mode.SCROLLBAR_BACKGROUND_COLOR );
                ColorBlock block = allScrollBars[ i ].colors;
                block.normalColor = Colors.BaseColor( Colors.Mode.SCROLLBAR_COLOR );
                block.highlightedColor = Colors.BaseColor( Colors.Mode.SCROLLBAR_COLOR );
                block.pressedColor = Colors.BaseColor( Colors.Mode.SCROLLBAR_COLOR );
                block.selectedColor = Colors.BaseColor( Colors.Mode.SCROLLBAR_COLOR );
                block.disabledColor = Colors.BaseColor( Colors.Mode.SCROLLBAR_COLOR );
                allScrollBars[ i ].colors = block;
            }
            #endregion

            #region SLIDERS
            var sliders = playerUI.GetComponentsInChildren<Slider>( true );
            for( int i = 0; i < sliders.Length; i++ ) {
                sliders[ i ].value = sliders[ i ].minValue;
                var sliderImage = sliders[ i ].transform.GetChild( 0 ).GetComponent<Image>();
                var block = sliders[ i ].colors;
                var sliderFillImage = sliders[ i ].transform.GetChild( 1 ).GetChild( 0 ).GetComponent<Image>();
                var sliderRectTransform = sliders[ i ].transform.GetChild( 0 ).GetComponent<RectTransform>();
                var sliderComponentTransform = sliders[ i ].transform.GetChild( 2 ).GetChild( 0 );
                var sliderHandleImage = sliderComponentTransform.GetComponent<Image>();
                sliderRectTransform.anchorMin.Set( 0f, 0f );
                sliderRectTransform.anchorMax.Set( 1f, 1f );
                sliderRectTransform.offsetMin = new Vector2( 0f, 0f );
                sliderRectTransform.offsetMax = new Vector2( 0f, 0f );
                sliderRectTransform.pivot = new Vector2( 0.5f, 0.5f );
                sliderHandleImage.sprite = null;
                sliderFillImage.sprite = null;
                sliderImage.sprite = null;
                sliderImage.color = Colors.BaseColor( Colors.Mode.SLIDER_BACKGROUND_COLOR );
                sliderHandleImage.color = Colors.BaseColor( Colors.Mode.SLIDER_COLOR );
                sliderFillImage.color = Colors.BaseColor( Colors.Mode.SLIDER_COLOR );
                block.normalColor = Colors.BaseColor( Colors.Mode.SLIDER_COLOR );
                block.highlightedColor = Colors.BaseColor( Colors.Mode.SLIDER_COLOR );
                block.pressedColor = Colors.BaseColor( Colors.Mode.SLIDER_COLOR );
                block.selectedColor = Colors.BaseColor( Colors.Mode.SLIDER_COLOR );
                block.disabledColor = Colors.BaseColor( Colors.Mode.SLIDER_COLOR );
                sliders[ i ].colors = block;
            }
            #endregion

            Debug.Log( "Кольори змінено!" );
        }



    }

}
#endif