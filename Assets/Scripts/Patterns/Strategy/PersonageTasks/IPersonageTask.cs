/*
	Ліцензія: CC-BY
	Автор: Василь (wmysterio)
	https://github.com/wmysterio/MyRPG
*/
namespace MyRPG.Patterns.Strategy.PersonageTasks {

    public interface IPersonageTask {

        bool Execute( Personage personage );

    }

}

/*
public enum Task {
    STAY_IDLE,
    WALK_AROUND,
    WALK_TO_NODE
}
*/