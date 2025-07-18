using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour, IMenuUI
{
    [Header("Menu")]
    public GameObject menuUI;
    public Button menuUIButton;
    [Header("Mute")]
    public Button muteToggleButton;
    public Color muteButtonColor;
    public Color unmuteButtonColor;
    private Image muteButonImage;
    private bool isMute = false;
    [Header("Close")]
    public Button closeButton;
    [Header("Quit")]
    public Button quitButton;
    public Color quitButtonColor;


    void Start()
    {
        menuUIButton.onClick.AddListener(Show);
        closeButton.onClick.AddListener(Hide);

        quitButton.onClick.AddListener(Quit);
        quitButton.GetComponent<Image>().color = quitButtonColor;

        muteToggleButton.onClick.AddListener(ToggleMute);
        muteButonImage = muteToggleButton.GetComponent<Image>();
        muteButonImage.color = unmuteButtonColor;
    }
    public void Hide()
    {
        menuUI.SetActive(false);
        menuUIButton.gameObject.SetActive(true);
        Time.timeScale = 1;
    }

    public bool IsVisible()
    {
        return menuUI.activeSelf;
    }

    public void Show()
    {
        Time.timeScale = 0;
        menuUI.SetActive(true);
        menuUIButton.gameObject.SetActive(false);
    }

    public void ToggleMute()
    {
        SoundManager.Instance.ToggleMute(out isMute);
        MuteButton();
    }

    private void MuteButton()
    {

        if (isMute)
            muteButonImage.color = muteButtonColor;
        else
            muteButonImage.color = unmuteButtonColor;
    }
    private void Quit()
    {
        Application.Quit();
    }
}
