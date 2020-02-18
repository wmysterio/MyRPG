/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using UnityEngine;
using MyRPG.Configuration;

namespace MyRPG.Sync {

    public sealed class GameSync : MonoBehaviour {

        private bool connectionReady;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public bool ConnectionReady { get { return connectionReady; } }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private void Awake() {
            connectionReady = DB.Connect();
            Coroutines.Init( this );
            Coroutines.Repeat( Calendar.Update, 0f, 1f );
            DontDestroyOnLoad( this );
        }
        private void Start() { }
        private void Update() {
            if( Player.Interface.IsInit )
                Player.Interface.Update();
            Camera.Update();
        }
        private void FixedUpdate() { }
        private void OnGUI() {
            if( Player.Interface.IsInit ) {
                //Player.Interface.Draw();
            }
        }
        private void OnApplicationQuit() { DB.Close(); }

    }

}