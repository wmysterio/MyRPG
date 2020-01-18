/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using UnityEngine;

namespace MyRPG {

    public class Object : Entity {

        public Object( int modelId, Vector3 position, int nameId = 1 ) : base( modelId, position, nameId ) { }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public void Freeze( bool state ) { 
            mainRigidbody.constraints = state ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None; 
        }

	}

}