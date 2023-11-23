using System.Collections;
using System.Collections.Generic;
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
    private BTN_Animation anim;

    [SerializeField] private CG_Fade fadeScreen;
    [SerializeField] private float delayAction;

    [Header("SceneChange Options")]
    [SerializeField] private string nextSceneName;

    [Header("OpenCloseMenu Options")]
    [SerializeField] private CG_Fade[] groupToClose;
    [SerializeField] private CG_Fade[] groupToOpen;

    [SerializeField] private bool pauseOnClick = false;
    [SerializeField] private bool unpause;

    public UnityEvent open;

    public bool pressed = false;

    //----------------------------------//

    private void Awake()
    {
        anim = GetComponent<BTN_Animation>();
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
            Pause();
    }

    private void SceneChange()
    {
        StartCoroutine(iSceneChange());
    }
    private IEnumerator iSceneChange()
    {
        yield return new WaitForSecondsRealtime(delayAction);

        SceneManager.LoadScene(nextSceneName);
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
                    if (groupToClose[i].gameObject.activeSelf) groupToClose[i].FadeOut(); groupToClose[i].gameObject.SetActive(false);
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

    public void Pause()
    {
        if (unpause)
        {
            Time.timeScale = 1;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!unpause)
        {
            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

    }

    public void UnPress()
    {
        pressed = false;
    }
}
