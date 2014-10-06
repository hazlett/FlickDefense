using UnityEngine;
using System.Collections;

public class BillboardLaub: MonoBehaviour {

	private Transform mainCamTransform;
	private Transform cachedTransform;
	
	// Use this for initialization
	void Start () {
		mainCamTransform = GameObject.Find("Main Camera").transform;
		cachedTransform = transform;
	}
	
	void Update(){
	
		if (mainCamTransform.InverseTransformPoint( cachedTransform.position).z>=0){
			Vector3 v = mainCamTransform.position - cachedTransform.position;
			cachedTransform.LookAt( mainCamTransform.position);
			renderer.enabled = true;
		}
		else{
			renderer.enabled = false;	
		}

	}

}
