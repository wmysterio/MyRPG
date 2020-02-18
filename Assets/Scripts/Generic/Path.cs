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
        private bool isEnd, isLoop;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public bool IsEnd { get { return isEnd; } }
        public bool IsLoop { get { return isLoop; } }
        public int Count { get { return nodes.Count; } }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private Path( bool isLoop ) {
            nodes = new List<Node>();
            this.isLoop = isLoop;
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
                if( !isLoop || nodes.Count == 1 ) {
                    node = null;
                    ToDefault();
                    return false;
                }
                nodeIndex = 0;
            }

            isEnd = false;
            node = nodes[ nodeIndex ];
            return true;
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        private void ToDefault() {
            isEnd = true;
            nodeIndex = -1;
        }

    }

}