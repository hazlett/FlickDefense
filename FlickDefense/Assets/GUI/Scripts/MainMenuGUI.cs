using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class MainMenuGUI : MonoBehaviour
{
    public GUISkin mainMenuSkin;
    public Texture2D gameLogo;
    public UserData currentUser;

    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI;
    private Vector2 buttonSize = new Vector2(500, 100);
    private string userSlot1, userSlot2, userSlot3;
    private int selectedUser;

    private bool chooseUserWindow, overwriteWindow, optionsWindow, slotSelected;

    void Start()
    {
        slotSelected = chooseUserWindow = overwriteWindow = false;
        optionsWindow = true;
        updateGUI = 0.5f;
        nativeVerticalResolution = 1080.0f;
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        GameStateManager.Instance.IsMainMenu();

        InvokeRepeating("TimedScreenResize", updateGUI, updateGUI);
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

        if (optionsWindow)
        {
            if (GUI.Button(new Rect(scaledResolutionWidth * 3 / 4 - buttonSize.x / 2, nativeVerticalResolution * 2 / 7 - buttonSize.y / 2, buttonSize.x, buttonSize.y), "Start"))
            {
                GameStateManager.Instance.IsPrewave();
            }

            if (GUI.Button(new Rect(scaledResolutionWidth * 3 / 4 - buttonSize.x / 2, nativeVerticalResolution * 3 / 7 - buttonSize.y / 2, buttonSize.x, buttonSize.y), "Choose User"))
            {
                chooseUserWindow = true;
                optionsWindow = false;
            }

            if (GUI.Button(new Rect(scaledResolutionWidth * 3 / 4 - buttonSize.x / 2, nativeVerticalResolution * 4 / 7 - buttonSize.y / 2, buttonSize.x, buttonSize.y), "Settings"))
            {
            }

            if (GUI.Button(new Rect(scaledResolutionWidth * 3 / 4 - buttonSize.x / 2, nativeVerticalResolution * 5 / 7 - buttonSize.y / 2, buttonSize.x, buttonSize.y), "Quit"))
            {
            }
        }

        DrawChooseUser();
    }

    private void TimedScreenResize()
    {
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
    }

    private void DrawChooseUser()
    {
        if (chooseUserWindow)
        {
            SlotLabels();
            GUI.Box(new Rect(scaledResolutionWidth * 3 / 4 - 400, nativeVerticalResolution / 2 - 450, 800, 900), "");

            if (GUI.Button(new Rect(scaledResolutionWidth * 3 / 4 - 350, nativeVerticalResolution * 1.5f / 7 - 75, 700, 150), userSlot1))
            {
                selectedUser = 0;
            }
            if (GUI.Button(new Rect(scaledResolutionWidth * 3 / 4 - 350, nativeVerticalResolution * 3f / 7 - 75, 700, 150), userSlot2))
            {
                selectedUser = 1;
            }
            if (GUI.Button(new Rect(scaledResolutionWidth * 3 / 4 - 350, nativeVerticalResolution * 4.5f / 7 - 75, 700, 150), userSlot3))
            {
                selectedUser = 2;
            }

            if (slotSelected)
            {
                if (GUI.Button(new Rect(scaledResolutionWidth * 3 / 4 - 150, nativeVerticalResolution * 6 / 7 - 75, 300, 100), "Cancel"))
                {
                    chooseUserWindow = false;
                    optionsWindow = true;
                }
            }
            else
            {
                if (GUI.Button(new Rect(scaledResolutionWidth * 3 / 4 - 150, nativeVerticalResolution * 6 / 7 - 75, 300, 100), "Overwrite Data"))
                {
                    CheckOverwrite();
                }
                if (GUI.Button(new Rect(scaledResolutionWidth * 3 / 4 - 150, nativeVerticalResolution * 6 / 7 - 75, 300, 100), "Load Data"))
                {
                    chooseUserWindow = false;
                    optionsWindow = true;
                }
            }
        }
    }

    private void SlotLabels()
    {
        if (LoadSave.Instance.Users[0] == null) { userSlot1 = "New User"; }
        else { userSlot1 = "Slot 1 Data" }

        if (LoadSave.Instance.Users[1] == null) { userSlot2 = "New User"; }
        else { userSlot2 = "Slot 2 Data" }

        if (LoadSave.Instance.Users[2] == null) { userSlot3 = "New User"; }
        else { userSlot3 = "Slot 3 Data" }
    }

    private void CheckOverwrite()
    {
        if (LoadSave.Instance.Users[selectedUser] == null)
        {
            currentUser.userID = selectedUser;
            chooseUserWindow = false;
            optionsWindow = true;
        }
        else
        {
            overwriteWindow = true;
            chooseUserWindow = false;
        }
    }

    private void DrawOverwriteWindow()
    {
        if (overwriteWindow)
        {
            GUI.Box(new Rect(scaledResolutionWidth * 3 / 4 - 400, nativeVerticalResolution / 2 - 200, 800, 400), "Are you sure you want to overwrite this data?");

            if(GUI.Button(new Rect(scaledResolutionWidth * 2 / 5 - 150, nativeVerticalResolution / 2 + 100, 300, 100), "NO")){
                overwriteWindow = false;
                chooseUserWindow = true;
            }
            if(GUI.Button(new Rect(scaledResolutionWidth * 3 / 5 - 150, nativeVerticalResolution / 2 + 100, 300, 100), "YES")){
            }

        }

    }
}
