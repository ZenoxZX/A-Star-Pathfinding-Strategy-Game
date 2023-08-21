using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenoxZX.StrategyGame.CommandSystem
{
    public class MoveCommand : IProgressiveCommand
    {
        readonly Transform transform;
        readonly Vector3 endPosition;
        readonly float speed;

        readonly Vector3 startPosition;
        readonly float reachDistanceSqr;
        readonly float progressMultiplier;

        public MoveCommand(Transform transform, Vector3 endPosition, float speed, float reachDistance = .1f)
        {
            this.transform = transform;
            this.endPosition = endPosition;
            this.speed = speed;

            startPosition = transform.position;
            reachDistanceSqr = reachDistance * reachDistance;
            progressMultiplier = 1 / (endPosition - startPosition).sqrMagnitude;
        }

        public bool IsTerminated { get; private set; } = false;

        public void Execute()
        {
            if (IsTerminated)
                return;

            if ((endPosition - transform.position).sqrMagnitude < reachDistanceSqr)
            {
                Complete();
                return;
            }

            Vector3 direction = endPosition - transform.position;
            transform.position += speed * Time.deltaTime * direction.normalized;
        }

        public void Complete() => transform.position = endPosition;
        public bool IsCompleted() => (endPosition - transform.position).sqrMagnitude < reachDistanceSqr;
        public float Progress() => (endPosition - transform.position).sqrMagnitude * progressMultiplier;
        public void Terminate() => IsTerminated = true;
    }
}
