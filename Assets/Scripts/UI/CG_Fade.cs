using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CG_Fade : MonoBehaviour
{
    private CanvasGroup group;

    [SerializeField] private bool screenFade;
    [SerializeField] private bool startClosed;

    [Header("OpacityLerp Settings")]
    [SerializeField] private float lerpDuration = 0.5f;
    [SerializeField] private float startValue = 0;
    [SerializeField] private float endValue = 1f;

    private float curValue;

    //--------------------------------------//

    private void Awake()
    {
        group = GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        if (startClosed)
        {
            group.alpha = startValue;
            gameObject.SetActive(false);
        }
        if (screenFade)
        {
            FadeOut();
        }
    }

    public void FadeIn()
    {
        StartCoroutine(iFadeIn());
    }
    private IEnumerator iFadeIn()
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {

            curValue = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            group.alpha = curValue;

            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        curValue = endValue;
    }

    public void FadeOut()
    {
        StartCoroutine(iFadeOut());
    }
    private IEnumerator iFadeOut()
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {

            curValue = Mathf.Lerp(endValue, startValue, timeElapsed / lerpDuration);
            group.alpha = curValue;

            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        curValue = startValue;

       // gameObject.SetActive(false);
    }
}
