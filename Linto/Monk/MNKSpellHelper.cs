using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using System.Runtime.InteropServices;

namespace Linto.Monk
{
    internal class MNKSpellHelper
    {
        public static uint LastBaseGcd = 0;


        public static Spell GetBaseGCDSpell() //军体拳
        {
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) < SettingMgr.GetSetting<GeneralSettings>().AttackRange)
            {                
                if (Qt.GetQt("AOE")) 
                {
                    var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
                    if (aoeCount >= 3 &&(SpellsDefine.ArmOfTheDestroyer.IsReady()) )//敌人计数3并且学了AOE1（达到26级）
                    {
                        if ((Core.Me.HasMyAura(AurasDefine.OpoOpoForm)) ) //有魔猿型（1型）
                        {
                            return SpellsDefine.ArmOfTheDestroyer.GetSpell(); //学了AOE1打AOE1
                        }
                        if ((Core.Me.HasMyAura(AurasDefine.RaptorForm)) )//有盗龙型（2型）
                        { 
                            if(SpellsDefine.FourPointFury.IsReady()) //学了四面脚打四面脚
                                return SpellsDefine.FourPointFury.GetSpell(); 
                            if(!Core.Me.HasMyAuraWithTimeleft(AurasDefine.DisciplinedFist, 6000))//功力时间不足，打双掌
                                return SpellsDefine.TwinSnakes.GetSpell(); 
                            return SpellsDefine.ArmOfTheDestroyer.GetSpell(); //啥都不会就打AOE1吧
                        }
                        if ((Core.Me.HasMyAura(AurasDefine.CoeurlForm)) )//有猛豹型（3型）
                        {
                            if(SpellsDefine.Rockbreaker.IsReady()) //学了地裂劲打地裂劲
                                return SpellsDefine.Rockbreaker.GetSpell(); 
                            return SpellsDefine.ArmOfTheDestroyer.GetSpell(); //啥都不会就打AOE1吧
                        }
                        return SpellsDefine.ArmOfTheDestroyer.GetSpell(); //其他情况就打AOE1吧
                    }
                }
                if (LastBaseGcd != 0 && Core.Get<IMemApiSpell>().GetComboTimeLeft().TotalMilliseconds < 300)
                    LastBaseGcd = 0;    
                if (Core.Me.HasMyAura(AurasDefine.RaptorForm) && (SpellsDefine.TrueStrike.IsReady() && (!Core.Me.HasMyAuraWithTimeleft(AurasDefine.DisciplinedFist, 6000)))||//2型，学了正拳（大于4级）+功力时间小于6秒
                    (Core.Me.HasMyAura(AurasDefine.FormlessFist))&& (!Core.Me.HasMyAuraWithTimeleft(AurasDefine.DisciplinedFist, 6000)))//或者演武+功力时间小于6秒
                {
                    if (SpellsDefine.TwinSnakes.IsReady()) //学了双掌
                        return SpellsDefine.TwinSnakes.GetSpell();  //打双掌
                    return SpellsDefine.TrueStrike.GetSpell();  //没学打正拳
                } 
                if (Core.Me.HasMyAura(AurasDefine.RaptorForm) && (SpellsDefine.TrueStrike.IsReady() && (Core.Me.HasMyAuraWithTimeleft(AurasDefine.DisciplinedFist, 6000))))  //2型，学了正拳（大于4级），功力时间大于6秒
                {
                    return SpellsDefine.TrueStrike.GetSpell();  //正拳
                } 
                if (Core.Me.HasMyAura(AurasDefine.RaptorForm) && (!SpellsDefine.TrueStrike.IsReady() ))  //2型，没学正拳(4级)
                {
                    return SpellsDefine.Bootshine.GetSpell();  //打连击
                } 
                if (Core.Me.HasMyAura(AurasDefine.CoeurlForm) &&(SpellsDefine.SnapPunch.IsReady()&& !Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.Demolish, 6000))|| 
                    (Core.Me.HasMyAura(AurasDefine.FormlessFist)&& !Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.Demolish, 6000)))//3型，学了崩拳（大于6级），破碎时间小于6秒 或者演武+破碎时间小于6秒
                {             
                    if(SpellsDefine.Demolish.IsReady())//学了破碎
                        return SpellsDefine.Demolish.GetSpell();    //上破碎
                    return SpellsDefine.SnapPunch.GetSpell();//打崩拳
                }
                if (Core.Me.HasMyAura(AurasDefine.CoeurlForm) &&(SpellsDefine.SnapPunch.IsReady()&& Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.Demolish, 6000)))//3型，学了崩拳（大于6级），破碎时间大于6秒
                {             
                    return SpellsDefine.SnapPunch.GetSpell();//打崩拳
                }
                if (Core.Me.HasMyAura(AurasDefine.CoeurlForm)&&!SpellsDefine.SnapPunch.IsReady())//3型，没学崩拳(6级)
                {             
                    return SpellsDefine.Bootshine.GetSpell();//打连击
                }
                if ((Core.Me.HasMyAura(AurasDefine.OpoOpoForm) && Core.Me.HasMyAura(AurasDefine.LeadenFist)))//1型+连击效果提高
                {
                    return SpellsDefine.Bootshine.GetSpell();   //打连击
                }
                if ((Core.Me.HasMyAura(AurasDefine.OpoOpoForm) && !Core.Me.HasMyAura(AurasDefine.LeadenFist))|| //1型+没有连击效果提高或者演武+功力时间大于6秒
                    ((Core.Me.HasMyAura(AurasDefine.FormlessFist))&& !Core.Me.HasMyAuraWithTimeleft(AurasDefine.DisciplinedFist, 6000)))
                {
                    if (SpellsDefine.DragonKick.IsReady())//学了双龙
                        return SpellsDefine.DragonKick.GetSpell();   //踹双龙
                    return SpellsDefine.Bootshine.GetSpell();   //没学双龙打连击
                }
                if (SpellsDefine.DragonKick.IsReady())//学了双龙
                {
                    return SpellsDefine.DragonKick.GetSpell();   //踹双龙
                }
                return SpellsDefine.Bootshine.GetSpell(); //啥都没法打就打连击
            }
            if (Core.Get<IMemApiMonk>().ChakraCount >= 5 && SpellsDefine.FormShift.IsReady())//豆子满了,学了演武
                return SpellsDefine.FormShift.GetSpell();    //演武
            return SpellsDefine.Meditation.GetSpell(); //搓豆子
            
        }


    }
}
