using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenoxZX.StrategyGame.CommandSystem
{
    public interface ICommand
    {
        void Execute();
        void Complete();
        void Terminate();
        bool IsCompleted();
        bool IsTerminated { get; }
    }

    public interface IProgressiveCommand : ICommand
    {
        float Progress();
    }
}
