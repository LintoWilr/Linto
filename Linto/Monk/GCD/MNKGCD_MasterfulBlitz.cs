using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Linto.Monk.GCD
{
    public class MNKGCDMasterfulBlitz : ISlotResolver
    {
        
        public SlotMode SlotMode { get; } = SlotMode.Gcd;
        public int Check()
        {

            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) > SettingMgr.GetSetting<GeneralSettings>().AttackRange)
            {
                return -1;
            }
          
            
            if (Core.Get<IMemApiMonk>().BlitzTimer.TotalSeconds <= 0)
            {
                return -2;
            }
            

            if(SpellsDefine.Brotherhood.GetSpell().Cooldown.TotalMilliseconds < 2000 )
            {
                return -4;
            }
            
            if (SpellsDefine.PerfectBalance.IsReady())
            {
                return 1;
            }
            return 0;
        }

        public void Build(Slot slot)
        {
            slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.MasterfulBlitz.GetSpell().Id).GetSpell());
            MNKSettings.Instance.阳Step = 0;
            MNKSettings.Instance.阴Step = 0;
            MNKSettings.Instance.阳必杀 = 0;
            MNKSettings.Instance.阴必杀 = 0;
        }
    }
}