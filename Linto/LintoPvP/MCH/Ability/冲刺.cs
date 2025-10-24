using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist. MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.Ability;
public class 冲刺 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	public uint 技能净化 = 29056;

	public int Check ()
	{
		var me = Core.Me;
		if (!PvPMCHOverlay.MCHQt.GetQt("冲刺"))
		{
			return -9;
		}
		if (me.HasAura(1342u))
		{
			return -2;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (GCDHelper.GetGCDCooldown()!=0)
		{
			return -4;
		}
		if (Core. Resolve<MemApiSpellCastSuccess> (). LastSpell == 29057u)
			{
			return -99;
			}
		return 0;
	}
	public void Build(Slot slot)
	{
		var me = Core.Me;
		slot.Add(new Spell(29057u,me));
	}
}