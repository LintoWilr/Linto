using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Linto.Monk;

namespace Linto.Monk.Ability
{
    public class MNKAbility_PerfectBalance : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;
        public int Check()
        {
            if(!MNKSettings.Instance.爆发)
            {
                return -1; //爆发开关控制
            }
            if (!SpellsDefine.PerfectBalance.IsReady())
            {
                return -1; //震脚没好，没学？不打！
            } 
            if (AI.Instance.GetGCDCooldown()<600)
            {
                return -1;//gcd剩余时间不够！不打！
            }
            if (Core.Me.HasMyAura(AurasDefine.PerfectBalance))
            {
                return -1;  //开了震脚？不打！
            }
            if (Core.Me.HasMyAura(AurasDefine.FormlessFist))
            {
                return -1;  //有演武？不打！
            }
            if ((!Core.Me.HasMyAuraWithTimeleft(AurasDefine.RiddleOfFire, 6000)) && (SpellsDefine.RiddleofFire.GetSpell().Cooldown.TotalMilliseconds > 6000))
            {
                return -3;  //红莲快结束了？不打！
            }
            if ((Core.Me.HasMyAuraWithTimeleft(AurasDefine.Brotherhood, 6000)) && (Core.Me.HasMyAuraWithTimeleft(AurasDefine.RiddleOfFire, 6000)))
            {
                return 2;  //时间充足，打！
            }
            if (SpellsDefine.RiddleofFire.GetSpell().Cooldown.TotalMilliseconds > 5000) 
            {
                return -1;  //红莲cd大于5000?不打!
            }
            if(!(Core.Get<IMemApiMonk>().ActiveNadi == NaDi.Solar))
            {
                return 1;  //没有阳？打！
            }
            return 0;
        }


        public void Build(Slot slot)
        {
            MNKSettings.Instance.阴Step = 0 ;
            MNKSettings.Instance.阳Step = 0 ;
            MNKSettings.Instance.续过破碎了= 0 ;
            MNKSettings.Instance.续过功力了= 0 ;
            MNKSettings.Instance.阳必杀1型= 0 ;
            slot.Add(SpellsDefine.PerfectBalance.GetSpell());
        }

    }
}