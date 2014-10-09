using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot "User"]
public class UserData : MonoBehaviour {

    private bool barracks;
    private bool archeryRange;
    private bool alchemyLab;
    private int castleLevel;
    private int lightningLevel;
    private int fireLevel;
    private int iceLevel;
    private int gruntsKilled;
    private int archersKilled;
    private int bombersKilled;
    private int flyersKilled;
    private int catapultsKilled;
    private int bossesKilled;
    private int maxCastleHealth;
    private int castleHealth;
    private int gold;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
