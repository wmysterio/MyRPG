/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/
using UnityEngine;

namespace MyRPG {

    public interface IDescription {

        Texture2D Icon { get; }
        string Name { get; }
        string Description { get; }

    }

}