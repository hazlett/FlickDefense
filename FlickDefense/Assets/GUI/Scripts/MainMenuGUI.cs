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
    private string userSlot1, userSlot2, userSlot3, loadNew;
    private int selectedUser;

    private bool chooseUserWindow, overwriteWindow, optionsWindow, slotSelected, newUser, toggle, toggle2active, toggle3active;

    void Start()
    {
        toggle2active = toggle3active = toggle = slotSelected = chooseUserWindow = overwriteWindow = false;
        optionsWindow = true;
        updateGUI = 0.5f;
        nativeVerticalResolution = 1080.0f;
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        GameStateManager.Instance.IsMainMenu();
        LoadSave.Instance.BlankList();
        UserStatus.Instance.currentUser.LoadAllUsers();

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
                Application.LoadLevel("TowerPrefabs");
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
        DrawOverwriteWindow();
    }

    private void TimedScreenResize()
    {
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
    }

    private void DrawChooseUser()
    {
        if (chooseUserWindow)
        {
            bool toggle1 = false, toggle2 = false, toggle3 = false;
            SlotLabels();
            GUI.Box(new Rect(scaledResolutionWidth * 3 / 4 - 400, nativeVerticalResolution / 2 - 450, 800, 900), "");

            toggle1 = GUI.Toggle(new Rect(scaledResolutionWidth * 3 / 4 - 350, nativeVerticalResolution * 1.5f / 7 - 75, 700, 150), toggle, userSlot1, mainMenuSkin.button);
            if(toggle1 != toggle)
            {
                toggle = toggle1;
                toggle2 = toggle3 = toggle2active = toggle3active = false;
                selectedUser = 0;
                slotSelected = true;
                NewOrLoad();
            }
            toggle2 = GUI.Toggle(new Rect(scaledResolutionWidth * 3 / 4 - 350, nativeVerticalResolution * 3f / 7 - 75, 700, 150), toggle2active, userSlot2, mainMenuSkin.button);
            if (toggle2 != toggle2active)
            {
                toggle2active = toggle2;
                toggle1 = toggle3 = toggle = toggle3active = false;
                selectedUser = 1;
                slotSelected = true;
                NewOrLoad();
            }
            toggle3 = GUI.Toggle(new Rect(scaledResolutionWidth * 3 / 4 - 350, nativeVerticalResolution * 4.5f / 7 - 75, 700, 150), toggle3active, userSlot3, mainMenuSkin.button);
            if (toggle3 != toggle3active)
            {
                toggle3active = toggle = toggle2active = false;
                selectedUser = 2;
                slotSelected = true;
                NewOrLoad();
            }

            if (!slotSelected)
            {
                if (GUI.Button(new Rect(scaledResolutionWidth * 3 / 4 - 150, nativeVerticalResolution * 6 / 7 - 75, 300, 100), "Cancel"))
                {
                    chooseUserWindow = false;
                    optionsWindow = true;
                }
            }
            else
            {
                if (!newUser)
                {
                    if (GUI.Button(new Rect(scaledResolutionWidth * 5 / 8 - 150, nativeVerticalResolution * 6 / 7 - 75, 300, 100), "Overwrite Data"))
                    {
                        CheckOverwrite();
                    }
                }
                if (GUI.Button(new Rect(scaledResolutionWidth * 7 / 8 - 150, nativeVerticalResolution * 6 / 7 - 75, 300, 100), loadNew))
                {
                    chooseUserWindow = false;
                    optionsWindow = true; 
                    toggle3active = toggle3 = toggle1 = toggle2 = toggle = toggle2active = false;
                    if (!newUser)
                    {
                        UserStatus.Instance.currentUser.LoadData(selectedUser);
                    }
                    else
                    {
                        UserStatus.Instance.currentUser.SetDefaultValues();
                        UserStatus.Instance.currentUser.userID = selectedUser;
                        UserStatus.Instance.currentUser.SaveData();
                    }

                }
            }
        }
    }

    private void SlotLabels()
    {
        if (LoadSave.Instance.Users[0].userID == -1) { userSlot1 = "New User"; }
        else { userSlot1 = "Wave " + LoadSave.Instance.Users[0].waveLevel.ToString(); }

        if (LoadSave.Instance.Users[1].userID == -1) { userSlot2 = "New User"; }
        else { userSlot2 = "Wave " + LoadSave.Instance.Users[1].waveLevel.ToString(); }

        if (LoadSave.Instance.Users[2].userID == -1) { userSlot3 = "New User"; }
        else { userSlot3 = "Wave " + LoadSave.Instance.Users[2].waveLevel.ToString(); }
    }

    private void NewOrLoad()
    {
        if (LoadSave.Instance.Users[selectedUser].userID == -1)
        {
            newUser = true;
            loadNew = "Use Selected";
        }
        else
        {
            newUser = false;
            loadNew = "Load Selected";
        }
    }

    private void CheckOverwrite()
    {
        if (LoadSave.Instance.Users[selectedUser].userID == -1)
        {
            UserStatus.Instance.currentUser.userID = selectedUser;
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

            if (GUI.Button(new Rect(scaledResolutionWidth * 5 / 8 - 150, nativeVerticalResolution / 2 + 50, 300, 100), "NO"))
            {
                overwriteWindow = false;
                chooseUserWindow = true;
            }
            if (GUI.Button(new Rect(scaledResolutionWidth * 7 / 8 - 150, nativeVerticalResolution / 2 + 50, 300, 100), "YES"))
            {
                toggle3active = toggle = toggle2active = false;
                UserStatus.Instance.currentUser.SetDefaultValues();
                UserStatus.Instance.currentUser.userID = selectedUser;
                LoadSave.Instance.Users[selectedUser] = UserStatus.Instance.currentUser;
                UserStatus.Instance.currentUser.SaveData();
                overwriteWindow = chooseUserWindow = false;
                optionsWindow = true;
            }

        }

    }
}
