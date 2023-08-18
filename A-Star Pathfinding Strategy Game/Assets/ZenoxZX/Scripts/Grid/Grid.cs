using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using TMPro;
using ZenoxZX.StrategyGame.Utils;

namespace ZenoxZX.StrategyGame.GridS
{
    public class Grid
    {
        public int width;
        private int height;
        private float cellSize;
        private Vector3 originPosition;
        private int[,] gridArray;
        private TMP_Text[,] tmpArray;

        public Grid(int width, int height, float cellSize, Vector3 originPosition, Transform tParent = null)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;

            gridArray = new int[width, height];
            tmpArray = new TMP_Text[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    DrawInLines(x, y);
                    tmpArray[x, y] = UtilClass.CreateWorldTMP(gridArray[x, y].ToString(), GetWorldPosition(x, y) + .5f * new Vector3(cellSize, cellSize), parent: tParent);
                }
            }

            DrawOutLines();
        }


        public void DrawInLines(int x, int y)
        {
            Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100);
            Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100);
        }

        public void DrawOutLines()
        {
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100);
        }

        public bool IsValidCoordinate(int x, int y)
        {
            return x >= 0 &&
                   y >= 0 &&
                   x < width &&
                   y < height;
        }

        public void SetValue(int x, int y, int value)
        {
            if (IsValidCoordinate(x, y))
                gridArray[x, y] = value;
        }

        public void SetValue(Vector3 worldPosition, int value)
        {
            int2 i2 = GetXY(worldPosition);
            SetValue(i2.x, i2.y, value);
        }

        public int GetValue(int x, int y)
        {
            if (IsValidCoordinate(x, y))
            {
                return gridArray[x, y];
            }

            else
            {
                return 0;
                //return default;
            }
        }

        public int GetValue(Vector3 worldPosition)
        {
            int2 i2 = GetXY(worldPosition);
            return GetValue(i2.x, i2.y);
        }

        private int CalculateIndex(int x, int y, int gridWidth) => x + y * gridWidth;
        private Vector3 GetWorldPosition(int x, int y) => cellSize * new Vector3(x, y) + originPosition;
        private int2 GetXY(Vector3 worldPosition) => new()
        {
            x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize),
            y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize)
        };
    }
}
