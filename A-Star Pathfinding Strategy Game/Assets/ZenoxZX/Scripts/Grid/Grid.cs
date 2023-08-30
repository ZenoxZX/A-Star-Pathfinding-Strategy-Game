using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using TMPro;
using ZenoxZX.StrategyGame.Utils;
using System;

namespace ZenoxZX.StrategyGame.GridS
{
    public class Grid<TGridObject>
    {
        public event EventHandler<OnGridValueChangeEventArgs> OnGridValueChange;
        public class OnGridValueChangeEventArgs : EventArgs
        {
            public int x;
            public int y;
            public int index;

            public OnGridValueChangeEventArgs(int x, int y, int index)
            {
                this.x = x;
                this.y = y;
                this.index = index;
            }
        }

        public readonly int width;
        public readonly int height;
        public readonly float cellSize;
        public readonly Vector3 originPosition;
        public readonly TGridObject[,] gridArray;
        private readonly TMP_Text[,] tmpArray;

        public Grid(int width, int height, float cellSize, Func<Grid<TGridObject>, int, int, TGridObject> createFunc, Vector3 originPosition = default, Transform tParent = null)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;

            gridArray = new TGridObject[width, height];

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    gridArray[x, y] = createFunc(this, x, y);

            bool showDebug = true;
            if (showDebug)
            {
                tmpArray = new TMP_Text[width, height];

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        DrawInLines(x, y);
                        tmpArray[x, y] = UtilClass.CreateWorldTMP(gridArray[x, y]?.ToString(), GetWorldPosition(x, y) + .5f * new Vector3(cellSize, cellSize), parent: tParent);
                    }
                }

                DrawOutLines();
            }

            OnGridValueChange += (s, args) => tmpArray[args.x, args.y].SetText(gridArray[args.x, args.y]?.ToString());
        }


        private void DrawInLines(int x, int y)
        {
            Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100);
            Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100);
        }
        private void DrawOutLines()
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

        public void TriggerGridObjectChange(int x, int y) => OnGridValueChange?.Invoke(this, new OnGridValueChangeEventArgs(x, y, CalculateIndex(x, y, width)));

        public void SetGridObject(int x, int y, TGridObject value)
        {
            if (!IsValidCoordinate(x, y)) return;
            gridArray[x, y] = value;
            tmpArray[x, y].SetText(value.ToString());
            TriggerGridObjectChange(x, y);
        }

        public void SetGridObject(Vector3 worldPosition, TGridObject value)
        {
            int2 i2 = GetXY(worldPosition);
            SetGridObject(i2.x, i2.y, value);
        }

        public TGridObject GetGridObject(int x, int y) => IsValidCoordinate(x, y) ? gridArray[x, y] : default;

        public TGridObject GetGridObject(Vector3 worldPosition)
        {
            int2 i2 = GetXY(worldPosition);
            return GetGridObject(i2.x, i2.y);
        }

        public int CalculateIndex(int x, int y, int gridWidth) => x + y * gridWidth;
        public Vector3 GetWorldPosition(int x, int y) => cellSize * new Vector3(x, y) + originPosition;
        public int2 GetXY(Vector3 worldPosition) => new()
        {
            x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize),
            y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize)
        };
    }
}
