using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Linto.Monk.Ability
{
    public class MNKAbility_SecondWind : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;
        public int Check()
        {
            if (!Qt.GetQt("自动内丹"))
            {
                return -1;
            }
            if(!SpellsDefine.SecondWind.IsReady())
            {
                return -1; 
            }
            if (Core.Me.MaxHealth * MNKSettings.Instance.内丹阈值设置  <  Core.Me.CurrentHealth)
            {
                return -2;
            }
            return 1;
        }


        public void Build(Slot slot)
        {
            slot.Add(SpellsDefine.SecondWind.GetSpell());
        }

    }
}