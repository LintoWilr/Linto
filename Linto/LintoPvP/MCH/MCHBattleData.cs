namespace Linto.LintoPvP.MCH;

public class PvPMCHBattleData
{
    public static PvPMCHBattleData Instance = new();

    public void Reset() => Instance = new PvPMCHBattleData();
}
