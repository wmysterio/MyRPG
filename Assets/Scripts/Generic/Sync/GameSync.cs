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
            DontDestroyOnLoad( this );
        }
        private void Update() { Camera.Update(); }
        private void OnApplicationQuit() { DB.Close(); }

    }

}