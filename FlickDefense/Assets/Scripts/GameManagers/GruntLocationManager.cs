using UnityEngine;
using System.Collections;

public class GruntLocationManager : MonoBehaviour {

    
    void OnTriggerEnter(Collider collider)
    {
        switch (collider.tag)
        {
            case "Bomber":
            case "Grunt":
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
            case "Bomber":
            case "Grunt":
                {
                    collider.GetComponent<EnemyBehaviour>().OffLocation();
                }
                break;
        }
    }
}
