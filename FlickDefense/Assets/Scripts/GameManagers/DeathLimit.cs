using UnityEngine;
using System.Collections;

public class DeathLimit : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        collider.GetComponent<EnemyBehaviour>().DestroyEnemy();
    }

}
