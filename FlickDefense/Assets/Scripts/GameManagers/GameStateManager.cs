using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour {

    private static  GameStateManager instance;
    public static GameStateManager Instance { get { return instance; } set { instance = value; } }

    internal int enemyCount { get { return WaveSystem.Instance.EnemyCount; } private set { } }
    internal bool flicking = true;
    internal bool InTransition = false;

    private const float DEFAULT_LOAD_TIME = 2;

    void Awake()
    {
        instance = this;
    }

    public enum GameState
    {
        MAINMENU,
        PREWAVE,
        PLAYING,
        POSTWAVE,
        UPGRADE,
        SKILLS,
        GAMEOVER,
        PAUSED
    }

    //Done in order to pass multiple values to corountine
    public class StateAndWait
    {
        public StateAndWait(GameState state, float time) {State = state; Time = time; }
        public GameState State;
        public float Time;
    }

    private GameState currentState = GameState.MAINMENU;
    internal GameState CurrentState{
        get { return currentState;}
        set {
            currentState = value;
            InTransition = false;
            //UpdateAndSave();
        }
    }

    public void IsMainMenu() {
        CurrentState = GameState.MAINMENU;
        Application.LoadLevel("NewMainLevel");
    }
    public void IsPaused()
    {
        if (CurrentState == GameState.PAUSED)
        {
            CurrentState = GameState.PLAYING;
            Debug.Log("Unpaused");
            Time.timeScale = 1.0f;
        }
        else if (CurrentState == GameState.PLAYING)
        {
            CurrentState = GameState.PAUSED;
            Debug.Log("Paused");
            Time.timeScale = 0;
        }
    }
    public void IsPrewave()
    {
        UserData.Instance.LoadData();
        CurrentState = GameState.PREWAVE;
        LoadCastle();
        UserStatus.Instance.NewWave();
        GUIManager.Instance.MaximizeGUI(GUIManager.GUISystem.WaveGUI);
        GUIManager.Instance.MaximizeGUI(GUIManager.GUISystem.GameplayGUI);
        StartCoroutine(GameStateManager.Instance.QueueStateTransition(
                new GameStateManager.StateAndWait(GameStateManager.GameState.PLAYING, 5f)));
    }

    public void IsPlaying()
    {
        CurrentState = GameState.PLAYING;
        WaveSystem.NewSession();
        WaveSystem.SpawnEnemies();
        GameController.Instance.enabled = true;
        GUIManager.Instance.MaximizeGUI(GUIManager.GUISystem.GameplayGUI);
        GUIManager.Instance.MaximizeGUI(GUIManager.GUISystem.WaveGUI);
    }

    public void IsPostWave()
    {
        CurrentState = GameState.POSTWAVE;
        GameController.Instance.enabled = false;

        UserStatus.Instance.SetPastKilled();
        GUIManager.Instance.MaximizeGUI(GUIManager.GUISystem.PostgameGUI);
        GUIManager.Instance.MinimizeGUI(GUIManager.GUISystem.GameplayGUI);
        GUIManager.Instance.MinimizeGUI(GUIManager.GUISystem.WaveGUI);
        WaveSystem.NextWave();
        UserData.Instance.SaveData();
    }

    public void IsUpgrading() { 
        CurrentState = GameState.UPGRADE;
        GUIManager.Instance.MaximizeGUI(GUIManager.GUISystem.UpgradeGUI);
    }

    public void IsSkills() {
        CurrentState = GameState.SKILLS;
        GUIManager.Instance.MaximizeGUI(GUIManager.GUISystem.SkillGUI);
    }

    public void IsGameOver() { 
        CurrentState = GameState.GAMEOVER;
        GUIManager.Instance.MaximizeGUI(GUIManager.GUISystem.GameoverGUI);
        WaveSystem.ResetWaveSystem();
        UserData.Instance.SetDefaultValues();
        Application.LoadLevel("NewMainLevel");
    }

    private void LoadCastle()
    {
        GameObject castle;

        CheckForCastle();

        switch (UserData.Instance.castleLevel)
        {
            case 1:  castle = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Castles/Faceted/Level1"));
                break;
            case 2: castle = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Castles/Faceted/Level2"));
                break;
            case 3: castle = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Castles/Faceted/Level3"));
                break;
            case 4: castle = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Castles/Faceted/Level4"));
                break;
            case 5: castle = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Castles/Faceted/Level5"));
                break;
        }
    }

    private void CheckForCastle()
    {
        if (GameObject.Find("Level1(Clone)") != null)
        {
            GameObject.Destroy(GameObject.Find("Level1(Clone)"));
        }
        if (GameObject.Find("Level2(Clone)") != null)
        {
            GameObject.Destroy(GameObject.Find("Level2(Clone)"));
        }
        if (GameObject.Find("Level3(Clone)") != null)
        {
            GameObject.Destroy(GameObject.Find("Level3(Clone)"));
        }
        if (GameObject.Find("Level4(Clone)") != null)
        {
            GameObject.Destroy(GameObject.Find("Level4(Clone)"));
        }
        if (GameObject.Find("Level5(Clone)") != null)
        {
            GameObject.Destroy(GameObject.Find("Level5(Clone)"));
        }
    }

    public IEnumerator QueueStateTransition(GameState state)
    {
        InTransition = true;
        StateAndWait sw = new StateAndWait(state, DEFAULT_LOAD_TIME);
        yield return StartCoroutine("QueueStateTransition", sw);
    }

    public IEnumerator QueueStateTransition(StateAndWait parameters)
    {
        InTransition = true;
        yield return new WaitForSeconds(parameters.Time);
        ChangeState(parameters.State);
    }

    private void ChangeState(GameState state)
    {
        switch (state)
        {
            case GameState.GAMEOVER:
                IsGameOver();
                break;
            case GameState.PLAYING:
                IsPlaying();
                break;
            case GameState.POSTWAVE:
                IsPostWave();
                break;
            case GameState.PREWAVE:
                IsPrewave();
                break;
            case GameState.SKILLS:
                IsSkills();
                break;
            case GameState.UPGRADE:
                IsUpgrading();
                break;
            case GameState.PAUSED:
                IsPaused();
                break;
            default:
                IsMainMenu();
                break;
        }
    }

    public void UpdateAndSave()
    {
        UserData.Instance.SaveData();
    }

}
