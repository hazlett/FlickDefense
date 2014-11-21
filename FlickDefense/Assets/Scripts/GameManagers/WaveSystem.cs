using UnityEngine;
using System.Collections;

public class WaveSystem {

    internal int waveNumber = 0;

    private static WaveSystem instance = new WaveSystem();
    public static WaveSystem Instance { get { return instance; } set { instance = value; } }

    private int gruntsKilled, archersKilled, catapultsKilled, bombersKilled, flyersKilled, bossesKilled;
    private int gruntNumber, archerNumber, catapultNumber, bomberNumber, flyingGruntNumber, bossNumber;
    private int minGrunt, maxGrunt, minArcher, maxArcher, minBomber, maxBomber, minCatapult, maxCatapult, minFly, maxFly;
    internal bool enemyRangeSet, enemyNumberSet, enemiesSpawning;

    internal enum WaveState
    {
        PREWAVE,
        INWAVE,
        POSTWAVE
    }

    internal WaveState currentState;

    void Start ()
    {
        enemiesSpawning = enemyNumberSet = enemyRangeSet = false;
        gruntNumber = archerNumber = catapultNumber = bomberNumber = flyingGruntNumber = 0;
        minGrunt = maxGrunt = minArcher = maxArcher = minBomber = maxBomber = minCatapult = maxCatapult = minFly = maxFly = 0;
        SetEnemyRange();
    }

    internal void SetWaveEnemies()
    {
        switch (waveNumber)
        {
            case 1: SetEnemyNumber();
                break;
            case 2: SetEnemyNumber();
                break;
            case 3: SetEnemyNumber();
                break;
            case 4: SetEnemyNumber();
                break;
            case 5: SetEnemyNumber();
                break;
            default: SetEnemyNumber();
                break;
        }
    }

    void SetEnemyNumber()
    {
        if (!enemyNumberSet)
        {
            gruntNumber = Random.Range(minGrunt, maxGrunt);
            archerNumber = Random.Range(minArcher, maxArcher);
            bomberNumber = Random.Range(minBomber, maxBomber);
            catapultNumber = Random.Range(minCatapult, maxCatapult);
            flyingGruntNumber = Random.Range(minFly, maxFly);

            if (waveNumber % 7 == 0)
            {
                bossNumber = waveNumber / 7;
            }

            enemyNumberSet = true;
        }
    }

    internal void SpawnEnemies()
    {
        if (!enemiesSpawning)
        {
            Debug.Log("Spawning Enemies");
            //SpawnGrunts();
            //SpawnArchers();
            //SpawnBombers();
            //SpawnFlyers();
            //SpawnCatapults();
            SpawnBosses();

            enemiesSpawning = true;
        }
    }

    internal void SetEnemyRange()
    {
        if (!enemyRangeSet)
        {
            waveNumber++;
            switch (waveNumber)
            {
                case 1: minGrunt = 8;
                    maxGrunt = 14;
                    break;
                case 2: minGrunt += Random.Range(2, 5);
                    maxGrunt += Random.Range(3, 5);
                    minArcher = 3;
                    maxArcher = 7;
                    break;
                case 3: minGrunt += Random.Range(2, 5);
                    maxGrunt += Random.Range(3, 5);
                    minArcher += Random.Range(2, 3);
                    maxArcher += Random.Range(2, 3);
                    minBomber = maxBomber = 1;
                    break;
                case 4: minGrunt += Random.Range(2, 5);
                    maxGrunt += Random.Range(3, 5);
                    minArcher += Random.Range(2, 3);
                    maxArcher += Random.Range(2, 3);
                    minFly = maxFly = 1;
                    break;
                case 5: minGrunt += Random.Range(2, 5);
                    maxGrunt += Random.Range(3, 5);
                    minArcher += Random.Range(2, 3);
                    maxArcher += Random.Range(2, 3);
                    minBomber += Random.Range(0, 2);
                    maxBomber += Random.Range(0, 3);
                    minCatapult = maxCatapult = 1;
                    break;
                default: IncrementEnemies();
                    break;
            }
            enemyRangeSet = true;
        }
    }

    void IncrementEnemies()
    {
        minGrunt += Random.Range(2, 5);
        maxGrunt += Random.Range(3, 5);
        minArcher += Random.Range(2, 3);
        maxArcher += Random.Range(2, 3);
        minBomber += Random.Range(0, 2);
        maxBomber += Random.Range(0, 3);
        minFly += Random.Range(1, 3);
        maxFly += Random.Range(2, 4);

        if (waveNumber % 3 == 0)
        {
            minCatapult += Random.Range(1, 4);
            maxCatapult += Random.Range(2, 4);
        }
    }

    void SpawnGrunts()
    {
        Debug.Log("GruntNumber: " + gruntNumber);
        GruntSpawner.Instance.spawning = true;
        GruntSpawner.Instance.gruntNumber = gruntNumber;
        GruntSpawner.Instance.gruntsSpawned = 0;
        GruntSpawner.Instance.StartSpawn();
    }

    void SpawnArchers()
    {
        Debug.Log("ArcherNumber: " + archerNumber);
        ArcherSpawner.Instance.spawning = true;
        ArcherSpawner.Instance.archerNumber = archerNumber;
        ArcherSpawner.Instance.archersSpawned = 0;
        ArcherSpawner.Instance.StartSpawn();
    }

    void SpawnBombers()
    {
        Debug.Log("BomberNumber: " + bomberNumber);
        BomberSpawner.Instance.spawning = true;
        BomberSpawner.Instance.bomberNumber = bomberNumber;
        BomberSpawner.Instance.bombersSpawned = 0;
        BomberSpawner.Instance.StartSpawn();
    }

    void SpawnFlyers()
    {
        Debug.Log("FlyerNumber: " + flyingGruntNumber);
        FlyerSpawner.Instance.spawning = true;
        FlyerSpawner.Instance.flyerNumber = flyingGruntNumber;
        FlyerSpawner.Instance.flyersSpawned = 0;
        FlyerSpawner.Instance.StartSpawn();
    }

    void SpawnCatapults()
    {
        Debug.Log("CatapultNumber: " + catapultNumber);
        CatapultSpawner.Instance.spawning = true;
        CatapultSpawner.Instance.catapultNumber = catapultNumber;
        CatapultSpawner.Instance.catapultsSpawned = 0;
        CatapultSpawner.Instance.StartSpawn();
    }

    void SpawnBosses()
    {
        Debug.Log("BossNumber: " + bossNumber);
        BossSpawner.Instance.spawning = true;
        BossSpawner.Instance.bossNumber = bossNumber;
        BossSpawner.Instance.bossesSpawned = 0;
        BossSpawner.Instance.StartSpawn();
    }

    internal int EnemyCount()
    {
        return gruntNumber + archerNumber + bomberNumber + flyingGruntNumber + catapultNumber + bossNumber;
    }
}
