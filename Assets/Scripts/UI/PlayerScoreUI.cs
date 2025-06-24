using TMPro;
using UnityEngine;

public class PlayerScoreUI : MonoBehaviour, IPlayerScoreUI
{
    public GameObject scoreUI;
    public TMP_Text playerNameText;
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
    public void SetPlayerName(string name)
    {
        string displayName;
        if (name.StartsWith("AI"))
        {
            displayName = "COM";
        }
        else if (name.StartsWith("Player_"))
        {
            string number = name.Substring("Player_".Length);
            displayName = number + "P";
        }
        else
        {
            displayName = name;
        }
        playerNameText.text = displayName;
    }
    public void UpdateScoreText(string value)
    {
        scoreText.text = value;
    }
}
