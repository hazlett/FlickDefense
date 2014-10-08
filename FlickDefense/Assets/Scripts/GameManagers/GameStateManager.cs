using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateManager  {

    private int castleHealth;
    public int CastleHealth { get { return castleHealth; } }
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
}
