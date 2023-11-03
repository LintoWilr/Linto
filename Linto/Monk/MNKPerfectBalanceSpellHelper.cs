using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using System.Runtime.InteropServices;

namespace Linto.Monk
{
    internal class MNKPerfectBalanceSpellHelper
    {
        public static uint LastBaseGcd = 0;


        public static Spell GetPerfectBalanceSpell() //震脚
        {
            if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) < SettingMgr.GetSetting<GeneralSettings>().AttackRange)
            {   
                if (Qt.GetQt("AOE")) //AOE逻辑
                {
                    var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
                    if (aoeCount >= 3 )//敌人计数3
                    {
                        if(Core.Get<IMemApiMonk>().ActiveNadi == NaDi.Both||Core.Me.ClassLevel < 60)//有阴阳脉轮或者未达到60级
                        { 
                            if(!Core.Me.HasMyAuraWithTimeleft(AurasDefine.DisciplinedFist, 6000))//功力时间不够
                            { 
                                return SpellsDefine.FourPointFury.GetSpell(); //打四面脚
                            } 
                            return SpellsDefine.ArmOfTheDestroyer.GetSpell(); //打AOE1
                        }
                        if(Core.Get<IMemApiMonk>().ActiveNadi != NaDi.Solar)//没有阳脉轮
                        { 
                            if(MNKSettings.Instance.阳Step == 0) //第0步判断
                            { 
                                    if(!Core.Me.HasMyAuraWithTimeleft(AurasDefine.DisciplinedFist, 6000))//功力时间不够
                                    { 
                                        MNKSettings.Instance.阳Step = 1; 
                                        MNKSettings.Instance.续过功力了= 1;
                                        return SpellsDefine.FourPointFury.GetSpell(); //打2
                                    } 
                                    MNKSettings.Instance.阳Step = 1;
                                    MNKSettings.Instance.阳必杀1型 = 1;
                                    return SpellsDefine.ArmOfTheDestroyer.GetSpell(); //打1
                            } 
                            if (MNKSettings.Instance.阳Step == 1) //第1步判断 第0步打2则打1 第0步打1则打3
                            { 
                                    if (MNKSettings.Instance.阳必杀1型 == 1) //打过1了
                                    {
                                        MNKSettings.Instance.阳Step = 2; 
                                        MNKSettings.Instance.续过破碎了 =1; 
                                        return SpellsDefine.Rockbreaker.GetSpell(); //打3
                                    }
                                    MNKSettings.Instance.阳Step = 2; 
                                    MNKSettings.Instance.阳必杀1型 = 1;
                                    return SpellsDefine.ArmOfTheDestroyer.GetSpell(); //打1
                            } 
                            if (MNKSettings.Instance.阳Step == 2) //第2步判断 
                            { 
                                    if (MNKSettings.Instance.续过功力了 == 1) //打过2了
                                    {
                                        if (MNKSettings.Instance.阳必杀1型 == 1) //打过1了
                                        {
                                            MNKSettings.Instance.阳Step = 0; 
                                            MNKSettings.Instance.阳必杀 = 0;
                                            MNKSettings.Instance.阳必杀1型 = 0;
                                            return SpellsDefine.Rockbreaker.GetSpell(); //打3
                                        }
                                        MNKSettings.Instance.阳Step = 0; 
                                        MNKSettings.Instance.阳必杀 = 0;
                                        MNKSettings.Instance.阳必杀1型 = 0;
                                        return SpellsDefine.ArmOfTheDestroyer.GetSpell(); //打1
                                    }
                                    MNKSettings.Instance.阳Step = 0; 
                                    MNKSettings.Instance.阳必杀 = 0;
                                    return SpellsDefine.FourPointFury.GetSpell(); //打2
                            }
                        }
                        if(Core.Get<IMemApiMonk>().ActiveNadi != NaDi.Lunar)//没有阴脉轮
                        { 
                            MNKSettings.Instance.阴必杀 = 1; //开启阴必杀
                            if(MNKSettings.Instance.阴Step == 0) //第0步判断
                            { 
                                MNKSettings.Instance.阴Step = 1; 
                                return SpellsDefine.ArmOfTheDestroyer.GetSpell(); //打AOE1
                            } 
                            if (MNKSettings.Instance.阴Step == 1) //第1步判断
                            { 
                                MNKSettings.Instance.阴Step = 2; 
                                return SpellsDefine.ArmOfTheDestroyer.GetSpell(); //打AOE1
                            } 
                            if (MNKSettings.Instance.阴Step == 2) //第2步判断
                            { 
                                MNKSettings.Instance.阴Step = 3; 
                                MNKSettings.Instance.阴必杀 = 0;//关闭阴必杀
                                return SpellsDefine.ArmOfTheDestroyer.GetSpell(); //打AOE1
                            }
                                
                        }
                    } 
                } 
                if (LastBaseGcd != 0 && Core.Get<IMemApiSpell>().GetComboTimeLeft().TotalMilliseconds < 300) 
                    LastBaseGcd = 0; 
                if(Core.Get<IMemApiMonk>().ActiveNadi == NaDi.Both||Core.Me.ClassLevel < 60)//有阴阳脉轮或者未达到60级
                { 
                        if(!Core.Me.HasMyAuraWithTimeleft(AurasDefine.DisciplinedFist, 6000))//功力时间不够
                        {
                            return SpellsDefine.TwinSnakes.GetSpell(); //双掌打
                        } 
                        if(!Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.Demolish, 6000))//破碎时间不够
                        {
                                return SpellsDefine.Demolish.GetSpell(); //续破碎
                        } 
                        if(Core.Me.HasMyAura(AurasDefine.LeadenFist))//有连击效果提高
                        { 
                            return SpellsDefine.Bootshine.GetSpell(); //打连击
                        } 
                        return SpellsDefine.DragonKick.GetSpell(); //踹双龙
                } 
                if(Core.Get<IMemApiMonk>().ActiveNadi != NaDi.Solar)//没有阳脉轮
                { 
                            if(MNKSettings.Instance.阳Step == 0) //第0步判断
                            { 
                                    MNKSettings.Instance.阳Step = 1; 
                                    if(!Core.Me.HasMyAuraWithTimeleft(AurasDefine.DisciplinedFist, 6000))//功力时间不够
                                    { 
                                        MNKSettings.Instance.续过功力了= 1;
                                        return SpellsDefine.TwinSnakes.GetSpell(); //打2
                                    } 
                                    MNKSettings.Instance.阳必杀1型 = 1;
                                    if(Core.Me.HasMyAura(AurasDefine.LeadenFist))//有连击效果提高
                                    {
                                        return SpellsDefine.Bootshine.GetSpell(); //打连击
                                    }
                                    return SpellsDefine.DragonKick.GetSpell(); //踹双龙
                            } 
                            if (MNKSettings.Instance.阳Step == 1) //第1步判断
                            { 
                                    MNKSettings.Instance.阳Step = 2; 
                                    if (MNKSettings.Instance.阳必杀1型 == 1) //打过1了
                                    {
                                        MNKSettings.Instance.续过破碎了 =1; 
                                        if(!Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.Demolish, 6000))//破碎时间不够
                                        {
                                            return SpellsDefine.Demolish.GetSpell(); //续破碎
                                        } 
                                        return SpellsDefine.SnapPunch.GetSpell(); //崩拳
                                    }
                                    MNKSettings.Instance.阳必杀1型 = 1;
                                    if(Core.Me.HasMyAura(AurasDefine.LeadenFist))//有连击效果提高
                                    {
                                        return SpellsDefine.Bootshine.GetSpell(); //打连击
                                    }
                                    return SpellsDefine.DragonKick.GetSpell(); //踹双龙
                            } 
                            if (MNKSettings.Instance.阳Step == 2) //第2步判断 
                            { 
                                    if (MNKSettings.Instance.续过功力了 == 1) //打过2了
                                    {
                                        if (MNKSettings.Instance.阳必杀1型 == 1) //打过1了
                                        {
                                            MNKSettings.Instance.阳Step = 0; 
                                            MNKSettings.Instance.阳必杀 = 0;
                                            MNKSettings.Instance.阳必杀1型 = 0;
                                            if(!Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.Demolish, 6000))//破碎时间不够
                                            {
                                                return SpellsDefine.Demolish.GetSpell(); //续破碎
                                            } 
                                            return SpellsDefine.SnapPunch.GetSpell(); //崩拳
                                        }
                                        MNKSettings.Instance.阳Step = 0; 
                                        MNKSettings.Instance.阳必杀 = 0;
                                        MNKSettings.Instance.阳必杀1型 = 0;
                                        if(Core.Me.HasMyAura(AurasDefine.LeadenFist))//有连击效果提高
                                        {
                                            return SpellsDefine.Bootshine.GetSpell(); //打连击
                                        }
                                        return SpellsDefine.DragonKick.GetSpell(); //踹双龙
                                    }
                                    MNKSettings.Instance.阳Step = 0; 
                                    MNKSettings.Instance.阳必杀 = 0;
                                    return SpellsDefine.TwinSnakes.GetSpell(); //打2
                            }
                } 
                if(Core.Get<IMemApiMonk>().ActiveNadi != NaDi.Lunar)//没有阴脉轮
                { 
                    if(Core.Me.HasMyAura(AurasDefine.LeadenFist))//有连击效果提高
                    {
                        return SpellsDefine.Bootshine.GetSpell(); //打连击
                    }
                    return SpellsDefine.DragonKick.GetSpell(); //踹双龙
                } 
            }
            return SpellsDefine.Meditation.GetSpell(); //搓豆子
        }
           
    }
}