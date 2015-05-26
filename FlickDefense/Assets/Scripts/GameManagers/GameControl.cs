﻿using UnityEngine;
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
        //Debug.Log(GameStateManager.Instance.currentState.ToString());
        switch (GameStateManager.Instance.currentState)
        {
            case GameStateManager.GameState.PREWAVE: UpdateAndSave();
                waveGUI.enabled = true;
                gameplayGUI.enabled = true;
                break;
            case GameStateManager.GameState.PLAYING:
                if (UserStatus.Instance.CastleHealth <= 0)
                {
                    GameStateManager.Instance.IsGameOver();
                    Application.LoadLevel("NewMainLevel");
                }
                if (GameStateManager.Instance.enemyCount == 0)
                {
                    gameplayGUI.enabled = false;
                    GameStateManager.Instance.IsPostWave();
                    SoundManager.PlaySoundClip(SoundClip.GameState.WinGame);
                }
                break;
            case GameStateManager.GameState.POSTWAVE: UpdateAndSave();
                postgameGUI.enabled = true;
                break;
            case GameStateManager.GameState.UPGRADE: UpdateAndSave();
                upgradeGUI.enabled = true;
                break;
            case GameStateManager.GameState.SKILLS: UpdateAndSave();
                skillGUI.enabled = true;
                break;
            case GameStateManager.GameState.GAMEOVER: gameoverGUI.enabled = true;
                break;
            default:
                break;
        }
    }

    private void UpdateAndSave()
    {
        UserStatus.Instance.UpdateUserDataValues();
       // UserStatus.Instance.currentUser.SaveData();
    }

    private void CheckEnemyList()
    {
    }
}
