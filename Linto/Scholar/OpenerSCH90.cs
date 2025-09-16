using CombatRoutine;
using CombatRoutine.Opener;
using Common;
using Common.Define;
using Common.Helper;

namespace Linto.Scholar;

public class OpenerSCH90 : IOpener, ISlotSequence
{
	public List<Action<Slot>> Sequence { get; } = new List<Action<Slot>> { Step0, Step1, Step2 };


	public Action CompeltedAction { get; set; }

	public uint Level { get; } = 90u;


	public int StartCheck()
	{
		if (PartyHelper.NumMembers <= 4)
		{
			CharacterAgent currTarget = UnitHelper.GetCurrTarget(Core.Me);
			if (!currTarget.IsDummy())
			{
				return -100;
			}
		}
		if (!SpellHelper.IsReady(7436u))
		{
			return -5;
		}
		if (!SpellHelper.IsReady(3587u))
		{
			return -6;
		}
		return 0;
	}

	public int StopCheck(int index)
	{
		return -1;
	}

	private static void Step0(Slot slot)
	{
		slot.Add(new Spell(16540u, (SpellTargetType)2));
		slot.Add(new Spell(3587u, (SpellTargetType)1));
	}

	private static void Step1(Slot slot)
	{
		slot.Add(new Spell(17869u, (SpellTargetType)2));
	}

	private static void Step2(Slot slot)
	{
		slot.Add(new Spell(17869u, (SpellTargetType)2));
	}

	public void InitCountDown(CountDownHandler countDownHandler)
	{
		countDownHandler.AddPotionAction(3000);
		countDownHandler.AddAction(1500, 17869u, (SpellTargetType)2);
	}
}
