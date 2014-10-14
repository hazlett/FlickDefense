using UnityEngine;
using System.Collections;

public class DeathLimit : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("DeathLimit");
        GameObject.Destroy(collider.gameObject);
    }

}
