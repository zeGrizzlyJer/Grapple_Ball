using GrappleBall;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BTN_Animation))]
public class BTN_Function : MonoBehaviour
{
    public enum ButtonType
    {
        SceneChange,
        OpenCloseMenu,
        Quit,
    }
    public ButtonType buttonType;

    [SerializeField] private CG_Fade fadeScreen;
    [SerializeField] private float delayAction;

    [Header("SceneChange Options")]
    [SerializeField] private string nextSceneName;
    [SerializeField] private bool restart;

    [Header("OpenCloseMenu Options")]
    [SerializeField] private CG_Fade[] groupToClose;
    [SerializeField] private CG_Fade[] groupToOpen;

    [Header("Pause Settings")]
    [SerializeField] private bool pauseOnClick = false;
    [SerializeField] private INP_Pause pauseHandler;

    public UnityEvent open;

    private bool pressed = false;

    //----------------------------------//

    private void Start()
    {
        if (restart)
        {
            nextSceneName = SceneManager.GetActiveScene().name;
        }
    }
    private void OnDisable()
    {
        pressed = false;
    }

    public void OnPress()
    {
        // Implement audio here

        if (buttonType == ButtonType.SceneChange)
        {
            SceneChange();

            if (fadeScreen != null)
            {
                fadeScreen.gameObject.SetActive(true);
                fadeScreen.FadeIn();
            }
        }
        else if (buttonType == ButtonType.OpenCloseMenu)
        {
            OpenCloseMenu();
        }
        else if (buttonType == ButtonType.Quit)
        {
            Quit();

            if (fadeScreen != null)
            {
                fadeScreen.gameObject.SetActive(true);
                fadeScreen.FadeIn();
            }
        }

        if (pauseOnClick)
            pauseHandler.Pause();
    }

    private void SceneChange()
    {
        StartCoroutine(iSceneChange());
    }
    private IEnumerator iSceneChange()
    {
        yield return new WaitForSecondsRealtime(delayAction);

        SceneManager.LoadScene(nextSceneName);
        Time.timeScale = 1;
    }

    private void OpenCloseMenu()
    {
        StartCoroutine(iOpenCloseMenu());
    }
    private IEnumerator iOpenCloseMenu()
    {
        yield return new WaitForSecondsRealtime(delayAction);

        if (groupToClose != null)
        {
            if (!pressed)
            {
                for (int i = 0; i < groupToClose.Length; i++)
                {
                    if (groupToClose[i].gameObject.activeSelf) groupToClose[i].gameObject.SetActive(true); groupToClose[i].FadeOut();
                }
            }
        }
        if (groupToOpen != null)
        {
            if (!pressed)
            {
                for (int i = 0; i < groupToOpen.Length; i++)
                {
                    groupToOpen[i].gameObject.SetActive(true);
                    groupToOpen[i].FadeIn();

                    open.Invoke();
                }
            }
            else
            {
                for (int i = 0; i < groupToOpen.Length; i++)
                {
                    groupToOpen[i].gameObject.SetActive(true);
                    groupToOpen[i].FadeOut();
                }
            }
        }
        pressed = !pressed;
    }

    private void Quit()
    {
        StartCoroutine(iQuit());
    }
    private IEnumerator iQuit()
    {
        yield return new WaitForSecondsRealtime(delayAction);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void UnPress()
    {
        pressed = false;
    }
}
