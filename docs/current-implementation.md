# Linto 当前结构与实现基线

本文记录当前项目的主要结构和职责，用作后续清理 `dotnet format --severity info` 诊断时的对照。除非明确确认不会影响 AEAssist/Dalamud 约定、配置序列化、反射或外部调用，否则不要因为 info 级建议机械改名、改命名空间、改 public/static 字段形态。

## 解决方案与构建

- `Linto.sln` 只有一个项目：`Linto/Linto.csproj`。
- 解决方案中 `Debug|Any CPU` 和 `Release|Any CPU` 都映射到项目的 `Release|Any CPU`。
- `Linto.csproj` 当前目标框架为 `net10.0`，引用 `AEAssist.NET 1.2.9`，并显式 `Compile Include` 当前启用的 PvP 文件。
- `csproj` 显式排除了部分旧目录/旧入口，例如 `Wilr/**`、`Scholar/**`、`Monk/**`、`RedMage/**`、`LintoPvP/SCH/**` 等；这些文件可能仍在仓库中，但不参与当前项目编译。
- Release 输出路径为 `E:\AEAssist 国服 1001 1828\ACR\Linto\`。

## 顶层入口

根目录下的 `PvP*RotationEntry.cs` 是 AEAssist 识别/加载各职业 ACR 的主要入口。

典型结构参考 `PvPBLMRotationEntry.cs`：

- 实现 `IRotationEntry`。
- `SlotResolvers` 注册该职业的 `Ability`、`GCD` 等 resolver，决定技能优先级与执行顺序。
- `Build(string settingFolder)`：
  - 调用职业配置 `PvP<JOB>Settings.Build(settingFolder)`。
  - 调用共通配置 `PvPSettings.Build(settingFolder)`。
  - 创建 `Rotation`，设置 `TargetJob`、`AcrType = PVP`、等级范围和说明。
  - 绑定职业 `IRotationEventHandler`。
  - 初始化 JobView/QT/Hotkey UI。
- `JobViewWindow` 是职业 UI、QT、热键注册的共享入口；不要随意改名或改访问形态。

当前可见入口包括：

- `PvPBLMRotationEntry.cs`
- `PvPBRDRotationEntry.cs`
- `PvPDRGRotationEntry.cs`
- `PvPGNBRotationEntry.cs`
- `PvPMCHRotationEntry.cs`
- `PvPRDMRotationEntry.cs`
- `PvPSAMRotationEntry.cs`
- `PvPSMNRotationEntry.cs`
- `PvP双刀小子RotationEntry.cs`
- `PvP董慧敏RotationEntry.cs`

## 共通配置

`PvPSettings.cs` 是全职业共通配置模型。

- `PvPSettings.Instance` 是当前运行时配置单例。
- `Build(settingPath)` 从 `PvPSettings.json` 读取配置，不存在时创建默认配置并保存。
- `Save()` 将当前实例序列化为 JSON。
- 该类的大量 public 字段会参与 JSON 序列化和 UI 绑定；不要因为 `CA2211`/命名建议随意改为 private、property 或重命名。

典型字段职责：

- `自动选中`、`技能自动选中`、`最合适目标`：控制 PvP 自动目标选择。
- `无目标坐骑`、`无目标坐骑范围`、`坐骑cd`、`指定坐骑`、`坐骑名`：控制无目标坐骑逻辑。
- `监控`、`警报`、`监控透明背景`、`监控点击穿透`、`监控数量` 等：控制监控窗口显示。

## 职业目录结构

当前 PvP 职业代码集中在 `LintoPvP/<JOB>/` 下，例如 `BLM`、`BRD`、`DRG`、`GNB`、`MCH`、`PCT`、`RDM`、`SAM`、`SMN`、`VPR`。

常见子结构：

- `Ability/`：能力技、职能技、药、净化、冲刺等 `ISlotResolver`。
- `GCD/`：GCD 技能 resolver。
- `Triggers/`：职业设置 UI、QT 辅助类。
- `<JOB>BattleData.cs`：职业战斗状态缓存，通常提供 `Instance` 和 `Reset()`。
- `PvP<JOB>Settings.cs`：职业配置模型，通常读写 `PvP<JOB>Settings.json`。
- `PvP<JOB>Overlay.cs`：职业 UI 绘制、QT 访问包装。
- `PvP<JOB>RotationEventHandler.cs`：进入/退出、战斗更新、无目标、预战斗、施法事件等回调。

## Resolver 实现模式

典型 resolver 参考 `LintoPvP/BLM/Ability/净化.cs`：

- 实现 `ISlotResolver`。
- `SlotMode` 通常为 `SlotMode.Always`。
- `Check()` 返回优先级或负数跳过原因：
  - 检查 QT 是否开启。
  - 检查技能是否可用。
  - 检查目标、状态、距离、权限等业务条件。
- `Build(Slot slot)` 向 `slot` 添加 `Spell`。

这些类名和中文方法名有时会直接表达技能或逻辑含义。私有局部变量可按风格清理，但 public/internal 类型和成员名不要轻易按 `IDE1006` 机械改名。

## PVPApi 共通层

`LintoPvP/PVPApi/` 存放跨职业共享逻辑。

### `PVPHelper.cs`

职责集中且较重：

- 几何/朝向工具：`向量位移`、`GetCameraRotation()` 等。
- 权限判断：`通用码权限`、`高级码`，当前 CID 来自 `Svc.PlayerState.ContentId`。
- PvP 地图判断：`是否55()` 和限制场地 ID 集合。
- 共通 UI：`配置`、`监控`、`PvP调试窗口`、权限展示等。
- 进入 ACR 时的权限提示：`进入ACR()`。
- 监控窗口绘制与目标/队伍信息展示。

注意事项：

- 权限列表、领地列表和 UI/QT 字符串可能被用户或运行环境依赖。
- `check坐骑` 等命名虽然触发 `IDE1006`，但可能已有调用习惯，重命名前先查全局引用。
- `CA2211` 指向的 public/static 字段要谨慎，因为配置、UI 或外部约定可能依赖字段形态。

### `PVPApi.Target/PVPTargetHelper.cs`

职责：目标选择与目标过滤。

- `自动选中()`：根据 PvP 状态、权限和设置自动设置当前目标。
- `目标模式(int 距离, uint 技能id)`：根据设置在“最合适目标”和“最近目标”之间选择。
- `TargetSelector` 内部类提供具体目标选择：最近目标、最合适目标、野火目标、看着目标的人、状态过滤等。
- 目标过滤依赖距离、视线、敌我、免疫/防御/地天等状态。

注意事项：

- 该文件命名空间和文件夹不完全匹配，触发 `IDE0130`；改命名空间会改变类型全名，需要谨慎。
- 目标选择逻辑属于战斗行为核心，优先避免“为了 style”重写复杂条件。

### `HotkeyData.cs`

职责：JobView 热键解析器集合。

- 多个嵌套类实现 `IHotkeyResolver`。
- `Draw` / `DrawExternal` 绘制技能图标和外显。
- `Check()` 返回是否可执行。
- `Run()` 向 `AI.Instance.BattleData.NextSlot` 注入技能。
- 内部工具方法统一处理图标、NextSlot、按 aura 状态切技能。

注意事项：

- 热键类名、字符串、技能 ID 与 JobView/宏命令相关，谨慎改名。
- 命名空间当前为 `Linto.LintoPvP.PVPApi.PVPApi.Target`，虽与路径不一致，但已被入口和职业事件处理器引用。

### `MountHandler.cs`

职责：无目标骑乘逻辑。

- 检查 PvP、权限、目标、附近敌人、技能冷却、骑乘冷却和限制地图。
- 根据配置发送 `/mcancel` 和 `/mount ...` 指令。
- `RestrictedTerritoryIds` 是限制骑乘地图集合。

注意事项：

- 该类型当前无 namespace，触发 `CA1050`；移动进 namespace 会改变完整类型名，需要同步所有引用并确认没有反射/外部约定。

## MCH 特殊宏支持

`LintoPvP/MCH/PvPMCHRotationEventHandler.cs` 除常规事件回调外，还注册 `/Linto_MCH` 命令。

- `OnEnterRotation()`：注册命令、构建 QT 字典、调用 `PVPHelper.进入ACR()`、设置 `Share.Pull`。
- `OnExitRotation()`：关闭 `Share.Pull`。
- 命令后缀：
  - `_qt`：切换 QT。
  - `_hk`：执行热键。
  - `_gt`：执行自定义共通开关。
- `QTKey` 的 public static 字段通过反射枚举生成命令映射；不要轻易改字段名、可见性或 static 形态。

## UI 与 Overlay

职业 Overlay 例如 `LintoPvP/BLM/PvPBLMOverlay.cs`：

- `DrawGeneral(JobViewWindow jobViewWindow)` 绘制职业配置、调试入口等。
- 嵌套 `BLMQt` 包装 `JobViewWindow.GetQt/SetQt/ReverseQt` 等操作，供 resolver 检查 QT 状态。

职业 Entry 的 `BuildQT()` 通常负责：

- 创建 `JobViewWindow`。
- 添加职业配置、监控、共通配置 Tab。
- 添加 QT 开关。
- 添加热键。
- 绘制 QT 窗口。

## 后续修 info 的风险分层

建议处理顺序：

1. 低风险局部风格：`IDE0021`、`IDE0025`、`IDE0040`、`IDE0062`、明显局部的 `IDE0059`。
2. 有条件处理：`CA1864`、私有纯 helper 的 `CA1822`。
3. 默认不机械处理：`IDE1006`、`IDE0130`、`CA1050`、`CA1816`、`CA2211`。

处理任何高风险项前必须确认：

- 是否会改变类型全名、成员名、命令字符串、JSON 字段名或反射可见性。
- 是否被 AEAssist/Dalamud 通过接口、反射、约定或序列化使用。
- 是否影响插件加载/卸载、UI 打开、配置读写、宏命令、职业进入和技能执行。

## 验证基线

每批修改后至少执行：

```powershell
dotnet format "I:\ACR\Linto\Linto.sln" --verify-no-changes --severity warn
dotnet build "I:\ACR\Linto\Linto.sln"
```

如果修改了特定 info 规则，再补充：

```powershell
dotnet format "I:\ACR\Linto\Linto.sln" --verify-no-changes --severity info --diagnostics <诊断ID列表>
```
