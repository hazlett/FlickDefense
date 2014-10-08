using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class MainMenuGUI : MonoBehaviour
{
    public GUISkin mainMenuSkin;
    public Texture2D gameLogo;

    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI;
    private Vector2 buttonSize = new Vector2(500, 100);

    void Start()
    {
        updateGUI = 0.5f;
        nativeVerticalResolution = 1080.0f;
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        GameStateManager.Instance.IsMainMenu();

        InvokeRepeating("TimedScreenResize", updateGUI, updateGUI);
    }

    void Update()
    {

    }

    void OnGUI()
    {
        if (mainMenuSkin)
        {
            GUI.skin = mainMenuSkin;
        }
        else
        {
            Debug.Log("MainMenuGUI: GUI Skin object missing!");
        }

        // Scale the GUI to any resolution based on 1920 x 1080 base resolution
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.height / nativeVerticalResolution, Screen.height / nativeVerticalResolution, 1));

        GUI.DrawTexture(new Rect(scaledResolutionWidth / 4 - gameLogo.width / 2, nativeVerticalResolution / 2 - gameLogo.height / 2, gameLogo.width, gameLogo.height), gameLogo);

        if(GUI.Button(new Rect(scaledResolutionWidth * 3 / 4 - buttonSize.x / 2, nativeVerticalResolution / 2 - 3 * buttonSize.y / 2 - 50, buttonSize.x, buttonSize.y), "Start")){
            GameStateManager.Instance.IsPrewave();
        }

        if (GUI.Button(new Rect(scaledResolutionWidth * 3 / 4 - buttonSize.x / 2, nativeVerticalResolution / 2 - buttonSize.y / 2, buttonSize.x, buttonSize.y), "Settings"))
        {
        }

        if (GUI.Button(new Rect(scaledResolutionWidth * 3 / 4 - buttonSize.x / 2, nativeVerticalResolution / 2 + buttonSize.y / 2 + 50, buttonSize.x, buttonSize.y), "Quit"))
        {
        }
    }

    private void TimedScreenResize()
    {
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
    }
}
