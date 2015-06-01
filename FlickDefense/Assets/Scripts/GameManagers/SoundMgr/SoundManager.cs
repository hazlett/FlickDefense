using UnityEngine;
using System.Collections;

public class SoundManager{

    private enum EnemyType
    {
        Grunt,
        Archer,
        Knight,
        Catapult,
        BabyDragon,
        Boss
    }

    #region SoundDeclarations
    private static string HUDClickButton;
    private static string OpenWindow;
    private static string BuyCastleUpgrade = "Sounds/Clips/Coins";
    private static string BuySkillUpgrade;

    private static string GruntWalk;
    private static string GruntRun;
    private static string GruntPickedUp;
    private static string GruntAttack;
    private static string GruntBattleCry;
    private static string GruntThrownAway;
    private static string GruntDie = "Sounds/Clips/QuietSplat";

    private static string ArcherWalk;
    private static string ArcherRun;
    private static string ArcherPickedUp;
    private static string ArcherAttack;
    private static string ArcherBattleCry;
    private static string ArcherThrownAway;
    private static string ArcherDie;

    private static string KnightWalk;
    private static string KnightRun;
    private static string KnightExplode;
    private static string KnightPickedUp;
    private static string ThrowRock;
    private static string KnightBattleCry;
    private static string KnightThrownAway;

    private static string MiniDragonFly;
    private static string MiniDragonExplode;
    private static string MiniDragonClicked;
    private static string MiniDragonBattleCry;
    private static string MiniDragonDie;

    private static string CatapultWalk;
    private static string CatapultRun;
    private static string CatapultClicked;
    private static string CatapultThrowRock;
    private static string CatapultBattleCry;
    private static string CatapultDie;

    private static string BossWalk;
    private static string BossRun;
    private static string BossClicked;
    private static string BossAttack;
    private static string BossThrowRock;
    private static string BossBattleCry;
    private static string BossDie;

    private static string CastFire;
    private static string CastLightning;
    private static string CastTornado;

    private static string CastleHit;
    private static string CastleExplosion;
    private static string ScreenHit;

    private static string StartGame = "Sounds/Clips/BeginRound";
    private static string WinGame = "Sounds/Clips/WinRound";
    private static string LoseGame = "Sounds/Clips/GameLoss";

    private static string MainMenuMusic = "Sounds/Music";
    private static string InGameMusic;
    private static string PostGameMusic;
    #endregion

    public static void PlaySoundClip(GameObject source, SoundClip.HUD sound)
    {
        PlaySoundClip(sound);
    }

    public static void PlaySoundClip(GameObject source, SoundClip.Enemy sound)
    {
        EnemyType type = GetEnemyType(source);
        switch(type)
        {
            case EnemyType.BabyDragon:
                PlayDragonSoundClip(sound);
                break;
            case EnemyType.Boss:
                PlayBossSoundClip(sound);
                break;
            case EnemyType.Catapult:
                PlayCatapultSoundClip(sound);
                break;
            case EnemyType.Knight:
                PlayKnightSoundClip(sound);
                break;
            case EnemyType.Archer:
                PlayArcherSoundClip(sound);
                break;
            default:
                PlayGruntSoundClip(sound);
                break;
        }
    }

    public static void PlaySoundClip(GameObject source, SoundClip.Environment sound)
    {
        PlaySoundClip(sound);
    }

    public static void PlaySoundClip(GameObject source, SoundClip.GameState sound)
    {
        PlaySoundClip(sound);
    }

    public static void PlaySoundClip(GameObject source, SoundClip.Skills sound)
    {
        PlaySoundClip(sound);
    }

    public static void PlaySoundClip(SoundClip.HUD sound)
    {
        switch(sound)
        {
            case SoundClip.HUD.BuyCastleUpgrade:
                PlaySoundClip(BuyCastleUpgrade);
                break;
            case SoundClip.HUD.BuySkillUpgrade:
                PlaySoundClip(BuySkillUpgrade);
                break;
            case SoundClip.HUD.OpenWindow:
                PlaySoundClip(OpenWindow);
                break;
            default:
                PlaySoundClip(HUDClickButton);
                break;
        }
    }

    public static void PlaySoundClip(SoundClip.Environment sound)
    {
        switch (sound)
        {
            case SoundClip.Environment.CastleExplosion:
                PlaySoundClip(CastleExplosion);
                break;
            case SoundClip.Environment.ScreenHit:
                PlaySoundClip(ScreenHit);
                break;
            default:
                PlaySoundClip(CastleHit);
                break;
        }
    }

    public static void PlaySoundClip(SoundClip.GameState sound)
    {
        switch (sound)
        {
            case SoundClip.GameState.WinGame:
                PlaySoundClip(WinGame);
                break;
            case SoundClip.GameState.LoseGame:
                PlaySoundClip(LoseGame);
                break;
            default:
                PlaySoundClip(StartGame);
                break;
        }
    }

    public static void PlaySoundClip(SoundClip.Skills sound)
    {
        switch(sound)
        {
            case SoundClip.Skills.CastLightning:
                PlaySoundClip(CastLightning);
                break;
            case SoundClip.Skills.CastTornado:
                PlaySoundClip(CastTornado);
                break;
            default:
                PlaySoundClip(CastFire);
                break;
        }
    }

    public static void PlayMusic(Song song)
    {
        switch(song)
        {
            case Song.InGame:
                PlayMusic(InGameMusic);
                break;
            case Song.PostGame:
                PlayMusic(PostGameMusic);
                break;
            default:
                PlayMusic(MainMenuMusic);
                break;
        }
    }

    private static void PlayMusic(string song)
    {
        AudioClip clip = Resources.Load<AudioClip>(song);
        AudioSource source = Camera.main.GetComponent<AudioSource>();
        source.clip = clip;
        source.Play();
        source.loop = true;
    }

    private static void PlayGruntSoundClip(SoundClip.Enemy sound)
    {
        switch(sound)
        {
            case SoundClip.Enemy.Attack:
                PlaySoundClip(GruntAttack);
                break;
            case SoundClip.Enemy.Die:
                PlaySoundClip(GruntDie);
                break;
            case SoundClip.Enemy.PickedUp:
                PlaySoundClip(GruntPickedUp);
                break;
            case SoundClip.Enemy.Run:
                PlaySoundClip(GruntRun);
                break;
            case SoundClip.Enemy.Walk:
                PlaySoundClip(GruntWalk);
                break;
            case SoundClip.Enemy.ThrownAway:
                PlaySoundClip(GruntThrownAway);
                break;
            default:
                PlaySoundClip(GruntBattleCry);
                break;
        }
    }

    private static void PlayArcherSoundClip(SoundClip.Enemy sound)
    {
        switch (sound)
        {
            case SoundClip.Enemy.Attack:
                PlaySoundClip(ArcherAttack);
                break;
            case SoundClip.Enemy.Die:
                PlaySoundClip(ArcherDie);
                break;
            case SoundClip.Enemy.PickedUp:
                PlaySoundClip(ArcherPickedUp);
                break;
            case SoundClip.Enemy.Run:
                PlaySoundClip(ArcherRun);
                break;
            case SoundClip.Enemy.Walk:
                PlaySoundClip(ArcherWalk);
                break;
            case SoundClip.Enemy.ThrownAway:
                PlaySoundClip(ArcherThrownAway);
                break;
            default:
                PlaySoundClip(ArcherBattleCry);
                break;
        }
    }

    private static void PlayKnightSoundClip(SoundClip.Enemy sound)
    {
        switch (sound)
        {
            case SoundClip.Enemy.Explode:
                PlaySoundClip(KnightExplode);
                break;
            case SoundClip.Enemy.PickedUp:
                PlaySoundClip(KnightPickedUp);
                break;
            case SoundClip.Enemy.Run:
                PlaySoundClip(KnightRun);
                break;
            case SoundClip.Enemy.ThrownAway:
                PlaySoundClip(KnightThrownAway);
                break;
            case SoundClip.Enemy.Walk:
                PlaySoundClip(KnightWalk);
                break;
            default:
                PlaySoundClip(KnightBattleCry);
                break;
        }
    }

    private static void PlayDragonSoundClip(SoundClip.Enemy sound)
    {
        switch (sound)
        {
            case SoundClip.Enemy.Clicked:
                PlaySoundClip(MiniDragonClicked);
                break;
            case SoundClip.Enemy.Die:
                PlaySoundClip(MiniDragonDie);
                break;
            case SoundClip.Enemy.Explode:
                PlaySoundClip(MiniDragonExplode);
                break;
            case SoundClip.Enemy.Fly:
                PlaySoundClip(MiniDragonFly);
                break;
            default:
                PlaySoundClip(MiniDragonBattleCry);
                break;
        }
    }

    private static void PlayCatapultSoundClip(SoundClip.Enemy sound)
    {
        switch (sound)
        {
            case SoundClip.Enemy.Clicked:
                PlaySoundClip(CatapultClicked);
                break;
            case SoundClip.Enemy.Die:
                PlaySoundClip(CatapultDie);
                break;
            case SoundClip.Enemy.Run:
                PlaySoundClip(CatapultRun);
                break;
            case SoundClip.Enemy.Walk:
                PlaySoundClip(CatapultRun);
                break;
            case SoundClip.Enemy.ThrowRock:
                PlaySoundClip(CatapultThrowRock);
                break;
            default:
                PlaySoundClip(CatapultBattleCry);
                break;
        }
    }

    private static void PlayBossSoundClip(SoundClip.Enemy sound)
    {
        switch (sound)
        {
            case SoundClip.Enemy.Attack:
                PlaySoundClip(BossAttack);
                break;
            case SoundClip.Enemy.Clicked:
                PlaySoundClip(BossClicked);
                break;
            case SoundClip.Enemy.Die:
                PlaySoundClip(BossDie);
                break;
            case SoundClip.Enemy.Run:
                PlaySoundClip(BossRun);
                break;
            case SoundClip.Enemy.ThrowRock:
                PlaySoundClip(BossThrowRock);
                break;
            case SoundClip.Enemy.Walk:
                PlaySoundClip(BossWalk);
                break;
            default:
                PlaySoundClip(BossBattleCry);
                break;
        }
    }

    private static void PlaySoundClip(string sound)
    {
        AudioClip clip = Resources.Load<AudioClip>(sound);
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }

    private static EnemyType GetEnemyType(GameObject enemy)
    {
        bool found = ContainsEnemyScript(enemy);
        GameObject checkingObj = enemy;
        //Find Script in hierarchy
        while (!found && checkingObj.transform.childCount > 0)
        {
            checkingObj = checkingObj.transform.GetChild(0).gameObject;
            found = ContainsEnemyScript(checkingObj);
        }
        checkingObj = enemy;
        while (!found && checkingObj.transform.parent != null)
        {
            checkingObj = checkingObj.transform.parent.gameObject;
            found = ContainsEnemyScript(checkingObj);
        }
        //default
        if (!found) return EnemyType.Grunt;
        //else
        if (checkingObj.GetComponent<ArcherBehaviour>() != null) return EnemyType.Archer;
        if (checkingObj.GetComponent<BomberBehaviour>() != null) return EnemyType.Knight;
        if (checkingObj.GetComponent<CatapultBehaviour>() != null) return EnemyType.Catapult;
        if (checkingObj.GetComponent<FlyerBehaviour>() != null) return EnemyType.BabyDragon;
        return EnemyType.Grunt;
    }

    private static bool ContainsEnemyScript(GameObject checkingObj)
    {
        return checkingObj.GetComponent<ArcherBehaviour>() != null
            || checkingObj.GetComponent<BomberBehaviour>() != null
            || checkingObj.GetComponent<BossBehaviour>() != null
            || checkingObj.GetComponent<CatapultBehaviour>() != null
            || checkingObj.GetComponent<EnemyBehaviour>() != null
            || checkingObj.GetComponent<FlyerBehaviour>() != null
            || checkingObj.GetComponent<GruntBehaviour>() != null;
    }
}
