using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupFade : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Start Settings")]
    [SerializeField] private bool startOn;

    [Header("Opacity Lerp Settings")]
    [SerializeField] private float lerpDuration = 1.5f;
    [SerializeField] private float startValue = 0;
    [SerializeField] private float endValue = 1;

    // ---------------------------------- //

    private void Awake()
    {
        canvasGroup = transform.GetChild(0).GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        if (startOn)
        {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.gameObject.SetActive(false);
        }
    }

    public void FadeIn()
    {
        StartCoroutine(iFadeIn());
    }
    public void FadeOut()
    {
        StartCoroutine(iFadeOut());
    }

    private IEnumerator iFadeIn()
    {
        //Debug.Log("Fading In");
        canvasGroup.gameObject.SetActive(true);

        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        canvasGroup.alpha = endValue;
    }
    private IEnumerator iFadeOut()
    {
        //Debug.Log("Fading Out");
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(endValue, startValue, timeElapsed / lerpDuration);
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        canvasGroup.alpha = endValue;

        canvasGroup.gameObject.SetActive(false);
    }
}
