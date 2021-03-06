﻿using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class UpgradeGUI : MonoBehaviour
{
    public GUISkin postgameSkin;
    public Texture2D castleUpgrade, castleFix, cooldown, background;

    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI;
    private Vector2 labelSize = new Vector2(700, 150), buttonSize = new Vector2(500, 100), headerSize = new Vector2(750, 100), upgradeSize = new Vector2(300, 300);
    private bool skillWindow;
    private string skillName, skillDescription, skillType;
    private int skillPrice;

    void OnEnable()
    {
        updateGUI = 0.5f;
        nativeVerticalResolution = 1080.0f;
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;

        InvokeRepeating("TimedScreenResize", updateGUI, updateGUI);
    }

    void OnGUI()
    {
        GUI.skin = postgameSkin;

        // Scale the GUI to any resolution based on 1920 x 1080 base resolution
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.height / nativeVerticalResolution, Screen.height / nativeVerticalResolution, 1));

        GUI.DrawTexture(new Rect(0, 0, scaledResolutionWidth, nativeVerticalResolution), background);

        GUI.Label(new Rect(scaledResolutionWidth / 2 - headerSize.x / 2, headerSize.y * 0.25f, headerSize.x, headerSize.y), "Castle Upgrades", "Header");

        if (!skillWindow)
        {
            DrawCastleUpgrade();
            DrawCastleFix();
            DrawBarracksUpgrade();
            DrawArcheryUpgrade();
            DrawAlchemyUpgrade();
        }

        DrawSkillWindow();

        GUI.Label(new Rect(scaledResolutionWidth / 2 - labelSize.x, nativeVerticalResolution * 5.7f / 7 - labelSize.y / 2, labelSize.x, labelSize.y), "Gold: " + UserData.Instance.gold.ToString(), "CenterLabel");
        GUI.Label(new Rect(scaledResolutionWidth / 2, nativeVerticalResolution * 5.7f / 7 - labelSize.y / 2, labelSize.x, labelSize.y), "Castle Health: " + UserData.Instance.castleHealth + " out of " + UserData.Instance.maxCastleHealth, "CenterLabel");

        if (GUI.Button(new Rect(scaledResolutionWidth / 4 - buttonSize.x * 3 / 4, nativeVerticalResolution - buttonSize.y - 50, buttonSize.x, buttonSize.y), "Main Menu"))
        {
            GameStateManager.Instance.IsMainMenu();
            this.enabled = false;
        }
        if (GUI.Button(new Rect(scaledResolutionWidth / 2 - buttonSize.x / 2, nativeVerticalResolution - buttonSize.y - 50, buttonSize.x, buttonSize.y), "Skill Upgrades"))
        {
            GameStateManager.Instance.IsSkills();
            this.enabled = false;
        }

        if (GUI.Button(new Rect(scaledResolutionWidth * 3 / 4 - buttonSize.x / 4, nativeVerticalResolution - buttonSize.y - 50, buttonSize.x, buttonSize.y), "Next Wave"))
        {
            GameStateManager.Instance.IsPrewave();
            this.enabled = false;
        }
    }

    private void OnDisable()
    {
        CancelInvoke("TimedScreenResize");
    }

    private void TimedScreenResize()
    {
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
    }

    private void DrawCastleUpgrade()
    {
        if (GUI.Button(new Rect(scaledResolutionWidth / 2 - upgradeSize.x / 2, nativeVerticalResolution / 2 - upgradeSize.y / 2,
                upgradeSize.x, upgradeSize.y), castleUpgrade, "BlankButton"))
        {
            switch (UserData.Instance.castleLevel)
            {
                case 1: SetSkillInfo(200, "Castle Up", "Level 2 Castle", "Castle");
                    skillWindow = true;
                    break;
                case 2: SetSkillInfo(300, "Castle Up", "Level 3 Castle", "Castle");
                    skillWindow = true;
                    break;
                case 3: SetSkillInfo(400, "Castle Up", "Level 4 Castle", "Castle");
                    skillWindow = true;
                    break;
                case 4: SetSkillInfo(500, "Castle Up", "Level 5 Castle", "Castle");
                    skillWindow = true;
                    break;
                default:
                    break;
            }
        }
    }

    private void DrawBarracksUpgrade()
    {
        if (GUI.Button(new Rect(scaledResolutionWidth * 4 / 16 - upgradeSize.x / 2, nativeVerticalResolution * 2 / 7 - upgradeSize.y / 2,
                upgradeSize.x, upgradeSize.y), castleUpgrade, "BlankButton"))
        {
            if (!UserData.Instance.barracks)
            {
                skillWindow = true;
                SetSkillInfo(250, "Barracks Description", "Barracks Upgrade", "Barracks");
            }
        }
    }

    private void DrawArcheryUpgrade()
    {
        if (GUI.Button(new Rect(scaledResolutionWidth * 12 / 16 - upgradeSize.x / 2, nativeVerticalResolution * 2 / 7 - upgradeSize.y / 2,
                upgradeSize.x, upgradeSize.y), castleUpgrade, "BlankButton"))
        {
            if (!UserData.Instance.archeryRange)
            {
                skillWindow = true;
                SetSkillInfo(250, "Archery Description", "Archery Range", "Archery");
            }
        }
    }

    private void DrawAlchemyUpgrade()
    {
        if (GUI.Button(new Rect(scaledResolutionWidth * 4 / 16 - upgradeSize.x / 2, nativeVerticalResolution * 4.5f / 7 - upgradeSize.y / 2,
                upgradeSize.x, upgradeSize.y), cooldown, "BlankButton"))
        {
            if (!UserData.Instance.alchemyLab)
            {
                skillWindow = true;
                SetSkillInfo(250, "Alchemy Description", "Alchemy Lab", "Alchemy");
            }
        }
    }

    private void DrawCastleFix()
    {
        if (GUI.Button(new Rect(scaledResolutionWidth * 12 / 16 - upgradeSize.x / 2, nativeVerticalResolution * 4.5f / 7 - upgradeSize.y / 2,
                upgradeSize.x, upgradeSize.y), castleFix, "BlankButton"))
        {
            if (UserData.Instance.castleHealth < UserData.Instance.maxCastleHealth)
            {
                skillWindow = true;
                SetSkillInfo(250, "Fix Castle", "Fix Castle", "Fix");
            }
        }
    }

    private void CheckCastleAvailable(int price)
    {
        if (UserData.Instance.gold > price)
        {
            UserStatus.Instance.GoldExchange(-price);
            UserStatus.Instance.IncreaseCastle();
            skillWindow = false;
        }
    }

    private void CheckBarracksAvailable(int price)
    {
        if (UserData.Instance.gold > price)
        {
            UserStatus.Instance.GoldExchange(-price);
            UserStatus.Instance.SetBarracks();
            skillWindow = false;
        }
    }

    private void CheckArcheryAvailable(int price)
    {
        if (UserData.Instance.gold > price)
        {
            UserStatus.Instance.GoldExchange(-price);
            UserStatus.Instance.SetArcheryRange();
            skillWindow = false;
        }
    }

    private void CheckAlchemyAvailable(int price)
    {
        if (UserData.Instance.gold > price)
        {
            UserStatus.Instance.GoldExchange(-price);
            UserStatus.Instance.SetAlchemyLab();
            skillWindow = false;
        }
    }

    private void CheckCastleFixAvailable(int price)
    {
        if (UserData.Instance.gold > price)
        {
            UserStatus.Instance.GoldExchange(-price);
            UserStatus.Instance.DamageCastle(-15);
            skillWindow = false;
        }
    }

    private void DrawSkillWindow()
    {
        if (skillWindow)
        {
            GUI.Box(new Rect(scaledResolutionWidth / 2 - 600, nativeVerticalResolution / 2 - 425, 1200, 750), skillName, "Name");
            if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 350, nativeVerticalResolution / 2 + 125, 300, 100), "Cancel"))
            {
                skillWindow = false;
            }
            if (GUI.Button(new Rect(scaledResolutionWidth / 2 + 100, nativeVerticalResolution / 2 + 125, 300, 100), "Buy (" + skillPrice + "G)"))
            {
                switch (skillType)
                {
                    case "Castle": CheckCastleAvailable(skillPrice);
                        break;
                    case "Barracks": CheckBarracksAvailable(skillPrice);
                        break;
                    case "Archery": CheckArcheryAvailable(skillPrice);
                        break;
                    case "Cooldown": CheckAlchemyAvailable(skillPrice);
                        break;
                    case "Fix": CheckCastleFixAvailable(skillPrice);
                        break;
                }
            }
            GUI.Label(new Rect(scaledResolutionWidth / 2 - 550, nativeVerticalResolution / 2 - 250, 1100, 400), skillDescription, "Description");
        }
    }

    private void SetSkillInfo(int price, string description, string name, string type)
    {
        skillPrice = price;
        skillDescription = description;
        skillName = name;
        skillType = type;
    }
}
