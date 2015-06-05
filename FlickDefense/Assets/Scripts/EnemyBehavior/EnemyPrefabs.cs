using UnityEngine;
using System.Collections;

public class EnemyPrefabs : MonoBehaviour{

    public GameObject Archer;
    public GameObject Bomber;
    public GameObject Boss;
    public GameObject Catapult;
    public GameObject Dragon;
    public GameObject Grunt;

    private static EnemyPrefabs instance;
    public static EnemyPrefabs Instance { get { return instance; } private set { } }

    void Awake()
    {
        instance = this;
    }

    void Update()
    {

    }

    public static GameObject GetEnemyPrefab(Enemies type)
    {
        switch(type)
        {
            case Enemies.Archer:
                return Instance.Archer;
            case Enemies.Bomber:
                return Instance.Bomber;
            case Enemies.Boss:
                return Instance.Boss;
            case Enemies.Catapult:
                return Instance.Catapult;
            case Enemies.Dragon:
                return Instance.Dragon;
            default:
                return Instance.Grunt;
        }
    }
}
