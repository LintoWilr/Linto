using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.RDM.Ability
{
    public class 鼓励 : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.Always;
        public int Check()
        {

            if (!PvPRDMOverlay.RDMQt.GetQt("鼓励"))
            {
                return -9;
            }
            if (!PVPHelper.CanActive())
            {
                return -3;
            }
            if (!41494u.GetSpell().IsReadyWithCanCast())
            {
                return -2;
            }
            var aoeCount = PartyHelper.CastableAlliesWithin30.Count;
            if (aoeCount >= PvPRDMSettings.Instance.鼓励人数)
            {
                return 1;
            }
            return -25;
        }

        public void Build(Slot slot)
        {
            slot.Add(PVPHelper.等服务器Spell(41494u, Core.Me));
        }
    }
    public class 光芒四射 : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.Always;
        public int Check()
        {
            if (!PvPRDMOverlay.RDMQt.GetQt("光芒四射"))
            {
                return -9;
            }
            if (!PVPHelper.CanActive())
            {
                return -3;
            }
            if (!Core.Me.HasAura(4322u))
            {
                return -3;
            }
            if (PvPRDMSettings.Instance.鼓励光芒四射 && !Core.Me.HasLocalPlayerAura(2282U))
            {
                return -3;
            }
            if (!PVPHelper.CanActive())
            {
                return -2;
            }
            if (PVPHelper.通用距离检查(25))
            {
                return -5;
            }
            if (PVPHelper.通用技能释放Check(41495u, 25) == null)
            {
                return -6;
            }
            return 1;
        }
        public void Build(Slot slot)
        {
            PVPHelper.通用技能释放(slot, 41495u, 25);
        }
    }
}
