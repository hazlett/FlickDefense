using UnityEngine;
using System.Collections;

public class XMLTester : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyUp(KeyCode.L))
        {
            Waves.Instance.LoadWaves();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            Waves.Instance.SaveWaves();
        }
	}
}
