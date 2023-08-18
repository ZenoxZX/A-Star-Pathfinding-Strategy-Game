using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenoxZX.StrategyGame.HealthSystem
{
    public interface IHealthComponent
    {
        HealthComponent HealthComponent { get; }

        void TakeDamage(float value) => HealthComponent.TakeDamage(value);
        void Heal(float value) => HealthComponent.Heal(value);
    }
}
