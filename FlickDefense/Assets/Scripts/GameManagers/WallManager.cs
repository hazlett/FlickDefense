using UnityEngine;
using System.Collections;

public class WallManager : MonoBehaviour {

    private int health;
	void Start () {
        health = 5;
	}
	
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    { 
        switch (collider.tag)
        {
            case "Enemy":
                {
                    collider.GetComponent<EnemyBehaviour>().AtLocation();
                }
                break;
        }
    }
    //void OnTriggerStay(Collider collider)
    //{

    //}
    void OnTriggerExit(Collider collider)
    {
        switch (collider.tag)
        {
            case "Enemy":
                {
                    collider.GetComponent<EnemyBehaviour>().OffLocation();
                }
                break;
        }
    }

}
