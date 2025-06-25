using UnityEngine;

public class PlayerUIInput : MonoBehaviour, IMenuInput
{
    private IMenuUI menuUI;

    void Start()
    {
        menuUI = UIManager.Instance.GetUI<IMenuUI>(UITypes.MenuUI);

        if (menuUI == null)
            Debug.LogWarning("Menu UI not found in UIManager!");
    }

    void Update()
    {
        if (menuUI != null && IsPausePressed())
        {
            if (menuUI.IsVisible())
                menuUI.Hide();
            else
                menuUI.Show();
        }
    }

    public bool IsPausePressed()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }
}
