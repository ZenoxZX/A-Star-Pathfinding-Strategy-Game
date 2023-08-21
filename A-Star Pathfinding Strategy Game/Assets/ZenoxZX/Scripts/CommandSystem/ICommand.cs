using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenoxZX.StrategyGame.CommandSystem
{
    public interface ICommand
    {
        bool IsTerminated { get; }
        void Execute();
        void Complete();
        void Terminate();
        bool IsCompleted();
    }

    public interface IProgressiveCommand : ICommand
    {
        float Progress();
    }

    public interface ICommandListener
    {
        public CommandMachine CommandMachine { get; }
        public void AddCommand(ICommand command) => CommandMachine.Add(command);
    }
}
