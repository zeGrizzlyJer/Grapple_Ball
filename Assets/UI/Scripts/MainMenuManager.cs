using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string nextScene = "RyanF";

    private ScreenFader fader;
    private Image brightness;

    [Header("Menus")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;

    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button menuButton;

    [Header("Settings Listings")]
    [SerializeField] private Scrollbar[] scrollbar;
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private TextMeshProUGUI brightnessText;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI volumeText;

    public float brightnessAlpha;

    // --------------------------------------------- //

    private void Awake()
    {
        fader = GameObject.FindGameObjectWithTag("ScreenFade").GetComponent<ScreenFader>();
        brightness = GameObject.FindGameObjectWithTag("BrightnessOverlay").GetComponent<Image>();

        if (startButton)
            startButton.onClick.AddListener(StartGame);

        if (settingsButton)
            settingsButton.onClick.AddListener(ShowSettingsMenu);

        if (menuButton)
            menuButton.onClick.AddListener(ShowMainMenu);

        if (quitButton)
            quitButton.onClick.AddListener(Quit);

        if (brightnessSlider)
            brightnessSlider.onValueChanged.AddListener(BrightnessSlider);

        if (volumeSlider)
            volumeSlider.onValueChanged.AddListener(VolumeSlider);
    }
    private void Start()
    {
        for (int i = 0; i < scrollbar.Length; i++)
        {
            scrollbar[i].value = 1;
        }

        brightnessSlider.value = StatHolder.brightness;
        volumeSlider.value = StatHolder.volume;

        if (StatHolder.brightness < 0.5)
            brightness.color = new Color(0, 0, 0, (1 - StatHolder.brightness) - 0.5f);
        else
            brightness.color = new Color(355, 355, 355, (StatHolder.brightness - 0.5f));
    }

    void StartGame()
    {
        StartCoroutine(iStartGame());
    }
    void ShowSettingsMenu()
    {
        StartCoroutine(iShowSettingsMenu());
    }
    void ShowMainMenu()
    {
        StartCoroutine(iShowMainMenu());
    }
    void Quit()
    {
        StartCoroutine(iQuit());
    }

    IEnumerator iStartGame()
    {
        fader.FadeScreen(true);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(nextScene);
    }
    IEnumerator iShowSettingsMenu()
    {
        fader.FadeScreen(true);

        yield return new WaitForSeconds(2f);

        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);

        fader.FadeScreen(false);
    }
    IEnumerator iShowMainMenu()
    {
        fader.FadeScreen(true);

        yield return new WaitForSeconds(2f);

        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);

        fader.FadeScreen(false);
    }
    IEnumerator iQuit()
    {
        yield return new WaitForSeconds(1f);

        fader.FadeScreen(true);

        yield return new WaitForSeconds(2f);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void BrightnessSlider(float i)
    {
        StatHolder.brightness = i;

        brightnessText.text = (StatHolder.brightness).ToString("0%");
        if (i < 0.5)
            brightness.color = new Color(0, 0, 0, (1 - StatHolder.brightness) - 0.5f);
        else
            brightness.color = new Color(355, 355, 355, (StatHolder.brightness - 0.5f));
    }
    void VolumeSlider(float i)
    {
        StatHolder.volume = volumeSlider.value;

        volumeText.text = (StatHolder.volume).ToString("0%");
    }
}
