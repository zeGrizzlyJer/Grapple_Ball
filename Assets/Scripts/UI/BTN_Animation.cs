using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BTN_Animation : MonoBehaviour
{
    [Header("Lerp Settings")]
    [SerializeField] private float lerpDuration = 0.5f;
    private float startValue = 1;
    private float endValue = 1.25f;

    private float curScale;
    private float lastScale;

    //-----------------------------------//

    private void Start()
    {
        curScale = transform.localScale.x;
        lastScale = curScale;

        startValue = curScale;
        endValue = startValue + (startValue * 0.25f);
    }
    private void OnEnable()
    {
        transform.localScale = new Vector3(startValue, startValue, startValue);
    }

    public void OnEnter()
    {
        StopAllCoroutines();
        StartCoroutine(iOnEnter());
    }
    private IEnumerator iOnEnter()
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {

            curScale = Mathf.Lerp(lastScale, endValue, timeElapsed / lerpDuration);
            transform.localScale = new Vector3(curScale, curScale, curScale);
            lastScale = curScale;
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        curScale = endValue;
        transform.localScale = new Vector3(curScale, curScale, curScale);
    }

    public void OnExit()
    {
        StopAllCoroutines();
        StartCoroutine(iOnExit());
    }
    private IEnumerator iOnExit()
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {

            curScale = Mathf.Lerp(lastScale, startValue, timeElapsed / lerpDuration);
            transform.localScale = new Vector3(curScale, curScale, curScale);
            lastScale = curScale;
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        curScale = startValue;
        transform.localScale = new Vector3(curScale, curScale, curScale);
    }
}
