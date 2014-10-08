using UnityEngine;
using System.Collections;

public class BossSpawner : MonoBehaviour
{

    private static BossSpawner instance;
    public static BossSpawner Instance { get { return instance; } set { instance = value; } }

    internal bool spawning;
    internal int bossNumber, bossesSpawned;
    private float frequency = 15.0f;

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
            InvokeRepeating("SpawnBosses", 1, frequency);
        }

    }

    void SpawnBosses()
    {
        if (bossesSpawned < bossNumber)
        {
            int xStart = Random.Range(20, 30);
            int zStart = Random.Range(15, 30);
            GameObject newboss;

            newboss = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/Boss"));
            newboss.transform.position = new Vector3(xStart, 0, zStart);
            newboss.GetComponent<EnemyBehaviour>().enabled = true;
            bossesSpawned++;
            frequency = Random.Range(0.5f, 2.5f);
        }
        else if (bossesSpawned == bossNumber)
        {
            spawning = false;
            CancelInvoke("SpawnBosses");
        }
    }
}
