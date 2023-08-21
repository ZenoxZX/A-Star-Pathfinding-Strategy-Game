using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenoxZX.StrategyGame.CommandSystem;

namespace ZenoxZX.StrategyGame
{
    public class SoldierUnit : MoveableUnitBase, IAttackModule<UnitBase>
    {
        private AttackableUnitStat attackableUnitStat;

        public void Attack(UnitBase unit)
        {
            bool unitInRange = AttackCommand<UnitBase>.InRange(unit, transform, attackableUnitStat.attackRange);

            if (unitInRange)
            {
                ICommand command = new AttackCommand<UnitBase>(unit, transform, 1, 1, 1, null);
                CommandMachine.ClearAdd(command);
            }

            else
            {
                // Move to closest path to attack
            }
        }

        #region MONO

        public override void Awake()
        {
            base.Awake();
            attackableUnitStat = unitStats as AttackableUnitStat;
            CommandMachine = new(15);
        }

        #endregion
    }
}
