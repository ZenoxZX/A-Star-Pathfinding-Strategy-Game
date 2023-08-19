using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenoxZX.StrategyGame
{
    public abstract class MoveableUnitStatBaseSO : UnitStatBaseSO
    {
        [Header("Moveable")]
        public float speed = 5;
        public float reachDistance = 1;
    }
}
