using UnityEngine;
using System.Collections;

public class BomberSpawner : MonoBehaviour
{

    private static BomberSpawner instance;
    public static BomberSpawner Instance { get { return instance; } set { instance = value; } }

    internal bool spawning;
    internal int bomberNumber, bombersSpawned;
    private float frequency = 4.0f;

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
            InvokeRepeating("SpawnBombers", 1, frequency);
        }

    }

    void SpawnBombers()
    {
        if (bombersSpawned < bomberNumber)
        {
            int xStart = Random.Range(20, 30);
            int zStart = Random.Range(15, 30);
            GameObject newbomber;

            newbomber = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/Bomber"));
            newbomber.transform.position = new Vector3(xStart, 0, zStart);
            newbomber.GetComponent<EnemyBehaviour>().enabled = true;
            bombersSpawned++;
            frequency = Random.Range(8.5f, 12.5f);
        }
        else if (bombersSpawned == bomberNumber)
        {
            spawning = false;
            CancelInvoke("SpawnBombers");
        }
    }
}
