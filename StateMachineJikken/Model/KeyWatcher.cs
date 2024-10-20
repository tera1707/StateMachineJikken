using StateMachineJikken.Contract;

namespace StateMachineJikken.Model;

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
