using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenoxZX.StrategyGame.Modules;
using ZenoxZX.StrategyGame.Grid;

namespace ZenoxZX.StrategyGame
{
    // FIX : Moveable unit work with module.
    public abstract class MoveableUnitBase : UnitBase, IMovementModule<GridNode>
    {
        public bool IsMoving { get; private set; } = false;
        public virtual void Move(GridNode node) => IsMoving = true;
        public virtual void StopMove() => IsMoving = false;
    }
}
