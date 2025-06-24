using UnityEngine;
using System.Collections.Generic;

public class UIManager : Singleton<UIManager>
{
    private Dictionary<UITypes, IUIElement> uiElements = new();

    protected override void Awake()
    {
        base.Awake();
        if (TryGetComponent(out RemainingFallingObjectUI objectSpawnLeftOverCounterUI))
            RegisterUI(UITypes.ObjectSpawnLeftOverCounterUI, objectSpawnLeftOverCounterUI);
        if (TryGetComponent(out MenuUI menuUI))
            RegisterUI(UITypes.MenuUI, menuUI);
    }
    public void RegisterUI(UITypes key, IUIElement uiElement)
    {
        if (uiElement != null)
        {
            uiElements[key] = uiElement;
        }
        else
        {
            Debug.LogWarning($"UI Element '{key}' not found on {gameObject.name}");
        }
    }
    public T GetUI<T>(UITypes key) where T : class, IUIElement
    {
        return uiElements.TryGetValue(key, out IUIElement uiElement) ? uiElement as T : null;
    }
    public void ShowUI(UITypes key)
    {
        if (uiElements.TryGetValue(key, out IUIElement element))
            element.Show();
    }

    public void HideUI(UITypes key)
    {
        if (uiElements.TryGetValue(key, out IUIElement element))
            element.Hide();
    }
}
public enum UITypes
{
    ObjectSpawnLeftOverCounterUI, MenuUI
}
