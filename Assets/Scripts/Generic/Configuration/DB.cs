/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System.Data.SQLite;
using UnityEngine;
using System.IO;

namespace MyRPG.Configuration {

    public static class DB {

        public static string ConnectionString = $"Data Source={Application.dataPath}/data.db;Version=3;";

        private static SQLiteConnection connection = null;

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