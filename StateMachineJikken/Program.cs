using StateMachineJikken.Model;
using StateMachineJikken.StateMachine;

namespace StateMachineJikken;

internal class Program
{
    static void Main(string[] args)
    {
        var stateMachine = new ErrorWatcherStateMachine(new KeyWatcher());

        stateMachine.CurrentState.Start();

        Thread.Sleep(Timeout.Infinite);
    }
}
