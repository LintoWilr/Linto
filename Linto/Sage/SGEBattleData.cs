namespace Linto.Sage;

public class SGEBattleData
{
    public static SGEBattleData Instance = new();

    public void Reset()
    {
        Instance = new SGEBattleData();
    }
}