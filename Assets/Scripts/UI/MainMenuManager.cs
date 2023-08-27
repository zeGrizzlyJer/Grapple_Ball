using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private ScreenFader fader;

    [Header("Menus")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;

    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button menuButton;

    // --------------------------------------------- //

    private void Awake()
    {
        fader = GameObject.FindGameObjectWithTag("ScreenFade").GetComponent<ScreenFader>();

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

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("WhiteBox");
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

        yield return new WaitForSeconds(3f);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
