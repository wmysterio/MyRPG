/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace MyRPG {
 
	public static class Audio {

        private static AudioSource backgroundSourse = null;
        private static AudioSource tuneSourse = null;

        private static AudioClip[] backgroundTracks = null;
        private static AudioClip[] tuneTracks = null;

        private static float backgroundVolume = 1f;
        private static float tuneVolume = 1f;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public static bool IsInit { get; private set; } = false;

        public static BackgroundID CurrentBackground { get; private set; } = BackgroundID.OFF;
        public static TuneID CurrentTune { get; private set; } = TuneID.OFF;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public static void SetBackgroundVolume( float val ) {
            backgroundVolume = Mathf.Clamp( val, 0f, 1f );
            backgroundSourse.volume = backgroundVolume;
        }
        public static void SetTuneVolume( float val ) {
            tuneVolume = Mathf.Clamp( val, 0f, 1f );
            tuneSourse.volume = tuneVolume;
        }

        public static void PlayBackground( BackgroundID id, bool loop = false ) {
            if( backgroundSourse.clip != null ) {
                backgroundSourse.Stop();
                backgroundSourse.clip = null;
            }
            if( id == BackgroundID.OFF ) {
                CurrentBackground = id;
                if( backgroundSourse.clip != null ) {
                    backgroundSourse.Stop();
                    backgroundSourse.clip = null;
                }
                return;
            }
            CurrentBackground = id;
            backgroundSourse.clip = backgroundTracks[ ( int ) CurrentBackground ];
            if( loop )
                backgroundSourse.loop = loop;
            backgroundSourse.Play();
        }
        public static void PlayTune( TuneID id ) {
            if( tuneSourse.clip != null ) {
                tuneSourse.Stop();
                tuneSourse.clip = null;
            }
            if( id == TuneID.OFF ) {
                CurrentTune = id;
                if( tuneSourse.clip != null ) {
                    tuneSourse.Stop();
                    tuneSourse.clip = null;
                }
                return;
            }
            CurrentTune = id;
            tuneSourse.clip = tuneTracks[ ( int ) CurrentTune ];
            tuneSourse.Play();
        }

        public static IEnumerator Init() {
            if( IsInit )
                yield return null;
            List<AudioClip> clips = new List<AudioClip>();
            List<AudioClip> clipsTune = new List<AudioClip>();
            ResourceRequest request = null;
            clips.Add( null );
            clipsTune.Add( null );
            var ids = ( BackgroundID[] ) Enum.GetValues( typeof( BackgroundID ) );
            var idTunes = ( TuneID[] ) Enum.GetValues( typeof( TuneID ) );
            for( var i = 1; i < idTunes.Length; i++ ) {
                request = Resources.LoadAsync<AudioClip>( string.Format( "Audio/Tunes/{0}", i ) );
                yield return request;
                clipsTune.Add( ( AudioClip ) request.asset );
            }
            //for( var i = 1; i < ids.Length; i++ ) {
            //    request = Resources.LoadAsync<AudioClip>( string.Format( "Audio/Background/{0}", i ) );
            //    yield return request;
            //    clips.Add( ( AudioClip ) request.asset );
            //}
            backgroundTracks = clips.ToArray();
            tuneTracks = clipsTune.ToArray();
            IsInit = true;
        }
        
        public static void SyncCamara() {
            if( !IsInit )
                return;
            if( backgroundSourse != null )
                return;
            if( tuneSourse != null )
                return;
            backgroundSourse = Camera.GetAudioBackgroundSource();
            backgroundSourse.minDistance = 0f;
            backgroundSourse.maxDistance = 0.01f;
            tuneSourse = Camera.GetAudioTuneSource();
            tuneSourse.minDistance = 0f;
            tuneSourse.maxDistance = 0.01f;
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public enum BackgroundID : int {
            OFF = 0,
            BEFORE_DAWN = 1,
            DRIFTING_2 = 2,
            EPIC_TV_THEME = 3,
            HERO_IN_PERIL = 4,
            HIGH_TENSION = 5,
            HORROR_EPIC = 6,
            MODERN_COMBAT = 7,
            MOVIE_THEME_1 = 8,
            SITTING_BULL = 9,
            SPORTS_ACTION = 10,
            THE_DEADLY_YEAR = 11,
            THE_GREAT_UNKNOWN = 12,
            THE_TALK = 13,
            THE_VOYAGE = 14
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public enum TuneID {
            OFF = 0,
            CONSOLE_OPEN = 1,
            CONSOLE_SUCCESS = 2,
            CONSOLE_ERROR = 3,
            MESSAGE_BOX = 4
        }

    }
 
}