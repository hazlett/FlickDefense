using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class SkillGUI : MonoBehaviour
{
    public GUISkin postgameSkin;
    public Texture2D[] fireTexture = new Texture2D[10], lightningTexture = new Texture2D[10], iceTexture = new Texture2D[10];
    public Texture2D background;

    private Texture2D[] fire = new Texture2D[5], lightning = new Texture2D[5], ice = new Texture2D[5];
    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI;
    private Vector2 labelSize = new Vector2(700, 150), buttonSize = new Vector2(500, 100), headerSize = new Vector2(750, 100), skillBlock = new Vector2(200, 200);
    private bool skillWindow;
    private string skillName, skillDescription, skillType;
    private int skillPrice;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            fire[i] = fireTexture[i + 5];
            lightning[i] = lightningTexture[i + 5];
            ice[i] = iceTexture[i + 5];
        }
    }

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

        GUI.Label(new Rect(scaledResolutionWidth / 2 - headerSize.x / 2, headerSize.y * 0.25f, headerSize.x, headerSize.y), "Skill Upgrades", "Header");

        if (!skillWindow)
        {
            LightningSkill();
            FireSkill();
            IceSkill();
        }

        DrawSkillWindow();

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

        GUI.DrawTexture(new Rect(scaledResolutionWidth * 4 / 16 - skillBlock.x / 2, nativeVerticalResolution * 1.5f / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), lightning[0]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 6 / 16 - skillBlock.x / 2, nativeVerticalResolution * 1.5f / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), lightning[1]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 8 / 16 - skillBlock.x / 2, nativeVerticalResolution * 1.5f / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), lightning[2]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 10 / 16 - skillBlock.x / 2, nativeVerticalResolution * 1.5f / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), lightning[3]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 12 / 16 - skillBlock.x / 2, nativeVerticalResolution * 1.5f / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), lightning[4]);

        switch (UserStatus.Instance.LightningLevel)
        {
            case 0: if (GUI.Button(new Rect(scaledResolutionWidth * 4 / 16 - skillBlock.x / 2, nativeVerticalResolution * 1.5f / 7 - skillBlock.y / 2, 
                skillBlock.x, skillBlock.y), lightningTexture[0], "BlankButton"))
                {
                    skillWindow = true;
                    SetSkillInfo(100, "Strike down 1 enemy with a powerful lightning spell.  Kills any enemy (aside from bosses) on contact. ", "Lightning Strike", "Lightning");
                }
                break;
            case 1:
                if (GUI.Button(new Rect(scaledResolutionWidth * 6 / 16 - skillBlock.x / 2, nativeVerticalResolution * 1.5f / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), lightningTexture[1], "BlankButton"))
                {
                    skillWindow = true;
                    SetSkillInfo(200, "Strike down up to 4 enemies with this more powerful spell. Lighting will chain from one enemy to another.", "Chain Strike 1", "Lightning");
                }
                break;
            case 2:
                if (GUI.Button(new Rect(scaledResolutionWidth * 8 / 16 - skillBlock.x / 2, nativeVerticalResolution * 1.5f / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), lightningTexture[2], "BlankButton"))
                {
                    skillWindow = true;
                    SetSkillInfo(300, "Strike down up to 7 enemies with this more powerful spell. Lighting will chain from one enemy to another.", "Chain Strike 2", "Lightning");
                }
                break;
            case 3:
                if (GUI.Button(new Rect(scaledResolutionWidth * 10 / 16 - skillBlock.x / 2, nativeVerticalResolution * 1.5f / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), lightningTexture[3], "BlankButton"))
                {
                    skillWindow = true;
                    SetSkillInfo(400, "Strike down up to 10 enemies with this more powerful spell. Lighting will chain from one enemy to another.", "Chain Strike 3", "Lightning");
                }
                break;
            case 4:
                if (GUI.Button(new Rect(scaledResolutionWidth * 12 / 16 - skillBlock.x / 2, nativeVerticalResolution * 1.5f / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), lightningTexture[4], "BlankButton"))
                {
                    skillWindow = true;
                    SetSkillInfo(500, "Summon a powerful storm of lighting that will shock enemies all over the map for a short amount of time.", "Lightning Storm", "Lightning");
                }
                break;
            default:
                break;
        }
    }

    private void FireSkill()
    {

        GUI.DrawTexture(new Rect(scaledResolutionWidth * 4 / 16 - skillBlock.x / 2, nativeVerticalResolution * 3 / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), fire[0]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 6 / 16 - skillBlock.x / 2, nativeVerticalResolution * 3 / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), fire[1]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 8 / 16 - skillBlock.x / 2, nativeVerticalResolution * 3 / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), fire[2]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 10 / 16 - skillBlock.x / 2, nativeVerticalResolution * 3 / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), fire[3]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 12 / 16 - skillBlock.x / 2, nativeVerticalResolution * 3 / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), fire[4]);

        switch (UserStatus.Instance.FireLevel)
        {
            case 0: if (GUI.Button(new Rect(scaledResolutionWidth * 4 / 16 - skillBlock.x / 2, nativeVerticalResolution * 3 / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), fireTexture[0], "BlankButton"))
                {
                    skillWindow = true;
                    SetSkillInfo(100, "Unleash a fireball that will engulf your foe and burn them over time. ", "Fireball", "Fire");
                }
                break;
            case 1:
                if (GUI.Button(new Rect(scaledResolutionWidth * 6 / 16 - skillBlock.x / 2, nativeVerticalResolution * 3 / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), fireTexture[1], "BlankButton"))
                {
                    skillWindow = true;
                    SetSkillInfo(200, "The fireball will now damage surrounding enemies (Up to 3) and damage them over time. ", "Fire Blast", "Fire");
                }
                break;
            case 2:
                if (GUI.Button(new Rect(scaledResolutionWidth * 8 / 16 - skillBlock.x / 2, nativeVerticalResolution * 3 / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), fireTexture[2], "BlankButton"))
                {
                    skillWindow = true;
                    SetSkillInfo(300, "All enemies who try to pass through the fire storm will be burned, damaging them over time.", "Fire Storm", "Fire");
                }
                break;
            case 3:
                if (GUI.Button(new Rect(scaledResolutionWidth * 10 / 16 - skillBlock.x / 2, nativeVerticalResolution * 3 / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), fireTexture[3], "BlankButton"))
                {
                    skillWindow = true;
                    SetSkillInfo(400, "A wall of fire will be summoned in the path of your foes, damaging any that attempt to pass through.", "Fire Wall", "Fire");
                }
                break;
            case 4:
                if (GUI.Button(new Rect(scaledResolutionWidth * 12 / 16 - skillBlock.x / 2, nativeVerticalResolution * 3 / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), fireTexture[4], "BlankButton"))
                {
                    skillWindow = true;
                    SetSkillInfo(500, "Open up the sky and rain fire down upon all foes for a short period of time.", "Rain of Fire", "Fire");
                }
                break;
            default:
                break;
        }

    }

    private void IceSkill()
    {

        GUI.DrawTexture(new Rect(scaledResolutionWidth * 4 / 16 - skillBlock.x / 2, nativeVerticalResolution * 4.5f / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), ice[0]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 6 / 16 - skillBlock.x / 2, nativeVerticalResolution * 4.5f / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), ice[1]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 8 / 16 - skillBlock.x / 2, nativeVerticalResolution * 4.5f / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), ice[2]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 10 / 16 - skillBlock.x / 2, nativeVerticalResolution * 4.5f / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), ice[3]);
        GUI.DrawTexture(new Rect(scaledResolutionWidth * 12 / 16 - skillBlock.x / 2, nativeVerticalResolution * 4.5f / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), ice[4]);

        switch (UserStatus.Instance.IceLevel)
        {
            case 0: if (GUI.Button(new Rect(scaledResolutionWidth * 4 / 16 - skillBlock.x / 2, nativeVerticalResolution * 4.5f / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), iceTexture[0], "BlankButton"))
                {
                    skillWindow = true;
                    SetSkillInfo(100, "Slows down 1 enemy for a short amount of time. ", "Iceball", "Ice");
                }
                break;
            case 1:
                if (GUI.Button(new Rect(scaledResolutionWidth * 6 / 16 - skillBlock.x / 2, nativeVerticalResolution * 4.5f / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), iceTexture[1], "BlankButton"))
                {
                    skillWindow = true;
                    SetSkillInfo(200, "Slows down up to 3 enemies for a short amount of time. ", "Ice Blast", "Ice");
                }
                break;
            case 2:
                if (GUI.Button(new Rect(scaledResolutionWidth * 8 / 16 - skillBlock.x / 2, nativeVerticalResolution * 4.5f / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), iceTexture[2], "BlankButton"))
                {
                    skillWindow = true;
                    SetSkillInfo(300, "Slows down enemies that it hits for a short amount of time ", "Ice Explosion", "Ice");
                }
                break;
            case 3:
                if (GUI.Button(new Rect(scaledResolutionWidth * 10 / 16 - skillBlock.x / 2, nativeVerticalResolution * 4.5f / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), iceTexture[3], "BlankButton"))
                {
                    skillWindow = true;
                    SetSkillInfo(400, "Slows down all enemies who pass through a set area for a short amount of time.  Helpful to hold back a lot of enemies. ", "Ice Storm", "Ice");
                }
                break;
            case 4:
                if (GUI.Button(new Rect(scaledResolutionWidth * 12 / 16 - skillBlock.x / 2, nativeVerticalResolution * 4.5f / 7 - skillBlock.y / 2, skillBlock.x, skillBlock.y), iceTexture[4], "BlankButton"))
                {
                    skillWindow = true;
                    SetSkillInfo(500, "Slows down all of the enemies on screen for a short amount of time.  Very useful when you're being overwhelmed.", "Blizzard", "Ice");
                }
                break;
            default:
                break;
        }

    }

    private void CheckLightningAvailable(int level)
    {
        if (UserStatus.Instance.Gold > (level + 1) * 100)
        {
            lightning[level] = lightningTexture[level];
            UserStatus.Instance.GoldExchange(-(level + 1) * 100);
            UserStatus.Instance.IncreaseLightning();
            skillWindow = false;
        }
    }

    private void CheckFireAvailable(int level)
    {
        if (UserStatus.Instance.Gold > (level + 1) * 100)
        {
            fire[level] = fireTexture[level];
            UserStatus.Instance.GoldExchange(-(level + 1) * 100);
            UserStatus.Instance.IncreaseFire();
            skillWindow = false;
        }
    }

    private void CheckIceAvailable(int level)
    {
        if (UserStatus.Instance.Gold > (level + 1) * 100)
        {
            ice[level] = iceTexture[level];
            UserStatus.Instance.GoldExchange(-(level + 1) * 100);
            UserStatus.Instance.IncreaseIce();
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
                    case "Lightning": CheckLightningAvailable(UserStatus.Instance.LightningLevel);
                        break;
                    case "Fire": CheckFireAvailable(UserStatus.Instance.FireLevel);
                        break;
                    case "Ice": CheckIceAvailable(UserStatus.Instance.IceLevel);
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
