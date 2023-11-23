using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Listing_Selection : MonoBehaviour
{
    [Header("References: Options & Arrows")]
    [SerializeField] private GameObject[] options;
    [SerializeField] private Button arrowL;
    [SerializeField] private Button arrowR;

    [Tooltip("Set this to the index of options[] you would like to start on. (Can be overridden by SettingsManager.cs)")]
    public int startOptionIndex = 0;

    [HideInInspector] public int curOption;

    public UnityEvent SettingChanged;

    //----------------------------------------//

    private void Start()
    {
        curOption = startOptionIndex;

        options[curOption].SetActive(true);
        for (int i = 0; i < options.Length; i++)
        {
            if (i != curOption)
            {
                options[i].SetActive(false);
            }
        }

        SettingChanged.Invoke();

        if (curOption == 0)
        {
            arrowL.gameObject.SetActive(false);
        }
        if (curOption == options.Length - 1)
        {
            arrowR.gameObject.SetActive(false);
        }
    }

    public void ArrowRight()
    {
        if ((curOption + 1) < options.Length)
        {
            curOption++;

            options[curOption].SetActive(true);
            for (int i = 0; i < options.Length; i++)
            {
                if (i != curOption)
                {
                    options[i].SetActive(false);
                }
            }

            SettingChanged.Invoke();
        }

        if (curOption == options.Length - 1)
        {
            arrowR.gameObject.SetActive(false);
        }
        if (curOption > 0)
        {
            arrowL.gameObject.SetActive(true);
        }
    }
    public void ArrowLeft()
    {
        if ((curOption - 1) >= 0)
        {
            curOption--;

            options[curOption].SetActive(true);
            for (int i = 0; i < options.Length; i++)
            {
                if (i != curOption)
                {
                    options[i].SetActive(false);
                }
            }
        }

        if (curOption == 0)
        {
            arrowL.gameObject.SetActive(false);
        }
        if (curOption < options.Length - 1)
        {
            arrowR.gameObject.SetActive(true);
        }

        SettingChanged.Invoke();
    }
}
