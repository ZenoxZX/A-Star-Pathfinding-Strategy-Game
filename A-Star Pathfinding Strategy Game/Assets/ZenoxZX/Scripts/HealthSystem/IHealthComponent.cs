using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenoxZX.StrategyGame.HealthSystem
{
    public interface IHealthComponent
    {
        HealthComponent HealthComponent { get; }

        void TakeDamage(int value) => HealthComponent.TakeDamage(value);
        void Heal(int value) => HealthComponent.Heal(value);
    }
}
