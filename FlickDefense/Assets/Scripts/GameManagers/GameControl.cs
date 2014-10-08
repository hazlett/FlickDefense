using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour
{
    public WaveGUI waveGUI;
    public GameplayGUI gameplayGUI;
    public PostgameGUI postgameGUI;
    public UpgradeGUI upgradeGUI;
    public SkillGUI skillGUI;
    public GameoverGUI gameoverGUI;

    // Update is called once per frame
    void Update()
    {
        switch (GameStateManager.Instance.currentState)
        {
            case GameStateManager.GameState.PREWAVE: waveGUI.enabled = true;
                break;
            case GameStateManager.GameState.PLAYING: gameplayGUI.enabled = true;
                if (GameStateManager.Instance.enemyList.Count == 0)
                {
                    gameplayGUI.enabled = false;
                    GameStateManager.Instance.IsPostWave();
                }
                break;
            case GameStateManager.GameState.POSTWAVE: postgameGUI.enabled = true;
                break;
            case GameStateManager.GameState.UPGRADE: upgradeGUI.enabled = true;
                break;
            case GameStateManager.GameState.SKILLS: skillGUI.enabled = true;
                break;
            case GameStateManager.GameState.GAMEOVER: gameoverGUI.enabled = true;
                break;
            default:
                break;
        }
    }
}
