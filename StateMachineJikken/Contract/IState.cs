namespace StateMachineJikken.Contract;

internal interface IState
{
    void Start();
    void ErrorOccurred();
    void ErrorResolved();
}
