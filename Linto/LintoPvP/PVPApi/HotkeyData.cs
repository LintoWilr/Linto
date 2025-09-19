

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
			ImGui.Image(textureWrap.Handle, size1);

		}

		public void DrawExternal(Vector2 size, bool isActive) =>
			SpellHelper.DrawSpellInfo(new Spell(39190u, Core.Me), size, isActive);

		public int Check() => 0;

		public void Run()
		{
			if (AI.Instance.BattleData.NextSlot == null)
				AI.Instance.BattleData.NextSlot = new Slot();
			if (!Core.Me.HasLocalPlayerAura(4094u) & Core.Me.InCombat() & Core.Me.LimitBreakCurrentValue() >= 3000 &
			    Core.Me.GetCurrTarget() != null && PVPTargetHelper.TargetSelector.Get最合适目标(20,39190u) != Core.Me &&
			    Core.Me.GetCurrTarget().DistanceToPlayer() < 25)
			{
				if (PvPSettings.Instance.技能自动选中)
				{
					if (PvPSettings.Instance.最合适目标)
					{
						if (PVPTargetHelper.TargetSelector.Get最合适目标(20,39190u) != null &&
						    PVPTargetHelper.TargetSelector.Get最合适目标(20,39190u) != Core.Me)
						{
							AI.Instance.BattleData.NextSlot.Add(new Spell(39190u,
								PVPTargetHelper.TargetSelector.Get最合适目标(20,39190u)));
						}
					}
					else
					{
						if (PVPTargetHelper.TargetSelector.Get最近目标() != null &&
						    PVPTargetHelper.TargetSelector.Get最合适目标(20,39190u) != Core.Me)
						{
							AI.Instance.BattleData.NextSlot.Add(new Spell(39190u,
								PVPTargetHelper.TargetSelector.Get最近目标()));
						}

					}
				}
				else
				{
					if (Core.Me.GetCurrTarget() != null)
					{
						AI.Instance.BattleData.NextSlot.Add(new Spell(39190u, Core.Me.GetCurrTarget()));
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
			if (!Core.Resolve<MemApiIcon>().GetActionTexture(29415u, out textureWrap,true))
				return;
			ImGui.Image(textureWrap.Handle, size1);

		}
		
		public void DrawExternal(Vector2 size, bool isActive) => 
			SpellHelper.DrawSpellInfo(new Spell(29415u,Core.Me.GetCurrTarget()), size, isActive);

		public int Check()=>0;

		public void Run()
		{
			if (AI.Instance.BattleData.NextSlot == null)
				AI.Instance.BattleData.NextSlot = new Slot();
			if (Core.Me.LimitBreakCurrentValue() >= 3000)
			{
				if (PvPMCHSettings.Instance.智能魔弹)
				{
					if (PVPTargetHelper.TargetSelector.Get最合适目标(50,29415u).IsTargetable)
					{
						AI.Instance.BattleData.NextSlot.Add(new Spell(29415u,
							PVPTargetHelper.TargetSelector.Get最合适目标(50,29415u)));
						Core.Resolve<MemApiChatMessage>().Toast2($"正在尝试对 {PVPTargetHelper.TargetSelector.Get最合适目标(50,29415u).Name} 释放 魔弹射手,距离你{PVPTargetHelper.TargetSelector.Get最合适目标(50,29415u).DistanceToPlayer()}米!", 1, 3000);
					}
				}
				else
				{
					AI.Instance.BattleData.NextSlot.Add(new Spell(29415u, Core.Me.GetCurrTarget()));
					Core.Resolve<MemApiChatMessage>().Toast2($"正在尝试对 {Core.Me.GetCurrTarget().Name} 释放 魔弹射手,距离你{Core.Me.GetCurrTarget().DistanceToPlayer()}米!", 1, 3000);
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
			ImGui.Image(textureWrap.Handle, size1);

		}
		public void DrawExternal(Vector2 size, bool isActive) => SpellHelper.DrawSpellInfo(new Spell(29404u, Core.Me), size, isActive);
		public int Check()
		{
			if (Core.Me.GetCurrTarget()==null)
			{
				return -1;
			}
			if (Core.Me.GetCurrTarget().DistanceToPlayer()>12)
			{
				return -3;
			}
			if(!29404u.GetSpell().IsReadyWithCanCast())
			{
				return -2;
			}
			return 0;
		} 
		public void Run()
		{
			if (AI.Instance.BattleData.NextSlot == null)
				AI.Instance.BattleData.NextSlot = new Slot();
			AI.Instance.BattleData.NextSlot.Add(new Spell(29404u, Core.Me.GetCurrTarget()));
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
			ImGui.Image(textureWrap.Handle, size1);

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
				if (PvPSAMSettings.Instance.多斩模式)
				{
					if (PvPSAMSettings.Instance.斩铁调试)
					{
						LogHelper.Print($"尝试斩铁目标：{PVPTargetHelper.TargetSelector.Get多斩Target(PvPSAMSettings.Instance.多斩人数)}");
					}

					AI.Instance.BattleData.NextSlot.Add(
						new Spell(29537u, PVPTargetHelper.TargetSelector.Get多斩Target(PvPSAMSettings.Instance.多斩人数)));
					Core.Resolve<MemApiChatMessage>()
						.Toast2($"正在尝试对 {PVPTargetHelper.TargetSelector.Get多斩Target(PvPSAMSettings.Instance.多斩人数).Name} 释放 斩铁剑", 1, 1500);
				}
				else
				{
					if (PvPSAMSettings.Instance.斩铁调试)
					{
						LogHelper.Print($"尝试斩铁目标：{PVPTargetHelper.TargetSelector.Get斩铁目标()}");
					}

					AI.Instance.BattleData.NextSlot.Add(new Spell(29537u, PVPTargetHelper.TargetSelector.Get斩铁目标()));
					Core.Resolve<MemApiChatMessage>().Toast2($"正在尝试对 {PVPTargetHelper.TargetSelector.Get斩铁目标().Name} 释放 斩铁剑", 1, 1500);
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
			if (Core.Me.InCombat()&Core.Me.LimitBreakCurrentValue()>=4000)
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
			else SpellHelper.DrawSpellInfo(new Spell(29698u, Core.Me), size, isActive);
		}
		
		public int Check()
		{
			if (!29697u.IsUnlockWithCDCheck()&&!29698u.IsUnlockWithCDCheck())
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
			else SpellHelper.DrawSpellInfo(new Spell(29703u, Core.Me), size, isActive);
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
			SpellHelper.DrawSpellInfo(new Spell(41498u, Core.Me.GetCurrTarget), size, isActive);

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
			if (Core.Me.InCombat()&Core.Me.LimitBreakCurrentValue()>=3000)
				if(PvPRDMSettings.Instance.南天自己)
					AI.Instance.BattleData.NextSlot.Add(new Spell(41498u, Core.Me));
				else
				{
					AI.Instance.BattleData.NextSlot.Add(new Spell(41498u, Core.Me.GetCurrTarget()));
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
			AI.Instance.BattleData.NextSlot.Add(new Spell(29399,Core.Me.GetCurrTarget()));
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
			Core.Resolve<MemApiMove>().SetRot(PVPHelper.GetCameraRotation反向());
			Core.Resolve<MemApiSpell>().Cast(29494u, PVPHelper.向量位移反向(Core.Me.Position, PVPHelper.GetCameraRotation(), 15));
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
			Core.Resolve<MemApiMove>().SetRot(PVPHelper.GetCameraRotation());
			Core.Resolve<MemApiSpell>().Cast(39210u, PVPHelper.向量位移(Core.Me.Position, PVPHelper.GetCameraRotation(), 15));
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
			AI.Instance.BattleData.NextSlot.Add(new Spell(29660u,Core.Me.GetCurrTarget));
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