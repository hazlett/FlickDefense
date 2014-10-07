using UnityEngine;
using System.Collections;

public class GameStateManager  {

    private int castleHealth;
    public int CastleHealth { get { return castleHealth; } }
    private static  GameStateManager instance = new GameStateManager();
    public static GameStateManager Instance { get { return instance; } set { instance = value; } }

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

    internal GameState currentState;

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
