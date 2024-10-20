using StateMachineJikken.Contract;
using StateMachineJikken.StateMachine;

namespace StateMachineJikken.State;

internal class AbNormalState : IState
{
    private ErrorWatcherStateMachine _stateMachine;

    public AbNormalState(ErrorWatcherStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Start()
    {
        throw new NotImplementedException();
    }

    public void ErrorOccurred()
    {
        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} 異常中に異常発生が来てもなにもしない");
    }

    public void ErrorResolved()
    {
        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} 異常解消！！！！");
        _stateMachine.StopErrorRecoverTimer();
        _stateMachine.SetState(_stateMachine.Idle);
    }
}
