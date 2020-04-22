/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

namespace MyRPG.Configuration {

    public sealed class XMLFile<T> where T : class {

        public static XMLFile<T> Create( string path ) { return new XMLFile<T>( path ); }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private string path;
        private XmlSerializer serializer;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private XMLFile( string path ) {
            this.path = $"{Application.dataPath}/{path}.xml";
            serializer = new XmlSerializer( typeof( T ) );
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

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