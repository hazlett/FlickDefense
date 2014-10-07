using UnityEngine;
using System.Collections;

public class CatapultSpawner : MonoBehaviour
{

    private static CatapultSpawner instance;
    public static CatapultSpawner Instance { get { return instance; } set { instance = value; } }

    internal bool spawning;
    internal int catapultNumber, catapultsSpawned;
    private float frequency = 7.0f;

    // Use this for initialization
    void Awake()
    {
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
    internal void StartSpawn()
    {

        if (spawning)
        {
            InvokeRepeating("SpawnCatapults", 1, frequency);
        }

    }

    void SpawnCatapults()
    {
        if (catapultsSpawned < catapultNumber)
        {
            int xStart = Random.Range(20, 30);
            int zStart = Random.Range(15, 30);
            GameObject newcatapult;

            newcatapult = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/Catapult"));
            newcatapult.transform.position = new Vector3(xStart, 0, zStart);
            catapultsSpawned++;
            frequency = Random.Range(6.5f, 8.5f);
        }
        else if (catapultsSpawned == catapultNumber)
        {
            spawning = false;
            CancelInvoke("SpawnCatapults");
        }
    }
}
