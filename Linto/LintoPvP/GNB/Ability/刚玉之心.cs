using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.SMN;
using System.Linq;

namespace Linto.LintoPvP.GNB.Ability;

public class 刚玉之心 : ISlotResolver
{
    private const uint SpellId = 41443u;

    public SlotMode SlotMode { get; } = SlotMode.Always;

    public int Check()
    {
        if (!PVPHelper.CanActive())
            return -1;

        if (!PvPGNBOverlay.GNBQt.GetQt("刚玉之心"))
            return -9;

        var spell = SpellId.GetSpell();
        if (!spell.IsReadyWithCanCast())
            return -2;

        bool 刚玉队友 = PvPGNBSettings.Instance.刚玉队友;
        float 刚玉血量Threshold = PvPGNBSettings.Instance.刚玉血量 / 100f;
        float 守护之光血量Threshold = PvPSMNSettings.Instance.守护之光血量 / 100f;

        if (刚玉队友)
        {
            // 校验索引有效性
            int targetIndex = PvPGNBSettings.Instance.刚玉对象;
            if (targetIndex < 0 || targetIndex >= PartyHelper.Party.Count)
                return -93;

            IBattleChara specifiedMember = PartyHelper.Party[targetIndex];
            if (specifiedMember != null &&
                specifiedMember.CurrentHpPercent() > 刚玉血量Threshold &&
                Core.Me.CurrentHpPercent() > 守护之光血量Threshold)
                return -93;

            if (specifiedMember != null &&
                specifiedMember.CurrentHpPercent() <= 刚玉血量Threshold &&
                specifiedMember.DistanceToPlayer() > 30)
                return -91;

            IBattleChara member = PartyHelper.CastableParty.FirstOrDefault(chara =>
                chara != null && chara.CurrentHpPercent() <= 刚玉血量Threshold);

            if (member == null)
                return -92;

            if (member.DistanceToPlayer() > 30)
                return -6;
        }
        else
        {
            if (Core.Me.CurrentHpPercent() > 守护之光血量Threshold)
                return -10;
        }

        return 0;
    }

    public void Build(Slot slot)
    {
        bool 刚玉队友 = PvPGNBSettings.Instance.刚玉队友;
        bool 刚玉播报 = PvPGNBSettings.Instance.刚玉播报;
        IBattleChara target = null;

        if (刚玉队友)
        {
            int targetIndex = PvPGNBSettings.Instance.刚玉对象;
            if (targetIndex >= 0 && targetIndex < PartyHelper.Party.Count)
            {
                IBattleChara specifiedTarget = PartyHelper.Party[targetIndex];
                float 刚玉血量Threshold = PvPGNBSettings.Instance.刚玉血量 / 100f;

                if (specifiedTarget != null &&
                    specifiedTarget.CurrentHpPercent() <= 刚玉血量Threshold &&
                    PartyHelper.CastableParty.Contains(specifiedTarget))
                {
                    target = specifiedTarget;
                }
            }

            if (target == null)
            {
                target = PartyHelper.CastableParty.FirstOrDefault(chara =>
                    chara != null && chara.CurrentHpPercent() <= PvPGNBSettings.Instance.刚玉血量 / 100f);
            }

            if (target == null)
                return;
        }
        else
        {
            target = Core.Me;
        }

        slot.Add(new Spell(SpellId, target));

        if (刚玉播报)
            LogHelper.Print($"刚玉目标:{target.Name}");
    }
}