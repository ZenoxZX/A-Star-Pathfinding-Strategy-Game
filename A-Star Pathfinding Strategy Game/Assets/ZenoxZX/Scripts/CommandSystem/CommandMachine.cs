using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenoxZX.StrategyGame.CommandSystem
{
    public class CommandMachine
    {
        readonly int maxCommandCount;
        readonly Queue<ICommand> commandQ;

        public CommandMachine() => commandQ = new();
        public CommandMachine(int maxCommandCount)
        {
            this.maxCommandCount = maxCommandCount;
            commandQ = new(maxCommandCount);
        }

        public void Add(ICommand command)
        {
            if (maxCommandCount > 0 && commandQ.Count < maxCommandCount)
                commandQ.Enqueue(command);
        }

        public void RemoveAll() => commandQ.Clear();
        public void ClearAdd(ICommand command)
        {
            RemoveAll();
            Add(command);
        }
        public void Terminate(ICommand command)
        {
            foreach (ICommand item in commandQ)
            {
                if (command == item)
                {
                    item.Terminate();
                    return;
                }
            }
        }

        public void Tick()
        {
            if (commandQ.Count > 0)
            {
                ICommand command = commandQ.Peek();
                if (command.IsTerminated || command.IsCompleted())
                    commandQ.Dequeue();

                else
                    command.Execute();
            }
        }
    }
}
