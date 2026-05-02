namespace Linto.LintoPvP.VPR;

public class PvPVPRBattleData
{
    public static PvPVPRBattleData Instance = new();
    public bool 祖灵之牙已完成;

    public static void Reset() => Instance = new PvPVPRBattleData();
}
