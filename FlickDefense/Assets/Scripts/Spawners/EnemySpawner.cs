using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

    [System.Serializable]
    public class SpawnParameters
    {
        public SpawnParameters(Enemies type, int num, float freq, float delay) { EnemyType = type; Number = num; Freqeuncy = freq; InitDelay = delay; }
        public Enemies EnemyType;
        public int Number = 0;
        public float Freqeuncy = 0;
        //Set to -1 to pause
        public float InitDelay = 0;
    }

    public List<SpawnParameters> EnemySpawns { get; private set; }
    private static EnemySpawner instance;
    public static EnemySpawner Instance { get { return instance; } private set { } }

    void Awake()
    {
        instance = this;
        if (!AttachedToZone()) Debug.LogError("EnemySpawner must be attached to gameobject with a renderer");
        NewWave();
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void NewWave()
    {
        Instance.EnemySpawns = new List<SpawnParameters>();
    }

    public void InitSpawns()
    {
        foreach (SpawnParameters enemy in EnemySpawns)
        {
            StartCoroutine("InitSpawn", enemy);
        }
    }

    public void SetSpawnParams(SpawnParameters parameters)
    {
        EnemySpawns.Add(parameters);
    }

    private IEnumerator InitSpawn(SpawnParameters parameters)
    {
        float delay = parameters.InitDelay;
        if (delay > 0) yield return new WaitForSeconds(delay);

        while (parameters.Number > 0)
        {
            //Wait for unpause
            if (parameters.InitDelay >= 0)
            {
                SpawnEnemy(parameters.EnemyType);
                parameters.Number--;
                yield return new WaitForSeconds(parameters.Freqeuncy);
            }
            else yield return new WaitForSeconds(.05f);
        }
    }

    private void SpawnEnemy(Enemies enemyType)
    {
        GameObject enemy = Instantiate<GameObject>(EnemyPrefabs.GetEnemyPrefab(enemyType));
        enemy.transform.position = RandomLocInSpawnZone(enemy);
        if (enemyType == Enemies.Dragon) enemy.transform.position += new Vector3(0,10,0);
        enemy.GetComponent<EnemyBehaviour>().enabled = true;
        enemy.GetComponent<EnemyBehaviour>().SetLevel(WaveSystem.Instance.WaveNumber);
        WaveSystem.AddEnemyToSystem(enemy, enemyType);
    }

    public void TogglePauseSpawn(Enemies enemyType)
    {
        foreach (SpawnParameters parameters in GetSpawnParams(enemyType))
        {
            parameters.InitDelay = parameters.InitDelay == -1 ? 0 : -1f;
        }
    }

    public List<SpawnParameters> GetSpawnParams(Enemies enemyType)
    {
        List<SpawnParameters> parameters = new List<SpawnParameters>();
        for(int i = 0; i < EnemySpawns.Count; i++)
        {
            if (EnemySpawns[i].EnemyType == enemyType) parameters.Add(EnemySpawns[i]);
        }
        return parameters;
    }

    private Vector3 RandomLocInSpawnZone(GameObject enemy)
    {
        Vector2 rand = UnityEngine.Random.insideUnitCircle;
        return this.transform.position + new Vector3(rand.x * GetComponent<Renderer>().bounds.extents.x, 
            OnGround(enemy.GetComponent<CapsuleCollider>()), 
            rand.y * GetComponent<Renderer>().bounds.extents.z);
    }

    private bool AttachedToZone()
    {
        return this.GetComponent<Renderer>() != null;
    }

    private float OnGround(CapsuleCollider enemy)
    {
        return GetComponent<Renderer>().bounds.center.y - GetComponent<Renderer>().bounds.extents.y + enemy.GetComponent<Collider>().bounds.extents.y;
    }
}
