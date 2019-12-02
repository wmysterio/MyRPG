/*
	Ліцензія: CC-BY
	Автор: Василь ( wmysterio )
	Сайт: http://metal-prog.zzz.com.ua/
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using MyRPG.Items;

namespace MyRPG.AnimGroups {

    public abstract class GroupOfAnimation {

        private delegate GroupOfAnimation HadlerGroupOfAnimation( Personage personage );

        private static Dictionary<TypeOfPersonage, HadlerGroupOfAnimation> createGroup = new Dictionary<TypeOfPersonage, HadlerGroupOfAnimation>() {
            { TypeOfPersonage.Humanoid, p => { return new HumanoidGroupOfAnimation( p ); } }
            // ...
        };

        public static GroupOfAnimation Greate( Personage personage ) {
            return createGroup[ personage.Type ].Invoke( personage );
        }


        protected Animator animator;

        private GroupOfAnimation() { }
        protected GroupOfAnimation( Personage personage ) {
            animator = personage.GetGameObject().GetComponent<Animator>();
        }

        public abstract bool IsInAir { get; set; }
        public abstract bool IsMagicAttack { get; set; }
        public abstract bool HasWeapon { get; set; }
        public abstract float MoveSpeed { get; set; }
        public abstract float MoveForwardBack { get; set; }
        public abstract float MoveLeftRight { get; set; }
        public abstract float TurnLeftRight { get; set; }
        public abstract TypeOfSpell MagicAttackType { get; set; }
        public abstract TypeOfWeapon WeaponType { get; set; }

        public abstract void StopAttack();
        public abstract void StartAttack();
        public abstract void Jump();
        public abstract void StopJump();
        public abstract void Reset();

    }

}