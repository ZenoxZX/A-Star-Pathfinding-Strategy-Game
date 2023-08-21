using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenoxZX.StrategyGame.Modules
{
    public interface IMovementModule<T> : IModule<T>
    {
        bool IsMoving { get; }
        void Move(T t);
        void StopMove();
    }
}
