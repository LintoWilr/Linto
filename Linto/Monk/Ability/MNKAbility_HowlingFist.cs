using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Linto.Monk;

namespace Linto.Monk.Ability;

public class MNKAbility_HowlingFist : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;
    public int Check()
    {
        if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) > SettingMgr.GetSetting<GeneralSettings>().AttackRange)
        {
            return -1;
        } 
        if(AI.Instance.GetGCDCooldown()<750)//检测gcd剩余时间
        {
            return -1;
        }
        if (Core.Me.ClassLevel < 40)//等级不够 没学
        {
            return -1;
        }
        if (SpellsDefine.RiddleofFire.IsReady())
        {
            return -1; //红莲好了先等红莲
        } 
        var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
        if (aoeCount >= 3 &&Core.Get<IMemApiMonk>().ChakraCount == 5 )//敌人计数3+学了斗气圈+查克拉满了
        {
            return 1;
        }

        return -1;
    }


    public void Build(Slot slot)
    {
        slot.Add(SpellsDefine.HowlingFist.GetSpell());//加入斗气圈
    }

}
