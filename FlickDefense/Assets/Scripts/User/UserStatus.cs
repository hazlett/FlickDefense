using UnityEngine;
using System.Collections;

public class UserStatus : MonoBehaviour {

    private static UserStatus instance;
    public static UserStatus Instance { get { return instance; } set { instance = value; } }
        
    private UserStatus()
    {
        castleHealth = 5;
        iceLevel = lightningLevel = fireLevel = gruntsKilled = archersKilled = bombersKilled = flyersKilled = catapultsKilled = bossesKilled = 0;
    }

    private int castleHealth;
    public int CastleHealth { get { return castleHealth; } }

    // Enemy Killed Stats
    private int gruntsKilled, archersKilled, bombersKilled, flyersKilled, catapultsKilled, bossesKilled;
    public int GruntsKilled { get { return gruntsKilled; } }
    public int ArchersKilled { get { return archersKilled; } }
    public int BombersKilled { get { return bombersKilled; } }
    public int FlyersKilled { get { return flyersKilled; } }
    public int CatapultsKilled { get { return catapultsKilled; } }
    public int BossesKilled { get { return bossesKilled; } }

    private int pastGruntsKilled, pastArchersKilled, pastBombersKilled, pastFlyersKilled, pastCatapultsKilled, pastBossesKilled;

    // Skills Purchased
    private int lightningLevel, fireLevel, iceLevel;
    public int LightningLevel { get { return lightningLevel; } }
    public int FireLevel { get { return fireLevel; } }
    public int IceLevel { get { return iceLevel; } }

    // Upgrades Purchased
    private bool barracks, archeryRange, alchemyLab, workshop;
    private int castleLevel, multiFingerLevel;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void DamageCastle() { castleHealth--; }

    public void DamageCastle(int damage) { castleHealth -= damage; }

    public void GruntKilled() { gruntsKilled++; }

    public void ArcherKilled() { archersKilled++; }

    public void BomberKilled() { bombersKilled++; }

    public void FlyerKilled() { flyersKilled++; }

    public void CatapultKilled() { catapultsKilled++; }

    public void BossKilled() { bossesKilled++; }

    public void SetPastKilled()
    {
        pastGruntsKilled = gruntsKilled;
        pastArchersKilled = archersKilled;
        pastBombersKilled = bombersKilled;
        pastFlyersKilled = flyersKilled;
        pastCatapultsKilled = catapultsKilled;
        pastBossesKilled = bossesKilled;
    }

    public int GruntsPastWave() { return gruntsKilled - pastGruntsKilled; }

    public int ArchersPastWave() { return archersKilled - pastArchersKilled; }

    public int BombersPastWave() { return bombersKilled - pastBombersKilled; }

    public int FlyersPastWave() { return flyersKilled - pastFlyersKilled; }

    public int CatapultsPastWave() { return catapultsKilled - pastCatapultsKilled; }

    public int BossesPastWave() { return bossesKilled - pastBossesKilled; }

    public void IncreaseLightning()
    {
        if (lightningLevel < 5)
        {
            lightningLevel++;
        }
    }

    public void IncreaseFire()
    {
        if (fireLevel < 5)
        {
            fireLevel++;
        }
    }

    public void IncreaseIce()
    {
        if (iceLevel < 5)
        {
            iceLevel++;
        }
    }

    public void SetBarracks() { barracks = true; }
    public void SetArcheryRange() { archeryRange = true; }
    public void SetAlchemyLab() { alchemyLab = true; }
    public void SetWorkshop() { workshop = true; }

    public void IncreaseCastle()
    {
        if (castleLevel < 5)
        {
            castleLevel++;
            castleHealth += 50 * castleLevel;
        }
    }

    public void IncreaseFlicks()
    {
        if (multiFingerLevel < 5)
        {
            multiFingerLevel++;
        }
    }
}
