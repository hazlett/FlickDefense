using UnityEngine;
using System.Collections;

public class WaveControl : MonoBehaviour {

    void Start()
    {
        WaveSystem.Instance.SetEnemyRange();
    }
	
	// Update is called once per frame
	void Update () {

        switch (WaveSystem.Instance.currentState)
        {
            case WaveSystem.WaveState.PREWAVE: WaveSystem.Instance.SetWaveEnemies();
                break;
            case WaveSystem.WaveState.INWAVE: WaveSystem.Instance.enemyNumberSet = WaveSystem.Instance.enemyRangeSet = false;
                WaveSystem.Instance.SpawnEnemies();
                break;
            case WaveSystem.WaveState.POSTWAVE: WaveSystem.Instance.enemiesSpawning = false;
                WaveSystem.Instance.SetEnemyRange();
                break;
            default: break;
        }

	}
}
