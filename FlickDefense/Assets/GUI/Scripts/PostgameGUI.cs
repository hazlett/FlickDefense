using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class PostgameGUI : MonoBehaviour
{
    public GUISkin postgameSkin;

    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI;
    private Vector2 labelSize = new Vector2(700, 150), buttonSize = new Vector2(500, 100), headerSize = new Vector2(750, 100);

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
        GUI.skin = postgameSkin;

        // Scale the GUI to any resolution based on 1920 x 1080 base resolution
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.height / nativeVerticalResolution, Screen.height / nativeVerticalResolution, 1));

        GUI.Label(new Rect(scaledResolutionWidth / 2 - headerSize.x / 2, headerSize.y * 0.25f, headerSize.x, headerSize.y), "Wave: " + (WaveSystem.Instance.waveNumber - 1).ToString() + " Stats", "Header");


        GUI.Label(new Rect(scaledResolutionWidth / 2 - labelSize.x / 3, nativeVerticalResolution / 4 - labelSize.y, labelSize.x, labelSize.y), "Wave # (Total #)", "SmallLabel");
        // Left Side Stats
        GUI.Label(new Rect(scaledResolutionWidth / 4 - labelSize.x / 2, nativeVerticalResolution / 4 - labelSize.y / 2, labelSize.x, labelSize.y), "Grunts Slaughtered:\t" + UserStatus.Instance.GruntsPastWave() + " (" + UserStatus.Instance.GruntsKilled + ")");
        GUI.Label(new Rect(scaledResolutionWidth / 4 - labelSize.x / 2, nativeVerticalResolution / 4 + labelSize.y / 2, labelSize.x, labelSize.y), "Archers Slaughtered:\t" + UserStatus.Instance.ArchersPastWave() + " (" + UserStatus.Instance.ArchersKilled + ")");
        GUI.Label(new Rect(scaledResolutionWidth / 4 - labelSize.x / 2, nativeVerticalResolution / 4 + 3 * labelSize.y / 2, labelSize.x, labelSize.y),"Catapults Demolished:\t" + UserStatus.Instance.CatapultsPastWave() + " (" + UserStatus.Instance.CatapultsKilled + ")");


        GUI.Label(new Rect(scaledResolutionWidth / 2 + labelSize.x * 2 / 3, nativeVerticalResolution / 4 - labelSize.y, labelSize.x, labelSize.y), "Wave # (Total #)", "SmallLabel");
        // Right Side Stats
        GUI.Label(new Rect(scaledResolutionWidth * 3 / 4 - labelSize.x / 2, nativeVerticalResolution / 4 - labelSize.y / 2, labelSize.x, labelSize.y), "Flyers Downed:\t" + UserStatus.Instance.FlyersPastWave() + " (" + UserStatus.Instance.FlyersKilled + ")");
        GUI.Label(new Rect(scaledResolutionWidth * 3 / 4 - labelSize.x / 2, nativeVerticalResolution / 4 + labelSize.y / 2, labelSize.x, labelSize.y), "Bombers Defused:\t" + UserStatus.Instance.BombersPastWave() + " (" + UserStatus.Instance.BombersKilled + ")");
        GUI.Label(new Rect(scaledResolutionWidth * 3 / 4 - labelSize.x / 2, nativeVerticalResolution / 4 + 3 * labelSize.y / 2, labelSize.x, labelSize.y), "Bosses Slain:\t" + UserStatus.Instance.BossesPastWave() + " (" + UserStatus.Instance.BossesKilled + ")");

        GUI.Label(new Rect(scaledResolutionWidth / 2 - labelSize.x / 2, nativeVerticalResolution * 3 / 4 - labelSize.y / 2, labelSize.x, labelSize.y), "Gold Earned: ");

        if (GUI.Button(new Rect(scaledResolutionWidth / 4 - buttonSize.x * 3 / 4, nativeVerticalResolution - buttonSize.y - 50, buttonSize.x, buttonSize.y), "Main Menu"))
        {
            GameStateManager.Instance.IsMainMenu();
            this.enabled = false;
        }

        if (GUI.Button(new Rect(scaledResolutionWidth / 2 - buttonSize.x / 2, nativeVerticalResolution - buttonSize.y - 50, buttonSize.x, buttonSize.y), "Upgrades"))
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

    private void TimedScreenResize()
    {
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
    }
}
