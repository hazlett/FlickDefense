using UnityEngine;
using System.Collections;

public class DeathLimit : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        GameObject.Destroy(collider.gameObject);
    }

}
