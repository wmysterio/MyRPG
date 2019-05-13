/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
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