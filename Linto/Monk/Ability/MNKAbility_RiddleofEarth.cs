using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Linto.Monk.Ability
{
    public class MNKAbility_RiddleofEarth: ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;
        public int Check()
        {
            if (!Qt.GetQt("自动金刚"))
            {
                return -1;
            }
            if (!SpellsDefine.RiddleofEarth.IsReady())
            {
                return -1;
            }
            if (Core.Me.MaxHealth * MNKSettings.Instance.金刚阈值设置 < Core.Me.CurrentHealth)
            { 
                return -2;//血这么多你用个屁
            }
            return 1;
        }
        
        public void Build(Slot slot)
        {
            slot.Add(SpellsDefine.RiddleofEarth.GetSpell());
        }
    }
}   