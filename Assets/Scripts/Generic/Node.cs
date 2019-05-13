/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using UnityEngine;

namespace MyRPG {

    public sealed partial class Path {

        public sealed class Node {

            public static Node Create( float x, float y, float z, float radius = 1f ) { return new Node( x, y, z, radius ); }
            public static Node Create( Vector3 point, float radius = 1f ) { return new Node( point, radius ); }

            public Vector3 Point { get; private set; }
            public float Radius { get; private set; }

            private Node( float x, float y, float z, float radius = 1f ) {
                Point = new Vector3( x, y, z );
                Radius = radius;
            }
            private Node( Vector3 point, float radius = 1f ) {
                Point = point;
                Radius = radius;
            }

        }

    }

}