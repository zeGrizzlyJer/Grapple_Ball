using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    private ScreenFader fader;
    private Image brightness;

    public KeyCode pauseKey = KeyCode.Escape;
    private bool menuOpened;

    [Header("Menus")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;

    [Header("Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;

    [Header("Settings Listings")]
    [SerializeField] private Scrollbar[] scrollbar;
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private TextMeshProUGUI brightnessText;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI volumeText;

    // ------------------------------------- //

    private void Awake()
    {
        fader = GameObject.FindGameObjectWithTag("ScreenFade").GetComponent<ScreenFader>();
        brightness = GameObject.FindGameObjectWithTag("BrightnessOverlay").GetComponent<Image>();

        if (resumeButton)
            resumeButton.onClick.AddListener(ResumeGame);

        if (settingsButton)
            settingsButton.onClick.AddListener(ShowSettingsMenu);

        if (backButton)
            backButton.onClick.AddListener(ShowPauseMenu);

        if (restartButton)
            restartButton.onClick.AddListener(RestartGame);

        if (menuButton)
            menuButton.onClick.AddListener(MainMenu);

        if (brightnessSlider)
            brightnessSlider.onValueChanged.AddListener(BrightnessSlider);

        if (volumeSlider)
            volumeSlider.onValueChanged.AddListener(VolumeSlider);
    }
    private void Start()
    {
        menu.SetActive(false);

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

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey) && !fader.transform.GetChild(0).gameObject.activeSelf)
        {
            
            if (!menuOpened)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    void PauseGame()
    {
        StartCoroutine(iPauseGame());
    }
    void ResumeGame()
    {
        StartCoroutine(iResumeGame());
    }
    void ShowSettingsMenu()
    {
        StartCoroutine(iShowSettingsMenu());
    }
    void ShowPauseMenu()
    {
        StartCoroutine(iShowPauseMenu());
    }
    void RestartGame()
    {
        StartCoroutine(iRestartGame());
    }
    void MainMenu()
    {
        StartCoroutine(iMainMenu());
    }

    IEnumerator iPauseGame()
    {
        menuOpened = true;
        fader.FadeScreen(true);

        yield return new WaitForSeconds(2f);

        menu.SetActive(true);
        fader.FadeScreen(false);
    }
    IEnumerator iResumeGame()
    {
        menuOpened = false;
        fader.FadeScreen(true);

        yield return new WaitForSeconds(2f);

        menu.SetActive(false);
        fader.FadeScreen(false);
    }
    IEnumerator iShowSettingsMenu()
    {
        fader.FadeScreen(true);

        yield return new WaitForSeconds(2f);

        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);

        fader.FadeScreen(false);
    }
    IEnumerator iShowPauseMenu()
    {
        fader.FadeScreen(true);

        yield return new WaitForSeconds(2f);

        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);

        fader.FadeScreen(false);
    }
    IEnumerator iRestartGame()
    {
        fader.FadeScreen(true);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("WhiteBox");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    IEnumerator iMainMenu()
    {
        fader.FadeScreen(true);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("MainMenu");
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
