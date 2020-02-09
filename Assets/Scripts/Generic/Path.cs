/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System.Collections.Generic;
using UnityEngine;

namespace MyRPG {

	public sealed partial class Path {

        public static Path Create( bool isLoop ) { return new Path( isLoop ); }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private List<Node> nodes;
        private int nodeIndex;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public bool IsEnd { get; private set; }
        public bool IsLoop { get; private set; }
        public int Count { get { return nodes.Count; } }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private Path( bool isLoop ) {
            nodes = new List<Node>();
            IsLoop = isLoop;
            ToDefault();
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public void AddNode( float x, float y, float z, float radius = 1f ) { nodes.Add( Node.Create( x, y, z, radius ) ); }
        public void AddNode( Vector3 point, float radius = 1f ) { nodes.Add( Node.Create( point, radius ) ); }
        public void AddNode( Node node ) { nodes.Add( node ); }
        public bool NextNode( out Node node ) {
            if( nodes.Count == 0 ) {
                node = null;
                ToDefault();
                return false;
            }

            nodeIndex++;

            if( nodeIndex == nodes.Count ) {
                if( !IsLoop || nodes.Count == 1 ) {
                    node = null;
                    ToDefault();
                    return false;
                }
                nodeIndex = 0;
            }

            IsEnd = false;
            node = nodes[ nodeIndex ];
            return true;
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private void ToDefault() {
            IsEnd = true;
            nodeIndex = -1;
        }

    }

}