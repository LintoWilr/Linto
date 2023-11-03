using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Linto.Monk.Ability
{
    public class MNKAbility_RiddleofFire : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;
      //  public double GCDCooldown => AI.Instance.GetBaseGCDSpell().Cooldown.TotalMilliseconds;
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
            if(AI.Instance.GetGCDCooldown()>750)//检测gcd剩余时间
            {
                return -1;
            }

            if (!SpellsDefine.RiddleofFire.IsReady())
            {
                return -1;
            }

            return 0;
        }


        public void Build(Slot slot)
        {
          //  slot.Add(new SlotAction(,0,SpellsDefine.))
            slot.Add(SpellsDefine.RiddleofFire.GetSpell());
        }

    }
}