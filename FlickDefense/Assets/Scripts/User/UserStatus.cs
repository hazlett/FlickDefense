﻿using UnityEngine;
using System.Collections;

public class UserStatus {

    private static UserStatus instance = new UserStatus();
    public static UserStatus Instance { get { return instance; } set { instance = value; } }

    public UserData currentUser = new UserData();
        
    private UserStatus()
    {
        waveGold = gold = 0;
        castleHealth = 100;
        maxCastleHealth = 100;
        castleLevel = 1;
        iceLevel = lightningLevel = fireLevel = gruntsKilled = archersKilled = bombersKilled = flyersKilled = catapultsKilled = bossesKilled = 0;
    }

    internal void SetCastleHealth(int castleHealth)
    {
        this.castleHealth = castleHealth;
    }
    private int waveGold;
    public int WaveGold { get { return waveGold; } }

    private int gold;
    public int Gold { get { return gold; } }

    private int castleHealth;
    public int CastleHealth { get { return castleHealth; } }
    private int maxCastleHealth;
    public int MaxCastleHealth { get { return maxCastleHealth; } }

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
    private bool barracks, archeryRange, alchemyLab;
    private int castleLevel, multiFingerLevel;
    public bool Barracks { get { return barracks; } }
    public bool ArcheryRange { get { return archeryRange; } }
    public bool AlchemyLab { get { return alchemyLab; } }
    public int CastleLevel { get { return castleLevel; } }


    public void SetStatusValues(int goldAmount, int currentCastleHeath, int maximumCastleHealth, int gruntNumber, int archerNumber, int bomberNumber, int flyerNumber, int catapultNumber, int bossNumber, int lightningSkill, int fireSkill, int iceSkill, int userCastleLevel, bool barracksUpgrade, bool archeryRangeUpgrade, bool alchemyLabUpgrade)
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
    public void UpdateUserDataValues()
    {
        currentUser.UpdateUserData(gold, castleHealth, maxCastleHealth, gruntsKilled, archersKilled, bombersKilled, flyersKilled, catapultsKilled, bossesKilled, lightningLevel, fireLevel, iceLevel, castleLevel, barracks, archeryRange, alchemyLab);
    }

    public void GoldExchange(int amount)
    {
        gold += amount;
        waveGold += amount;
    }

    public void DamageCastle() { castleHealth--; }

    public void DamageCastle(int damage)
    {
        castleHealth -= damage;
        if (castleHealth < 0)
        {
            castleHealth = 0;
        }
        if (castleHealth > maxCastleHealth)
        {
            castleHealth = maxCastleHealth;
        }
    }

    public void GruntKilled()
    {
        gruntsKilled++;

        Debug.Log("Grunts killed: " + gruntsKilled);

        GoldExchange(1 * (1 + WaveSystem.Instance.waveNumber / 5));
    }

    public void ArcherKilled()
    {
        archersKilled++;

        GoldExchange(3 * (1 + WaveSystem.Instance.waveNumber / 5));
    }

    public void BomberKilled()
    {
        bombersKilled++;

        GoldExchange(7 * (1 + WaveSystem.Instance.waveNumber / 5));
    }

    public void FlyerKilled()
    {
        flyersKilled++;

        GoldExchange(5 * (1 + WaveSystem.Instance.waveNumber / 5));
    }

    public void CatapultKilled()
    {
        catapultsKilled++;

        GoldExchange(10 * (1 + WaveSystem.Instance.waveNumber / 5));
    }

    public void BossKilled()
    {
        bossesKilled++;

        GoldExchange(50 * (1 + WaveSystem.Instance.waveNumber / 5));
    }

    public void ResetWaveGold()
    {
        waveGold = 0;
    }

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

    public void IncreaseCastle()
    {
        if (castleLevel < 5)
        {
            castleLevel++;
            maxCastleHealth += 100 * castleLevel;
            castleHealth = maxCastleHealth;
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
