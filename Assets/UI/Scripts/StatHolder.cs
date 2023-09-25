using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHolder
{
    public static float brightness = 0f;
    public static float contrast = 0f;
    public static float volume = 0.5f;

    public static float timer;
    public static float score;

    // ---------------- //

    public static void SaveSettings()
    {
        PlayerPrefs.SetFloat("Brightness", brightness);
        PlayerPrefs.SetFloat("Contrast", contrast);
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();

        Debug.Log("Settings Saved");
    }
    public static void LoadSettings()
    {
        brightness = PlayerPrefs.GetFloat("Brightness");
        contrast = PlayerPrefs.GetFloat("Contrast");
        volume = PlayerPrefs.GetFloat("Volume");

        Debug.Log("Settings Loaded");
    }
}
