/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/
using UnityEngine;

namespace MyRPG.Sync {

    public static class Sky {

        private static SkyMode mode = SkyMode.Off;
        private static GameObject skyObject = null;
        private static GameObject sunObject = null;
        private static GameObject moonObject = null;
        private static GameObject heavenlyBody = null;
        private static int lastMinute = 0;
        private static int curMinute = 0;
        private static Light sunLight = null;
        private static Light moonLight = null;
        private static Light heavenlyLight = null;
        private static Vector3 rotation = Vector3.zero;
        private static bool isEnable = true;
        private static UnityEngine.Camera cameraScript;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public static bool IsInit { get; private set; }
        public static bool Enable {
            get { return isEnable; }
            private set {
                isEnable = value;
                if( !isEnable ) {
                    Mode = SkyMode.Off;
                    return;
                }
            }
        }
        public static SkyMode Mode {
            get { return mode; }
            private set {
                mode = value;
                switch( mode ) {
                    case SkyMode.Day:
                    heavenlyBody = sunObject;
                    heavenlyLight = sunLight;
                    moonObject.SetActive( false );
                    sunObject.SetActive( true );
                    break;
                    case SkyMode.Night:
                    heavenlyBody = moonObject;
                    heavenlyLight = moonLight;
                    sunObject.SetActive( false );
                    moonObject.SetActive( true );
                    break;
                    default:
                    sunObject.SetActive( false );
                    moonObject.SetActive( false );
                    heavenlyBody = null;
                    heavenlyLight = null;
                    break;
                }
            }
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public static void Init( UnityEngine.Camera cameraScript ) {
            if( IsInit || cameraScript == null )
                return;

            Sky.cameraScript = cameraScript;
            skyObject = new GameObject( "Sky" );

            sunObject = new GameObject( "Sun" );
            sunObject.transform.parent = skyObject.transform;
            sunObject.transform.localPosition = new Vector3( 0f, 5000f, 0f );
            sunObject.transform.localEulerAngles = new Vector3( 0f, 180f, 0f );

            moonObject = new GameObject( "Moon" );
            moonObject.transform.parent = skyObject.transform;
            moonObject.transform.localPosition = new Vector3( 0f, -5000f, 0f );
            moonObject.transform.localEulerAngles = new Vector3( 0f, 0f, 0f );

            rotation.y = 90f;
            GameObject.DontDestroyOnLoad( skyObject );

            sunLight = sunObject.AddComponent<Light>();
            sunLight.type = LightType.Directional;
            sunLight.shadows = LightShadows.Soft;

            moonLight = moonObject.AddComponent<Light>();
            moonLight.type = LightType.Directional;
            moonLight.shadows = LightShadows.Soft;

            Mode = SkyMode.Night;
            IsInit = true;
        }
        public static void Update() {
            if( !IsInit || !isEnable )
                return;
            curMinute = Calendar.Minute;
            if( curMinute == lastMinute )
                return;
            lastMinute = curMinute;
            rotation.x = ( Calendar.Hour * -15f + curMinute * -0.25f ) + 90f;
            skyObject.transform.localEulerAngles = rotation;
            if( mode != SkyMode.Day && Calendar.Hour > 5 && 18 > Calendar.Hour ) {
                Mode = SkyMode.Day;
            } else if( mode != SkyMode.Night && ( 6 > Calendar.Hour || Calendar.Hour > 17 ) ) {
                Mode = SkyMode.Night;
            }
        }

    }

    public enum SkyMode : byte {
        Off = 0,
        Day = 1,
        Night = 2
    }

}