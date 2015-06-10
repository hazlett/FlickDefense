using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController Instance
    {
        get { return GameObject.FindObjectOfType<GameController>(); }
        private set { }
    }

    void Awake()
    {
        if (Instance != null && Instance != this) Debug.LogError("Cannot create more than one GameController");
        else Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.Instance.InTransition) return;
        if (UserData.Instance.castleHealth <= 0)
        {
            SoundManager.PlaySoundClip(SoundClip.GameState.LoseGame);
            StartCoroutine(GameStateManager.Instance.QueueStateTransition(
                new GameStateManager.StateAndWait(GameStateManager.GameState.GAMEOVER, 6f)));
        }
        if (GameStateManager.Instance.enemyCount == 0)
        {
            SoundManager.PlaySoundClip(SoundClip.GameState.WinGame);
            //wait 3 seconds before proceeding to next wave
            StartCoroutine(GameStateManager.Instance.QueueStateTransition(
                new GameStateManager.StateAndWait(GameStateManager.GameState.POSTWAVE, 3f)));
        }
    }
}
