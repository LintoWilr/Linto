

using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Interface.Textures.TextureWraps;
using Dalamud.Bindings.ImGui;
using Linto.LintoPvP.RDM;
using Linto.LintoPvP.SAM;

namespace Linto.LintoPvP.PVPApi.PVPApi.Target;

public class HotkeyData
{
    public class 蛇LB : IHotkeyResolver
    {
        public void Draw(Vector2 size)
        {
            Vector2 size1 = size * 0.8f;
            ImGui.SetCursorPos(size * 0.1f);
            IDalamudTextureWrap textureWrap;
            if (!Core.Resolve<MemApiIcon>().GetActionTexture(39190u, out textureWrap))
                return;
            if (textureWrap != null) ImGui.Image(textureWrap.Handle, size1);
        }

        public void DrawExternal(Vector2 size, bool isActive) =>
            SpellHelper.DrawSpellInfo(new Spell(39190u, Core.Me), size, isActive);

        public int Check() => 0;

        public void Run()
        {
            if (AI.Instance.BattleData.NextSlot == null)
                AI.Instance.BattleData.NextSlot = new Slot();

            var target = Core.Me.GetCurrTarget();
            if (!Core.Me.HasLocalPlayerAura(4094u) & Core.Me.InCombat() & Core.Me.LimitBreakCurrentValue() >= 3000 &
                target != null && PVPTargetHelper.TargetSelector.Get最合适目标(20, 39190u) != Core.Me &&
                target.DistanceToPlayer() < 25)
            {
                if (PvPSettings.Instance?.技能自动选中 ?? false)
                {
                    if (PvPSettings.Instance?.最合适目标 ?? false)
                    {
                        var suitableTarget = PVPTargetHelper.TargetSelector.Get最合适目标(20, 39190u);
                        if (suitableTarget != null && suitableTarget != Core.Me)
                        {
                            AI.Instance.BattleData.NextSlot.Add(new Spell(39190u, suitableTarget));
                        }
                    }
                    else
                    {
                        var nearestTarget = PVPTargetHelper.TargetSelector.Get最近目标();
                        if (nearestTarget != null && PVPTargetHelper.TargetSelector.Get最合适目标(20, 39190u) != Core.Me)
                        {
                            AI.Instance.BattleData.NextSlot.Add(new Spell(39190u, nearestTarget));
                        }
                    }
                }
                else
                {
                    if (target != null)
                    {
                        AI.Instance.BattleData.NextSlot.Add(new Spell(39190u, target));
                    }
                }
            }
        }
    }

    public class 机工LB : IHotkeyResolver
    {
        public void Draw(Vector2 size)
        {
            Vector2 size1 = size * 0.8f;
            ImGui.SetCursorPos(size * 0.1f);
            IDalamudTextureWrap textureWrap;
            if (!Core.Resolve<MemApiIcon>().GetActionTexture(29415u, out textureWrap, true))
                return;
            if (textureWrap != null) ImGui.Image(textureWrap.Handle, size1);
        }

        public void DrawExternal(Vector2 size, bool isActive) =>
            SpellHelper.DrawSpellInfo(new Spell(29415u, Core.Me.GetCurrTarget()), size, isActive);

        public int Check() => 0;

        public void Run()
        {
            if (AI.Instance.BattleData.NextSlot == null)
                AI.Instance.BattleData.NextSlot = new Slot();
            if (Core.Me.LimitBreakCurrentValue() >= 3000)
            {
                var machSettings = PvPMCHSettings.Instance;
                if (machSettings?.智能魔弹 ?? false)
                {
                    var target = PVPTargetHelper.TargetSelector.Get最合适目标(50, 29415u);
                    if (target is { IsTargetable: true })
                    {
                        AI.Instance.BattleData.NextSlot.Add(new Spell(29415u, target));
                        Core.Resolve<MemApiChatMessage>()?.Toast2($"正在尝试对 {target.Name} 释放 魔弹射手,距离你{target.DistanceToPlayer()}米!", 1, 3000);
                    }
                }
                else
                {
                    var target = Core.Me.GetCurrTarget();
                    if (target != null)
                    {
                        AI.Instance.BattleData.NextSlot.Add(new Spell(29415u, target));
                        Core.Resolve<MemApiChatMessage>()?.Toast2($"正在尝试对 {target.Name} 释放 魔弹射手,距离你{target.DistanceToPlayer()}米!", 1, 3000);
                    }
                }
            }
        }
    }

    public class 霰弹枪 : IHotkeyResolver
    {
        public void Draw(Vector2 size)
        {
            Vector2 size1 = size * 0.8f;
            ImGui.SetCursorPos(size * 0.1f);
            IDalamudTextureWrap textureWrap;
            if (!Core.Resolve<MemApiIcon>().GetActionTexture(29404u, out textureWrap))
                return;
            if (textureWrap != null) ImGui.Image(textureWrap.Handle, size1);
        }
        public void DrawExternal(Vector2 size, bool isActive) => SpellHelper.DrawSpellInfo(new Spell(29404u, Core.Me), size, isActive);
        public int Check()
        {
            var target = Core.Me.GetCurrTarget();
            if (target == null)
            {
                return -1;
            }
            if (target.DistanceToPlayer() > 12)
            {
                return -3;
            }
            if (!29404u.GetSpell().IsReadyWithCanCast())
            {
                return -2;
            }
            return 0;
        }
        public void Run()
        {
            if (AI.Instance.BattleData.NextSlot == null)
                AI.Instance.BattleData.NextSlot = new Slot();
            var target = Core.Me.GetCurrTarget();
            if (target != null)
            {
                AI.Instance.BattleData.NextSlot.Add(new Spell(29404u, target));
            }
        }
    }
    public class 画家LB : IHotkeyResolver
    {
        public void Draw(Vector2 size)
        {
            Vector2 size1 = size * 0.8f;
            ImGui.SetCursorPos(size * 0.1f);
            IDalamudTextureWrap textureWrap;
            if (!Core.Resolve<MemApiIcon>().GetActionTexture(39215u, out textureWrap))
                return;
            if (textureWrap != null) ImGui.Image(textureWrap.Handle, size1);
        }

        public void DrawExternal(Vector2 size, bool isActive) =>
            SpellHelper.DrawSpellInfo(new Spell(39215u, Core.Me), size, isActive);

        public int Check() => 0;

        public void Run()
        {
            if (AI.Instance.BattleData.NextSlot == null)
                AI.Instance.BattleData.NextSlot = new Slot();
            if (!Core.Me.HasLocalPlayerAura(4094u) & Core.Me.InCombat() & Core.Me.LimitBreakCurrentValue() >= 3500)
            {
                AI.Instance.BattleData.NextSlot.Add(new Spell(39215u, Core.Me));
            }
        }
    }
    public class 绝枪LB : IHotkeyResolver
    {
        public void Draw(Vector2 size)
        {
            Vector2 size1 = size * 0.8f;
            ImGui.SetCursorPos(size * 0.1f);
            IDalamudTextureWrap textureWrap;
            if (!Core.Resolve<MemApiIcon>().GetActionTexture(29130u, out textureWrap))
                return;
            ImGui.Image(textureWrap.Handle, size1);

        }

        public void DrawExternal(Vector2 size, bool isActive) =>
            SpellHelper.DrawSpellInfo(new Spell(29130u, Core.Me), size, isActive);

        public int Check()
        {
            if (Core.Me.LimitBreakCurrentValue() < 2000)
                return -1;
            return 0;
        }

        public void Run()
        {
            if (AI.Instance.BattleData.NextSlot == null)
                AI.Instance.BattleData.NextSlot = new Slot();
            if (!Core.Me.HasLocalPlayerAura(4094u) & Core.Me.InCombat())
            {
                AI.Instance.BattleData.NextSlot.Add(new Spell(29130u, Core.Me));
            }
        }
    }
    public class 武士LB : IHotkeyResolver
    {
        public void Draw(Vector2 size)
        {
            Vector2 size1 = size * 0.8f;
            ImGui.SetCursorPos(size * 0.1f);
            IDalamudTextureWrap textureWrap;
            if (!Core.Resolve<MemApiIcon>().GetActionTexture(29537u, out textureWrap))
                return;
            ImGui.Image(textureWrap.Handle, size1);

        }

        public void DrawExternal(Vector2 size, bool isActive) =>
            SpellHelper.DrawSpellInfo(new Spell(29537u, Core.Me), size, isActive);

        public int Check() => 0;

        public void Run()
        {
            if (AI.Instance.BattleData.NextSlot == null)
                AI.Instance.BattleData.NextSlot = new Slot();
            if (!PVPHelper.CanActive() && Core.Me.InCombat() && Core.Me.LimitBreakCurrentValue() > 3500)
            {
                var samSettings = PvPSAMSettings.Instance;
                if (samSettings?.多斩模式 ?? false)
                {
                    var target = PVPTargetHelper.TargetSelector.Get多斩Target(samSettings.多斩人数);
                    if (target != null)
                    {
                        if (samSettings.斩铁调试)
                        {
                            LogHelper.Print($"尝试斩铁目标：{target}");
                        }

                        AI.Instance.BattleData.NextSlot.Add(new Spell(29537u, target));
                        Core.Resolve<MemApiChatMessage>()?
                            .Toast2($"正在尝试对 {target.Name} 释放 斩铁剑", 1, 1500);
                    }
                }
                else
                {
                    var target = PVPTargetHelper.TargetSelector.Get斩铁目标();
                    if (target != null)
                    {
                        if (samSettings?.斩铁调试 ?? false)
                        {
                            LogHelper.Print($"尝试斩铁目标：{target}");
                        }

                        AI.Instance.BattleData.NextSlot.Add(new Spell(29537u, target));
                        Core.Resolve<MemApiChatMessage>()?.Toast2($"正在尝试对 {target.Name} 释放 斩铁剑", 1, 1500);
                    }
                }
            }
        }
    }

    public class 诗人LB : IHotkeyResolver
    {
        public void Draw(Vector2 size)
        {
            Vector2 size1 = size * 0.8f;
            ImGui.SetCursorPos(size * 0.1f);
            IDalamudTextureWrap textureWrap;
            if (!Core.Resolve<MemApiIcon>().GetActionTexture(29401u, out textureWrap))
                return;
            ImGui.Image(textureWrap.Handle, size1);

        }

        public void DrawExternal(Vector2 size, bool isActive) =>
            SpellHelper.DrawSpellInfo(new Spell(29401u, Core.Me), size, isActive);

        public int Check() => 0;

        public void Run()
        {
            if (AI.Instance.BattleData.NextSlot == null)
                AI.Instance.BattleData.NextSlot = new Slot();
            if (Core.Me.InCombat() && Core.Me.LimitBreakCurrentValue() >= 4000)
                AI.Instance.BattleData.NextSlot.Add(new Spell(29401, Core.Me));
        }
    }
    public class 抗死 : IHotkeyResolver
    {
        public void Draw(Vector2 size)
        {
            Vector2 size1 = size * 0.8f;
            ImGui.SetCursorPos(size * 0.1f);
            IDalamudTextureWrap textureWrap;
            if (Core.Me.HasLocalPlayerAura(3245u))
            {
                if (!Core.Resolve<MemApiIcon>().GetActionTexture(29697u, out textureWrap))
                    return;
            }
            else
            {
                if (!Core.Resolve<MemApiIcon>().GetActionTexture(29698u, out textureWrap))
                    return;
            }
            ImGui.Image(textureWrap.Handle, size1);
        }

        public void DrawExternal(Vector2 size, bool isActive)
        {
            if (Core.Me.HasLocalPlayerAura(3245u))
            {
                SpellHelper.DrawSpellInfo(new Spell(29697u, Core.Me), size, isActive);
            }
            else
            {
                SpellHelper.DrawSpellInfo(new Spell(29698u, Core.Me), size, isActive);
            }
        }

        public int Check()
        {
            if (!29697u.IsUnlockWithCDCheck() && !29698u.IsUnlockWithCDCheck())
                return -1;
            return 1;
        }

        public void Run()
        {
            if (AI.Instance.BattleData.NextSlot == null)
                AI.Instance.BattleData.NextSlot = new Slot();
            if (Core.Me.HasLocalPlayerAura(3245u))
                AI.Instance.BattleData.NextSlot.Add(new Spell(29697u, Core.Me));
            else
            {
                AI.Instance.BattleData.NextSlot.Add(new Spell(29698u, Core.Me));
            }
        }
    }
    public class 黑白魔元切换 : IHotkeyResolver
    {
        public void Draw(Vector2 size)
        {
            Vector2 size1 = size * 0.8f;
            ImGui.SetCursorPos(size * 0.1f);
            IDalamudTextureWrap textureWrap;
            if (Core.Me.HasLocalPlayerAura(3245u))
            {
                if (!Core.Resolve<MemApiIcon>().GetActionTexture(29702u, out textureWrap))
                    return;
            }
            else
            {
                if (!Core.Resolve<MemApiIcon>().GetActionTexture(29703u, out textureWrap))
                    return;
            }
            ImGui.Image(textureWrap.Handle, size1);
        }

        public void DrawExternal(Vector2 size, bool isActive)
        {
            if (Core.Me.HasLocalPlayerAura(3245u))
            {
                SpellHelper.DrawSpellInfo(new Spell(29702u, Core.Me), size, isActive);
            }
            else
            {
                SpellHelper.DrawSpellInfo(new Spell(29703u, Core.Me), size, isActive);
            }
        }

        public int Check()
        {
            return 0;
        }

        public void Run()
        {
            if (AI.Instance.BattleData.NextSlot == null)
                AI.Instance.BattleData.NextSlot = new Slot();
            if (Core.Me.HasLocalPlayerAura(3245u))
                AI.Instance.BattleData.NextSlot.Add(new Spell(29702u, Core.Me));
            else
            {
                AI.Instance.BattleData.NextSlot.Add(new Spell(29703u, Core.Me));
            }
        }
    }
    public class 赤魔LB : IHotkeyResolver
    {
        public void Draw(Vector2 size)
        {
            Vector2 size1 = size * 0.8f;
            ImGui.SetCursorPos(size * 0.1f);
            IDalamudTextureWrap textureWrap;
            if (!Core.Resolve<MemApiIcon>().GetActionTexture(41498u, out textureWrap))
                return;
            ImGui.Image(textureWrap.Handle, size1);
        }
        public void DrawExternal(Vector2 size, bool isActive) =>
            SpellHelper.DrawSpellInfo(new Spell(41498u, Core.Me.GetCurrTarget()), size, isActive);

        public int Check()
        {
            if (Core.Me.InCombat() & Core.Me.LimitBreakCurrentValue() < 3000)
                return -1;
            return 1;
        }

        public void Run()
        {
            if (AI.Instance.BattleData.NextSlot == null)
                AI.Instance.BattleData.NextSlot = new Slot();
            if (Core.Me.InCombat() & Core.Me.LimitBreakCurrentValue() >= 3000)
            {
                var rdmaSettings = PvPRDMSettings.Instance;
                if (rdmaSettings?.南天自己 ?? false)
                    AI.Instance.BattleData.NextSlot.Add(new Spell(41498u, Core.Me));
                else
                {
                    var target = Core.Me.GetCurrTarget();
                    if (target != null)
                    {
                        AI.Instance.BattleData.NextSlot.Add(new Spell(41498u, target));
                    }
                }
            }
        }
    }
    public class 后射 : IHotkeyResolver
    {
        public void Draw(Vector2 size)
        {
            Vector2 size1 = size * 0.8f;
            ImGui.SetCursorPos(size * 0.1f);
            IDalamudTextureWrap textureWrap;
            if (!Core.Resolve<MemApiIcon>().GetActionTexture(29399u, out textureWrap))
                return;
            ImGui.Image(textureWrap.Handle, size1);

        }
        public void DrawExternal(Vector2 size, bool isActive) =>
            SpellHelper.DrawSpellInfo(new Spell(29399u, Core.Me), size, isActive);

        public int Check()
        {
            if (!29399u.GetSpell().IsReadyWithCanCast()) return -2;
            return 1;
        }
        public void Run()
        {
            if (AI.Instance.BattleData.NextSlot == null)
                AI.Instance.BattleData.NextSlot = new Slot();
            var target = Core.Me.GetCurrTarget();
            if (target != null)
            {
                AI.Instance.BattleData.NextSlot.Add(new Spell(29399, target));
            }
        }
    }
    public class 后跳 : IHotkeyResolver
    {
        public void Draw(Vector2 size)
        {
            Vector2 size1 = size * 0.8f;
            ImGui.SetCursorPos(size * 0.1f);
            IDalamudTextureWrap textureWrap;
            if (!Core.Resolve<MemApiIcon>().GetActionTexture(29494u, out textureWrap))
                return;
            ImGui.Image(textureWrap.Handle, size1);

        }
        public void DrawExternal(Vector2 size, bool isActive) =>
            SpellHelper.DrawSpellInfo(new Spell(29494u, Core.Me), size, isActive);

        public int Check()
        {
            if (!29494u.GetSpell().IsReadyWithCanCast()) return -2;
            return 1;
        }
        public void Run()
        {
            var helper = PVPHelper.GetCameraRotation反向();
            Core.Resolve<MemApiMove>()?.SetRot(helper);
            Core.Resolve<MemApiSpell>()?.Cast(29494u, PVPHelper.向量位移反向(Core.Me.Position, PVPHelper.GetCameraRotation(), 15));
        }
    }
    public class 速涂 : IHotkeyResolver
    {
        public void Draw(Vector2 size)
        {
            Vector2 size1 = size * 0.8f;
            ImGui.SetCursorPos(size * 0.1f);
            IDalamudTextureWrap textureWrap;
            if (!Core.Resolve<MemApiIcon>().GetActionTexture(39210u, out textureWrap))
                return;
            ImGui.Image(textureWrap.Handle, size1);

        }
        public void DrawExternal(Vector2 size, bool isActive) =>
            SpellHelper.DrawSpellInfo(new Spell(39210u, Core.Me), size, isActive);

        public int Check()
        {
            if (!39210u.GetSpell().IsReadyWithCanCast()) return -2;
            return 1;
        }
        public void Run()
        {
            var rotation = PVPHelper.GetCameraRotation();
            Core.Resolve<MemApiMove>()?.SetRot(rotation);
            Core.Resolve<MemApiSpell>()?.Cast(39210u, PVPHelper.向量位移(Core.Me.Position, rotation, 15));
        }
    }
    public class 以太步 : IHotkeyResolver
    {
        public void Draw(Vector2 size)
        {
            Vector2 size1 = size * 0.8f;
            ImGui.SetCursorPos(size * 0.1f);
            IDalamudTextureWrap textureWrap;
            if (!Core.Resolve<MemApiIcon>().GetActionTexture(29660u, out textureWrap))
                return;
            ImGui.Image(textureWrap.Handle, size1);

        }
        public void DrawExternal(Vector2 size, bool isActive) =>
            SpellHelper.DrawSpellInfo(new Spell(29660u, Core.Me), size, isActive);

        public int Check()
        {
            if (!29660u.GetSpell().IsReadyWithCanCast()) return -2;
            return 1;
        }
        public void Run()
        {
            if (AI.Instance.BattleData.NextSlot == null)
                AI.Instance.BattleData.NextSlot = new Slot();
            var target = Core.Me.GetCurrTarget();
            if (target != null)
            {
                AI.Instance.BattleData.NextSlot.Add(new Spell(29660u, target));
            }
        }
    }
    public class 喵 : IHotkeyResolver
    {
        public void Draw(Vector2 size)
        {
        }

        public void DrawExternal(Vector2 size, bool isActive) =>
            SpellHelper.DrawSpellInfo(new Spell(39215u, Core.Me), size, isActive);

        public int Check() => 0;

        public void Run()
        {

        }
    }
}