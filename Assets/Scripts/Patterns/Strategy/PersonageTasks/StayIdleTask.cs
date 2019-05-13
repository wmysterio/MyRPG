/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
namespace MyRPG.Patterns.Strategy.PersonageTasks {

    public sealed class StayIdleTask : IPersonageTask {

        public bool Execute( Personage personage ) {
            return true;
        }

    }

}