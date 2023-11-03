using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Linto.Monk.Ability
{
    public class MNKAbility_Potion : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;
        public int Check()
        {
            if (!Qt.GetQt("爆发药") || !Qt.GetQt("爆发"))
            {
                return -1;
            }
            if (AI.Instance.GetGCDCooldown() <= 1200)
            {
                return -2;
            }
            if (MNKSettings.Instance.吃爆发药)
            {
                return -2;
            }
            return -100;
        }

        public void Build(Slot slot)
        {
            slot.Add(Spell.CreatePotion());
            slot.Wait2NextGcd = true;
        }

    }
}