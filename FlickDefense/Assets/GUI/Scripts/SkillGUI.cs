using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class SkillGUI : MonoBehaviour
{
    public GUISkin postgameSkin;
    public Texture2D[] fireTexture = new Texture2D[15], lightningTexture = new Texture2D[15], iceTexture = new Texture2D[15];

    private Texture2D[] fire = new Texture2D[5], lightning = new Texture2D[5], ice = new Texture2D[5];
    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI;
    private Vector2 labelSize = new Vector2(700, 150), buttonSize = new Vector2(500, 100), headerSize = new Vector2(750, 100);

    void OnEnable()
    {
        updateGUI = 0.5f;
        nativeVerticalResolution = 1080.0f;
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;

        for (int i = 0; i < 5; i++)
        {
            fire[i] = fireTexture[i + 5];
            lightning[i] = lightningTexture[i + 5];
            ice[i] = iceTexture[i + 5];
        }

        InvokeRepeating("TimedScreenResize", updateGUI, updateGUI);
    }

    void Update()
    {

    }

    void OnGUI()
    {
        GUI.skin = postgameSkin;

        // Scale the GUI to any resolution based on 1920 x 1080 base resolution
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.height / nativeVerticalResolution, Screen.height / nativeVerticalResolution, 1));

        GUI.Label(new Rect(scaledResolutionWidth / 2 - headerSize.x / 2, headerSize.y * 0.25f, headerSize.x, headerSize.y), "Skill Upgrades", "Header");

        LightningSkill();
        FireSkill();
        IceSkill();

        GUI.Label(new Rect(scaledResolutionWidth / 2 - labelSize.x / 2, nativeVerticalResolution * 5.5f / 7 - labelSize.y / 2, labelSize.x, labelSize.y), "Gold: " + UserStatus.Instance.Gold.ToString(), "CenterLabel");

        if (GUI.Button(new Rect(scaledResolutionWidth / 4 - buttonSize.x * 3 / 4, nativeVerticalResolution - buttonSize.y - 50, buttonSize.x, buttonSize.y), "Main Menu"))
        {
            GameStateManager.Instance.IsMainMenu();
            this.enabled = false;
        }

        if (GUI.Button(new Rect(scaledResolutionWidth / 2 - buttonSize.x / 2, nativeVerticalResolution - buttonSize.y - 50, buttonSize.x, buttonSize.y), "Castle Upgrades"))
        {
            GameStateManager.Instance.IsUpgrading();
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

    private void LightningSkill()
    {

        switch (UserStatus.Instance.LightningLevel)
        {
            case 0: if (GUI.Button(new Rect(scaledResolutionWidth * 4 / 16 - lightningTexture[0].width / 2, nativeVerticalResolution * 1.5f / 7 - lightningTexture[0].height / 2, lightningTexture[0].width, lightningTexture[0].height), lightningTexture[0]))
                {
                    CheckLightningAvailable(UserStatus.Instance.LightningLevel);
                }
                break;
            case 1:
                if (GUI.Button(new Rect(scaledResolutionWidth * 6 / 16 - lightningTexture[0].width / 2, nativeVerticalResolution  * 1.5f / 7 - lightningTexture[0].height / 2, lightningTexture[0].width, lightningTexture[0].height), lightningTexture[1]))
                {
                    CheckLightningAvailable(UserStatus.Instance.LightningLevel);
                }
                break;
            case 2:
                if (GUI.Button(new Rect(scaledResolutionWidth * 8 / 16 - lightningTexture[0].width / 2, nativeVerticalResolution * 1.5f / 7 - lightningTexture[0].height / 2, lightningTexture[0].width, lightningTexture[0].height), lightningTexture[2]))
                {
                    CheckLightningAvailable(UserStatus.Instance.LightningLevel);
                }
                break;
            case 3:
                if (GUI.Button(new Rect(scaledResolutionWidth * 10 / 16 - lightningTexture[0].width / 2, nativeVerticalResolution * 1.5f / 7 - lightningTexture[0].height / 2, lightningTexture[0].width, lightningTexture[0].height), lightningTexture[3]))
                {
                    CheckLightningAvailable(UserStatus.Instance.LightningLevel);
                }
                break;
            case 4:
                if (GUI.Button(new Rect(scaledResolutionWidth * 12 / 16 - lightningTexture[0].width / 2, nativeVerticalResolution * 1.5f / 7 - lightningTexture[0].height / 2, lightningTexture[0].width, lightningTexture[0].height), lightningTexture[4]))
                {
                    CheckLightningAvailable(UserStatus.Instance.LightningLevel);
                }
                break;
            default:
                break;
        }
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 4 / 16 - lightningTexture[0].width / 2, nativeVerticalResolution * 1.5f / 7 - lightningTexture[0].height / 2, lightningTexture[0].width, lightningTexture[0].height), lightning[0]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 6 / 16 - lightningTexture[0].width / 2, nativeVerticalResolution * 1.5f / 7 - lightningTexture[0].height / 2, lightningTexture[0].width, lightningTexture[0].height), lightning[1]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 8 / 16 - lightningTexture[0].width / 2, nativeVerticalResolution * 1.5f / 7 - lightningTexture[0].height / 2, lightningTexture[0].width, lightningTexture[0].height), lightning[2]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 10 / 16 - lightningTexture[0].width / 2, nativeVerticalResolution * 1.5f / 7  - lightningTexture[0].height / 2, lightningTexture[0].width, lightningTexture[0].height), lightning[3]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 12 / 16 - lightningTexture[0].width / 2, nativeVerticalResolution * 1.5f / 7  - lightningTexture[0].height / 2, lightningTexture[0].width, lightningTexture[0].height), lightning[4]);
    }

    private void FireSkill()
    {

        switch (UserStatus.Instance.FireLevel)
        {
            case 0: if (GUI.Button(new Rect(scaledResolutionWidth * 4 / 16 - fireTexture[0].width / 2, nativeVerticalResolution * 3 / 7 - fireTexture[0].height / 2, fireTexture[0].width, fireTexture[0].height), fireTexture[0]))
                {
                    CheckFireAvailable(UserStatus.Instance.FireLevel);
                }
                break;
            case 1:
                if (GUI.Button(new Rect(scaledResolutionWidth * 6 / 16 - fireTexture[0].width / 2, nativeVerticalResolution * 3 / 7 - fireTexture[0].height / 2, fireTexture[0].width, fireTexture[0].height), fireTexture[1]))
                {
                    CheckFireAvailable(UserStatus.Instance.FireLevel);
                }
                break;
            case 2:
                if (GUI.Button(new Rect(scaledResolutionWidth * 8 / 16 - fireTexture[0].width / 2, nativeVerticalResolution * 3 / 7 - fireTexture[0].height / 2, fireTexture[0].width, fireTexture[0].height), fireTexture[2]))
                {
                    CheckFireAvailable(UserStatus.Instance.FireLevel);
                }
                break;
            case 3:
                if (GUI.Button(new Rect(scaledResolutionWidth * 10 / 16 - fireTexture[0].width / 2, nativeVerticalResolution * 3 / 7 - fireTexture[0].height / 2, fireTexture[0].width, fireTexture[0].height), fireTexture[3]))
                {
                    CheckFireAvailable(UserStatus.Instance.FireLevel);
                }
                break;
            case 4:
                if (GUI.Button(new Rect(scaledResolutionWidth * 12 / 16 - fireTexture[0].width / 2, nativeVerticalResolution * 3 / 7 - fireTexture[0].height / 2, fireTexture[0].width, fireTexture[0].height), fireTexture[4]))
                {
                    CheckFireAvailable(UserStatus.Instance.FireLevel);
                }
                break;
            default:
                break;
        }
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 4 / 16 - fireTexture[0].width / 2, nativeVerticalResolution * 3 / 7 - fireTexture[0].height / 2, fireTexture[0].width, fireTexture[0].height), fire[0]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 6 / 16 - fireTexture[0].width / 2, nativeVerticalResolution * 3 / 7 - fireTexture[0].height / 2, fireTexture[0].width, fireTexture[0].height), fire[1]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 8 / 16 - fireTexture[0].width / 2, nativeVerticalResolution * 3 / 7 - fireTexture[0].height / 2, fireTexture[0].width, fireTexture[0].height), fire[2]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 10 / 16 - fireTexture[0].width / 2, nativeVerticalResolution * 3 / 7 - fireTexture[0].height / 2, fireTexture[0].width, fireTexture[0].height), fire[3]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 12 / 16 - fireTexture[0].width / 2, nativeVerticalResolution * 3 / 7 - fireTexture[0].height / 2, fireTexture[0].width, fireTexture[0].height), fire[4]);

    }

    private void IceSkill()
    {

        switch (UserStatus.Instance.IceLevel)
        {
            case 0: if (GUI.Button(new Rect(scaledResolutionWidth * 4 / 16 - iceTexture[0].width / 2, nativeVerticalResolution * 4.5f / 7 - iceTexture[0].height / 2, iceTexture[0].width, iceTexture[0].height), iceTexture[0]))
                {
                    CheckIceAvailable(UserStatus.Instance.IceLevel);
                }
                break;
            case 1:
                if (GUI.Button(new Rect(scaledResolutionWidth * 6 / 16 - iceTexture[0].width / 2, nativeVerticalResolution * 4.5f / 7 - iceTexture[0].height / 2, iceTexture[0].width, iceTexture[0].height), iceTexture[1]))
                {
                    CheckIceAvailable(UserStatus.Instance.IceLevel);
                }
                break;
            case 2:
                if (GUI.Button(new Rect(scaledResolutionWidth * 8 / 16 - iceTexture[0].width / 2, nativeVerticalResolution * 4.5f / 7 - iceTexture[0].height / 2, iceTexture[0].width, iceTexture[0].height), iceTexture[2]))
                {
                    CheckIceAvailable(UserStatus.Instance.IceLevel);
                }
                break;
            case 3:
                if (GUI.Button(new Rect(scaledResolutionWidth * 10 / 16 - iceTexture[0].width / 2, nativeVerticalResolution * 4.5f / 7 - iceTexture[0].height / 2, iceTexture[0].width, iceTexture[0].height), iceTexture[3]))
                {
                    CheckIceAvailable(UserStatus.Instance.IceLevel);
                }
                break;
            case 4:
                if (GUI.Button(new Rect(scaledResolutionWidth * 12 / 16 - iceTexture[0].width / 2, nativeVerticalResolution * 4.5f / 7 - iceTexture[0].height / 2, iceTexture[0].width, iceTexture[0].height), iceTexture[4]))
                {
                    CheckIceAvailable(UserStatus.Instance.IceLevel);
                }
                break;
            default:
                break;
        }
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 4 / 16 - iceTexture[0].width / 2, nativeVerticalResolution * 4.5f / 7 - iceTexture[0].height / 2, iceTexture[0].width, iceTexture[0].height), ice[0]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 6 / 16 - iceTexture[0].width / 2, nativeVerticalResolution * 4.5f / 7 - iceTexture[0].height / 2, iceTexture[0].width, iceTexture[0].height), ice[1]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 8 / 16 - iceTexture[0].width / 2, nativeVerticalResolution * 4.5f / 7 - iceTexture[0].height / 2, iceTexture[0].width, iceTexture[0].height), ice[2]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 10 / 16 - iceTexture[0].width / 2, nativeVerticalResolution * 4.5f / 7 - iceTexture[0].height / 2, iceTexture[0].width, iceTexture[0].height), ice[3]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 12 / 16 - iceTexture[0].width / 2, nativeVerticalResolution * 4.5f / 7 - iceTexture[0].height / 2, iceTexture[0].width, iceTexture[0].height), ice[4]);

    }

    private void CheckLightningAvailable(int level)
    {
        if (UserStatus.Instance.Gold > (level + 1) * 100)
        {
            lightning[level] = lightningTexture[level + 10];
            UserStatus.Instance.GoldExchange(-(level + 1) * 100);
            UserStatus.Instance.IncreaseLightning();
        }
    }

    private void CheckFireAvailable(int level)
    {
        if (UserStatus.Instance.Gold > (level + 1) * 100)
        {
            fire[level] = fire[level + 10];
            UserStatus.Instance.GoldExchange(-(level + 1) * 100);
            UserStatus.Instance.IncreaseFire();
        }
    }

    private void CheckIceAvailable(int level)
    {
        if (UserStatus.Instance.Gold > (level + 1) * 100)
        {
            ice[level] = iceTexture[level + 10];
            UserStatus.Instance.GoldExchange(-(level + 1) * 100);
            UserStatus.Instance.IncreaseIce();
        }
    }

}
