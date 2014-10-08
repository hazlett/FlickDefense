using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateManager  {

    private static  GameStateManager instance = new GameStateManager();
    public static GameStateManager Instance { get { return instance; } set { instance = value; } }

    internal List<GameObject> enemyList = new List<GameObject>();

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
        WaveSystem.Instance.currentState = WaveSystem.WaveState.PREWAVE;
    }

    public void IsPlaying()
    {
        currentState = GameState.PLAYING;
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

}
