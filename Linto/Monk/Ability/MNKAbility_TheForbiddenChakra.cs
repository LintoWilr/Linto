using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Linto.Monk;

namespace Linto.Monk.Ability
{
    public class MNKAbility_TheforbiddenChakra : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;

        public int Check()
        {
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) > SettingMgr.GetSetting<GeneralSettings>().AttackRange)
            {
                return -1;
            } 
            if (Core.Me.ClassLevel < 15)//等级不够
            {
                return -1;
            }
            if (AI.Instance.GetGCDCooldown()<1200)//检测gcd剩余时间
            {
                return -1;
            }
            if (SpellsDefine.RiddleofFire.IsReady()||SpellsDefine.Brotherhood.IsReady())
            {
                return -1; //红莲和桃园好了先等红莲和桃园放了
            } 
            var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
            if (aoeCount >= 3 &&SpellsDefine.HowlingFist.IsReady() )//敌人计数3并且学了斗气圈
            {
                return -1;
            }
            if (Core.Get<IMemApiMonk>().ChakraCount == 5)
            {
                return 1;
            }

            return -1;
        }


        public void Build(Slot slot)
        {
            slot.Add(SpellsDefine.SteelPeak.GetSpell());//加入斗气斩
        }

    }
}