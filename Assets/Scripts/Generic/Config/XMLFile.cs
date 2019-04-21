/*
   Ліцензія: CC-BY
   Автор: Василь ( wmysterio )
   Сайт: http://metal-prog.zzz.com.ua/
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;
using System.IO;

namespace MyRPG {

    public sealed class XMLFile<T> where T : class {

        public static XMLFile<T> Create( string path ) { return new XMLFile<T>( path ); }

        private string path;
        private XmlSerializer serializer;

        private XMLFile( string path ) {
            this.path = string.Format( "{0}/{1}.xml", Application.dataPath, path );
            serializer = new XmlSerializer( typeof( T ) );
        }

        public bool Load( out T instance ) {
            instance = null;
            try {
                using( TextReader tr = new StreamReader( path ) ) {
                    instance = ( T ) serializer.Deserialize( tr );
                }
            } catch { return false; }
            return true;
        }
        public bool Save( T instance, bool replace = false ) {
            try {
                if( replace ) {
                    var newFile = string.Format( "{0}_new", path );
                    var bacFile = string.Format( "{0}.bac", path );
                    using( TextWriter tw = new StreamWriter( newFile ) ) {
                        serializer.Serialize( tw, instance );
                    }
                    File.Replace( newFile, path, bacFile );
                } else {
                    using( TextWriter tw = new StreamWriter( path ) ) {
                        serializer.Serialize( tw, instance );
                    }
                }
            } catch { return false; }
            return true;
        }

    }

}