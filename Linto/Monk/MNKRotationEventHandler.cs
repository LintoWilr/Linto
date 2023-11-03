#region

using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

#endregion

namespace Linto.Monk;

public class MNKRotationEventHandler : IRotationEventHandler
{
    public void OnResetBattle()
    {
        MNKBattleData.Instance.Reset();
        Qt.Reset();
    }

    // public Task OnNoTarget()
    // {
    //     return Task.CompletedTask;
    // }
    public async Task OnNoTarget()
    {
        if (!Qt.GetQt("脱战搓豆"))
        {
            return ;
        }
        var slot = new Slot();
        if ((Core.Get<IMemApiMonk>().ChakraCount != 5))//豆子没满
        {
                slot.Add(SpellsDefine.Meditation.GetSpell());//搓豆子
                await slot.Run(false);
        }
        if (!Core.Me.HasMyAura(AurasDefine.FormlessFist))//没有无相身形
        { 
                if (SpellsDefine.FormShift.IsReady()&&!Core.Me.HasMyAura(AurasDefine.PerfectBalance)&&MNKSettings.Instance.脱战演武)//学了演武+没有震脚
                { 
                    slot.Add(SpellsDefine.FormShift.GetSpell());//演武
                    await slot.Run(false); 
                }
        }
    }
    

       public void AfterSpell(Slot slot, Spell spell)
    {
        switch (spell.Id)
        {
            case SpellsDefine.Demolish:
                if (MNKBattleData.Instance.我是猛1 > 0)
                {
                   MNKBattleData.Instance.我是猛1 --;
                }

                break;
        }
    }

    public void OnBattleUpdate(int currTime)
    {
        var list = Core.Get<IMemApiData>().HighEndTerritory();
        if(list.Any(t=>t.Item1==Core.Get<IMemApiZoneInfo>().GetCurrTerrId()))
        {
            LogHelper.Print("检测到高难本，请切换ACR！");
        }
    }

    public Task OnPreCombat()
    {
        return Task.CompletedTask;
    }
    public void OnEnterRotation()
    {
        LogHelper.Print("Linto 日随武僧 Ver1.0:适配全等级武僧/格斗家");
    }
}
