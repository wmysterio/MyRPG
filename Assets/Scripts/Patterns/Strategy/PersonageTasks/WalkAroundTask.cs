/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/
namespace MyRPG.Patterns.Strategy.PersonageTasks {

    public sealed class WalkAroundTask : IPersonageTask {

        public bool Execute( Personage personage ) {
            return true;
        }

    }

}