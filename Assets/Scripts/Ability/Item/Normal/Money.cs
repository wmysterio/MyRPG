/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
 
namespace MyRPG {
 
	public sealed class Money : NormalItem {
 
        public Money( int amount = 1 ) : base( 1, TypeOfItemRarity.Normal ) {
            nameId = 39;
            if( 1 > amount )
                amount = 1;
            Count = amount;
            price = 0;
        }

        public override void Use( Personage target = null ) {
            if( !Player.Exist() )
                return;
            Player.Current.AddMoney( Count );
            Count = 0;
        }

    }
 
}