using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenoxZX.StrategyGame
{
    [CreateAssetMenu(fileName = "New Building Stat", menuName = GameConstraints.SO_PATH + GameConstraints.STAT_PATH + "Building Stat")]
    public class BuildingStatSO : UnitStatBaseSO
    {
        [Header("Building")]
        public float productionTime;
    }
}
