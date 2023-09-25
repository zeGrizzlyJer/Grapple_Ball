using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerValue;
    [SerializeField] private TextMeshProUGUI scoreValue;

    // ------------------------------- //

    private void Start()
    {
        gameObject.GetComponent<CanvasGroupFade>().FadeIn();
    }
    private void Update()
    {
        timerValue.text = StatHolder.timer.ToString("0.0s");
        scoreValue.text = StatHolder.score.ToString("0");
    }
}
