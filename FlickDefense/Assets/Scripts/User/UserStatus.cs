using UnityEngine;
using System.Collections;

public class UserStatus {

    private static UserStatus instance = new UserStatus();
    public static UserStatus Instance { get { return instance; } set { instance = value; } }

    public UserData currentUser = UserData.Instance;
        
    private UserStatus()
    {
        waveGold = UserData.Instance.gold = 0;
        UserData.Instance.castleHealth = 100;
        UserData.Instance.maxCastleHealth = 100;
        UserData.Instance.castleLevel = 1;
        UserData.Instance.iceLevel = UserData.Instance.lightningLevel = UserData.Instance.fireLevel = gruntsKilled = archersKilled = bombersKilled = flyersKilled = catapultsKilled = bossesKilled = 0;
    }

    internal void SetCastleHealth(int castleHealth)
    {
        UserData.Instance.castleHealth = castleHealth;
    }
    private int waveGold;
    public int WaveGold { get { return waveGold; } }

    // Enemy Killed Stats
    private int gruntsKilled, archersKilled, bombersKilled, flyersKilled, catapultsKilled, bossesKilled;

    public void NewWave()
    {
        gruntsKilled = archersKilled = bombersKilled = flyersKilled = catapultsKilled = bossesKilled = waveGold = 0;
    }

    public void SetStatusValues(int goldAmount, int currentCastleHeath, int maximumCastleHealth, int gruntNumber, int archerNumber, int bomberNumber, int flyerNumber, int catapultNumber, int bossNumber, int lightningSkill, int fireSkill, int iceSkill, int userCastleLevel, bool barracksUpgrade, bool archeryRangeUpgrade, bool alchemyLabUpgrade)
    {
        UserData.Instance.gold = goldAmount;
        UserData.Instance.castleHealth = currentCastleHeath;
        UserData.Instance.maxCastleHealth = maximumCastleHealth;
        UserData.Instance.lightningLevel = lightningSkill;
        UserData.Instance.fireLevel = fireSkill;
        UserData.Instance.iceLevel = iceSkill;
        UserData.Instance.castleLevel = userCastleLevel;
        UserData.Instance.barracks = barracksUpgrade;
        UserData.Instance.archeryRange = archeryRangeUpgrade;
        UserData.Instance.alchemyLab = alchemyLabUpgrade;
    }

    public void GoldExchange(int amount)
    {
        UserData.Instance.gold += amount;
        waveGold += amount;
    }

    public void DamageCastle() { UserData.Instance.castleHealth--; }

    public void DamageCastle(int damage)
    {
        UserData.Instance.castleHealth -= damage;
        if (UserData.Instance.castleHealth < 0)
        {
            UserData.Instance.castleHealth = 0;
        }
        if (UserData.Instance.castleHealth > UserData.Instance.maxCastleHealth)
        {
            UserData.Instance.castleHealth = UserData.Instance.maxCastleHealth;
        }
    }

    public void GruntKilled()
    {
        gruntsKilled++;

        GoldExchange(1 * (1 + WaveSystem.Instance.WaveNumber / 5));
    }

    public void ArcherKilled()
    {
        archersKilled++;

        GoldExchange(3 * (1 + WaveSystem.Instance.WaveNumber / 5));
    }

    public void BomberKilled()
    {
        bombersKilled++;

        GoldExchange(7 * (1 + WaveSystem.Instance.WaveNumber / 5));
    }

    public void FlyerKilled()
    {
        flyersKilled++;

        GoldExchange(5 * (1 + WaveSystem.Instance.WaveNumber / 5));
    }

    public void CatapultKilled()
    {
        catapultsKilled++;

        GoldExchange(10 * (1 + WaveSystem.Instance.WaveNumber / 5));
    }

    public void BossKilled()
    {
        bossesKilled++;

        GoldExchange(50 * (1 + WaveSystem.Instance.WaveNumber / 5));
    }

    public void ResetWaveGold()
    {
        waveGold = 0;
    }

    public void SetPastKilled()
    {
        UserData.Instance.gruntsKilled += gruntsKilled;
        UserData.Instance.archersKilled += archersKilled;
        UserData.Instance.bombersKilled += bombersKilled;
        UserData.Instance.flyersKilled += flyersKilled;
        UserData.Instance.catapultsKilled += catapultsKilled;
        UserData.Instance.bossesKilled += bossesKilled;
    }

    public int GruntsPastWave() { return gruntsKilled; }

    public int ArchersPastWave() { return archersKilled; }

    public int BombersPastWave() { return bombersKilled; }

    public int FlyersPastWave() { return flyersKilled; }

    public int CatapultsPastWave() { return catapultsKilled; }

    public int BossesPastWave() { return bossesKilled; }

    public void IncreaseLightning()
    {
        if (UserData.Instance.lightningLevel < 5)
        {
            UserData.Instance.lightningLevel++;
        }
    }

    public void IncreaseFire()
    {
        if (UserData.Instance.fireLevel < 5)
        {
            UserData.Instance.fireLevel++;
        }
    }

    public void IncreaseIce()
    {
        if (UserData.Instance.iceLevel < 5)
        {
            UserData.Instance.iceLevel++;
        }
    }

    public void SetBarracks() { UserData.Instance.barracks = true; }
    public void SetArcheryRange() { UserData.Instance.archeryRange = true; }
    public void SetAlchemyLab() { UserData.Instance.alchemyLab = true; }

    public void IncreaseCastle()
    {
        if (UserData.Instance.castleLevel < 5)
        {
            UserData.Instance.castleLevel++;
            UserData.Instance.maxCastleHealth += 100 * UserData.Instance.castleLevel;
        }
    }
}
