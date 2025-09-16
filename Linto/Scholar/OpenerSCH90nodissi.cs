using CombatRoutine;
using CombatRoutine.Opener;
using Common;
using Common.Define;
using Common.Helper;

namespace Linto.Scholar;

public class OpenerSCH90nodissi : IOpener, ISlotSequence
{
	public List<Action<Slot>> Sequence { get; } = new List<Action<Slot>> { Step0, Step1, Step2 };


	public Action CompeltedAction { get; set; }

	public uint Level { get; } = 90u;


	public int StartCheck()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Expected O, but got Unknown
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Expected O, but got Unknown
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected O, but got Unknown
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Expected O, but got Unknown
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Expected O, but got Unknown
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected O, but got Unknown
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Expected O, but got Unknown
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Expected O, but got Unknown
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Expected O, but got Unknown
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Expected O, but got Unknown
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Expected O, but got Unknown
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Expected O, but got Unknown
		if (!Core.Get<IMemApiScholar>().HasPet)
		{
			ChatHelper.SendMessage("/p OK了家人们 <se.1>");
			ChatHelper.SendMessage("/p 全体目光向我看齐嗷 <se.2>");
			ChatHelper.SendMessage("/p 看我看我 <se.3>");
			ChatHelper.SendMessage("/p 我宣布个事 <se.4>");
			ChatHelper.SendMessage("/p 我是个啥比 <se.5>");
			ChatHelper.SendMessage("/p 我没叫小仙女 <se.6>");
			ChatHelper.SendMessage("/p 大家看我表演 <se.7>");
			slot.Add(new Spell(7561u, (SpellTargetType)1));
			slot.Add(new Spell(17215u, (SpellTargetType)1));
			slot.Add(new Spell(166u, (SpellTargetType)1));
			slot.Add(new Spell(16537u, (SpellTargetType)1));
			slot.Add(new Spell(189u, (SpellTargetType)1));
			slot.Add(new Spell(16543u, (SpellTargetType)1));
			slot.Add(new Spell(25867u, (SpellTargetType)1));
			slot.Add(new Spell(16538u, (SpellTargetType)1));
			slot.Add(new Spell(25868u, (SpellTargetType)1));
			slot.Add(new Spell(16542u, (SpellTargetType)1));
			slot.Add(new Spell(3586u, (SpellTargetType)1));
			slot.Add(new Spell(3583u, (SpellTargetType)1));
			slot.Add(new Spell(189u, (SpellTargetType)1));
			slot.Add(new Spell(3585u, (SpellTargetType)1));
			slot.Add(new Spell(189u, (SpellTargetType)1));
			slot.Add(new Spell(3587u, (SpellTargetType)1));
			slot.Add(new Spell(189u, (SpellTargetType)1));
			slot.Add(new Spell(7434u, (SpellTargetType)1));
			slot.Add(new Spell(189u, (SpellTargetType)1));
		}
		slot.Add(new Spell(16540u, (SpellTargetType)2));
		slot.Add(new Spell(166u, (SpellTargetType)1));
	}

	private static void Step1(Slot slot)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		slot.Add(new Spell(17869u, (SpellTargetType)2));
	}

	private static void Step2(Slot slot)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		slot.Add(new Spell(17869u, (SpellTargetType)2));
	}

	public void InitCountDown(CountDownHandler countDownHandler)
	{
		countDownHandler.AddPotionAction(3000);
		countDownHandler.AddAction(1500, 17869u, (SpellTargetType)2);
	}
}
