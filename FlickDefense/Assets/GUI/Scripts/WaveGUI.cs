using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class WaveGUI : MonoBehaviour
{
    public GUISkin waveSkin;

    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI, timer;
    private float xIn, xOut;
    private Vector2 labelSize = new Vector2(750, 100);

    void OnEnable()
    {
        timer = 0.0f;
        xIn = xOut = 1.0f;
        updateGUI = 0.5f;
        nativeVerticalResolution = 1080.0f;
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;

        InvokeRepeating("TimedScreenResize", updateGUI, updateGUI);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer < 3.0f)
        {
            xIn = Mathf.Lerp(xIn, 0.0f, 0.025f);
        }
        else if(timer > 3.0f && timer < 7.0f)
        {
            xOut = Mathf.Lerp(xOut, 6.0f, 0.0075f);
        }
        else if (timer > 7.0f)
        {
            GameStateManager.Instance.IsPlaying();
            this.enabled = false;
        }

    }

    void OnGUI()
    {
        GUI.skin = waveSkin; 

        // Scale the GUI to any resolution based on 1920 x 1080 base resolution
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.height / nativeVerticalResolution, Screen.height / nativeVerticalResolution, 1));

        GUI.Label(new Rect(((scaledResolutionWidth / 2 - labelSize.x / 2) - scaledResolutionWidth * xIn) * xOut, labelSize.y * 0.25f, labelSize.x, labelSize.y), "Wave " + WaveSystem.Instance.waveNumber.ToString());
    }

    private void TimedScreenResize()
    {
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
    }
}
