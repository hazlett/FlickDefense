using UnityEngine;
using System.Collections;
using System;

public class ExplosionBehaviour : MonoBehaviour {

    private float timer = 0;
    void Update()
    {
        timer += Time.deltaTime;
        if (!particleSystem.IsAlive())
        {
            Destroy(gameObject);
        }
        if (timer > particleSystem.duration * 0.25f)
        {
            collider.enabled = false;
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        try
        {
            collider.GetComponent<EnemyBehaviour>().Damage();
        }
        catch (Exception) { }
    }

}
