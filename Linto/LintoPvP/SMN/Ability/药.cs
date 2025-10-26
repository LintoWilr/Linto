using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;

using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SMN.Ability
{
    public class 药 : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.Always;


        public int Check()
        {
            if (!SMNQt.GetQt("喝热水"))
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
            if (Core.Me.CurrentHp <= PvPSMNSettings.Instance.药血量 / 100f)
            {
                return 0;
            }

            return -1;
        }

        public void Build(Slot slot)
        {
            slot.Add(new Spell(29711u, Core.Me));
        }
    }
}
