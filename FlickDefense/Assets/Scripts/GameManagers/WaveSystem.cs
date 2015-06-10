using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSystem : MonoBehaviour {

    public int EnemyCount { 
        get {
            int num1 = ListOfEnemies.Count;
            foreach(EnemySpawner.SpawnParameters enemy in EnemySpawner.Instance.EnemySpawns)
            {
                num1 += enemy.Number;
            }
            return num1;
        } 
        private set { } 
    }
    public int WaveNumber { get; private set; }
    private List<GameObject> ListOfEnemies;
    private static WaveSystem instance;
    public static WaveSystem Instance { get { return instance; } private set { } }
    public bool ArchersTogglePause, BombersTogglePause, BossesTogglePause, CatapultsTogglePause, DragonsTogglePause, GruntsTogglePause;
    public List<EnemySpawner.SpawnParameters> EnemySpawns;
    public int num;

    void Awake()
    {
        instance = this;
        ListOfEnemies = new List<GameObject>();
        Instance.ListOfEnemies = ListOfEnemies;
        if(Instance.EnemySpawns == null) Instance.EnemySpawns = new List<EnemySpawner.SpawnParameters>();
        Instance.WaveNumber = UserData.Instance.waveLevel;
    }

    void Start ()
    {
        
    }

    void Update()
    {
        CheckPausedSpawns();
        num = Instance.ListOfEnemies.Count;
        //Clear out list of null enemies
        if (Instance.EnemyCount == 1) KillEnemy(null);
    }

    private void CheckPausedSpawns()
    {
        if (ArchersTogglePause) EnemySpawner.Instance.TogglePauseSpawn(Enemies.Archer);
        if (BombersTogglePause) EnemySpawner.Instance.TogglePauseSpawn(Enemies.Bomber);
        if (BossesTogglePause) EnemySpawner.Instance.TogglePauseSpawn(Enemies.Boss);
        if (CatapultsTogglePause) EnemySpawner.Instance.TogglePauseSpawn(Enemies.Catapult);
        if (DragonsTogglePause) EnemySpawner.Instance.TogglePauseSpawn(Enemies.Dragon);
        if (GruntsTogglePause) EnemySpawner.Instance.TogglePauseSpawn(Enemies.Grunt);
        ArchersTogglePause = BombersTogglePause = BossesTogglePause = CatapultsTogglePause = DragonsTogglePause = GruntsTogglePause = false;
    }

    internal static void SpawnEnemies()
    {
        foreach (EnemySpawner.SpawnParameters enemy in Instance.EnemySpawns)
        {
            EnemySpawner.Instance.SetSpawnParams(enemy);
        }
        EnemySpawner.Instance.InitSpawns();
    }

    public static void ResetWaveSystem()
    {
        Instance.WaveNumber = 1;
        UserData.Instance.waveLevel = Instance.WaveNumber;
        NewSession();
    }

    public static void NewSession()
    {
        EnemySpawner.NewWave();
        Instance.ListOfEnemies = new List<GameObject>();
        Instance.EnemySpawns = new List<EnemySpawner.SpawnParameters>();
    }

    public static void NextWave()
    {
        Instance.WaveNumber++;
    }

    public static void KillEnemy(GameObject enemy)
    {
        enemy.transform.SetParent(null);
        Instance.ListOfEnemies.Remove(enemy);
        for (int i = 0; i < Instance.ListOfEnemies.Count; i++)
        {
            if (Instance.ListOfEnemies[i] == null) Instance.ListOfEnemies.RemoveAt(i);
        }
    }

    public static void AddEnemyToSystem(GameObject enemy, Enemies type)
    {
        Instance.ListOfEnemies.Add(enemy);
        enemy.transform.SetParent(Instance.gameObject.transform);
    }

    public static void LoadWaveData()
    {
        Instance.EnemySpawns = Waves.Instance.GetWaveData(Instance.WaveNumber);
    }
}
