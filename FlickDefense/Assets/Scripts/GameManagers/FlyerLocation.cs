using UnityEngine;
using System.Collections;

public class FlyerLocation : MonoBehaviour {
   
    private GameObject owner;
    private bool taken = false;
    public bool Taken { get { return taken; } }
    public bool Assign(GameObject owner)
    {
        if (taken)
        {
            return false;
        }
        else
        {
            taken = true;
            this.owner = owner;
            return true;
        }
    }
    public void UnAssign()
    {
        taken = false;
    }

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
