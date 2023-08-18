using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenoxZX.StrategyGame.HealthSystem;
using ZenoxZX.StrategyGame.Selection;

namespace ZenoxZX.StrategyGame
{
    public abstract class UnitBase : MonoBehaviour, IHealthComponent, ISelectable
    {
        [SerializeField] protected UnitStatBaseSO unitStats;

        #region IHealthComponent Implementation
        public HealthComponent HealthComponent { get; private set; }
        protected virtual void OnHeal(float value) { }
        protected virtual void OnTakeDamage(float value) { }

        #endregion

        #region ISelectable Implementation
        public bool Selected { get; private set; } = false;
        public virtual void DeSelect() => Selected = false;
        public virtual void Select() => Selected = true;

        #endregion

        #region MONO

        public virtual void Awake()
        {
            HealthComponent = new HealthComponent(unitStats.health, 0, OnHeal: OnHeal, OnTakeDamage: OnTakeDamage);
        }

        #endregion
    }
}
