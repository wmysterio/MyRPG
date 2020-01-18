/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using UnityEngine;

namespace MyRPG {

    public abstract class Humanoid : Personage {

        private GenderOfHumanoid gender;
        private RaseOfHumanoid rase;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public GenderOfHumanoid Gender { get { return gender; } }
        public RaseOfHumanoid Rase { get { return rase; } }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public Humanoid( int level, RankOfPersonage rank, int modelId, Vector3 position, int nameId = 4 ) : base( level, rank, TypeOfPersonage.Humanoid, modelId, position, nameId ) {
            gender = modelId % 2 == 0 ? GenderOfHumanoid.Male : GenderOfHumanoid.Female;
            rase = ( RaseOfHumanoid ) ( modelId - ( byte ) Gender );
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        protected override void generateLoot() {
            base.generateLoot();
            var range = 100f;
            if( Rank == RankOfPersonage.Normal )
                range = UnityEngine.Random.Range( 0f, 100f );
            if( 80f > range )
                return;
            var money = UnityEngine.Random.Range( 1, 8 );
            if( Rank != RankOfPersonage.Normal ) {
                money = ( ( int ) Rank ) * 10 + money * 10;
                if( Rank == RankOfPersonage.Boss )
                    money += 2500;
            }
            Loot.Add( Generator.CreateMoney( money ) );
        }

    }

    public enum GenderOfHumanoid : byte {
        Male = 0,   // Чоловік
        Female = 1, // Жінка
    }

    public enum RaseOfHumanoid : byte {
        Human = 0,    // Людина
        Orc = 2,      // Орк
        Elf = 4,      // Ельф
        Goblіn = 6,   // Гоблін
    }

}