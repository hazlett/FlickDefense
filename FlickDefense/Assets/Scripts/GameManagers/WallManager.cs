using UnityEngine;
using System.Collections;

public class WallManager : MonoBehaviour {

    private GameObject gruntLocationManager;
    void Awake()
    {
        gruntLocationManager = GameObject.Instantiate(Resources.Load("Prefabs/Locations/GruntLocation")) as GameObject;
        gruntLocationManager.transform.position = transform.position;
    }
	void Start () {
        UserStatus.Instance.SetCastleHealth(5);
	}
	void OnGUI()
    {
        GUILayout.Label("HEALTH: " + UserStatus.Instance.CastleHealth);
    }
}
