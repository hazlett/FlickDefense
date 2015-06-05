using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class InputBehaviour : MonoBehaviour
{
    private struct Priority
    {
        private int priority;
        private bool canBlock;
        internal int PriorityValue { get { return priority; } }
        internal bool CanBlock { get { return canBlock; } }
        internal Priority(int priority, bool canBlock = false)
        {
            this.priority = priority;
            this.canBlock = canBlock;
        }
    }
    private Dictionary<string, Priority> priorities = new Dictionary<string, Priority>() { 
        {"ScreenRockCast", new Priority(0)},
        {"RockCast", new Priority(1)},
        {"Bomber", new Priority(2)},
        {"Flyer", new Priority(3)},
        {"Archer", new Priority(4)},
        {"Grunt", new Priority(5)},
        {"Catapult", new Priority(6, true)},
        {"Boss", new Priority(7, true)},
        {"Ground", new Priority(8, true)}
    };
    Touch userTouch;

    private Vector2 startPos, velocity;
    private bool hit;
    private float deltaTime, startTime, endTime, elapsedTime;
    private Ray ray;
    private RaycastHit raycastHit;
    private bool paused;

    private int enemyLayer = (1 << 10) | (1 << 9);

    void Start()
    {
        hit = false;
        deltaTime = 0.0000001f;
        raycastHit = new RaycastHit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameStateManager.Instance.IsPaused();
        }
        if (GameStateManager.Instance.CurrentState == GameStateManager.GameState.PAUSED)
            return;
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
            Began(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            Moved(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Ended(Input.mousePosition);
        }
        if (Input.touchCount > 0)
        {
            Debug.Log("Touching");
            userTouch = Input.GetTouch(0);

            if (userTouch.phase == TouchPhase.Began)
            {
                Began(userTouch.position);
            }
            if (userTouch.phase == TouchPhase.Moved)
            {
                Moved(userTouch.position);
            }
            if (userTouch.phase == TouchPhase.Ended)
            {
                Ended(userTouch.position);
            }
        }
    }

    private void Began(Vector2 screenPoint)
    {
        startPos = screenPoint;
        startTime = Time.time;
        CastToHit(screenPoint);
    }
    private void Moved(Vector2 screenPoint)
    {
        if (hit)
        {
            if (raycastHit.collider.tag == "Grunt" || raycastHit.collider.tag == "Archer" || raycastHit.collider.tag == "Bomber")
            {
                try
                {
                    raycastHit.collider.GetComponent<Flick>().SetPosition(screenPoint);
                }
                catch (Exception e)
                {
                    Debug.LogError("ERROR in mouse held: " + e.Message);
                }
            }
        }
        else
        {
            CastToHit(screenPoint);
        }

    }
    private void Ended(Vector2 endPos)
    {
        endTime = Time.time;

        elapsedTime = endTime - startTime;
        elapsedTime *= 75;

        velocity.x = (endPos.x - startPos.x) / elapsedTime;
        velocity.y = (endPos.y - startPos.y) / elapsedTime;

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
        raycastHit = new RaycastHit();
        hit = false;
    }

    private void SortHit(RaycastHit[] hits)
    {
        RaycastHit block = new RaycastHit();
        bool blocking = false;
        foreach (RaycastHit rayHit in hits)
        {
            if (priorities[rayHit.collider.tag].CanBlock)
            {
                if (!blocking)
                {
                    block = rayHit;
                    blocking = true;
                }
                else if (block.transform.position.z < rayHit.transform.position.z)
                {
                    block = rayHit;
                }
            }
        }

        bool initialized = false;
        raycastHit = hits[0];
        foreach (RaycastHit rayHit in hits)
        {
            if (!initialized)
            {
                raycastHit = rayHit;
                initialized = true;
            }
            else
            {
                if (priorities[rayHit.collider.tag].PriorityValue < priorities[raycastHit.collider.tag].PriorityValue)
                {
                    if ((blocking) && (!rayHit.Equals(block)))
                    {
                        float blockDistance = Vector3.Distance(Camera.main.transform.position, block.transform.position);
                        float compareDistance = Vector3.Distance(Camera.main.transform.position, raycastHit.transform.position);
                        if (blockDistance > compareDistance)
                        {
                            raycastHit = rayHit;
                        }
                    }
                    else
                    {
                        raycastHit = rayHit;
                    }
                }
                else if (priorities[rayHit.collider.tag].PriorityValue == priorities[raycastHit.collider.tag].PriorityValue)
                {
                    if (blocking)
                    {
                        if (Vector3.Distance(Camera.main.transform.position, block.transform.position) > Vector3.Distance(Camera.main.transform.position, raycastHit.transform.position))
                        {
                            if (Vector3.Distance(Camera.main.transform.position, rayHit.transform.position) < Vector3.Distance(Camera.main.transform.position, raycastHit.transform.position))
                            {
                                raycastHit = rayHit;
                            }
                        }
                    }
                    else
                    {
                        if (Vector3.Distance(Camera.main.transform.position, rayHit.transform.position) < Vector3.Distance(Camera.main.transform.position, raycastHit.transform.position))
                        {
                            raycastHit = rayHit;
                        }
                    }
                }
            }
        }
    }

    private void CastToHit(Vector2 screenPoint)
    {
        ray = Camera.main.ScreenPointToRay(screenPoint);

        RaycastHit[] hits = Physics.SphereCastAll(ray, 2.0f, Mathf.Infinity, enemyLayer);
        if (hits.Length > 0)
        {
            hit = true;
            SortHit(hits);
        }

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
    //void OnGUI()
    //{
    //    //GUILayout.Box("Start Pos: (" + startPos.x + ", " + startPos.y + ")");
    //    //GUILayout.Box("Current Pos: (" + movePos.x + ", " + movePos.y + ")");
    //    //GUILayout.Box("Current: " + Input.mousePosition.x + ", " + Input.mousePosition.y + ", " + Input.mousePosition.z);
    //    //GUILayout.Box("End Pos: (" + endPos.x + ", " + endPos.y + ")");
    //    //GUILayout.Box("Velocity: (" + velocity.x + ", " + velocity.y + ")");
    //}
}
