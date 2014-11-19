using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class GameplayGUI : MonoBehaviour
{
    public GUISkin gameplaySkin;
    public Texture2D[] fireTexture, lightningTexture, iceTexture;
    public Texture2D crackedScreen, popupWindow, currentSkillTexture, backdropActive, backdropInactive;

    internal bool cracked, skillPopup;
    internal int level = 5;

    private Texture2D backdropCurrent;
    private SkillHandler.Skills currentSkill = SkillHandler.Skills.NONE;
    private Rect skillActivate;
    private Rect skillButton;
    private float screenHeight, screenWidth, updateGUI, cooldownTransparency;
    private Vector2 labelSize = new Vector2(500, 200), buttonSize = new Vector2(500, 100), headerSize = new Vector2(750, 100);

    void OnEnable()
    {
        backdropCurrent = backdropInactive;

        updateGUI = 0.5f;
        screenHeight = 1080.0f;
        screenWidth = screenHeight / Screen.height * Screen.width;

        skillActivate = new Rect(10, 10, screenWidth / 9, screenWidth / 9);
        skillButton = new Rect(10, 10 + screenWidth / 9, screenWidth / 9, screenWidth / 27);

        InvokeRepeating("TimedScreenResize", updateGUI, updateGUI);
    }

    void Update()
    {
        CooldownBehavior();
    }

    void OnGUI()
    {
        GUI.skin = gameplaySkin;

        // Scale the GUI to any resolution based on 1920 x 1080 base resolution
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.height / screenHeight, Screen.height / screenHeight, 1));

        if (!skillPopup)
        {
            GUI.Label(new Rect(screenWidth - screenWidth / 4 - 10, 10, screenWidth / 4, screenWidth / 12), "Enemies: " + GameStateManager.Instance.enemyCount + " out of " + WaveSystem.Instance.EnemyCount());
        }

        if (cooldownTransparency < 1.0f)
        {
            GUI.DrawTexture(skillActivate, backdropCurrent);
        }
        else
        {
            if (GUI.Button(skillActivate, backdropCurrent))
            {
                SkillHandler.Instance.currentSkill = currentSkill;
                backdropCurrent = backdropActive;
            }
        }

        GUI.color = new Color(1.0f, 1.0f, 1.0f, cooldownTransparency);
        GUI.DrawTexture(new Rect(40, 40, screenWidth / 12.5f, screenWidth / 12.5f), currentSkillTexture);
        GUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        if (GUI.Button(skillButton, "Skills", "PopupButton"))
        {
            if (skillPopup) { skillPopup = false; }
            else
            {
                backdropCurrent = backdropInactive;
                SkillHandler.Instance.currentSkill = SkillHandler.Skills.NONE;
                skillPopup = true;
            }
        }

        DrawSkillPopup();

        if (cracked)
        {
            GUI.DrawTexture(new Rect(0, 0, screenWidth, screenHeight), crackedScreen);
        }
    }

    private void DrawSkillPopup()
    {
        if (skillPopup)
        {
            GUI.DrawTexture(new Rect(screenWidth * 2f / 15, screenHeight * 0.75f / 15, screenWidth * 11 / 15, screenHeight * 13.5f / 15), popupWindow);
            FireSkills();
            IceSkills();
            LightningSkills();
        }
    }

    private void FireSkills()
    {
        switch (level)
        {
            case 5: if (GUI.Button(new Rect(screenWidth * 11.5f / 15 - screenWidth / 20, screenHeight * 8f / 15 - screenWidth / 20, screenWidth / 10, screenWidth / 10), fireTexture[4]))
                {
                    currentSkill = SkillHandler.Skills.RAINOFFIRE;
                    currentSkillTexture = fireTexture[4];
                    skillPopup = false;
                }
                goto case 4;
            case 4: if (GUI.Button(new Rect(screenWidth * 9.5f / 15 - screenWidth / 20, screenHeight * 8f / 15 - screenWidth / 20, screenWidth / 10, screenWidth / 10), fireTexture[3]))
                {
                    currentSkill = SkillHandler.Skills.FIREWALL;
                    currentSkillTexture = fireTexture[3];
                    skillPopup = false;
                } 
                goto case 3;
            case 3: if (GUI.Button(new Rect(screenWidth * 7.5f / 15 - screenWidth / 20, screenHeight * 8f / 15 - screenWidth / 20, screenWidth / 10, screenWidth / 10), fireTexture[2]))
                {
                    currentSkill = SkillHandler.Skills.FIRESTORM;
                    currentSkillTexture = fireTexture[2];
                    skillPopup = false;
                } 
                goto case 2;
            case 2: if (GUI.Button(new Rect(screenWidth * 5.5f / 15 - screenWidth / 20, screenHeight * 8f / 15 - screenWidth / 20, screenWidth / 10, screenWidth / 10), fireTexture[1]))
                {
                    currentSkill = SkillHandler.Skills.FIREBLAST;
                    currentSkillTexture = fireTexture[1];
                    skillPopup = false;
                } goto case 1;
            case 1: if (GUI.Button(new Rect(screenWidth * 3.5f / 15 - screenWidth / 20, screenHeight * 8f / 15 - screenWidth / 20, screenWidth / 10, screenWidth / 10), fireTexture[0]))
                {
                    currentSkill = SkillHandler.Skills.FIREBALL;
                    currentSkillTexture = fireTexture[0];
                    skillPopup = false;
                } 
                break;
            default:
                break;

        }
    }

    private void IceSkills()
    {
        switch (level)
        {
            case 5: if (GUI.Button(new Rect(screenWidth * 11.5f / 15 - screenWidth / 20, screenHeight * 11.5f / 15 - screenWidth / 20, screenWidth / 10, screenWidth / 10), iceTexture[4]))
                {
                    currentSkill = SkillHandler.Skills.BLIZZARD;
                    currentSkillTexture = iceTexture[4];
                    skillPopup = false;
                }
                goto case 4;
            case 4: if (GUI.Button(new Rect(screenWidth * 9.5f / 15 - screenWidth / 20, screenHeight * 11.5f / 15 - screenWidth / 20, screenWidth / 10, screenWidth / 10), iceTexture[3]))
                {
                    currentSkill = SkillHandler.Skills.ICEWALL;
                    currentSkillTexture = iceTexture[3];
                    skillPopup = false;
                }
                goto case 3;
            case 3: if (GUI.Button(new Rect(screenWidth * 7.5f / 15 - screenWidth / 20, screenHeight * 11.5f / 15 - screenWidth / 20, screenWidth / 10, screenWidth / 10), iceTexture[2]))
                {
                    currentSkill = SkillHandler.Skills.ICESTORM;
                    currentSkillTexture = iceTexture[2];
                    skillPopup = false;
                }
                goto case 2;
            case 2: if (GUI.Button(new Rect(screenWidth * 5.5f / 15 - screenWidth / 20, screenHeight * 11.5f / 15 - screenWidth / 20, screenWidth / 10, screenWidth / 10), iceTexture[1]))
                {
                    currentSkill = SkillHandler.Skills.ICEBLAST;
                    currentSkillTexture = iceTexture[1];
                    skillPopup = false;
                } goto case 1;
            case 1: if (GUI.Button(new Rect(screenWidth * 3.5f / 15 - screenWidth / 20, screenHeight * 11.5f / 15 - screenWidth / 20, screenWidth / 10, screenWidth / 10), iceTexture[0]))
                {
                    currentSkill = SkillHandler.Skills.ICEBALL;
                    currentSkillTexture = iceTexture[0];
                    skillPopup = false;
                }
                break;
            default:
                break;

        }
    }

    private void LightningSkills()
    {
        switch (level)
        {
            case 5: if (GUI.Button(new Rect(screenWidth * 11.5f / 15 - screenWidth / 20, screenHeight * 4.5f / 15 - screenWidth / 20, screenWidth / 10, screenWidth / 10), lightningTexture[4]))
                {
                    currentSkill = SkillHandler.Skills.THUNDERSTORM;
                    currentSkillTexture = lightningTexture[4];
                    skillPopup = false;
                }
                goto case 4;
            case 4: if (GUI.Button(new Rect(screenWidth * 9.5f / 15 - screenWidth / 20, screenHeight * 4.5f / 15 - screenWidth / 20, screenWidth / 10, screenWidth / 10), lightningTexture[3]))
                {
                    currentSkill = SkillHandler.Skills.LIGHTNINGWALL;
                    currentSkillTexture = lightningTexture[3];
                    skillPopup = false;
                }
                goto case 3;
            case 3: if (GUI.Button(new Rect(screenWidth * 7.5f / 15 - screenWidth / 20, screenHeight * 4.5f / 15 - screenWidth / 20, screenWidth / 10, screenWidth / 10), lightningTexture[2]))
                {
                    currentSkill = SkillHandler.Skills.LIGHTNINGSTORM;
                    currentSkillTexture = lightningTexture[2];
                    skillPopup = false;
                }
                goto case 2;
            case 2: if (GUI.Button(new Rect(screenWidth * 5.5f / 15 - screenWidth / 20, screenHeight * 4.5f / 15 - screenWidth / 20, screenWidth / 10, screenWidth / 10), lightningTexture[1]))
                {
                    currentSkill = SkillHandler.Skills.CHAINLIGHTNING;
                    currentSkillTexture = lightningTexture[1];
                    skillPopup = false;
                } goto case 1;
            case 1: if (GUI.Button(new Rect(screenWidth * 3.5f / 15 - screenWidth / 20, screenHeight * 4.5f / 15 - screenWidth / 20, screenWidth / 10, screenWidth / 10), lightningTexture[0]))
                {
                    currentSkill = SkillHandler.Skills.LIGHTNINGSTRIKE;
                    currentSkillTexture = lightningTexture[0];
                    skillPopup = false;
                }
                break;
            default:
                break;

        }
    }

    internal bool Clicked(Vector2 touchPosition)
    {
        Vector2 touchFlipped = touchPosition;
        touchFlipped.y = Screen.height - touchPosition.y;

        if (skillButton.Contains(touchFlipped) || skillActivate.Contains(touchFlipped) || skillPopup)
        {
            return true;
        }
        return false;
    }

    private void CooldownBehavior()
    {
        if(SkillHandler.Instance.cooldownScale < 1.0f){
            backdropCurrent = backdropInactive;
            cooldownTransparency = SkillHandler.Instance.cooldownScale * 0.75f;
        }
        else if (SkillHandler.Instance.cooldownScale > 1.0f)
        {
            cooldownTransparency = 1.0f;
        }
    }

    private void TimedScreenResize()
    {
        screenWidth = screenHeight / Screen.height * Screen.width;
    }
}
