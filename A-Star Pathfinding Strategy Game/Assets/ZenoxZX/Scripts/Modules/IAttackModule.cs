using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenoxZX.StrategyGame
{
    public interface IAttackModule<T> : IModule<T>
    {
        void Attack(T t);
    }
}
