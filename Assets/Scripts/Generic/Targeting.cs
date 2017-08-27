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

namespace MyRPG {

	public class Targeting : MonoBehaviour {

        public bool MouseHover { get; private set; }
        public event Action MouseEnter, MouseExit;

		void Awake() {
            MouseHover = false;
        }
		
        void OnMouseEnter() {
            MouseHover = false;
            if( MouseEnter != null )
                MouseEnter.Invoke();
        }

        void OnMouseOver() {
            MouseHover = true;
        }

        void OnMouseExit() {
            MouseHover = false;
            if( MouseExit != null )
                MouseExit.Invoke();
        }

    }

}