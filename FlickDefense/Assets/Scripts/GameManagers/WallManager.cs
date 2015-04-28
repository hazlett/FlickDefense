using UnityEngine;
using System.Collections;

public class WallManager : MonoBehaviour {

    private GameObject gruntLocationManager;
    void Awake()
    {
        gruntLocationManager = GameObject.Instantiate(Resources.Load("Prefabs/Locations/GruntLocation")) as GameObject;
        gruntLocationManager.transform.position = transform.position + new Vector3(0.5f, 0, 0);
    }
	void Start () {
	}
}
