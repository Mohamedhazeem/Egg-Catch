using UnityEngine;

public class FocusWindowOnStart : MonoBehaviour
{
    void Start()
    {
        FocusWindow();
    }

    void FocusWindow()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
        Screen.fullScreen = false;
        Screen.fullScreen = true;
    }
}
