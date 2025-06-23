using TMPro;
using UnityEngine;

public class PlayerScoreUI : MonoBehaviour, IScoreUI
{
    public GameObject scoreUI;
    public TMP_Text scoreText;

    public void Hide()
    {
        scoreUI.SetActive(false);
    }

    public bool IsVisible() => scoreUI.activeSelf;

    public void Show()
    {
        scoreUI.SetActive(true);
    }

    public void UpdateScoreText(string value)
    {
        print("TEXT");
        scoreText.text = value;
    }
}
