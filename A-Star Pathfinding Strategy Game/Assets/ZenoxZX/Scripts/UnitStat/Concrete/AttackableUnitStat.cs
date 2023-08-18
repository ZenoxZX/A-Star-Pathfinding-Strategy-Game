using UnityEngine;

namespace ZenoxZX.StrategyGame
{
    // FIX : Actually static attackable unit doesn't work with this

    [CreateAssetMenu(fileName = "New Attackable Stat", menuName = GameConstraints.SO_PATH + GameConstraints.STAT_PATH + "Attackable Stat")]
    public class AttackableUnitStat : MoveableUnitStatBaseSO
    {
        [Header("Attackable")]
        public float damage = 1;
    }
}
