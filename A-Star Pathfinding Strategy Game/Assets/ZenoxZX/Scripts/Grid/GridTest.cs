using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenoxZX.StrategyGame.GridS
{
    public class GridTest : MonoBehaviour
    {
        private void Start()
        {
            Grid grid = new Grid(4, 2, 10, new Vector3(20, 0));
            gameObject.AddComponent<TMPro.TextMeshPro>();

        }
    }
}
