using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateManager  {

    private int castleHealth;
    public int CastleHealth { get { return castleHealth; } }
    private int gruntsKilled;
    public int GruntsKilled { get { return gruntsKilled; } }
    private int archersKilled;
    public int ArchersKilled { get { return archersKilled; } }
    private int bombersKilled;
    public int BombersKilled { get { return bombersKilled; } }
    private int flyersKilled;
    public int FlyersKilled { get { return flyersKilled; } }
    private int catapultsKilled;
    public int CatapultsKilled { get { return catapultsKilled; } }
    private int bossesKilled;
    public int BossesKilled { get { return bossesKilled; } }

    private static  GameStateManager instance = new GameStateManager();
    public static GameStateManager Instance { get { return instance; } set { instance = value; } }

    internal List<GameObject> enemyList = new List<GameObject>();
    
    private GameStateManager()
    {
        castleHealth = 5;
    }

    internal enum GameState
    {
        MAINMENU,
        PREWAVE,
        PLAYING,
        POSTWAVE,
        UPGRADE,
        GAMEOVER
    }

    internal GameState currentState = GameState.MAINMENU;

    public void Initialize(int castleHealth)
    {
        this.castleHealth = castleHealth;
    }
    public void DamageCastle()
    {
        castleHealth--;
    }
    public void DamageCastle(int damage)
    {
        castleHealth -= damage;
    }

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

    public void IsUpgrading()
    {
        currentState = GameState.UPGRADE;
    }

    public void IsGameOver() { currentState = GameState.GAMEOVER; }

    public void GruntKilled() { gruntsKilled++; }

    public void ArcherKilled() { archersKilled++; }

    public void BomberKilled() { bombersKilled++; }

    public void FlyerKilled() { flyersKilled++; }

    public void CatapultKilled() { catapultsKilled++; }

    public void BossKilled() { bossesKilled++; }
}
