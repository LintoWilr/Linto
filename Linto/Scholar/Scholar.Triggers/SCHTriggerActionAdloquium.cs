using CombatRoutine;
using CombatRoutine.TriggerModel;
using Common.Language;

namespace Linto.Scholar.Scholar.Triggers;

public class SCHTriggerActionAdloquium : ITriggerAction, ITriggerBase
{
	public string DisplayName => LanguageHelper.Loc("SCH/一键扩散盾");

	string ITriggerBase.Remark { get; set; }

	public bool Draw()
	{
		return true;
	}

	public int Check()
	{
		if (!SpellHelper.IsReady(16542u) || !SpellHelper.IsReady(3585u))
		{
			return -1;
		}
		return 1;
	}

	public bool Handle()
	{
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		if (SpellHelper.IsReady(25867u))
		{
			if (AI.Instance.BattleData.NextSlot != null)
			{
				AI.Instance.BattleData.NextSlot.Add(SpellHelper.GetSpell(16542u));
				AI.Instance.BattleData.NextSlot.Add(SpellHelper.GetSpell(25867u));
				AI.Instance.BattleData.NextSlot.Add(SpellHelper.GetSpell(185u));
				AI.Instance.BattleData.NextSlot.Add(SpellHelper.GetSpell(3585u));
			}
			else
			{
				AI.Instance.BattleData.NextSlot = new Slot(2500).Add(SpellHelper.GetSpell(16542u));
				AI.Instance.BattleData.NextSlot.Add(SpellHelper.GetSpell(25867u));
				AI.Instance.BattleData.NextSlot.Add(SpellHelper.GetSpell(185u));
				AI.Instance.BattleData.NextSlot.Add(SpellHelper.GetSpell(3585u));
			}
			return true;
		}
		if (AI.Instance.BattleData.NextSlot != null)
		{
			AI.Instance.BattleData.NextSlot.Add(SpellHelper.GetSpell(16542u));
			AI.Instance.BattleData.NextSlot.Add(SpellHelper.GetSpell(185u));
			AI.Instance.BattleData.NextSlot.Add(SpellHelper.GetSpell(3585u));
		}
		else
		{
			AI.Instance.BattleData.NextSlot = new Slot(2500).Add(SpellHelper.GetSpell(16542u));
			AI.Instance.BattleData.NextSlot.Add(SpellHelper.GetSpell(185u));
			AI.Instance.BattleData.NextSlot.Add(SpellHelper.GetSpell(3585u));
		}
		return true;
	}
}
