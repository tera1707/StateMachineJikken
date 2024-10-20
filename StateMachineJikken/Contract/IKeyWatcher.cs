namespace StateMachineJikken.Contract;

internal interface IKeyWatcher
{
    void StartWatchKeyOnce(IReadOnlyDictionary<ConsoleKey, Action> keyToAction);
}
