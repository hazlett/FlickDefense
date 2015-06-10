using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot]
public class UserData 
{
    [XmlAttribute("WaveNumber")]
    public int waveLevel;

    [XmlAttribute("BarracksUpgrade")]
    public bool barracks;

    [XmlAttribute("ArcheryRangeUpgrade")]
    public bool archeryRange;

    [XmlAttribute("AlchemyLabUpgrade")]
    public bool alchemyLab;

    [XmlAttribute("CastleLevel")]
    public int castleLevel;

    [XmlAttribute("LightningSkillLevel")]
    public int lightningLevel;

    [XmlAttribute("FireSkillLevel")]
    public int fireLevel;

    [XmlAttribute("IceSkillLevel")]
    public int iceLevel;

    [XmlAttribute("GruntsKilled")]
    public int gruntsKilled;

    [XmlAttribute("ArchersKilled")]
    public int archersKilled;

    [XmlAttribute("BombersKilled")]
    public int bombersKilled;

    [XmlAttribute("FlyersKilled")]
    public int flyersKilled;

    [XmlAttribute("CatapultsKilled")]
    public int catapultsKilled;

    [XmlAttribute("BossesKilled")]
    public int bossesKilled;

    [XmlAttribute("MaxCastleHealth")]
    public int maxCastleHealth;

    [XmlAttribute("CurrenCastleHealth")]
    public int castleHealth;

    [XmlAttribute("Gold")]
    public int gold;

    [XmlAttribute("UserID")]
    public int userID;


    private static UserData instance { get; set; }
    public static UserData Instance
    {
        get
        {
            if (instance == null) instance = new UserData();
            return instance;
        }
        set {
            instance = value;
        }
    }

    public UserData()
    {
    }

    public void SetDefaultValues()
    {
        waveLevel = 1;
        gold = gruntsKilled = archersKilled = bombersKilled = catapultsKilled = flyersKilled = bossesKilled = iceLevel = fireLevel = lightningLevel = 0;
        castleHealth = maxCastleHealth = 50;
        castleLevel = 1;
        barracks = archeryRange = alchemyLab = false;
        userID = -1;
        SaveData();
    }

    public void UpdateUserData(int goldAmount, int currentCastleHeath, int maximumCastleHealth, int gruntNumber, int archerNumber, int bomberNumber, int flyerNumber, int catapultNumber, int bossNumber, int lightningSkill, int fireSkill, int iceSkill, int userCastleLevel, bool barracksUpgrade, bool archeryRangeUpgrade, bool alchemyLabUpgrade)
    {
        waveLevel = WaveSystem.Instance.WaveNumber;
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

    public void SaveData()
    {
        instance.waveLevel = WaveSystem.Instance.WaveNumber;
        string path = Path.Combine(Application.persistentDataPath, "userData.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(UserData));
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }
    public void LoadData()
    {
        string path = Path.Combine(Application.persistentDataPath, "userData.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(UserData));
        if (File.Exists(path))
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                instance = serializer.Deserialize(stream) as UserData;
            }
        }
        else
        {
            SetDefaultValues();
        }
        WaveSystem.Instance.WaveNumber = instance.waveLevel;
    }

    public void LoadAllUsers()
    {
        //LoadSave.Instance.Load(Path.Combine(Application.persistentDataPath, "users.xml"));
    }

    public void LoadData(int userNumber)
    {
        waveLevel = LoadSave.Instance.Users[userNumber].waveLevel;
        userID = LoadSave.Instance.Users[userNumber].userID;
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