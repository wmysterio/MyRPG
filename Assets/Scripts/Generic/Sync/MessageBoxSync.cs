/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MyRPG.Sync {

	public sealed class MessageBoxSync : MonoBehaviour {

        private Coroutine coroutine = null;
        private Text text = null;
        private string content = string.Empty;
        private int duration = -1;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public void Setup( string content, int duration ) {
            this.content = content;
            if( 1 > duration )
                duration = 1;
            this.duration = duration;
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private void OnEnable() {
            if( text == null )
                text = gameObject.GetComponentInChildren<Text>( true );
            text.text = content;
            Audio.PlayTune( Audio.TuneID.MESSAGE_BOX );
            coroutine = StartCoroutine( wait() );
        }
        private void OnDisable() {
            if( coroutine != null ) {
                StopCoroutine( coroutine );
                coroutine = null;
            }
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private IEnumerator wait() {
            while( duration > 0 ) {
                duration -= 1;
                yield return new WaitForSeconds( 1f );
            }
            gameObject.SetActive( false );
        }

    }

}