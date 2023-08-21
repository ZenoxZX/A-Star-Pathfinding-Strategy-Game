using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenoxZX.StrategyGame.Modules;
using ZenoxZX.StrategyGame.GridS;
using ZenoxZX.StrategyGame.CommandSystem;

namespace ZenoxZX.StrategyGame
{
    // FIX : Moveable unit work with module.
    public abstract class MoveableUnitBase : UnitBase, ICommandListener, IMovementModule<GridNode>
    {
        public CommandMachine CommandMachine { get; protected set; }
        public bool IsMoving { get; private set; } = false;
        public virtual void Move(GridNode node) => IsMoving = true;
        public virtual void StopMove() => IsMoving = false;
    }
}
