using StateMachineJikken.Contract;
using StateMachineJikken.StateMachine;

namespace StateMachineJikken.State;

internal class BeforeStartState : IState
{
    private ErrorWatcherStateMachine _stateMachine;

    public BeforeStartState(ErrorWatcherStateMachine stateMachine)
    {
        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} 起動しました。");
        _stateMachine = stateMachine;
    }

    public void Start()
    {
        _stateMachine.SetState(_stateMachine.Idle);
    }

    public void ErrorOccurred()
    {
        throw new NotImplementedException();
    }

    public void ErrorResolved()
    {
        throw new NotImplementedException();
    }
}
