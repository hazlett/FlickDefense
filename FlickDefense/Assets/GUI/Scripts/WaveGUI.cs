using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class WaveGUI : MonoBehaviour
{
    public GUISkin waveSkin;

    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI, timer;
    private Vector2 labelSize = new Vector2(750, 250);

    void OnEnable()
    {
        timer = 0.0f;
        updateGUI = 0.5f;
        nativeVerticalResolution = 1080.0f;
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer < 5.0f)
        {
        }
        else
        {
        }

        TimedScreenResize();
    }

    void OnGUI()
    {
        if (waveSkin)
        {
            GUI.skin = waveSkin;
        }
        else
        {
            Debug.Log("MainMenuGUI: GUI Skin object missing!");
        }

        // Scale the GUI to any resolution based on 1920 x 1080 base resolution
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.height / nativeVerticalResolution, Screen.height / nativeVerticalResolution, 1));

        GUI.Label(new Rect(scaledResolutionWidth / 2 - labelSize.x / 2, labelSize.y, labelSize.x, labelSize.y), "Wave " + WaveSystem.Instance.waveNumber.ToString());
    }

    private void TimedScreenResize()
    {
        if (Time.time > updateGUI)
        {
            scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        }
    }
}
