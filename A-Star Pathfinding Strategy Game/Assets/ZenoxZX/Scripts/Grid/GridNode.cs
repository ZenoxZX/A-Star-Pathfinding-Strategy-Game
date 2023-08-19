using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenoxZX.StrategyGame.GridS
{
    [System.Serializable]
    public class GridNode
    {
        private Grid<GridNode> grid;
        public int x;
        public int y;

        private Transform buildingTransform;

        public int index;
        public bool isWalkable;
        private bool hasBuilding;

        public GridNode(Grid<GridNode> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
            index = x + y * grid.width;
            isWalkable = true;
            hasBuilding = false;
        }

        public bool CanBuild => !hasBuilding;

        public void SetWalkable(bool value)
        {
            isWalkable = value;
            grid.TriggerGridObjectChange(x, y);
        }

        public void SetBuilding(Transform buildingTransform)
        {
            this.buildingTransform = buildingTransform;
            hasBuilding = buildingTransform != null;
            grid.TriggerGridObjectChange(x, y);
        }

        public void ClearBuilding() => SetBuilding(null);
    }
}
