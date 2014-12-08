using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundIceBehavior : MonoBehaviour
{
    public int iceDuration;
    public float slowAmount;
    public bool iceWall;
    public bool blizzard;

    private float timer;
    private List<Collider> enemies = new List<Collider>();

    void Start()
    {
        timer = 0.0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > iceDuration)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        while (enemies.Count > 0)
        {
            enemies[0].GetComponent<EnemyBehaviour>().UnFreeze(slowAmount);
            enemies.RemoveAt(0);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Grunt" || collider.tag == "Archer" || collider.tag == "Bomber" ||
            collider.tag == "Flyer" || collider.tag == "Catapult" || collider.tag == "Boss")
        {
            enemies.Add(collider);
            collider.GetComponent<EnemyBehaviour>().Freeze(slowAmount);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Grunt" || collider.tag == "Archer" || collider.tag == "Bomber" ||
            collider.tag == "Flyer" || collider.tag == "Catapult" || collider.tag == "Boss")
        {
            enemies.Remove(collider);
            collider.GetComponent<EnemyBehaviour>().UnFreeze(slowAmount);
        }
    }
}
