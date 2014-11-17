using UnityEngine;
using System.Collections;
using System;

public class InputBehaviour : MonoBehaviour
{

    Touch userTouch;

    private Vector2 startPos, movePos, endPos, velocity;
    private bool hit;
    private float deltaTime;
    private Ray ray;
    private RaycastHit raycastHit;

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

        if(SkillHandler.Instance.currentSkill == SkillHandler.Skills.NONE){
            if (Input.touchCount > 0)
            {
                userTouch = Input.GetTouch(0);

                switch (userTouch.phase)
                {
                    case TouchPhase.Began:
                        {
                            startPos = userTouch.position;
                            ray = Camera.main.ScreenPointToRay(startPos);
                            hit = Physics.SphereCast(ray, 1.0f, out raycastHit);
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
                                    case "Flyer":
                                        {
                                            raycastHit.collider.gameObject.GetComponent<EnemyBehaviour>().Damage();
                                            hit = false;
                                        }
                                        break;
                                    case "Ground":
                                    case "Catapult":
                                    case "Boss":
                                    default:
                                        {
                                            hit = false;
                                        }
                                        break;
                                }
                            }
                        }
                        break;
                    default:
                    case TouchPhase.Moved:
                        {
                            if (hit)
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
                        break;
                    case TouchPhase.Ended:
                        {
                            if (hit)
                            {
                                endPos = userTouch.position;
                                velocity.x = userTouch.deltaPosition.x / deltaTime;
                                velocity.y = userTouch.deltaPosition.y / deltaTime;
                                raycastHit.collider.GetComponent<Flick>().SetVelocity(velocity);
                            }
                            deltaTime = 0.0000001f;
                            hit = false;
                        }
                        break;
                }
            }


            //if (userTouch.phase == TouchPhase.Began)
            //{
            //    Debug.Log("BEGAN");
            //    startPos = userTouch.position;
            //    Ray ray = Camera.main.ScreenPointToRay(startPos);
            //    RaycastHit raycastHit = new RaycastHit();
            //    hit = Physics.Raycast(ray, out raycastHit, 100.0f);
            //    if (hit)
            //    {
            //        Debug.Log("RAYCAST HIT: " + raycastHit.collider.name);
            //    }
            //}
            //if (userTouch.phase == TouchPhase.Moved)
            //{
            //    movePos = userTouch.position;
            //}
            //if (userTouch.phase == TouchPhase.Ended)
            //{
            //    endPos = userTouch.position;
            //    velocity.x = userTouch.deltaPosition.x / userTouch.deltaTime;
            //    velocity.y = userTouch.deltaPosition.y / userTouch.deltaTime;
            //}

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
