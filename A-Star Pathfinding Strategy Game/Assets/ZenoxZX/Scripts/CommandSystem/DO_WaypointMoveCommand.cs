using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenoxZX.StrategyGame.CommandSystem
{
    public class DO_WaypointMoveCommand : IProgressiveCommand
    {
        readonly Transform transform;
        readonly float speed;

        readonly float reachDistanceSqr;
        readonly Queue<Vector3> positionQ;
        readonly Vector3 endPoint;
        readonly int totalWaypointCount;
        readonly float progressMultiplier;

        public DO_WaypointMoveCommand(Transform transform, Vector3[] wayPointArray, float speed, float reachDistance = .1f)
        {
            this.transform = transform;
            this.speed = speed;

            reachDistanceSqr = reachDistance * reachDistance;
            totalWaypointCount = wayPointArray.Length;
            progressMultiplier = 1 / totalWaypointCount;
            positionQ = new Queue<Vector3>(wayPointArray);

            endPoint = wayPointArray[totalWaypointCount - 1];
        }

        public bool IsTerminated { get; private set; } = false;

        public void Execute()
        {
            if (IsTerminated)
                return;

            if (positionQ.Count <= 0)
            {
                Complete();
                return;
            }

            Vector3 endPosition = positionQ.Peek();

            if ((endPosition - transform.position).sqrMagnitude <= reachDistanceSqr)
                positionQ.Dequeue();

            else
            {
                Vector3 direction = endPosition - transform.position;
                transform.position += speed * Time.deltaTime * direction.normalized;
            }
        }

        public void Complete() => transform.position = endPoint;
        public bool IsCompleted() => positionQ.Count <= 0;
        public float Progress() => (totalWaypointCount - positionQ.Count) * progressMultiplier;
        public void Terminate() => IsTerminated = true;
    }
}
