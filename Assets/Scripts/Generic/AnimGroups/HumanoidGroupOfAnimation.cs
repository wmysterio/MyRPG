/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using MyRPG.Items;

namespace MyRPG.AnimGroups {

    public sealed class HumanoidGroupOfAnimation : GroupOfAnimation {

        private float lastMoveSpeed = 100f;
        private bool lastIsInAir = false;

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public HumanoidGroupOfAnimation( Personage personage ) : base( personage ) { }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public override float MoveSpeed {
            get { return animator.GetFloat( nameof( MoveSpeed ) ); }
            set {
                if( lastMoveSpeed == value )
                    return;
                if( 0f > value )
                    value = 0f;
                lastMoveSpeed = value;
                animator.SetFloat( nameof( MoveSpeed ), lastMoveSpeed * 0.01f ); 
            }
        }
        public override bool IsInAir {
            get { return animator.GetBool( nameof( IsInAir ) ); }
            set {
                if( lastIsInAir == value )
                    return;
                lastIsInAir = value;
                animator.SetBool( nameof( IsInAir ), lastIsInAir );
            }
        }

        public override bool IsMagicAttack {
            get { return animator.GetBool( nameof( IsMagicAttack ) ); }
            set { animator.SetBool( nameof( IsMagicAttack ), value ); }
        }
        public override bool HasWeapon {
            get { return animator.GetBool( nameof( HasWeapon ) ); }
            set { animator.SetBool( nameof( HasWeapon ), value ); }
        }
        public override float MoveForwardBack {
            get { return animator.GetFloat( nameof( MoveForwardBack ) ); }
            set { animator.SetFloat( nameof( MoveForwardBack ), value ); }
        }
        public override float MoveLeftRight {
            get { return animator.GetFloat( nameof( MoveLeftRight ) ); }
            set { animator.SetFloat( nameof( MoveLeftRight ), value ); }
        }
        public override float TurnLeftRight {
            get { return animator.GetFloat( nameof( TurnLeftRight ) ); }
            set { animator.SetFloat( nameof( TurnLeftRight ), value ); }
        }
        public override TypeOfSpell MagicAttackType {
            get { return ( TypeOfSpell ) animator.GetInteger( nameof( MagicAttackType ) ); }
            set { animator.SetInteger( nameof( MagicAttackType ), ( int ) value ); }
        }
        public override TypeOfWeapon WeaponType {
            get { return ( TypeOfWeapon ) animator.GetInteger( nameof( WeaponType ) ); }
            set { animator.SetInteger( nameof( WeaponType ), ( int ) value ); }
        }

        /* --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- */

        public override void StartAttack() {
            StopAttack();
            animator.SetTrigger( nameof( StartAttack ) ); 
        }
        public override void Jump() {
            StopJump();
            animator.SetTrigger( nameof( Jump ) );
        }
        public override void StopAttack() { animator.ResetTrigger( nameof( StartAttack ) ); }
        public override void StopJump() { animator.ResetTrigger( nameof( Jump ) ); }
        public override void Reset() { }

    }

}