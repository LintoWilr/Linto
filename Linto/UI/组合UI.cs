using AEAssist.CombatRoutine;

namespace Linto.UI;

/// <summary>
/// 自定义组合UI类，用于同时管理多个IRotationUI实例
/// </summary>
public class CompositeRotationUI : IRotationUI
{
    private readonly List<IRotationUI> _uiList = new();

    // 仅传入主窗口（QT窗口），移除GCD窗口
    public CompositeRotationUI(List<IRotationUI> uiList)
    {
        if (uiList != null)
        {
            // 过滤掉null实例，避免空引用异常
            _uiList.AddRange(uiList.Where(ui => ui != null));
        }
    }

    public void Update()
    {
        foreach (var ui in _uiList)
        {
            ui?.Update();
        }
    }

    public void OnDrawUI()
    {
        foreach (var ui in _uiList)
        {
            ui?.OnDrawUI();
        }
    }
}
