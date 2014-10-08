using UnityEngine;
using System.Collections;

public class DeathLimit : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("DeathLimit....muwahahahaha");
        GameObject.Destroy(collider.gameObject);
    }

}
