namespace StateMachineJikken;

internal class Program
{
    static void Main(string[] args)
    {
        var stateMachine = new StateMachine(new KeyWatcher());

        stateMachine.CurrentState.Start();

        Thread.Sleep(Timeout.Infinite);
    }
}

internal interface IKeyWatcher
{
    void StartWatchKeyOnce(IReadOnlyDictionary<ConsoleKey, Action> keyToAction);
}

internal class KeyWatcher : IKeyWatcher
{
    public void StartWatchKeyOnce(IReadOnlyDictionary<ConsoleKey, Action> keyToAction)
    {
        Task.Run(() =>
        {
            //処理ループ
            while (true)
            {
                //Console.WriteLine("キー待ち受け開始...");
                var key = Console.ReadKey().Key;
                Console.WriteLine(key);

                if (keyToAction.ContainsKey(key))
                {
                    //Console.WriteLine("指定のキーが押されたので、指定の処理を実行して監視を終了します。");
                    keyToAction[key].Invoke();
                }
            }
        });
    }
}

internal interface IState
{
    void Start();
    void ErrorOccurred();
    void ErrorResolved();
}

internal class BeforeStart : IState
{
    private StateMachine _stateMachine;

    public BeforeStart(StateMachine stateMachine)
    {
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

internal class Idle : IState
{
    private StateMachine _stateMachine;

    public Idle(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Start()
    {
        throw new NotImplementedException();
    }

    public void ErrorOccurred()
    {
        Console.WriteLine("異常発生！！");
        _stateMachine.StartErrorRecoverTimer();
        _stateMachine.SetState(_stateMachine.AbNormal);
    }

    public void ErrorResolved()
    {
        Console.WriteLine("アイドル中に異常解消が来てもなにもしない");
    }
}

internal class AbNormal : IState
{
    private StateMachine _stateMachine;

    public AbNormal(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Start()
    {
        throw new NotImplementedException();
    }

    public void ErrorOccurred()
    {
        Console.WriteLine("異常中に異常発生が来てもなにもしない");
    }

    public void ErrorResolved()
    {
        Console.WriteLine("異常解消！！！！");
        _stateMachine.StopErrorRecoverTimer();
        _stateMachine.SetState(_stateMachine.Idle);
    }
}

internal class StateMachine
{
    public IState BeforeStart;
    public IState Idle;
    public IState AbNormal;

    public IState CurrentState;

    private IKeyWatcher _keyWatcher;

    private System.Timers.Timer _errorRecoverTimer;

    public StateMachine(IKeyWatcher keyWatcher)
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

    public void SetState(IState dest)
    {
        Console.WriteLine($"状態遷移：{CurrentState} → {dest}");
        CurrentState = dest;
    }

    public void StartErrorRecoverTimer()
    {
        _errorRecoverTimer.Start();
    }

    public void StopErrorRecoverTimer()
    {
        _errorRecoverTimer.Stop();
    }

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




