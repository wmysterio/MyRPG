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
using System.Xml.Serialization;
using System.IO;

namespace MyRPG {

    public sealed class XMLFile<T> where T : class {

        public static XMLFile<T> Create( string path ) { return new XMLFile<T>( path ); }

        private Type type;
        private string path;
        private XmlSerializer serializer;

        private XMLFile( string path ) {
            type = typeof( T );
            this.path = string.Format( "{0}/{1}.xml", Application.dataPath, path );
            serializer = new XmlSerializer( type );
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

        public bool Save( T instance ) {
            try {
                using( TextWriter tw = new StreamWriter( path ) ) {
                    serializer.Serialize( tw, instance );
                }
            } catch { return false; }
            return true;
        }

    }

}