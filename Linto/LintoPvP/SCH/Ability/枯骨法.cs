using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SCH.Ability;

public class 枯骨法 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;


	public int Check()
	{
		if(!PVPHelper.通用权限())
		{
			return -999999;
		}
		if (!SCHQt.GetQt("枯骨法"))
		{
			return -9;
		}
		if(MoveHelper.IsMoving())
		{
			return -10;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (!SpellHelper.IsReady(29235u))
		{
			return -2;
		}
		CharacterAgent Me = Core.Me;
		var 扇形数量 =TargetHelper.GetEnemyCountInsideSector(Core.Me, Core.Me.GetCurrTarget(), 8f, 120f);
		if (扇形数量>PvPSCHSettings.Instance.枯骨法数量)
		{
			return 0;
		}
		return -3;
	}

	public void Build(Slot slot)
	{
		slot.Add(SpellHelper.GetSpell(29235u));
	}
}
