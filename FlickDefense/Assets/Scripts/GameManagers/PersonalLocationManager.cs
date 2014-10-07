using UnityEngine;
using System.Collections;

public class PersonalLocationManager : MonoBehaviour
{
    private GameObject owner;
    public void SetObject(GameObject owner)
    {
        this.owner = owner;
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == owner)
        {
            collider.GetComponent<EnemyBehaviour>().AtLocation();
        }

    }
    //void OnTriggerStay(Collider collider)
    //{

    //}
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == owner)
        {
            collider.GetComponent<EnemyBehaviour>().OffLocation();
        }
    }
}
