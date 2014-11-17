using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class GameplayGUI : MonoBehaviour
{
    public GUISkin gameplaySkin;
    public Texture2D[] fireTexture, lightningTexture, iceTexture;
    public Texture2D crackedScreen;

    internal bool cracked, skillPopup;

    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI;
    private Vector2 labelSize = new Vector2(500, 200), buttonSize = new Vector2(500, 100), headerSize = new Vector2(750, 100);

    void OnEnable()
    {
        updateGUI = 0.5f;
        nativeVerticalResolution = 1080.0f;
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;

        InvokeRepeating("TimedScreenResize", updateGUI, updateGUI);
    }

    void OnGUI()
    {
        GUI.skin = gameplaySkin;

        // Scale the GUI to any resolution based on 1920 x 1080 base resolution
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.height / nativeVerticalResolution, Screen.height / nativeVerticalResolution, 1));

        GUI.Label(new Rect(scaledResolutionWidth - scaledResolutionWidth / 4 - 10, 10, scaledResolutionWidth / 4, scaledResolutionWidth / 12), "Enemies: " + GameStateManager.Instance.enemyCount + " out of " + WaveSystem.Instance.EnemyCount());

        

        DrawSkillPopup();

        if (cracked)
        {
            GUI.DrawTexture(new Rect(0, 0, scaledResolutionWidth, nativeVerticalResolution), crackedScreen);
        }
    }

    private void DrawSkillPopup()
    {
        if (skillPopup)
        {
        }
    }

    internal bool Clicked(Vector2 touchPosition)
    {
        // Check if skill element was clicked
        return false;
    }

    private void TimedScreenResize()
    {
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
    }
}
