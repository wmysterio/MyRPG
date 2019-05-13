/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
namespace MyRPG.Patterns.Strategy.PersonageTasks {

    public sealed class PlayerMovementTask : IPersonageTask {

        public bool Execute( Personage personage ) {
            if( !personage.IsPlayer )
                return false;

            if( InputManager.IsKeyDown( KeyName.VIEW_BAG ) )
                Player.Interface.ToggleWindow( Window.Bag );
            if( InputManager.IsKeyDown( KeyName.VIEW_EFFECTS ) )
                Player.Interface.ToggleWindow( Window.Effects );
            if( InputManager.IsKeyDown( KeyName.VIEW_PERSONAGE ) )
                Player.Interface.ToggleWindow( Window.Personage );
            if( InputManager.IsKeyDown( KeyName.VIEW_QUESTS ) )
                Player.Interface.ToggleWindow( Window.Quests );
            if( InputManager.IsKeyDown( KeyName.VIEW_SPELLS ) )
                Player.Interface.ToggleWindow( Window.Spells );

            if( !InputManager.GetMouse( MouseKeyName.Right ) ) {
                if( InputManager.GetKey( KeyName.TURN_LEFT ) ) {
                    personage.Turn( -Player.TURN_SPEED );
                } else if( InputManager.GetKey( KeyName.TURN_RIGHT ) ) {
                    personage.Turn( Player.TURN_SPEED );
                }
                if( InputManager.GetKey( KeyName.LEFT ) ) {
                    personage.MoveLeft();
                } else if( InputManager.GetKey( KeyName.RIGHT ) ) {
                    personage.MoveRight();
                }
            }

            if( InputManager.GetKey( KeyName.FORWARD ) ) {
                personage.MoveForward();
            } else if( InputManager.GetKey( KeyName.BACK ) ) {
                personage.MoveBack();
            }
            if( InputManager.IsKeyDown( KeyName.JUMP ) ) {
                personage.Jump();
            }


            return true;
        }

    }

}