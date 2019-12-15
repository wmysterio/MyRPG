/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System.Collections;
using UnityEngine;
using MyRPG.Sync;

namespace MyRPG {

    public static class Camera {
        
        private const float MIN_DISTANCE = -16f;
        private const float MAX_DISTANCE = -3f;
        private const float MIN_ANGLE = 8f;
        private const float MAX_ANGLE = 30f;

        private static GameObject cameraObject = null, cameraScrollObject = null;
        private static UnityEngine.Camera cameraComponent = null;
        private static AudioListener listener = null;
        private static Vector3 scrollPosition, eulerAngles;
        private static Coroutine transversePositionCoroutine, transversePointCoroutine;
        private static bool isMovingPosition = false, isMovingPoint = false;
        private static float scrollSpeed = 0f;
        private static AudioSource audioBackgroundSource = null;
        private static AudioSource audioTuneSource = null;

        public static bool IsInit { get; private set; }
        public static bool IsAttached { get; private set; }
        public static float DistanceToPoint { get; private set; }

        public static Vector3 Position { get { return IsAttached ? cameraScrollObject.transform.TransformPoint( cameraScrollObject.transform.localPosition ) : cameraObject.transform.position; } }

        public static void Init() {
            if( IsInit )
                return;
            scrollPosition = new Vector3( 0f, 0f, ( MIN_DISTANCE + MAX_DISTANCE ) / 2f );
            cameraObject = new GameObject( "Camera" );
            cameraScrollObject = new GameObject( "Camera Scroll" );
            cameraScrollObject.transform.parent = cameraObject.transform;
            cameraScrollObject.tag = "MainCamera";
            cameraComponent = cameraScrollObject.AddComponent<UnityEngine.Camera>();
            cameraComponent.nearClipPlane = 0.01f;
            listener = cameraScrollObject.AddComponent<AudioListener>();
            audioBackgroundSource = cameraScrollObject.AddComponent<AudioSource>();
            audioTuneSource = cameraScrollObject.AddComponent<AudioSource>();
            GameObject.DontDestroyOnLoad( cameraObject );
            eulerAngles.x = ( MIN_ANGLE + MAX_ANGLE ) / 2f;
            DistanceToPoint = scrollPosition.z;
            IsAttached = false;
            IsInit = true;
            Sky.Init( cameraComponent );
        }

        public static void Update() {
            Sky.Update();
            if( !IsInit || IsMoving() )
                return;
            DistanceToPoint = scrollPosition.z;
            if( Player.Exist() && IsAttached ) {
                if( Player.Current.NoLongerNeeded || Player.Current.IsDead || !Player.Current.CanMove )
                    return;

                if( InputManager.GetMouse( MouseKeyName.Right ) ) {

                    Player.Current.Turn( Input.GetAxis( "Mouse X" ) * Player.TURN_SPEED );

                    Player.Current.AnimationGroup.TurnLeftRight = InputManager.GetKeyAxisRaw( KeyName.TURN_LEFT, KeyName.TURN_RIGHT );

                    //if( InputManager.GetKey( KeyName.TURN_LEFT ) ) {
                    //    Player.Current.MoveLeft();
                    //} else if( InputManager.GetKey( KeyName.TURN_RIGHT ) ) {
                    //    Player.Current.MoveRight();
                    //}

                    eulerAngles.x = Mathf.Clamp( eulerAngles.x - Player.TURN_SPEED * Input.GetAxis( "Mouse Y" ) * Time.deltaTime, MIN_ANGLE, MAX_ANGLE );

                }

                scrollSpeed = Input.GetAxis( "Mouse ScrollWheel" );
                if( scrollSpeed != 0f ) {
                    scrollPosition.z = Mathf.Clamp( scrollPosition.z + 2f * scrollSpeed, MIN_DISTANCE, MAX_DISTANCE );
                }

                DistanceToPoint = -Player.Current.RayCastDistance( Position, -MIN_DISTANCE );
                if( DistanceToPoint > MIN_DISTANCE ) {
                    scrollPosition.z = DistanceToPoint;
                    if( scrollPosition.z > MAX_DISTANCE )
                        eulerAngles.x = 24f;

                }

                cameraObject.transform.localEulerAngles = eulerAngles;
                cameraScrollObject.transform.localPosition = scrollPosition;
            }

        }

        public static AudioSource GetAudioBackgroundSource() { return audioBackgroundSource; }
        public static AudioSource GetAudioTuneSource() { return audioTuneSource; }

        public static void ResetRotation() { cameraObject.transform.rotation = Quaternion.identity; }
        public static bool IsMoving() { return isMovingPoint || isMovingPosition; }
        public static void AttachToPlayer() {
            if( !Player.Exist() )
                return;
            cameraObject.transform.parent = Player.Current.GetGameObject().transform;
            cameraObject.transform.localPosition = Vector3.zero;
            cameraScrollObject.transform.localPosition = scrollPosition;
            cameraObject.transform.localEulerAngles = eulerAngles;
            IsAttached = true;
        }
        public static void Detach() {
            endTransversePosition();
            endTransversePoint();
            cameraScrollObject.transform.localPosition = Vector3.zero;
            cameraObject.transform.localPosition = Vector3.zero;
            cameraObject.transform.parent = null;
            cameraObject.transform.rotation = Quaternion.identity;
            IsAttached = false;
        }
        public static void LookAt( Vector3 position ) {
            endTransversePosition();
            endTransversePoint();
            cameraObject.transform.LookAt( position );
        }
        public static void Move( Vector3 position, Vector3 point ) {
            Detach();
            cameraObject.transform.position = position;
            cameraObject.transform.LookAt( point );
        }
        public static void Transverse( Vector3 positionFrom, Vector3 positionTo, Vector3 pointFrom, Vector3 pointTo, float positionSpeed = 1f, float pointSpeed = 1f ) {
            TransversePosition( positionFrom, positionTo, positionSpeed );
            TransversePoint( pointFrom, pointTo, pointSpeed );
        }
        public static void TransversePosition( Vector3 from, Vector3 to, float speed = 1f ) {
            endTransversePosition();
            cameraObject.transform.position = from;
            transversePositionCoroutine = Coroutines.Start( transversePositionAsync( from, to, Time.time, speed, Vector3.Distance( from, to ) ) );
        }
        public static void TransversePoint( Vector3 from, Vector3 to, float speed = 1f ) {
            endTransversePoint();
            cameraObject.transform.LookAt( from );
            transversePointCoroutine = Coroutines.Start( transversePointAsync( from, to, Time.time, speed, Vector3.Distance( from, to ) ) );
        }

        private static void endTransversePosition() {
            if( isMovingPosition )
                Coroutines.Stop( transversePositionCoroutine );
            isMovingPosition = false;
        }
        private static void endTransversePoint() {
            if( isMovingPoint )
                Coroutines.Stop( transversePointCoroutine );
            isMovingPoint = false;
        }
        private static IEnumerator transversePositionAsync( Vector3 start, Vector3 stop, float startTime, float speed, float distance ) {
            isMovingPosition = true;
            while( isMovingPosition ) {
                var newPos = Vector3.Lerp( start, stop, ( ( Time.time - startTime ) * speed ) / distance );
                cameraObject.transform.position = newPos;
                if( newPos == stop )
                    break;
                yield return null;
            }
            isMovingPosition = false;
        }
        private static IEnumerator transversePointAsync( Vector3 start, Vector3 stop, float startTime, float speed, float distance ) {
            isMovingPoint = true;
            while( isMovingPoint ) {
                var newPos = Vector3.Lerp( start, stop, ( ( Time.time - startTime ) * speed ) / distance );
                cameraObject.transform.LookAt( newPos );
                if( newPos == stop )
                    break;
                yield return null;
            }
            isMovingPoint = false;
        }

    }

}