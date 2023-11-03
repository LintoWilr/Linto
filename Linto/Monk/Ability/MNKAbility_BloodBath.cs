using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Linto.Monk.Ability
{
    public class MNKAbility_BloodBath: ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;
        public int Check()
        {
            if (!Qt.GetQt("自动浴血"))
            {
                return -1;
            }
            if (!SpellsDefine.Bloodbath.IsReady())
            {
                return -1;
            }
            if (Core.Me.MaxHealth * MNKSettings.Instance.浴血阈值设置  <  Core.Me.CurrentHealth)
            {
                return -2;
            }
            return 2;
        }
        
        public void Build(Slot slot)
        {
            slot.Add(SpellsDefine.Bloodbath.GetSpell());
        }
    }
}   