using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenoxZX.StrategyGame.Selection;
using System;

namespace ZenoxZX.StrategyGame
{
    [CreateAssetMenu(fileName = "New Game Config", menuName = GameConstraints.SO_PATH + "Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] Config config;

        public MultipleSelectType MultipleSelectType => config.multipleSelectType;

        [Serializable]
        private class Config
        {
            public MultipleSelectType multipleSelectType;
        }
    }
}
