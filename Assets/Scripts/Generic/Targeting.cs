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

	    private void Awake() {
            MouseHover = false;
        }

        private void OnMouseEnter() {
            MouseHover = false;
            if( MouseEnter != null )
                MouseEnter.Invoke();
        }

        private void OnMouseOver() {
            MouseHover = true;
        }

        private void OnMouseExit() {
            MouseHover = false;
            if( MouseExit != null )
                MouseExit.Invoke();
        }

    }

}