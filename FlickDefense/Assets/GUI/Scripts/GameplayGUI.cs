using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class GameplayGUI : MonoBehaviour
{
    public GUISkin gameplaySkin;

    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI;
    private Vector2 labelSize = new Vector2(450, 100), buttonSize = new Vector2(500, 100), headerSize = new Vector2(750, 100);

    void OnEnable()
    {
        updateGUI = 0.5f;
        nativeVerticalResolution = 1080.0f;
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;

        InvokeRepeating("TimedScreenResize", updateGUI, updateGUI);
    }

    void Update()
    {

    }

    void OnGUI()
    {
        GUI.skin = gameplaySkin;

        // Scale the GUI to any resolution based on 1920 x 1080 base resolution
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.height / nativeVerticalResolution, Screen.height / nativeVerticalResolution, 1));

        GUI.Label(new Rect(scaledResolutionWidth - labelSize.x - 10, 10, labelSize.x, labelSize.y), "Enemies: " + GameStateManager.Instance.enemyCount + " out of " + WaveSystem.Instance.EnemyCount());
    }

    private void TimedScreenResize()
    {
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
    }
}
