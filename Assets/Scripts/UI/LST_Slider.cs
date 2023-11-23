using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Listing_Slider : MonoBehaviour
{
    [Header("References: Slider")]
    public Slider slider;
    public TextMeshProUGUI sliderValue;

    public UnityEvent SettingChanged;

    //-------------------------------------//

    public void OnSliderValueChanged()
    {
        sliderValue.text = ((slider.value + 20) * 5).ToString();
        SettingChanged.Invoke();

    }
}
