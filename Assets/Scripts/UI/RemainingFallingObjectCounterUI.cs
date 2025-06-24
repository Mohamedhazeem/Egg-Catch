
using TMPro;
using UnityEngine;

public class RemainingFallingObjectUI : MonoBehaviour, IRemainingFallingObjectCounterUI
{
    public GameObject objectSpawnLeftOverUI;
    public TMP_Text objectSpawnLeftOverText;

    public void Show()
    {
        objectSpawnLeftOverUI.SetActive(true);
    }
    public void Hide()
    {
        objectSpawnLeftOverUI.SetActive(false);
    }
    public bool IsVisible() => objectSpawnLeftOverUI.activeSelf;

    public void SetRemainingFallingObjectCounterText(string value)
    {
        objectSpawnLeftOverText.text = value;
    }

    public void UpdateRemainingFallingObjectCounterText(string value)
    {
        objectSpawnLeftOverText.text = value;
    }
}
