using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenoxZX.StrategyGame.HealthSystem;

namespace ZenoxZX.StrategyGame.CommandSystem
{
    public class AttackCommand<TEnemy> : IProgressiveCommand where TEnemy : Component, IHealthComponent
    {
        readonly Transform transform;
        readonly int damage;
        readonly float castTime;
        readonly Action OnAttackCall;
        readonly bool terminateOnOutOfRange;

        readonly IHealthComponent enemyHC;
        readonly Transform enemyTransform;
        readonly float attackRangeSqr;
        readonly float progressMultiplier;

        private float completeTime;

        public AttackCommand(TEnemy enemy, Transform transform, int damage, float attackRange, float castTime, Action OnAttackCall, bool terminateOnOutOfRange = false)
        {
            this.transform = transform;
            this.damage = damage;
            this.castTime = castTime;
            this.OnAttackCall = OnAttackCall;
            this.terminateOnOutOfRange = terminateOnOutOfRange;

            enemyHC = enemy;
            enemyTransform = enemy.transform;
            completeTime = Time.time + castTime;
            attackRangeSqr = attackRange * attackRange;
            progressMultiplier = 1 / castTime;
        }

        public bool IsTerminated { get; private set; } = false;

        public void Complete()
        {
            enemyHC.TakeDamage(damage);
            OnAttackCall?.Invoke();
            Terminate();
        }

        public void Execute()
        {
            if (IsTerminated)
                return;

            if ((enemyTransform.position - transform.position).sqrMagnitude < attackRangeSqr)
            {
                if (Time.time >= completeTime)
                    Complete();
            }

            else
            {
                if (terminateOnOutOfRange)
                    Terminate();
                else
                    completeTime = Time.time + castTime;
            }
        }

        public bool IsCompleted() => Time.time >= completeTime;
        public void Terminate() => IsTerminated = true;
        public float Progress() => (castTime - Time.time) * progressMultiplier;
    }
}
