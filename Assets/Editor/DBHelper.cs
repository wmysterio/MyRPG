/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System.Collections.Generic;
using UnityEngine.UI;
using System.Data.SQLite;
using MyRPG.Configuration;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
namespace MyRPG.Tools {

    public static class DBHelper {

        [MenuItem( "MyRPG/DBHelper/Execute custom request" )]
        public static void ExecuteCustomRequestToDB() {
            using( var connection = new SQLiteConnection( DB.ConnectionString ) ) {

                //using( var command = new SQLiteCommand( @"", connection ) ) {
                //    command.ExecuteNonQuery();
                //}

                Debug.Log( "Запит виконано!" );
            }
        }

    }

}
#endif