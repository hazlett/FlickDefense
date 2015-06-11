using UnityEngine;
using System.Collections;

public class PauseController : MonoBehaviour {

    private Canvas pauseCanvas;
    private GameplayGUI gameplayGUI;

    void Start()
    {
        pauseCanvas = GameObject.Find("PauseCanvas").GetComponent<Canvas>();
        gameplayGUI = GameObject.Find("GUI").GetComponent<GameplayGUI>();
    }

    public void ReturnToGame()
    {
        pauseCanvas.enabled = false;
        GameStateManager.Instance.IsPaused();
        gameplayGUI.enabled = true;
    }

    public void QuitGame()
    {
        Debug.Log("Do the quitting of the game");
    }
}
