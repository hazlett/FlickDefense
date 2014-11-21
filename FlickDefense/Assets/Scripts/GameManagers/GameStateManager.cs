using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateManager  {

    private static  GameStateManager instance = new GameStateManager();
    public static GameStateManager Instance { get { return instance; } set { instance = value; } }

    internal int enemyCount = 0;

    internal enum GameState
    {
        MAINMENU,
        PREWAVE,
        PLAYING,
        POSTWAVE,
        UPGRADE,
        SKILLS,
        GAMEOVER
    }

    internal GameState currentState = GameState.MAINMENU;

    public void IsMainMenu() { currentState = GameState.MAINMENU; }

    public void IsPrewave()
    {
        currentState = GameState.PREWAVE;
        LoadCastle();
        UserStatus.Instance.SetPastKilled();
        WaveSystem.Instance.currentState = WaveSystem.WaveState.PREWAVE;
    }

    public void IsPlaying()
    {
        currentState = GameState.PLAYING;
        enemyCount = WaveSystem.Instance.EnemyCount();
        WaveSystem.Instance.currentState = WaveSystem.WaveState.INWAVE;
    }

    public void IsPostWave()
    {
        currentState = GameState.POSTWAVE;
        WaveSystem.Instance.currentState = WaveSystem.WaveState.POSTWAVE;
    }

    public void IsUpgrading() { currentState = GameState.UPGRADE; }

    public void IsSkills() { currentState = GameState.SKILLS; }

    public void IsGameOver() { currentState = GameState.GAMEOVER; }

    private void LoadCastle()
    {
        GameObject castle;

        CheckForCastle();

        switch (UserStatus.Instance.CastleLevel)
        {
            case 1:  castle = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Castles/Level1"));
                break;
            case 2: castle = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Castles/Level2"));
                break;
            case 3: castle = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Castles/Level3"));
                break;
            case 4: castle = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Castles/Level4"));
                break;
            case 5: castle = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Castles/Level5"));
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

}
