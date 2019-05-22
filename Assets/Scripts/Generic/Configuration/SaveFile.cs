/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace MyRPG.Configuration {

    public partial class Player {

        public static readonly SaveFile Profile = new SaveFile();

        public sealed class SaveFile {

            private const int SIZE = 65536; // ( 1024 * 64 ) bytes
            private const string EXTENSION = "SAV";
            private const string DIRECTORY = "Saves";
            private const byte VERSION = 1;
            private const string PATTERN = "^[a-z]{1,}$";

            private byte[] lastData = new byte[ SIZE ];
            private byte[] currentData = new byte[ SIZE ];

            public string Name { get; private set; }
            public bool IsEmpty { get; private set; }

            public SaveFile() { Clear(); }

            public void Clear() {
                Array.Clear( lastData, 0, SIZE );
                Array.Clear( currentData, 0, SIZE );
                Name = string.Empty;
                IsEmpty = true;
            }
            public void Select( string name ) {
                Clear();
                Name = name;
                IsEmpty = false;
                currentData[ 0 ] = VERSION;
            }
            public void Refresh() { Array.Copy( currentData, lastData, SIZE ); }
            public void Reset() { Array.Copy( lastData, currentData, SIZE ); }
            public bool Load() {
                if( !checkDirectory() )
                    return false;
                var path = $"{Application.dataPath}/{DIRECTORY}/{Name}.{EXTENSION}";
                if( !File.Exists( path ) )
                    return false;
                try {
                    var data = File.ReadAllBytes( path );
                    if( data[ 0 ] != VERSION )
                        return false;
                    Array.Copy( data, lastData, SIZE );
                    Reset();
                } catch { return false; }
                return true;
            }
            public bool Save() {
                if( !checkDirectory() || IsEmpty )
                    return false;
                var path = $"{Application.dataPath}/{DIRECTORY}/{Name}.{EXTENSION}";
                try {
                    File.WriteAllBytes( path, currentData );
                    Refresh();
                } catch { return false; }
                return true;
            }

            public void WriteByte( int offset, byte val ) { currentData[ offset ] = val; }
            public byte ReadByte( int offset ) { return currentData[ offset ]; }
            public void WriteInt( int offset, int val ) { Array.Copy( BitConverter.GetBytes( val ), 0, currentData, offset, 4 ); }
            public int ReadInt( int offset ) { return BitConverter.ToInt32( currentData, offset ); }
            public void WriteFloat( int offset, float val ) { Array.Copy( BitConverter.GetBytes( val ), 0, currentData, offset, 4 ); }
            public float ReadFloat( int offset ) { return BitConverter.ToSingle( currentData, offset ); }
            public void WriteData( int offset, byte[] data ) { Array.Copy( data, 0, currentData, offset, data.Length ); }
            public byte[] ReadData( int offset, int length ) {
                var data = new byte[ length ];
                Array.Copy( currentData, offset, data, 0, length );
                return data;
            }


            private bool checkDirectory() {
                var check = false;
                var path = $"{Application.dataPath}/{DIRECTORY}";
                if( !Directory.Exists( path ) ) {
                    try {
                        Directory.CreateDirectory( path );
                        check = true;
                    } catch { }
                } else { check = true; }
                return check;
            }

        }
        
    }

}

/* МАПА СЕЙВ-ФАЙЛУ
----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- 
ЗМІЩЕННЯ          ТИП               ОПИС
----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- 
0                 BYTE              Версія                                                                                        

----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- 
*/
