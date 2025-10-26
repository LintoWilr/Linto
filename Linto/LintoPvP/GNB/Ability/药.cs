using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.GNB.Ability
{
    public class 药 : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.Always;
        public int Check()
        {
            if (!PvPGNBOverlay.GNBQt.GetQt("喝热水"))
            {
                return -9;
            }
            if (!PVPHelper.CanActive())
            {
                return -3;
            }
            if (Core.Me.CurrentMp < 2500)
            {
                return -2;
            }
            if (Core.Me.CurrentHpPercent() <= PvPGNBSettings.Instance.药血量 / 100f)
            {
                return 0;
            }
            return -1;
        }

        public void Build(Slot slot)
        {
            slot.Add(SpellHelper.GetSpell(29711, SpellTargetType.Self));
        }
    }
}
