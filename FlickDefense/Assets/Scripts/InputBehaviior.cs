using UnityEngine;
using System.Collections;

public class InputBehaviior : MonoBehaviour {

    Touch userTouch;

    private Vector2 startPos, movePos, endPos, velocity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.touchCount > 0)
        {
            userTouch = Input.GetTouch(0);
            if (userTouch.phase == TouchPhase.Began)
            {
                startPos = userTouch.position;
            }
            if (userTouch.phase == TouchPhase.Moved)
            {
                movePos = userTouch.position;
            }
            if (userTouch.phase == TouchPhase.Ended)
            {
                endPos = userTouch.position;
                velocity.x = userTouch.deltaPosition.x / userTouch.deltaTime;
                velocity.y = userTouch.deltaPosition.y / userTouch.deltaTime;
            }
        }
	}

    void OnGUI()
    {
        GUILayout.Box("Start Pos: (" + startPos.x + ", " + startPos.y + ")");
        GUILayout.Box("Current Pos: (" + movePos.x + ", " + movePos.y + ")");
        GUILayout.Box("End Pos: (" + endPos.x + ", " + endPos.y + ")");
        GUILayout.Box("Velocity: (" + velocity.x + ", " + velocity.y + ")"); 
    }
}
