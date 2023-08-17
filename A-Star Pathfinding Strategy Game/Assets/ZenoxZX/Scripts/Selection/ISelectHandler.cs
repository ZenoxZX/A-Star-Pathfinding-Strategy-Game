using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenoxZX.StrategyGame.Selection
{
    public interface ISelectHandler
    {
        void DeSelectAll();
        bool SelectSingle(out ISelectable selectable);
        bool SelectMultiple(out IEnumerable<ISelectable> selectables);
        bool GetSelectedUnits(out IEnumerable<ISelectable> selectables);
    }
}
