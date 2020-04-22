/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/

namespace MyRPG {

	public abstract class PassiveEffect : Effect {
        
		public PassiveEffect( Personage sender ) : base( true, sender ) {
            nameId = 7;
            Dispellable = false;
        }

	}

}