using CombatRoutine;
using Common;
using Common.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MNK;

public class 武僧小连招 : ISlotSequence
{
    public Action CompeltedAction { get; set; }
    
    public List<Action<Slot>> Sequence { get; } = new List<Action<Slot>> { Step0, Step1, Step2, Step3, Step4};
    public int StartCheck()
	{
		if(!PVPHelper.通用权限())
		{
			return -999999;
		}
		if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) > 20)
		{
			return -5;
		}
		if (PvPMNKSettings.Instance.武僧小连招)
		{
			return 2;
		}
		return -2;
	}

	public int StopCheck(int index)
	{
		return -10;
	}

	private static void Step0(Slot slot)
	{
		PvPMNKSettings.Instance.武僧小连招 = false;
		if(MNKQt.GetQt("轻身步法"))
		{ 
			slot.Add(SpellHelper.GetSpell(29484u));
		}
	}

	private static void Step1(Slot slot)
	{
		if(MNKQt.GetQt("星导脚"))
		{ 
			if(SpellHelper.IsReady(29479u)) 
			{ 
				slot.Add(SpellHelper.GetSpell(29479u)); 
			}
		}
		
	}

	private static void Step2(Slot slot)
	{
		if(MNKQt.GetQt("凤凰舞"))
		{ 
			if ((double)SpellHelper.GetSpell(29481u).Charges > 1) 
			{ 
				slot.Add(SpellHelper.GetSpell(29481u)); 
			}
		}
	}

	private static void Step3(Slot slot)
	{
		if(MNKQt.GetQt("斗气圈"))
		{ 
			if (SpellHelper.IsReady(29480u)) 
			{ 
				slot.Add(SpellHelper.GetSpell(29480u)); 
			}
		}
	}
	
	private static void Step4(Slot slot)
	{
		if(MNKQt.GetQt("陨石冲击"))
		{ 
			if (Core.Get<IMemApiLimitBreak>().GetLimitBreakCurrentValue() == 2500)
			{
				slot.Add(SpellHelper.GetSpell(29485u)); 
			}
			slot.Wait2NextGcd = true;
		}
	}


}