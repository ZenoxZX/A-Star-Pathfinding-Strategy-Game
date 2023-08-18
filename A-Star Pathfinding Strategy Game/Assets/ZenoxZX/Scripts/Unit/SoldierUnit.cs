using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenoxZX.StrategyGame.HealthSystem;

namespace ZenoxZX.StrategyGame
{
    public class SoldierUnit : MoveableUnitBase, IAttackModule<UnitBase>
    {
        private AttackableUnitStat attackableUnitStat;

        public void Attack(UnitBase unit)
        {
            unit.HealthComponent.TakeDamage(attackableUnitStat.damage);
        }

        #region MONO

        public override void Awake()
        {
            base.Awake();
            attackableUnitStat = unitStats as AttackableUnitStat;
        }

        #endregion
    }
}
