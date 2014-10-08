using UnityEngine;
using System.Collections;

public class FlyerSpawner : MonoBehaviour
{

    private static FlyerSpawner instance;
    public static FlyerSpawner Instance { get { return instance; } set { instance = value; } }

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
            int xStart = Random.Range(20, 30);
            int zStart = Random.Range(15, 30);
            GameObject newFlyer;

            newFlyer = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/Flyer"));
            newFlyer.transform.position = new Vector3(xStart, 10, zStart);
            flyersSpawned++;

            GameStateManager.Instance.enemyList.Add(newFlyer);
            frequency = Random.Range(2.5f, 5.5f);
        }
        else if (flyersSpawned == flyerNumber)
        {
            spawning = false;
            CancelInvoke("SpawnFlyers");
        }
    }
}
