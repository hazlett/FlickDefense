﻿using UnityEngine;
using System.Collections;

public class SnowCloudSpawner : MonoBehaviour {

    private static SnowCloudSpawner instance;
    public static SnowCloudSpawner Instance { get { return instance; } set { instance = value; } }

    public float cooldownPeriod;

    private Ray ray;
    private RaycastHit hit;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    internal bool SpawnCloud(Vector2 touchPosition, bool level1)
    {
        ray = Camera.main.ScreenPointToRay(touchPosition);
        Physics.Raycast(ray, out hit, Mathf.Infinity);

        if (hit.collider.tag == "Grunt" || hit.collider.tag == "Archer" || hit.collider.tag == "Bomber" ||
            hit.collider.tag == "Flyer" || hit.collider.tag == "Catapult" || hit.collider.tag == "Boss")
        {
            GameObject cloud = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skills/Ice/SnowCloud"));
            cloud.transform.position = hit.collider.transform.position + new Vector3(0.0f, 3.0f, 0.0f);
            cloud.transform.parent = hit.collider.transform;
            cloud.GetComponent<SnowCloudBehavior>().level1 = level1;

            SkillHandler.Instance.cooldownPeriod = cooldownPeriod;

            if (!level1)
            {
                SkillHandler.Instance.cooldownPeriod = cooldownPeriod * 2.0f;
            }

            return true;
        }
        else
        {
            return false;
        }
    }
}
