using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.BRD
{
    public class 药 : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.Always;
        public uint 技能药 = 29711u;
        public int Check()
        {
            if (!PvPBRDOverlay.BRDQt.GetQt("喝热水"))
            {
                return -9;
            }
            if (!PVPHelper.CanActive())
            {
                return -3;
            }
            if (!29711u.GetSpell().IsReadyWithCanCast() || Core.Me.CurrentMp < 2500)
            {
                return -2;
            }
            if (Core.Me.CurrentHpPercent() <= PvPBRDSettings.Instance.药血量 / 100f)
            {
                return 0;
            }
            return -1;
        }

        public void Build(Slot slot)
        {
            slot.Add(PVPHelper.不等服务器Spell(29711, Core.Me));
        }
    }
}
