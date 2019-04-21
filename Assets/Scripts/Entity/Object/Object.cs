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

namespace MyRPG {

    public class Object : Entity {

        public Object( int modelId, Vector3 position ) : base( modelId, position ) {
            nameId = 1;
            Name = Localization.Current.EntityNames[ nameId ];
        }

        public void Freeze( bool state ) { rigidbody.constraints = state ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None; }

	
	}

}