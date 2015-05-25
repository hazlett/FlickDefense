public class SoundClip {
    
	public enum HUD
    {
        ClickButton,
        BuyCastleUpgrade,
        BuySkillUpgrade
    }

    public enum Enemy
    {
        Walk,
        Run,
        Fly,
        Explode,
        PickedUp,
        Clicked,
        Attack,
        ThrowRock,
        BattleCry,
        ThrownAway,
        Die
    }

    public enum Skills
    {
        CastFire,
        CastLightning,
        CastTornado
    }

    public enum Environment
    {
        CastleHit,
        CastleExplosion,
        ScreenHit
    }

    public enum GameState
    {
        StartGame,
        WinGame,
        LoseGame
    }

}
