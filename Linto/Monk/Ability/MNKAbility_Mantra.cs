using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Linto.Monk.Ability
{
    public class MNKAbility_Mantra: ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;
        public int Check()
        {
            if (!Qt.GetQt("自动真言"))
            {
                return -1;
            }
            if (!SpellsDefine.Mantra.IsReady())
            {
                return -1;
            }
            if (MNKSettings.Instance.真言队友)
            { 
                var 真言血量 = PartyHelper.CastableAlliesWithin30
                    .Where(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= MNKSettings.Instance.真言队友阈值设置 && !r.IsInvincible())
                    .OrderBy(r => r.CurrentHealthPercent)
                    .FirstOrDefault();
                if (真言血量.IsValid)
                    return 2;
                return -1;
            }
            if (Core.Me.MaxHealth * (ulong)MNKSettings.Instance.真言阈值设置 < Core.Me.CurrentHealth &&!MNKSettings.Instance.真言队友)
            { 
                return -1;
            }
            return 1;
        }
        
        public void Build(Slot slot)
        {
            slot.Add(SpellsDefine.Mantra.GetSpell());
        }
    }
}   