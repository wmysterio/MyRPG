/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://www.unity3d.tk/
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
 
namespace MyRPG {
 
	public static class Audio {

        private static AudioSource backgroundSourse = null;
        private static AudioClip[] backgroundTracks = null;
        private static float backgroundVolume = 1f;


        public static bool IsInit { get; private set; } = false;
        public static BackgroundID CurrentBackground { get; private set; } = BackgroundID.OFF;

        public static void SetBackgroundVolume( float val ) {
            backgroundVolume = Mathf.Clamp( val, 0f, 1f );
            backgroundSourse.volume = backgroundVolume;
        }
        public static void PlayBackground( BackgroundID id, bool loop = false ) {
            if( backgroundSourse.clip != null ) {
                backgroundSourse.Stop();
                backgroundSourse.clip = null;
            }
            if( id == BackgroundID.OFF )
                return;
            CurrentBackground = id;
            backgroundSourse.clip = backgroundTracks[ ( int ) CurrentBackground ];
            if( loop )
                backgroundSourse.loop = loop;
            backgroundSourse.Play();
        }

        public static IEnumerator Init( AudioSource sourse ) {
            if( IsInit )
                yield return null;
            backgroundSourse = sourse;
            backgroundSourse.minDistance = 0f;
            backgroundSourse.maxDistance = 0.01f;
            List<AudioClip> clips = new List<AudioClip>();
            ResourceRequest request = null;
            clips.Add( null );
            var ids = ( BackgroundID[] ) Enum.GetValues( typeof( BackgroundID ) );
            for( var i = 1; i < ids.Length; i++ ) {
                request = Resources.LoadAsync<AudioClip>( string.Format( "Audio/Background/{0}", i ) );
                yield return request;
                clips.Add( ( AudioClip ) request.asset );
            }
            backgroundTracks = clips.ToArray();
            IsInit = true;
        }

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

    }
 
}