using UnityEngine;
using System.Collections;

public class ArcherSpawner : MonoBehaviour
{

    private static ArcherSpawner instance;
    public static ArcherSpawner Instance { get { return instance; } set { instance = value; } }

    public GameObject spawnBox;

    internal bool spawning;
    internal int archerNumber, archersSpawned;
    private float frequency = 1.0f;

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
            InvokeRepeating("SpawnArchers", 1, frequency);
        }

    }

    void SpawnArchers()
    {
        if (archersSpawned < archerNumber)
        {
            float xStart = Random.Range((spawnBox.transform.position.x + spawnBox.transform.localScale.x / 2), (spawnBox.transform.position.x - spawnBox.transform.localScale.x / 2));
            float zStart = Random.Range((spawnBox.transform.position.z + spawnBox.transform.localScale.z / 2), (spawnBox.transform.position.z - spawnBox.transform.localScale.z / 2));
            GameObject newarcher;

            newarcher = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/Archer"));
            newarcher.transform.position = new Vector3(xStart, 0, zStart);
            newarcher.GetComponent<EnemyBehaviour>().enabled = true;
            archersSpawned++;
            frequency = Random.Range(3.5f, 4.5f);
        }
        else if (archersSpawned == archerNumber)
        {
            spawning = false;
            CancelInvoke("SpawnArchers");
        }
    }
}
