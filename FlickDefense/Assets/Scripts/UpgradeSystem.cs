using UnityEngine;
using System.Collections;

public class UpgradeSystem : MonoBehaviour {

    private UpgradeSystem instance;
    public UpgradeSystem Instance { get { return instance; } set { instance = value; } }

    internal enum CastleLevel
    {
        LEVEL1,
        LEVEL2,
        LEVEL3,
        LEVEL4,
        LEVEL5
    }

    internal CastleLevel currentCastleLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
