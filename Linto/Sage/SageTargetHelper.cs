using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Interface.Textures.TextureWraps;
using ImGuiNET;

namespace Linto.Sage;

public class SageTargetHelper
{
    public static IBattleChara Get最低血量T()
    {
        if (PartyHelper.CastableTanks == null || PartyHelper.CastableTanks.Count == 0|| PartyHelper.CastableParty.Count <=1)
        {
            return Core.Me;
        }
        if (PartyHelper.CastableTanks.Count == 1)
        {
            return PartyHelper.CastableTanks.FirstOrDefault();
        }
        var MT = PartyHelper.CastableTanks.FirstOrDefault();
        var ST = PartyHelper.CastableTanks.ElementAtOrDefault(1);
        if (ST == null)
        {
            return MT;
        }
        return MT.CurrentHpPercent() > ST.CurrentHpPercent() ? ST : MT;
    }
    public static IBattleChara Get最低血量队友()
    {
        var castableParty = PartyHelper.CastableParty;
        if (castableParty == null || castableParty.Count <= 1)
        {
            return Core.Me;
        }
        var 最低血量队友 = castableParty
            .Where(队友 => 队友 != null) // 确保队友不为空
            .OrderBy(队友 => 队友.CurrentHp) // 按绝对血量排序
            .FirstOrDefault(); // 获取血量最低的队友
        return 最低血量队友 ?? Core.Me;
    }



    public static IBattleChara GetH1康复目标()
    {
        if (PartyHelper.CastableParty.Count <= 1)
            return Core.Me;
        var partyMembers = new[]
        {
            Core.Me,
            PartyHelper.CastableTanks.FirstOrDefault(),
            PartyHelper.CastableDps.FirstOrDefault(),
            PartyHelper.CastableHealers.ElementAtOrDefault(1)
        };
        return partyMembers.FirstOrDefault(member => member?.HasAura(938u) == true || member?.HasAura(700u) == true) ?? Core.Me;
    }

    public static IBattleChara GetH2康复目标()
    {
        if (PartyHelper.CastableParty.Count <= 1)
            return Core.Me;
        var partyMembers = new[]
        {
            Core.Me,
            PartyHelper.CastableDps.ElementAtOrDefault(3),
            PartyHelper.CastableDps.ElementAtOrDefault(2),
            PartyHelper.CastableDps.ElementAtOrDefault(1),
            PartyHelper.CastableDps.ElementAtOrDefault(0),
            PartyHelper.CastableTanks.ElementAtOrDefault(1),
            PartyHelper.CastableTanks.ElementAtOrDefault(0),
            PartyHelper.CastableHealers.ElementAtOrDefault(0)
        };
        return partyMembers.FirstOrDefault(member => member?.HasAura(938u) == true || member?.HasAura(700u) == true) ?? Core.Me;
    } 
    public class 单盾: IHotkeyResolver
    {
        public void Draw(Vector2 size)
        {
            Vector2 size1 = size * 0.8f;
            ImGui.SetCursorPos(size * 0.1f);
            IDalamudTextureWrap textureWrap;
            if (!Core.Resolve<MemApiIcon>().GetActionTexture(24291u, out textureWrap))
                return;
            ImGui.Image(textureWrap.ImGuiHandle, size1);
            
        }
        public void DrawExternal(Vector2 size, bool isActive)
            => SpellHelper.DrawSpellInfo(new Spell(24291u, Core.Me), size, isActive);
        public int Check() => 0;
        public void Run()
        {
            {
                if (AI.Instance.BattleData.NextSlot == null)
                    AI.Instance.BattleData.NextSlot = new Slot();
                if(!Core.Resolve<JobApi_Sage>().Eukrasia)
                    AI.Instance.BattleData.NextSlot.Add(new Spell(24290U, Core.Me));
                AI.Instance.BattleData.NextSlot.Add(new Spell(24291u,Get最低血量T()));
            }
        }
    }
    public class 单盾最低血量: IHotkeyResolver
    {
        public void Draw(Vector2 size)
        {
            Vector2 size1 = size * 0.8f;
            ImGui.SetCursorPos(size * 0.1f);
            IDalamudTextureWrap textureWrap;
            if (!Core.Resolve<MemApiIcon>().GetActionTexture(24291u, out textureWrap))
                return;
            ImGui.Image(textureWrap.ImGuiHandle, size1);
            
        }
        public void DrawExternal(Vector2 size, bool isActive)
            => SpellHelper.DrawSpellInfo(new Spell(24291u, Core.Me), size, isActive);
        public int Check() => 0;
        public void Run()
        {
            {
                if (AI.Instance.BattleData.NextSlot == null)
                    AI.Instance.BattleData.NextSlot = new Slot();
                if(!Core.Resolve<JobApi_Sage>().Eukrasia)
                    AI.Instance.BattleData.NextSlot.Add(new Spell(24290U, Core.Me));
                AI.Instance.BattleData.NextSlot.Add(new Spell(24291u,Get最低血量队友()));
            }
        }
    }
    public class 群盾消化: IHotkeyResolver
    {
        public void Draw(Vector2 size)
        {
            Vector2 size1 = size * 0.8f;
            ImGui.SetCursorPos(size * 0.1f);
            IDalamudTextureWrap textureWrap;
            if (!Core.Resolve<MemApiIcon>().GetActionTexture(24301u, out textureWrap))
                return;
            ImGui.Image(textureWrap.ImGuiHandle, size1);
            
        }
        public void DrawExternal(Vector2 size, bool isActive)
            => SpellHelper.DrawSpellInfo(new Spell(24301u, Core.Me), size, isActive);
        public int Check() => 0;
        public void Run()
        {
            if (AI.Instance.BattleData.NextSlot == null)
                AI.Instance.BattleData.NextSlot = new Slot();
            if (24301u.GetSpell().IsReadyWithCanCast())
            {
                if(!Core.Resolve<JobApi_Sage>().Eukrasia)
                    AI.Instance.BattleData.NextSlot.Add(SpellsDefine.Eukrasia.GetSpell());
                AI.Instance.BattleData.NextSlot.Add(SpellsDefine.EukrasianPrognosis.GetSpell());
                AI.Instance.BattleData.NextSlot.Add(new Spell(24301u,Core.Me));
            }
        }
    }
    public class 群盾: IHotkeyResolver
    { 
        public void Draw(Vector2 size)
        {
            Vector2 size2 = size * 0.8f;
            ImGui.SetCursorPos(size * 0.1f);
            IDalamudTextureWrap textureWrap = default(IDalamudTextureWrap);
            if (Core.Resolve<MemApiIcon>().GetActionTexture(SpellsDefine.EukrasianPrognosis, out textureWrap, true))
            {
                ImGui.Image(textureWrap.ImGuiHandle, size2);
            }
        }

        public void DrawExternal(Vector2 size, bool isActive)
        {
            SpellHelper.DrawSpellInfo(new Spell(SpellsDefine.EukrasianPrognosis, (SpellTargetType)1), size, isActive);
        }
        public int Check()
        {
            return 0;
        }

        public void Run()
        {
            if (AI.Instance.BattleData.NextSlot == null)
            {
                AI.Instance.BattleData.NextSlot = new Slot(2500);
            }
            if (!Core.Resolve<JobApi_Sage>().Eukrasia)
            {
                AI.Instance.BattleData.NextSlot.Add(SpellHelper.GetSpell(24290u));
            }
            AI.Instance.BattleData.NextSlot.Add(SpellHelper.GetSpell(SpellsDefine.EukrasianPrognosis));
        }
    }
}
