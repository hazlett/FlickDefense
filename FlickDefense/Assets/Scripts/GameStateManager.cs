using UnityEngine;
using System.Collections;

public class GameStateManager  {

    private GameStateManager instance;
    public GameStateManager Instance { get { return instance; } set { instance = value; } }

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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
