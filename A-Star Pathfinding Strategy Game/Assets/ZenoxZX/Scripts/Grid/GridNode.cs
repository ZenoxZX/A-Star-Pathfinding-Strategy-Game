using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenoxZX.StrategyGame.GridS
{
    [System.Serializable]
    public class GridNode
    {
        public int x;
        public int y;

        public int index;
        public bool isWalkable;

        public void SetWalkable(bool value) => isWalkable = value;
    }
}
