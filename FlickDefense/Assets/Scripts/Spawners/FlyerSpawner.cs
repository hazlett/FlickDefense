using UnityEngine;
using System.Collections;

public class FlyerSpawner : MonoBehaviour
{

    private static FlyerSpawner instance;
    public static FlyerSpawner Instance { get { return instance; } set { instance = value; } }

    public GameObject spawnBox;

    internal bool spawning;
    internal int flyerNumber, flyersSpawned;
    private float frequency = 3.0f;

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
            InvokeRepeating("SpawnFlyers", 1, frequency);
        }

    }

    void SpawnFlyers()
    {
        if (flyersSpawned < flyerNumber)
        {
            float xStart = Random.Range((spawnBox.transform.position.x + spawnBox.transform.localScale.x / 2), (spawnBox.transform.position.x - spawnBox.transform.localScale.x / 2));
            float zStart = Random.Range((spawnBox.transform.position.z + spawnBox.transform.localScale.z / 2), (spawnBox.transform.position.z - spawnBox.transform.localScale.z / 2));
            GameObject newflyer;

            newflyer = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/Flyer"));
            newflyer.transform.position = new Vector3(xStart, 10, zStart);
            newflyer.GetComponent<EnemyBehaviour>().enabled = true;
            flyersSpawned++;
            frequency = Random.Range(3.5f, 5.5f);
        }
        else if (flyersSpawned == flyerNumber)
        {
            spawning = false;
            CancelInvoke("SpawnFlyers");
        }
    }
}
