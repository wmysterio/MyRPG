/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using UnityEngine;

namespace MyRPG {

    public interface IDescription {

        Texture2D Icon { get; }
        string Name { get; }
        string Description { get; }

    }

}