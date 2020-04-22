/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/

namespace MyRPG {

    public enum TypeOfAbility { Effect, Item, Spell }

    public interface IAbility : IDescription {

        TypeOfAbility AbilityType { get; }
        float Timer { get; }
        int Count { get; }
        void Use( Personage target = null );

    }

}