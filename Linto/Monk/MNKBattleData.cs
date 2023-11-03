#region

using Common.Define;

#endregion

namespace Linto.Monk;

public class MNKBattleData
{//其实没啥有用的x
    public static MNKBattleData Instance = new();
    public Queue<string> NextOathQueue =
        new(new string[] { "0,0", "2000,0" });
    public int 我是猛1 = 0;
    public int Oath = 0;
    public Queue<Spell> SpellQueueGCD = new();
    public Queue<Spell> SpellQueueoGCD = new();
    public int time = 0;

    public void Reset()
    {
        Instance = new MNKBattleData();
        SpellQueueGCD.Clear();
        SpellQueueoGCD.Clear();
        NextOathQueue.Clear();
        NextOathQueue.Enqueue("0,0");
        NextOathQueue.Enqueue("2000,0");
    }
}