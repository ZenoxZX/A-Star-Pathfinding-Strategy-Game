using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenoxZX.StrategyGame
{
    public abstract class UnitStatBaseSO : ScriptableObject
    {
        [Header("Base")]
        public string unitName = "New Unit";
        public int health = 1;
        public Sprite uiSprite;
    }
}
