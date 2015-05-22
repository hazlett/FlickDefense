﻿using UnityEngine;
using System.Collections;
using System;

public class InputBehavioiur2 : MonoBehaviour {
    
    Touch userTouch;

    private Vector2 startPos, movePos, endPos, velocity;
    private bool hit;
    private float deltaTime, startTime, endTime, elapsedTime, stationaryTimer, stationaryThreshold;
    private Ray ray;
    private RaycastHit raycastHit;

    private int enemyLayer = (1 << 10) | (1 << 9);

    void Start()
    {
        hit = false;
        deltaTime = 0.0000001f;
        raycastHit = new RaycastHit();
        stationaryThreshold = 0.1f;
    }
	
	void Update () {
        if (hit)
        {
            deltaTime += Time.deltaTime;
        }

        if (SkillHandler.Instance.currentSkill == SkillHandler.Skills.NONE)
        {
            if (GameStateManager.Instance.flicking)
            {
                GetCoordinates();
            }
        }
	}


    private void GetCoordinates()
    {

        if (Input.touchCount > 0)
        {
            userTouch = Input.GetTouch(0);
            TouchPhase phase = userTouch.phase;

            if (userTouch.phase == TouchPhase.Began)
            {
                Debug.Log("TOUCH PHASE: BEGAN");
                startPos = userTouch.position;

                startTime = Time.time;

                ray = Camera.main.ScreenPointToRay(startPos);

                hit = Physics.SphereCast(ray, 2.0f, out raycastHit, Mathf.Infinity, enemyLayer);
                if (hit)
                {
                    switch (raycastHit.collider.tag)
                    {
                        case "Archer":
                        case "Bomber":
                        case "Grunt":
                            {
                            }
                            break;
                        case "RockCast":
                            {
                                hit = false;
                                raycastHit.collider.gameObject.GetComponentInParent<RockBehaviour>().Tap();
                            }
                            break;
                        case "ScreenRockCast":
                            {
                                raycastHit.collider.gameObject.GetComponentInParent<ScreenRock>().Tapped();
                                hit = false;
                            }
                            break;
                        case "Flyer":
                            {
                                raycastHit.collider.gameObject.GetComponent<EnemyBehaviour>().Damage();
                                hit = false;
                            }
                            break;
                        case "Boss":
                            {
                                raycastHit.collider.gameObject.GetComponent<BossBehaviour>().Tap();
                                hit = false;
                            }
                            break;
                        case "Ground":
                        case "Catapult":
                        default:
                            {
                                hit = false;
                            }
                            break;
                    }
                }

            }

            if (userTouch.phase == TouchPhase.Moved)
            {
                Debug.Log("TOUCH PHASE: MOVED");
                movePos = userTouch.position;

                ray = Camera.main.ScreenPointToRay(movePos);

                if (hit)
                {
                    if (raycastHit.collider.tag == "Grunt" || raycastHit.collider.tag == "Archer" || raycastHit.collider.tag == "Bomber")
                    {
                        try
                        {
                            raycastHit.collider.GetComponent<Flick>().SetPosition(userTouch.position);
                        }
                        catch (Exception e)
                        {
                            Debug.LogError("ERROR in TouchPhase.Moved: " + e.Message);
                        }
                    }
                }
                else
                {
                    hit = Physics.SphereCast(ray, 2.0f, out raycastHit, Mathf.Infinity, enemyLayer);
                }
            }
            if (userTouch.phase == TouchPhase.Stationary)
            {
                Debug.Log("TOUCH PHASE: STATIONARY");
                stationaryTimer += Time.deltaTime;
                if (stationaryTimer > stationaryThreshold)
                {
                    startPos = userTouch.position;
                    startTime = Time.time;
                }
            }
            else
            {
                stationaryTimer = 0;
            }
            if (userTouch.phase == TouchPhase.Ended)
            {
                Debug.Log("TOUCH PHASE: ENDED");
                endPos = userTouch.position;

                endTime = Time.time;

                elapsedTime = endTime - startTime;
                elapsedTime *= 75;

                velocity.x = (endPos.x - startPos.x) / elapsedTime;
                velocity.y = (endPos.y - startPos.y) / elapsedTime;

                ray = Camera.main.ScreenPointToRay(endPos);

                if (hit)
                {

                    if (raycastHit.collider.tag == "Grunt" || raycastHit.collider.tag == "Archer" || raycastHit.collider.tag == "Bomber")
                    {
                        try
                        {
                            raycastHit.collider.GetComponent<Flick>().SetVelocity(velocity);
                        }
                        catch (Exception e)
                        {
                            Debug.LogError("ERROR in mouse held: " + e.Message);
                        }
                    }
                }
            }

        }
    }

}
