using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

    public static GUIManager Instance
    {
        get { return GameObject.FindObjectOfType<GUIManager>(); }
        private set { }
    }

    private WaveGUI Wave;
    private GameplayGUI Gameplay;
    private PostgameGUI Postgame;
    private UpgradeGUI Upgrade;
    private SkillGUI Skill;
    private GameoverGUI Gameover;
    private MainMenuGUI MainMenu;

    void Awake()
    {
        if (Instance != null && Instance != this) Debug.LogError("Cannot create more than one GUI manager");
        else Instance = this;
        Wave = this.GetComponent<WaveGUI>();
        Gameplay = this.GetComponent<GameplayGUI>();
        Postgame = this.GetComponent<PostgameGUI>();
        Upgrade = this.GetComponent<UpgradeGUI>();
        Skill = this.GetComponent<SkillGUI>();
        Gameover = this.GetComponent<GameoverGUI>();
        MainMenu = this.GetComponent<MainMenuGUI>();
    }

    public enum GUISystem
    {
        WaveGUI,
        GameplayGUI,
        PostgameGUI,
        UpgradeGUI,
        SkillGUI,
        GameoverGUI,
        MainMenuGUI
    }

	public void MaximizeGUI(GUISystem guiType)
    {
        ChangeGUIState(guiType, true);
        //Set up any custom ops for maximizing
        switch (guiType)
        {
            case GUISystem.GameoverGUI:
                break;
            case GUISystem.GameplayGUI:
                break;
            case GUISystem.PostgameGUI:
                break;
            case GUISystem.SkillGUI:
                break;
            case GUISystem.UpgradeGUI:
                break;
            case GUISystem.WaveGUI:
                break;
            case GUISystem.MainMenuGUI:
                break;
            default:
                break;
        }
    }

    public void MinimizeGUI(GUISystem guiType)
    {
        ChangeGUIState(guiType, false);
        //Set up any custom ops for maximizing
        switch (guiType)
        {
            case GUISystem.GameoverGUI:
                break;
            case GUISystem.GameplayGUI:
                break;
            case GUISystem.PostgameGUI:
                break;
            case GUISystem.SkillGUI:
                break;
            case GUISystem.UpgradeGUI:
                break;
            case GUISystem.WaveGUI:
                break;
            case GUISystem.MainMenuGUI:
                break;
            default:
                break;
        }
    }

    private void ChangeGUIState(GUISystem system, bool state)
    {
        switch (system)
        {
            case GUISystem.GameoverGUI:
                Gameover.enabled = state;
                break;
            case GUISystem.GameplayGUI:
                Gameplay.enabled = state;
                break;
            case GUISystem.PostgameGUI:
                Postgame.enabled = state;
                break;
            case GUISystem.SkillGUI:
                Skill.enabled = state;
                break;
            case GUISystem.UpgradeGUI:
                Upgrade.enabled = state;
                break;
            case GUISystem.WaveGUI:
                Wave.enabled = state;
                break;
            case GUISystem.MainMenuGUI:
                MainMenu.enabled = state;
                break;
            default:
                break;
        }
    }
}
