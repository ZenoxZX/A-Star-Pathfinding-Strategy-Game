using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenoxZX.StrategyGame.CommandSystem
{
    public class WaypointMoveCommand : IProgressiveCommand
    {
        readonly Transform transform;
        readonly Queue<MoveCommand> moveCommandQ;
        readonly Vector3 endPoint;

        readonly int totalWaypointCount;
        readonly float progressMultiplier;

        public WaypointMoveCommand(Transform transform, Vector3[] wayPointArray, float speed, float reachDistance = .1f)
        {
            this.transform = transform;

            totalWaypointCount = wayPointArray.Length;
            progressMultiplier = 1 / totalWaypointCount;
            moveCommandQ = new Queue<MoveCommand>(totalWaypointCount);

            for (int i = 0; i < totalWaypointCount; i++)
                moveCommandQ.Enqueue(new MoveCommand(transform, wayPointArray[i], speed, reachDistance));

            endPoint = wayPointArray[totalWaypointCount - 1];
        }

        public bool IsTerminated { get; private set; } = false;

        public void Execute()
        {
            if (IsTerminated)
                return;
            
            if (moveCommandQ.Count <= 0)
            {
                Complete();
                return;
            }

            MoveCommand moveCommand = moveCommandQ.Peek();

            if (moveCommand.IsCompleted())
                moveCommandQ.Dequeue();

            else
                moveCommand.Execute();
        }

        public void Complete() => transform.position = endPoint;
        public bool IsCompleted() => moveCommandQ.Count <= 0;
        public float Progress() => (totalWaypointCount - moveCommandQ.Count) * progressMultiplier;
        public void Terminate() => IsTerminated = true;
    }
}
