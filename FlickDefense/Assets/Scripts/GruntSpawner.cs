using UnityEngine;
using System.Collections;

public class GruntSpawner : MonoBehaviour {

    private static GruntSpawner instance;
    public static GruntSpawner Instance { get { return instance; } set { instance = value; } }

    internal bool spawning;
    internal int gruntNumber;

    private int gruntsSpawned = 0;
    private float frequency = 1.0f;

	// Use this for initialization
	void Awake () {
        instance = this;
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
            InvokeRepeating("SpawnGrunts", 1, frequency);
        }

	}

    void SpawnGrunts()
    {
        if (gruntsSpawned < gruntNumber)
        {
            int xStart = Random.Range(20, 30);
            int zStart = Random.Range(15, 30);
            GameObject newGrunt;

            newGrunt = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/Grunt"));
            newGrunt.transform.position = new Vector3(xStart, 0, zStart);
            newGrunt.GetComponent<EnemyBehaviour>().enabled = true;
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
