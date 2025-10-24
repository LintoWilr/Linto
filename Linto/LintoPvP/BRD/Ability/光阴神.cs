using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;
using Linto.LintoPvP.PVPApi;
using System.Linq;

namespace Linto.LintoPvP.BRD.Ability
{
    public class 光阴神 : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.Always;

        public int Check()
        {
            bool 光阴队友 = PvPBRDSettings.Instance.光阴队友;
            if (!PvPBRDOverlay.BRDQt.GetQt("光阴神")) return -3;
            if (!PVPHelper.CanActive()) return -1;
            if (!29400u.GetSpell().IsReadyWithCanCast()) return -2;
            
            if (光阴队友)
            {
                // 添加边界检查，确保索引在有效范围内
                if (PvPBRDSettings.Instance.光阴对象 < 0 || 
                    PvPBRDSettings.Instance.光阴对象 >= PartyHelper.Party.Count)
                    return -9;
                    
                if(!PartyHelper.Party[PvPBRDSettings.Instance.光阴对象].HasCanDispel()&& !Core.Me.HasCanDispel()) 
                    return -9;
                if(PartyHelper.Party[PvPBRDSettings.Instance.光阴对象].HasCanDispel()&& 
                   PartyHelper.Party[PvPBRDSettings.Instance.光阴对象].DistanceToPlayer() > 30) 
                    return -9;
                    
                IBattleChara member = PartyHelper.CastableParty.FirstOrDefault(chara => chara.HasCanDispel());
                if (member == null) return -9;
                if (member.DistanceToPlayer() > 30) return -6;
            }
            else
            {
                if (!Core.Me.HasCanDispel()) return -10;
            }
            return 1;
        }

        public void Build(Slot slot)
        {
            bool 光阴队友 = PvPBRDSettings.Instance.光阴队友;
            bool 光阴播报 = PvPBRDSettings.Instance.光阴播报;
            IBattleChara target;
            
            if (光阴队友)
            {
                // 添加边界检查
                if (PvPBRDSettings.Instance.光阴对象 >= 0 && 
                    PvPBRDSettings.Instance.光阴对象 < PartyHelper.Party.Count)
                {
                    if (PartyHelper.Party[PvPBRDSettings.Instance.光阴对象].HasCanDispel()&&
                        PartyHelper.CastableParty.Contains(PartyHelper.Party[PvPBRDSettings.Instance.光阴对象]))
                        target = PartyHelper.Party[PvPBRDSettings.Instance.光阴对象];
                    else 
                        target = PartyHelper.CastableParty.FirstOrDefault(chara => chara.HasCanDispel());
                }
                else
                {
                    target = PartyHelper.CastableParty.FirstOrDefault(chara => chara.HasCanDispel());
                }
                
                if (target == null)
                {
                    return;
                }
            }
            else
            {
                target = Core.Me;
            }
            
            slot.Add(new Spell(29400u, target));
            if(光阴播报) LogHelper.Print($"光阴目标:{target.Name}");
        }
    }
}