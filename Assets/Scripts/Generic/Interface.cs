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

    public partial class Player {

        public static class Interface {

            public const int MAX_ICON_COUNT = 10;

            public static Texture2D[] Icons { get; private set; }

            private static bool isInit = false, enable = false;

            public static bool IsInit {
                get { return isInit; }
                private set { isInit = value; }
            }
            public static bool Enable {
                get { return enable; }
                set { enable = value; }
            }

            private static ResourceRequest request;

            public static IEnumerator Init() {
                if( IsInit )
                    yield return null;

                yield return Console.Init();

                Icons = new Texture2D[ MAX_ICON_COUNT ];
                for( int i = 0; i < MAX_ICON_COUNT; i++ ) {
                    request = Resources.LoadAsync<Texture2D>( string.Format( "UI/Interface/images/{0}", i ) );
                    yield return request;
                    Icons[ i ] = request.asset as Texture2D;
                }


                IsInit = true;
            }




            public static void Update() {

            }
            

            public static void Draw() {



                Console.Draw();
            }

        }

    }

}