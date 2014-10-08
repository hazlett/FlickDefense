using UnityEngine;
using System.Collections;

public class UserStatus : MonoBehaviour {

    private static UserStatus instance;
    public static UserStatus Instance { get { return instance; } set { instance = value; } }
        
    private UserStatus()
    {
        castleHealth = 5;
        gruntsKilled = archersKilled = bombersKilled = flyersKilled = catapultsKilled = bossesKilled = 0;
    }

    private int castleHealth;
    public int CastleHealth { get { return castleHealth; } }
    private int gruntsKilled;
    public int GruntsKilled { get { return gruntsKilled; } }
    private int archersKilled;
    public int ArchersKilled { get { return archersKilled; } }
    private int bombersKilled;
    public int BombersKilled { get { return bombersKilled; } }
    private int flyersKilled;
    public int FlyersKilled { get { return flyersKilled; } }
    private int catapultsKilled;
    public int CatapultsKilled { get { return catapultsKilled; } }
    private int bossesKilled;
    public int BossesKilled { get { return bossesKilled; } }

    private int pastGruntsKilled, pastArchersKilled, pastBombersKilled, pastFlyersKilled, pastCatapultsKilled, pastBossesKilled;

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
}
