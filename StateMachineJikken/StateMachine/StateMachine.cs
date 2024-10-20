using StateMachineJikken.Contract;
using StateMachineJikken.State;

namespace StateMachineJikken.StateMachine;

internal class ErrorWatcherStateMachine
{
    public IState BeforeStart;
    public IState Idle;
    public IState AbNormal;

    public IState CurrentState;

    private IKeyWatcher _keyWatcher;

    private System.Timers.Timer _errorRecoverTimer;

    public ErrorWatcherStateMachine(IKeyWatcher keyWatcher)
    {
        var keyToAction = new Dictionary<ConsoleKey, Action>()
        {
            { ConsoleKey.RightArrow, RightArrowKeyPushed },
            { ConsoleKey.LeftArrow, LeftArrowKeyPushed },
        };
        _keyWatcher = keyWatcher;
        _keyWatcher.StartWatchKeyOnce(keyToAction);

        _errorRecoverTimer = new System.Timers.Timer(TimeSpan.FromSeconds(5)) { AutoReset = false };
        _errorRecoverTimer.Elapsed += ErrorRecoverTimerEllapsed;

        BeforeStart = new BeforeStart(this);
        Idle = new Idle(this);
        AbNormal = new AbNormal(this);

        CurrentState = BeforeStart;
    }

    // 状態遷移用メソッド

    public void SetState(IState dest)
    {
        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} 状態遷移：{CurrentState} → {dest}\r\n");
        CurrentState = dest;
    }

    // 処理用メソッド

    public void StartErrorRecoverTimer()
    {
        _errorRecoverTimer.Start();
    }

    public void StopErrorRecoverTimer()
    {
        _errorRecoverTimer.Stop();
    }

    // イベント

    private void ErrorRecoverTimerEllapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        CurrentState.ErrorResolved();
    }

    private void RightArrowKeyPushed()
    {
        CurrentState.ErrorOccurred();
    }

    private void LeftArrowKeyPushed()
    {
        CurrentState.ErrorResolved();
    }
}
