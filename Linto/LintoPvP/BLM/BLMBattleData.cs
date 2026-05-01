namespace Linto.LintoPvP.BLM;

public class PvPBLMBattleData
{
    public static PvPBLMBattleData Instance = new();

    public static void Reset() => Instance = new PvPBLMBattleData();
}
