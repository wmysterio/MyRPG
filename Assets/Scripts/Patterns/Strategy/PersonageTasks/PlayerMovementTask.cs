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
                personage.AnimationGroup.MoveLeftRight = InputManager.GetKeyAxisRaw( KeyName.LEFT, KeyName.RIGHT );
            }

            personage.AnimationGroup.MoveForwardBack = InputManager.GetKeyAxisRaw( KeyName.BACK, KeyName.FORWARD );
            if( InputManager.IsKeyDown( KeyName.JUMP ) )
                personage.AnimationGroup.Jump();

            return true;
        }

    }

}