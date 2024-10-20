using StateMachineJikken.Contract;
using StateMachineJikken.StateMachine;

namespace StateMachineJikken.State;

internal class IdleState : IState
{
    private ErrorWatcherStateMachine _stateMachine;

    public IdleState(ErrorWatcherStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Start()
    {
        throw new NotImplementedException();
    }

    public void ErrorOccurred()
    {
        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} 異常発生！！");
        _stateMachine.StartErrorRecoverTimer();
        _stateMachine.SetState(_stateMachine.AbNormal);
    }

    public void ErrorResolved()
    {
        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} アイドル中に異常解消が来てもなにもしない");
    }
}
