using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;
using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Objects;
using ECommons.GameFunctions;
using Dalamud.Plugin.Services;

namespace Linto.LintoPvP.GNB.GCD;

public class 利刃斩 : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check()//29098 利刃斩
	{
		{
		
			if(!PvPGNBOverlay.GNBQt.GetQt("爆发击连击"))
			{
				return -1;
			}
			if (!PVPHelper.CanActive())
			{
				return -1;
			}
			if (PVPHelper.通用距离检查(5))
			{
				return -5;
			}
			if (Core.Resolve<MemApiSpell>().CheckActionChange(29102)!=29102u)
			{
				return -4;
			}
			if (!Core.Resolve<MemApiSpell>().CheckActionChange(29098).GetSpell().IsReadyWithCanCast())
			{
				return -3;
			}
			if (PVPHelper.通用技能释放Check(29098u,5)==null)
			{
				return -5;
			}
			if (PVPHelper.通用技能释放Check(29098u,5)==null)
			{
				return -5;
			}
			return 0;
		}
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,29098u,5);
	}
}
