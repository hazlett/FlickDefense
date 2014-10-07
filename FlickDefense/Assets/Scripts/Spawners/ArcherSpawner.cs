using UnityEngine;
using System.Collections;

public class ArcherSpawner : MonoBehaviour
{

    private static ArcherSpawner instance;
    public static ArcherSpawner Instance { get { return instance; } set { instance = value; } }

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
            int xStart = Random.Range(20, 30);
            int zStart = Random.Range(15, 30);
            GameObject newarcher;

            newarcher = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/Archer"));
            newarcher.transform.position = new Vector3(xStart, 0, zStart);
            archersSpawned++;
            frequency = Random.Range(0.5f, 2.5f);
        }
        else if (archersSpawned == archerNumber)
        {
            spawning = false;
            CancelInvoke("SpawnArchers");
        }
    }
}
