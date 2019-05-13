/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using UnityEngine;

namespace MyRPG {

    public class Object : Entity {

        public Object( int modelId, Vector3 position ) : base( modelId, position ) {
            nameId = 1;
            Name = Localization.Current.EntityDescriptions[ nameId ];
        }

        public void Freeze( bool state ) { rigidbody.constraints = state ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None; }

	
	}

}