﻿using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace ZenoxZX.StrategyGame.Pathfinder
{
    public class Pathfinding : MonoBehaviour
    {
        private const int MOVE_STRAIGHT_COST = 10;
        private const int MOVE_DIAGONAL_COST = 14;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FindPath(1, 1);
            }
        }

        public void FindPath(int x, int y)
        {
            float startTime = Time.realtimeSinceStartup;
            NativeList<int2> pathList = new NativeList<int2>(Allocator.TempJob);

            FindPathJob findPathJob = new FindPathJob
            {
                gridSize = new int2(100, 100),
                startPosition = new int2(0, 0),
                endPosition = new int2(99, 99),
                pathList = pathList
            };

            JobHandle jobHandle = findPathJob.Schedule();
            jobHandle.Complete();

            Debug.Log("Time: " + ((Time.realtimeSinceStartup - startTime) * 1000f));
            pathList.Dispose();
        }

        [BurstCompile]
        private struct FindPathJob : IJob
        {
            public int2 gridSize;
            public int2 startPosition;
            public int2 endPosition;
            public NativeList<int2> pathList;

            public void Execute()
            {
                NativeArray<PathNode> pathNodeArray = new(gridSize.x * gridSize.y, Allocator.Temp);

                for (int x = 0; x < gridSize.x; x++)
                {
                    for (int y = 0; y < gridSize.y; y++)
                    {
                        PathNode pathNode = new()
                        {
                            x = x,
                            y = y,
                            index = CalculateIndex(x, y, gridSize.x),

                            gCost = int.MaxValue,
                            hCost = CalculateDistanceCost(new int2(x, y), endPosition)
                        };
                        pathNode.CalculateFCost();
                        pathNode.isWalkable = true;
                        pathNode.cameFromNodeIndex = -1;

                        pathNodeArray[pathNode.index] = pathNode;
                    }
                }

                NativeArray<int2> neighbourOffsetArray = new(8, Allocator.Temp);
                neighbourOffsetArray[0] = new int2(-1, 0); // Left
                neighbourOffsetArray[1] = new int2(+1, 0); // Right
                neighbourOffsetArray[2] = new int2(0, +1); // Up
                neighbourOffsetArray[3] = new int2(0, -1); // Down
                neighbourOffsetArray[4] = new int2(-1, -1); // Left Down
                neighbourOffsetArray[5] = new int2(-1, +1); // Left Up
                neighbourOffsetArray[6] = new int2(+1, -1); // Right Down
                neighbourOffsetArray[7] = new int2(+1, +1); // Right Up

                int endNodeIndex = CalculateIndex(endPosition.x, endPosition.y, gridSize.x);

                PathNode startNode = pathNodeArray[CalculateIndex(startPosition.x, startPosition.y, gridSize.x)];
                startNode.gCost = 0;
                startNode.CalculateFCost();
                pathNodeArray[startNode.index] = startNode;

                NativeList<int> openList = new(Allocator.Temp);
                NativeList<int> closedList = new(Allocator.Temp);

                openList.Add(startNode.index);

                while (openList.Length > 0)
                {
                    int currentNodeIndex = GetLowestCostFNodeIndex(openList, pathNodeArray);
                    PathNode currentNode = pathNodeArray[currentNodeIndex];

                    if (currentNodeIndex == endNodeIndex)
                        break;

                    for (int i = 0; i < openList.Length; i++)
                    {
                        if (openList[i] == currentNodeIndex)
                        {
                            openList.RemoveAtSwapBack(i);
                            break;
                        }
                    }

                    closedList.Add(currentNodeIndex);

                    for (int i = 0; i < neighbourOffsetArray.Length; i++)
                    {
                        int2 neighbourOffset = neighbourOffsetArray[i];
                        int2 neighbourPosition = new(currentNode.x + neighbourOffset.x, currentNode.y + neighbourOffset.y);

                        if (!IsPositionInsideGrid(neighbourPosition, gridSize))
                            continue;

                        int neighbourNodeIndex = CalculateIndex(neighbourPosition.x, neighbourPosition.y, gridSize.x);

                        if (closedList.Contains(neighbourNodeIndex))
                            continue;

                        PathNode neighbourNode = pathNodeArray[neighbourNodeIndex];
                        if (!neighbourNode.isWalkable)
                            continue;

                        int2 currentNodePosition = new(currentNode.x, currentNode.y);

                        int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNodePosition, neighbourPosition);
                        if (tentativeGCost < neighbourNode.gCost)
                        {
                            neighbourNode.cameFromNodeIndex = currentNodeIndex;
                            neighbourNode.gCost = tentativeGCost;
                            neighbourNode.CalculateFCost();
                            pathNodeArray[neighbourNodeIndex] = neighbourNode;

                            if (!openList.Contains(neighbourNode.index))
                                openList.Add(neighbourNode.index);
                        }
                    }
                }

                PathNode endNode = pathNodeArray[endNodeIndex];
                if (endNode.cameFromNodeIndex != -1)
                {
                    NativeList<int2> path = CalculatePath(pathNodeArray, endNode);
                    NativeArray<int2> reversePathArray = new NativeArray<int2>(path.Length, Allocator.Temp);

                    for (int i = 0; i < path.Length; i++)
                    {
                        reversePathArray[i] = path[path.Length - i - 1];
                    }

                    pathList.AddRange(reversePathArray);
                    path.Dispose();
                    reversePathArray.Dispose();
                }

                pathNodeArray.Dispose();
                neighbourOffsetArray.Dispose();
                openList.Dispose();
                closedList.Dispose();
            }

            private NativeList<int2> CalculatePath(NativeArray<PathNode> pathNodeArray, PathNode endNode)
            {
                NativeList<int2> path = new(Allocator.Temp);

                if (endNode.cameFromNodeIndex != -1)
                {
                    path.Add(new int2(endNode.x, endNode.y));

                    PathNode currentNode = endNode;
                    while (currentNode.cameFromNodeIndex != -1)
                    {
                        PathNode cameFromNode = pathNodeArray[currentNode.cameFromNodeIndex];
                        path.Add(new int2(cameFromNode.x, cameFromNode.y));
                        currentNode = cameFromNode;
                    }
                }

                return path;
            }

            private bool IsPositionInsideGrid(int2 gridPosition, int2 gridSize)
            {
                return
                    gridPosition.x >= 0 &&
                    gridPosition.y >= 0 &&
                    gridPosition.x < gridSize.x &&
                    gridPosition.y < gridSize.y;
            }

            private int CalculateIndex(int x, int y, int gridWidth) => x + y * gridWidth;

            private int CalculateDistanceCost(int2 aPosition, int2 bPosition)
            {
                int xDistance = math.abs(aPosition.x - bPosition.x);
                int yDistance = math.abs(aPosition.y - bPosition.y);
                int remaining = math.abs(xDistance - yDistance);
                return MOVE_DIAGONAL_COST * math.min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
            }

            private int GetLowestCostFNodeIndex(NativeList<int> openList, NativeArray<PathNode> pathNodeArray)
            {
                PathNode lowestCostPathNode = pathNodeArray[openList[0]];
                for (int i = 1; i < openList.Length; i++)
                {
                    PathNode testPathNode = pathNodeArray[openList[i]];
                    if (testPathNode.fCost < lowestCostPathNode.fCost)
                        lowestCostPathNode = testPathNode;
                }

                return lowestCostPathNode.index;
            }

            private struct PathNode
            {
                public int x;
                public int y;

                public int index;

                public int gCost;
                public int hCost;
                public int fCost;

                public bool isWalkable;

                public int cameFromNodeIndex;

                public void CalculateFCost() => fCost = gCost + hCost;
                public void SetIsWalkable(bool isWalkable) => this.isWalkable = isWalkable;
            }

        }
    }
}
