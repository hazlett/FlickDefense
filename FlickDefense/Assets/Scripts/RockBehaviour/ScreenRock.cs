using UnityEngine;
using System.Collections;

public class ScreenRock : MonoBehaviour {

    private RockState state;
    private Vector3 endLocation;
    private float speed;
    private float clipping;
    private float offset;
    private GameplayGUI gui;
    private enum RockState
    {
        UNTOUCHED,
        GRABBED,
        THROWN
    }
	void Start () {
        clipping = Camera.main.nearClipPlane;
        speed = 5.0f;
        state = RockState.THROWN;
        offset = 0.55f;
        gui = GameObject.Find("GUI").GetComponent<GameplayGUI>();
        endLocation = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.55f, Screen.height * 0.6f, + clipping));
        Debug.Log(endLocation);
	}
	void OnDestroy()
    {
        GameObject explosion = Instantiate(Resources.Load("Prefabs/Particles/BomberExplosion"), transform.position, Quaternion.identity) as GameObject;
    }
	void Update () {
        switch (state)
        {
            case RockState.UNTOUCHED:
                break;
            case RockState.GRABBED:
                break;
            case RockState.THROWN:
                transform.position = Vector3.MoveTowards(transform.position, endLocation, Time.deltaTime * speed);
                
                if (Camera.main.WorldToViewportPoint(gameObject.transform.position).z < clipping + offset)
                {
                    gui.CrackScreen();
                    Destroy(gameObject);
                }

                break;
        }
	}
    public void Tapped()
    {
        Destroy(gameObject);
    }
}
