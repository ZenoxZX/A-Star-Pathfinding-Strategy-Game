using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenoxZX.StrategyGame.Modules;

namespace ZenoxZX.StrategyGame
{
    public class BuildingUnit : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            gameObject.GetComponent<IModule<Transform>>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
