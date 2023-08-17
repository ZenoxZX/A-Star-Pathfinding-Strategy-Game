using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenoxZX.StrategyGame.Selection
{
    public interface ISelectable
    {
        void Select();
        void DeSelect();
        bool Selected { get; }
    }
}
