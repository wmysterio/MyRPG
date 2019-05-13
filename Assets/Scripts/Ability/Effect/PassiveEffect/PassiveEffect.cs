/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/

namespace MyRPG {

	public abstract class PassiveEffect : Effect {
        
		public PassiveEffect( Personage sender ) : base( true, sender ) {
            nameId = 7;
            Dispellable = false;
        }

	}

}