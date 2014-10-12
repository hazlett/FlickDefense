using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class UserData : MonoBehaviour
{

    [XmlAttribute("UserName")]
    internal string userName;

    [XmlAttribute("BarracksUpgrade")]
    private bool barracks;

    [XmlAttribute("ArcheryRangeUpgrade")]
    private bool archeryRange;

    [XmlAttribute("AlchemyLabUpgrade")]
    private bool alchemyLab;

    [XmlAttribute("CastleLevel")]
    private int castleLevel;

    [XmlAttribute("LightningSkillLevel")]
    private int lightningLevel;

    [XmlAttribute("FireSkillLevel")]
    private int fireLevel;

    [XmlAttribute("IceSkillLevel")]
    private int iceLevel;

    [XmlAttribute("GruntsKilled")]
    private int gruntsKilled;

    [XmlAttribute("ArchersKilled")]
    private int archersKilled;

    [XmlAttribute("BombersKilled")]
    private int bombersKilled;

    [XmlAttribute("FlyersKilled")]
    private int flyersKilled;

    [XmlAttribute("CatapultsKilled")]
    private int catapultsKilled;

    [XmlAttribute("BossesKilled")]
    private int bossesKilled;

    [XmlAttribute("MaxCastleHealth")]
    private int maxCastleHealth;

    [XmlAttribute("CurrenCastleHealth")]
    private int castleHealth;

    [XmlAttribute("Gold")]
    private int gold;

    [XmlAttribute("UserID")]
    internal int userID;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetUserStatus()
    {
        UserStatus.Instance.SetStatusValues(gold, castleHealth, maxCastleHealth, gruntsKilled, archersKilled, bombersKilled, flyersKilled, catapultsKilled, bossesKilled, lightningLevel, fireLevel, iceLevel, castleLevel, barracks, archeryRange, alchemyLab);
    }

    public void UpdateUserData(int goldAmount, int currentCastleHeath, int maximumCastleHealth, int gruntNumber, int archerNumber, int bomberNumber, int flyerNumber, int catapultNumber, int bossNumber, int lightningSkill, int fireSkill, int iceSkill, int userCastleLevel, bool barracksUpgrade, bool archeryRangeUpgrade, bool alchemyLabUpgrade)
    {
        gold = goldAmount;
        castleHealth = currentCastleHeath;
        maxCastleHealth = maximumCastleHealth;
        gruntsKilled = gruntNumber;
        archersKilled = archerNumber;
        bombersKilled = bomberNumber;
        flyersKilled = flyerNumber;
        catapultsKilled = catapultNumber;
        bossesKilled = bossNumber;
        lightningLevel = lightningSkill;
        fireLevel = fireSkill;
        iceLevel = iceSkill;
        castleLevel = userCastleLevel;
        barracks = barracksUpgrade;
        archeryRange = archeryRangeUpgrade;
        alchemyLab = alchemyLabUpgrade;

    }

    public void SaveData(string path)
    {
        LoadSave.Instance.Users[userID] = this;
        LoadSave.Instance.Save(Path.Combine(Application.persistentDataPath, "users.xml"));
    }

    public void LoadAllUsers()
    {
        LoadSave.Instance.Load(Path.Combine(Application.persistentDataPath, "users.xml"));
    }

    public void LoadData(int userNumber)
    {
        userName = LoadSave.Instance.Users[userNumber].userName;
        userID = userNumber;
        gold = LoadSave.Instance.Users[userNumber].gold;
        castleHealth = LoadSave.Instance.Users[userNumber].castleHealth;
        maxCastleHealth = LoadSave.Instance.Users[userNumber].maxCastleHealth;
        gruntsKilled = LoadSave.Instance.Users[userNumber].gruntsKilled;
        archersKilled = LoadSave.Instance.Users[userNumber].archersKilled;
        bombersKilled = LoadSave.Instance.Users[userNumber].bombersKilled;
        flyersKilled = LoadSave.Instance.Users[userNumber].flyersKilled;
        catapultsKilled = LoadSave.Instance.Users[userNumber].catapultsKilled;
        bossesKilled = LoadSave.Instance.Users[userNumber].bossesKilled;
        lightningLevel = LoadSave.Instance.Users[userNumber].lightningLevel;
        fireLevel = LoadSave.Instance.Users[userNumber].fireLevel;
        iceLevel = LoadSave.Instance.Users[userNumber].iceLevel;
        castleLevel = LoadSave.Instance.Users[userNumber].castleLevel;
        barracks = LoadSave.Instance.Users[userNumber].barracks;
        archeryRange = LoadSave.Instance.Users[userNumber].archeryRange;
        alchemyLab = LoadSave.Instance.Users[userNumber].alchemyLab;
    }
}
