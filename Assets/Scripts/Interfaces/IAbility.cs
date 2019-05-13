/*
   Ліцензія: CC-BY
   Автор: Василь ( wmysterio )
   Сайт: http://metal-prog.zzz.com.ua/
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