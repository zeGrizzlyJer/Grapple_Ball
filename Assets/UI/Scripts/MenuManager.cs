using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
using GrappleBall.States;

public class MenuManager : MonoBehaviour
{
    public enum MenuType
    {
        MainMenu,
        PauseMenu,
    }
    public MenuType menuType;

    [Header("Menus")]
    public CanvasGroupFade menu;
    public CanvasGroupFade settingsMenu;
    public CanvasGroupFade pauseMenu;

    [Header("Settings Listings")]
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private TextMeshProUGUI brightnessText;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI volumeText;

    public KeyCode pauseKey = KeyCode.Escape;
    private ScreenFader fader;

    private bool menuOpened;

    // --------------------------------------------- //

    private void Awake()
    {
        fader = GameObject.FindGameObjectWithTag("ScreenFade").GetComponent<ScreenFader>();

        if (brightnessSlider)
            brightnessSlider.onValueChanged.AddListener(BrightnessSlider);

        if (volumeSlider)
            volumeSlider.onValueChanged.AddListener(VolumeSlider);
    }
    private void Start()
    {
        StatHolder.LoadSettings();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible= false;

        brightnessSlider.value = StatHolder.brightness;
        volumeSlider.value = StatHolder.volume;
    }
    private void Update()
    {
        if (Input.GetKeyDown(pauseKey) && !fader.transform.GetChild(0).gameObject.activeSelf && menuType == MenuType.PauseMenu)
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

        Debug.Log(StatHolder.timer);
    }

    public void PauseGame()
    {
        StartCoroutine(iPauseGame());
        //fader.FadeScreen(true);
    }
    public void ResumeGame()
    {
        StartCoroutine(iResumeGame());
        //fader.FadeScreen(true);
    }
    public void ShowSettingsMenu()
    {
        StartCoroutine(iShowSettingsMenu());
        //fader.FadeScreen(true);
    }
    public void CloseSettingsMenu()
    {
        StartCoroutine(iCloseSettingsMenu());
        //fader.FadeScreen(true);
    }

    IEnumerator iPauseGame()
    {
        Debug.Log("Pausing Game");
        menuOpened = true;
        GameManager.Instance.GameState = GameStates.PAUSE;

        Time.timeScale = 0;

        pauseMenu.FadeIn();
        yield return new WaitForSecondsRealtime(0.25f);
        menu.FadeIn();

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        //fader.FadeScreen(false);
    }
    IEnumerator iResumeGame()
    {
        Debug.Log("Resuming Game");
        menuOpened = false;

        menu.FadeOut();
        pauseMenu.FadeOut();

        yield return new WaitForSecondsRealtime(2f);

        Time.timeScale = 1;
        GameManager.Instance.GameState = GameStates.PLAY;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //fader.FadeScreen(false);
    }
    IEnumerator iShowSettingsMenu()
    {
        Debug.Log("Showing Settings Menu");

        menu.FadeOut();
        yield return new WaitForSecondsRealtime(1.55f);
        settingsMenu.FadeIn();

        //fader.FadeScreen(false);
    }
    IEnumerator iCloseSettingsMenu()
    {
        Debug.Log("Closing Settings Menu");

        settingsMenu.FadeOut();
        yield return new WaitForSecondsRealtime(1.55f);
        menu.FadeIn();

        //fader.FadeScreen(false);
    }

    void BrightnessSlider(float i)
    {
        StatHolder.brightness = i;

        brightnessText.text = (StatHolder.brightness).ToString("0%");
    }
    void VolumeSlider(float i)
    {
        StatHolder.volume = volumeSlider.value;

        volumeText.text = (StatHolder.volume).ToString("0%");
    }

    private void OnDisable()
    {
        StatHolder.SaveSettings();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}




