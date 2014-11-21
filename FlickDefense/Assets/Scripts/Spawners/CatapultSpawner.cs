using UnityEngine;
using System.Collections;

public class CatapultSpawner : MonoBehaviour
{

    private static CatapultSpawner instance;
    public static CatapultSpawner Instance { get { return instance; } set { instance = value; } }

    public GameObject spawnBox;

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
            float xStart = Random.Range((spawnBox.transform.position.x + spawnBox.transform.localScale.x / 2), (spawnBox.transform.position.x - spawnBox.transform.localScale.x / 2));
            float zStart = Random.Range((spawnBox.transform.position.z + spawnBox.transform.localScale.z / 2), (spawnBox.transform.position.z - spawnBox.transform.localScale.z / 2));
            GameObject newcatapult;

            newcatapult = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/Catapult"));
            newcatapult.transform.position = new Vector3(xStart, 0, zStart);
            newcatapult.GetComponent<EnemyBehaviour>().enabled = true;
            catapultsSpawned++;
        }
        else if (catapultsSpawned == catapultNumber)
        {
            spawning = false;
            CancelInvoke("SpawnCatapults");
        }
    }
}
