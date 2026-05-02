namespace Linto.LintoPvP.MCH;

public class PvPMCHBattleData
{
    public static PvPMCHBattleData Instance = new();

    public ulong WildfireTargetEntityId { get; private set; }
    public long WildfireWindowEndMs { get; private set; }
    public long MarksmanPreAnimEndMs { get; private set; }
    public uint LastSuccessfulSpellId { get; private set; }

    public static void Reset() => Instance = new PvPMCHBattleData();

    public void ResetState()
    {
        WildfireTargetEntityId = 0;
        WildfireWindowEndMs = 0;
        MarksmanPreAnimEndMs = 0;
        LastSuccessfulSpellId = 0;
    }

    public void RecordWildfireWindow(ulong targetEntityId, int durationMs)
    {
        WildfireTargetEntityId = targetEntityId;
        WildfireWindowEndMs = Environment.TickCount64 + durationMs;
    }

    public void ClearWildfireWindow() => ResetWildfireWindow();

    public void ConfirmWildfireWindow() => WildfireWindowEndMs = Environment.TickCount64 + 8000L;

    public void PreOpenWildfireWindow() => WildfireWindowEndMs = Environment.TickCount64 + 2000L;

    public void RecordMarksmanPreAnim() => MarksmanPreAnimEndMs = Environment.TickCount64 + 1500L;

    public void ClearMarksmanPreAnim() => MarksmanPreAnimEndMs = 0;

    public void MarkSuccessfulSpell(uint spellId) => LastSuccessfulSpellId = spellId;

    public bool InWildfireWindow => WildfireTargetEntityId != 0 && Environment.TickCount64 < WildfireWindowEndMs;

    public bool InMarksmanPreAnim => Environment.TickCount64 < MarksmanPreAnimEndMs;

    private void ResetWildfireWindow()
    {
        WildfireTargetEntityId = 0;
        WildfireWindowEndMs = 0;
    }
}
