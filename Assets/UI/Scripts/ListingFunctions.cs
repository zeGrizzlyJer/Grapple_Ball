using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ListingFunctions : MonoBehaviour
{
    public enum ListingType
    {
        Brightness,
        Contrast,
        Volume,
    }
    private SliderJoint2D slider;
    private TextMeshProUGUI valueText;
}
