using GrappleBall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INP_Pause : MonoBehaviour
{
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;
    [SerializeField] private CG_Fade menu;
    [SerializeField] private float delay = 0.5f;

    private bool paused = false;

    //--------------------------------//

    private void Update()
    {
        if (Input.GetKeyUp(pauseKey))
        {
            Pause();
        }
    }

    public void Pause()
    {
        StartCoroutine(iPause());
    }
    IEnumerator iPause()
    {
        if (paused)
        {
            menu.gameObject.SetActive(true);
            menu.FadeOut();

            yield return new WaitForSecondsRealtime(delay);

            Time.timeScale = 1;
            paused = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            GameManager.Instance.GameState = GameStates.PLAY;
        }
        else if (!paused)
        {
            Time.timeScale = 0;
            paused = true;

            menu.gameObject.SetActive(true);
            menu.FadeIn();

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            GameManager.Instance.GameState = GameStates.PAUSE;
        }
    }
}
