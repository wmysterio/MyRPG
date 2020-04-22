/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/
using UnityEngine;

namespace MyRPG {

    public sealed partial class Path {

        public sealed class Node {

            public static Node Create( float x, float y, float z, float radius = 1f ) { return new Node( x, y, z, radius ); }
            public static Node Create( Vector3 point, float radius = 1f ) { return new Node( point, radius ); }

            /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

            private Vector3 point;
            private float radius;

            /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

            public Vector3 Point { get { return point; } }
            public float Radius { get { return radius; } }

            /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

            private Node( Vector3 point, float radius = 1f ) { this.point = point; this.radius = radius; }
            private Node( float x, float y, float z, float radius = 1f ) : this( new Vector3( x, y, z ), radius ) { }

        }

    }

}