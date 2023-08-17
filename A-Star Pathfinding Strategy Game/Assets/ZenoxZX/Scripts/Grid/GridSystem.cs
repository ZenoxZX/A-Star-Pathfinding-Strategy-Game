using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

namespace ZenoxZX.StrategyGame.Grid
{
    public class GridSystem : MonoBehaviour
    {
        [SerializeField] GridNode[] gridNodeArray;
        [SerializeField] int2 gridSize;

        private void Start()
        {
            CreateGridArray(5, 5);
        }

        public void CreateGridArray(int width, int height)
        {
            gridSize = new int2(width, height);
            gridNodeArray = new GridNode[gridSize.x * gridSize.y];

            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    int index = CalculateIndex(x, y, gridSize.x);
                    gridNodeArray[index] = new GridNode()
                    {
                        x = x,
                        y = y,
                        index = index,
                        isWalkable = true
                    };
                }
            }
        }

        private int CalculateIndex(int x, int y, int gridWidth) => x + y * gridWidth;
    }
}
