using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenoxZX.StrategyGame.GridS
{
    public class GridBuildingSystem : MonoBehaviour
    {
        [SerializeField] Transform building;
        private Grid<GridNode> grid;


        private void Start()
        {
            grid = new Grid<GridNode>(4, 2, 10, (n, x, y) => new (n, x, y), new (20,0,0));
        }
    }
}
