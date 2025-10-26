using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SAM.GCD;

public class 花车连击 : ISlotResolver
{
    public SlotMode SlotMode { get; }
    public uint 技能雪风 = 29523u;
    public uint 技能月光 = 29524u;
    public uint 技能花车 = 29525u;
    public uint 技能冰雪 = 29526u;
    // public uint 技能满月 = 29527u;
    // public uint 技能樱花 = 29528u;
    // public uint 技能雪释放 = Core.Resolve<MemApiSpell>().CheckActionChange(29523u);
    // public uint 技能月释放 = Core.Resolve<MemApiSpell>().CheckActionChange(29524u);
    // public uint 技能花释放 = Core.Resolve<MemApiSpell>().CheckActionChange(29525u);
    // public bool 单体连 = Core.Resolve<MemApiSpell>().CheckActionChange(29523u) == 29523u;
    // public bool Aoe连 = Core.Resolve<MemApiSpell>().CheckActionChange(29523u) == 29526u;
    // public bool 雪连击中 =(Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 29523u||Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 29526u);
    // public bool 月连击中 =(Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 29524u||Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 29527u);
    // bool 花连击中 =(Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 29525u||Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 29528u);
    // public uint 技能早天 = 29532u;
    public int Check()
    {
        if (!PvPSAMOverlay.SAMQt.GetQt("连击"))
        {
            return -1;
        }
        if (!PVPHelper.CanActive())
        {
            return -2;
        }
        if (GCDHelper.GetGCDCooldown() > 0)
        {
            return -9;
        }
        if (Core.Resolve<MemApiSpell>().CheckActionChange(29523u) == 29523u)//单体连判定
        {
            if (PVPHelper.通用距离检查(5))
            {
                return -5;
            }
            if (PVPHelper.通用技能释放Check(技能雪风, 5) == null)
            {
                return -6;
            }
        }
        return 1;
    }

    public void Build(Slot slot)
    {
        //月连击&单体连
        if ((Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 29524u
             || Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 29527u)
            && Core.Resolve<MemApiSpell>().CheckActionChange(29523u) == 29523u)
        {
            PVPHelper.通用技能释放(slot, 技能花车, 5);
        }
        //雪连击&单体连
        else if ((Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 29523u || Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 29526u) && Core.Resolve<MemApiSpell>().CheckActionChange(29523u) == 29523u)
        {
            PVPHelper.通用技能释放(slot, 技能月光, 5);
        }
        //Aoe连
        else if (Core.Resolve<MemApiSpell>().CheckActionChange(29523u) == 29526u)
        {
            slot.Add(PVPHelper.等服务器Spell(技能冰雪, Core.Me));
        }
        //单体连
        else
        {
            PVPHelper.通用技能释放(slot, 技能雪风, 5);
        }
    }
}