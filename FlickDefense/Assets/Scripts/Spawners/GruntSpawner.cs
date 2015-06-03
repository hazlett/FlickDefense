using UnityEngine;
using System.Collections;

public class GruntSpawner : MonoBehaviour {

    private static GruntSpawner instance;
    public static GruntSpawner Instance { get { return instance; } set { instance = value; } }

    public GameObject spawnBox;

    internal bool spawning;
    internal int gruntNumber, gruntsSpawned;
    private float frequency = 1.0f;

	// Use this for initialization
	void Awake () {

        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	internal void StartSpawn () {

        if (spawning)
        {
            InvokeRepeating("SpawnGrunts", 0, frequency);
        }

	}

    void SpawnGrunts()
    {
        if (gruntsSpawned < gruntNumber)
        {
            float xStart = Random.Range((spawnBox.transform.position.x + spawnBox.transform.localScale.x / 2), (spawnBox.transform.position.x - spawnBox.transform.localScale.x / 2));
            float zStart = Random.Range((spawnBox.transform.position.z + spawnBox.transform.localScale.z / 2), (spawnBox.transform.position.z - spawnBox.transform.localScale.z / 2));

            GameObject newGrunt = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/Grunt"));
            newGrunt.transform.position = new Vector3(xStart, 0, zStart);
            newGrunt.GetComponent<EnemyBehaviour>().enabled = true;
            newGrunt.GetComponent<EnemyBehaviour>().SetLevel(WaveSystem.Instance.waveNumber);
            gruntsSpawned++;
            frequency = Random.Range(0.5f, 2.5f);
        }
        else if(gruntsSpawned == gruntNumber)
        {
            spawning = false;
            CancelInvoke("SpawnGrunts");
        }
    }
}
