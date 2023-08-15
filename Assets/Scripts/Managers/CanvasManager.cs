using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : Singleton<CanvasManager>
{
    [Header("Menus")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject pauseMenu;

    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button menuButton;

    [Header("Scene Names")]
    public string mainScene;
    public string menuScene;

    protected override void Awake()
    {
        base.Awake();

        if (startButton)
            startButton.onClick.AddListener(StartGame);

        if (settingsButton)
            settingsButton.onClick.AddListener(ShowSettingsMenu);

        if (menuButton)
            menuButton.onClick.AddListener(ShowMainMenu);

        if (quitButton)
            quitButton.onClick.AddListener(Quit);
    }

    void StartGame()
    {
        SceneManager.LoadScene(mainScene);
        settingsMenu.SetActive(false);
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }
    void ShowSettingsMenu()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }
    void ShowMainMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }
    void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    // Pause
    // Un-Pause
    // Audio Manager Reference
}
