using UnityEngine;
using System.Collections;
using System;

public class InputBehaviour : MonoBehaviour
{

    Touch userTouch;

    private Vector2 startPos, movePos, endPos, velocity;
    private bool hit;
    private float deltaTime, startTime, endTime, elapsedTime;
    private Ray ray;
    private RaycastHit raycastHit;

    private int enemyLayer = (1 << 10)|(1 << 9);

    void Start()
    {
        hit = false;
        deltaTime = 0.0000001f;
        raycastHit = new RaycastHit();
    }

    void Update()
    {
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
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;

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
        if (Input.GetMouseButton(0))
        {
            movePos = Input.mousePosition;

            ray = Camera.main.ScreenPointToRay(movePos);

            hit = Physics.SphereCast(ray, 2.0f, out raycastHit, Mathf.Infinity, enemyLayer);
            if (hit)
            {
                if (raycastHit.collider.tag == "Grunt" || raycastHit.collider.tag == "Archer" || raycastHit.collider.tag == "Bomber")
                {
                    try
                    {
                        raycastHit.collider.GetComponent<Flick>().SetPosition(Input.mousePosition);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("ERROR in mouse held: " + e.Message);
                    }
                }
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;

            endTime = Time.time;

            elapsedTime = endTime - startTime;
            elapsedTime *= 75;

            velocity.x = (endPos.x - startPos.x) / elapsedTime;
            velocity.y = (endPos.y - startPos.y) / elapsedTime;

            ray = Camera.main.ScreenPointToRay(endPos);

            hit = Physics.SphereCast(ray, 2.0f, out raycastHit, Mathf.Infinity, enemyLayer);
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
        if (Input.touchCount > 0)
        {
            userTouch = Input.GetTouch(0);

            if(userTouch.phase == TouchPhase.Began)
                    {

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
           
            if(userTouch.phase == TouchPhase.Moved)
                    {
                        movePos = userTouch.position;

                        ray = Camera.main.ScreenPointToRay(movePos);

                        hit = Physics.SphereCast(ray, 2.0f, out raycastHit, Mathf.Infinity, enemyLayer);
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
                                    Debug.LogError("ERROR in mouse held: " + e.Message);
                                }
                            }
                        }
                    }
            
            if(userTouch.phase == TouchPhase.Ended)
                    {
                        endPos = userTouch.position;

                        endTime = Time.time;

                        elapsedTime = endTime - startTime;
                        elapsedTime *= 75;

                        velocity.x = (endPos.x - startPos.x) / elapsedTime;
                        velocity.y = (endPos.y - startPos.y) / elapsedTime;

                        ray = Camera.main.ScreenPointToRay(endPos);

                        hit = Physics.SphereCast(ray, 2.0f, out raycastHit, Mathf.Infinity, enemyLayer);
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

    void OnGUI()
    {
        //GUILayout.Box("Start Pos: (" + startPos.x + ", " + startPos.y + ")");
        //GUILayout.Box("Current Pos: (" + movePos.x + ", " + movePos.y + ")");
        //GUILayout.Box("End Pos: (" + endPos.x + ", " + endPos.y + ")");
        //GUILayout.Box("Velocity: (" + velocity.x + ", " + velocity.y + ")");
    }
}
