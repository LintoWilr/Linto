using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Linto.Monk.Ability
{
    public class MNKAbility_Brotherhood : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;
        public int Check()
        {
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) > SettingMgr.GetSetting<GeneralSettings>().AttackRange)
            {
                return -1;
            }
            if (!Qt.GetQt("爆发"))
            {
                return -1;
            }
            if (!Core.Me.HasMyAura(AurasDefine.RiddleOfFire))
            {
                return -1;  
            }
            if (AI.Instance.GetGCDCooldown()<750)//检测gcd剩余时间
            {
                return -1;
            }
            if (!SpellsDefine.Brotherhood.IsReady())
            {
                return -1;
            }


            return 0;
        }


        public void Build(Slot slot)
        {
            slot.Add(SpellsDefine.Brotherhood.GetSpell());
        }

    }
}