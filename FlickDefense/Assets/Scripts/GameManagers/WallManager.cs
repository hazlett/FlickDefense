using UnityEngine;
using System.Collections;

public class WallManager : MonoBehaviour {

    private int health;
    private GameObject gruntLocationManager;
    void Awake()
    {
        gruntLocationManager = GameObject.Instantiate(Resources.Load("Prefabs/Locations/GruntLocation")) as GameObject;
        gruntLocationManager.transform.position = transform.position;
    }
	void Start () {
        health = 5;
	}
	
	void Update () {
	
	}

    

}
