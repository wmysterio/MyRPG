/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/
using System.Data.SQLite;
using UnityEngine;
using System.IO;

namespace MyRPG.Configuration {

    public static class DB {

        private static SQLiteConnection connection = null;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public static string ConnectionString = $"Data Source={Application.dataPath}/data.db;Version=3;";

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public static bool Connect() {
            if( connection != null )
                return true;
            if( !File.Exists( $"{Application.dataPath}/data.db" ) )
                return false;
            connection = new SQLiteConnection( ConnectionString );
            connection.Open();
            return true;
        }
        public static void Close() {
            if( connection != null ) {
                connection.Close();
                connection = null;
            }
        }

    }

}