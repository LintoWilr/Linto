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
        private const uint SkillId = 29400u;
        private const float MaxCastDistance = 30f;

        private static bool 光阴对象索引有效(int index)
        {
            return index >= 0 && index < PartyHelper.Party.Count;
        }

        public int Check()
        {
            bool 光阴队友 = PvPBRDSettings.Instance.光阴队友;
            if (!PvPBRDOverlay.BRDQt.GetQt("光阴神")) return -3;
            if (!PVPHelper.CanActive()) return -1;
            if (!SkillId.GetSpell().IsReadyWithCanCast()) return -2;
            
            if (光阴队友)
            {
                // 添加边界检查，确保索引在有效范围内
                if (!光阴对象索引有效(PvPBRDSettings.Instance.光阴对象))
                    return -9;
                    
                if(!PartyHelper.Party[PvPBRDSettings.Instance.光阴对象].HasCanDispel()&& !Core.Me.HasCanDispel()) 
                    return -9;
                if(PartyHelper.Party[PvPBRDSettings.Instance.光阴对象].HasCanDispel()&& 
                   PartyHelper.Party[PvPBRDSettings.Instance.光阴对象].DistanceToPlayer() > MaxCastDistance) 
                    return -9;
                    
				IBattleChara? member = PartyHelper.CastableParty.FirstOrDefault(chara => chara.HasCanDispel());
				if (member == null) return -9;
                if (member.DistanceToPlayer() > MaxCastDistance) return -6;
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
			IBattleChara? target;
            
            if (光阴队友)
            {
                // 添加边界检查
                if (光阴对象索引有效(PvPBRDSettings.Instance.光阴对象))
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
            
            slot.Add(new Spell(SkillId, target));
            if(光阴播报) LogHelper.Print($"光阴目标:{target.Name}");
        }
    }
}
