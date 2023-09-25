using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ButtonFunctions : MonoBehaviour
{
    public enum ButtonType
    {
        SceneChange, 
        Quit,
        Restart,
        Menu,

        Settings,
        Back,
        Pause,
        Resume,

    }
    public ButtonType buttonType;

    private Button button;
    private ScreenFader fader;

    [Header("Scene Change Button Settings")]
    [SerializeField] private string nextSceneName;

    // -------------------------------------- //

    private void Awake()
    {
        button = GetComponent<Button>();

        fader = GameObject.FindGameObjectWithTag("ScreenFade").GetComponent<ScreenFader>();

        if (button)
            button.onClick.AddListener(ButtonEffect);
    }

    void ButtonEffect()
    {
        if (buttonType == ButtonType.SceneChange)
            LoadScene();
        if (buttonType == ButtonType.Quit)
            QuitGame();
        if (buttonType == ButtonType.Restart)
            RestartGame();
        if (buttonType == ButtonType.Pause)
            PauseGame();
        if (buttonType == ButtonType.Resume)
            ResumeGame();
        if (buttonType == ButtonType.Settings)
            ShowSettingsMenu();
        if (buttonType == ButtonType.Back)
            CloseSettingsMenu();
    }

    public void LoadScene()
    {
        StartCoroutine(iLoadScene());
        fader.FadeScreen(true);
    }
    public void QuitGame()
    {
        StartCoroutine(iQuitGame());
        fader.FadeScreen(true);
    }
    public void RestartGame()
    {
        StartCoroutine(iRestartGame());
        fader.FadeScreen(true);
    }

    private IEnumerator iLoadScene()
    {
        Debug.Log("Enter Scene: " + nextSceneName);

        yield return new WaitForSecondsRealtime(2f);

        Time.timeScale = 1;

        SceneManager.LoadScene(nextSceneName);
    }
    private IEnumerator iQuitGame()
    {
        Debug.Log("Quitting Game");

        yield return new WaitForSecondsRealtime(2f);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    private IEnumerator iRestartGame()
    {
        Debug.Log("Restarting Game");

        yield return new WaitForSecondsRealtime(2f);

        Time.timeScale = 1;

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void PauseGame()
    {
        FindObjectOfType<MenuManager>().PauseGame();
    }
    public void ResumeGame()
    {
        FindObjectOfType<MenuManager>().ResumeGame();
    }
    public void ShowSettingsMenu()
    {
        FindObjectOfType<MenuManager>().ShowSettingsMenu();
    }
    public void CloseSettingsMenu()
    {
        FindObjectOfType<MenuManager>().CloseSettingsMenu();
    }
}
