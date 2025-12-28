using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.Extension;

namespace Linto.LintoPvP;

public class NewWindow : IRotationUI
{
    private readonly Action _saveSetting;
    public Dictionary<string, Action<NewWindow>> ExternalTab { get; } = new();
    public Action UpdateAction { get; private set; }    
    // （可选）如果后续添加了需要释放的资源（如事件监听、句柄），可在这里标记是否已释放
    private bool _disposed = false;

    public NewWindow(Action saveSettingAction)
    {
        _saveSetting = saveSettingAction ??
            throw new ArgumentNullException(nameof(saveSettingAction));
    }
    /// <summary>
    /// 设置UI上的Update处理
    /// </summary>
    /// <param name="updateAction"></param>
    public void SetUpdateAction(Action updateAction)
    {
        UpdateAction = updateAction;
    }
    // 关键：框架每帧强制调用此方法，不受主窗口折叠影响
    public void OnDrawUI()
    {
        // 1. 执行更新逻辑（可选，如刷新技能数据）
        UpdateAction?.Invoke();

        // 2. 绘制窗口
        if (Core.Me.IsPvP())
        {
            监控窗口.Draw(this);
        }
    }
    public bool IsCustomMain()
    {
        return true;
    }
    public void Update()
    {
        // 执行窗口的专属更新逻辑（如果有）
        UpdateAction?.Invoke();
    }
    // 2. 实现 IDisposable 接口的 Dispose() 方法
    public void Dispose()
    {
        // 调用带参数的 Dispose 方法，标记已释放
        Dispose(true);
        // 告诉GC不需要再调用析构函数（避免重复释放）
        GC.SuppressFinalize(this);
    }
    // 3. 核心释放逻辑（区分“用户主动释放”和“GC自动释放”）
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return; // 避免重复释放

        // 如果是用户主动调用 Dispose()（disposing=true），释放托管资源
        if (disposing)
        {
            // 示例：如果后续给 NewWindow 添加了事件监听（如 EventManager），需在这里移除
            // EventManager.Instance.RemoveEventListener(...);

            // 目前没有需要释放的托管资源，暂时留空（但方法必须存在）
        }

        // 释放非托管资源（如句柄、指针等，目前没有则留空）

        _disposed = true; // 标记为已释放
    }

    // （可选）析构函数：防止用户忘记调用 Dispose()，由GC自动调用时释放非托管资源
    ~NewWindow()
    {
        Dispose(false);
    }
}