using UnityEngine;
using System.Collections;

public class SkillHandler : MonoBehaviour {

    public GameplayGUI gameplayGUI;

    private static SkillHandler instance;
    public static SkillHandler Instance { get { return instance; } set { instance = value; } }

    internal float cooldownPeriod = 0.01f, cooldownScale;

    private Touch userTouch;
    private int level;
    private float timer = 0.0f, pastSkillSpawn = 0.0f;
    private bool usingSkill, clicked = false;
    private Vector2 touchOrigin = Vector2.zero, touchEnd = Vector2.zero;

    internal enum Skills
    {
        FIREBALL,
        FIREBLAST,
        FIRESTORM,
        FIREWALL,
        RAINOFFIRE,
        ICEBALL,
        ICEBLAST,
        ICESTORM,
        ICEWALL,
        BLIZZARD,
        LIGHTNINGSTRIKE,
        MULTISTRIKE,
        LIGHTNINGSTORM,
        LIGHTNINGWALL,
        THUNDERSTORM,
        NONE
    }

    internal Skills currentSkill = Skills.NONE;

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

    void Update()
    {
        timer += Time.deltaTime;

        cooldownScale = (timer - pastSkillSpawn) / cooldownPeriod;

        if (timer - pastSkillSpawn > cooldownPeriod && clicked)
        {
            if (!GUIClicked())
            {
                UseSkill();
            }
            else
            {
                GetCoordinates();
            }
        }
        if (currentSkill != Skills.NONE)
        {
            if (!GameStateManager.Instance.flicking)
            {
                GetCoordinates();
            }
        }
    }

    private void GetCoordinates()
    {
        if (Input.touchCount > 0)
        {
            userTouch = Input.GetTouch(0);

            switch (userTouch.phase)
            {
                case TouchPhase.Began: if (!gameplayGUI.Clicked(userTouch.position))
                    {
                        Debug.Log("TouchBegan");
                        touchOrigin = userTouch.position;
                    }
                    break;
                case TouchPhase.Moved:
                    break;
                case TouchPhase.Ended: if (!gameplayGUI.Clicked(userTouch.position))
                    {
                        Debug.Log("TouchEnded");
                        touchEnd = userTouch.position;
                    }
                    break;
            }

            clicked = true;
        }
        else { clicked = false; }

        if (Input.GetMouseButtonDown(0))
        {
            touchOrigin = Input.mousePosition;
            clicked = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            touchEnd = Input.mousePosition;
        }
    }

    private bool GUIClicked()
    {
        return gameplayGUI.Clicked(touchOrigin);
    }

    private void UseSkill()
    {
        switch (currentSkill)
        {
            case Skills.FIREBALL: Fireball();
                break;
            case Skills.FIREBLAST: FireBlast();
                break;
            case Skills.FIRESTORM: FireStorm();
                break;
            case Skills.FIREWALL: FireWall();
                break;
            case Skills.RAINOFFIRE: RainOfFire();
                break;
            case Skills.ICEBALL: SnowCloud();
                break;
            case Skills.ICEBLAST: FreezeCloud();
                break;
            case Skills.ICESTORM: SnowStorm();
                break;
            case Skills.ICEWALL: IceWall();
                break;
            case Skills.BLIZZARD: Blizzard();
                break;
            case Skills.LIGHTNINGSTRIKE: LightningStrike();
                break;
            case Skills.MULTISTRIKE: MultiStrike();
                break;
            case Skills.LIGHTNINGSTORM: LightningStorm();
                break;
            case Skills.LIGHTNINGWALL: LightningWall();
                break;
            case Skills.THUNDERSTORM: ThunderStorm();
                break;
        }
    }

    private void Fireball() 
    {
        // Then the user touched the screen somewhere
        if (touchOrigin != Vector2.zero)
        {
            FireballSpawner.Instance.LaunchFireball(touchOrigin, true);
            ResetForNewSkill();
        }
    }

    private void FireBlast()
    {
        // Then the user touched the screen somewhere
        if (touchOrigin != Vector2.zero)
        {
            FireballSpawner.Instance.LaunchFireball(touchOrigin, false);
            ResetForNewSkill();
        }
    }

    private void FireStorm()
    {
        // Then the user touched the screen somewhere
        if (touchOrigin != Vector2.zero)
        {
            GroundFireSpawner.Instance.CreateFireStorm(touchOrigin);
            ResetForNewSkill();
        }
    }

    private void FireWall()
    {
        // Then the user touched the screen somewhere
        if (touchOrigin != Vector2.zero && touchEnd != Vector2.zero)
        {
            GroundFireSpawner.Instance.CreateFirewall(touchOrigin, touchEnd);
            ResetForNewSkill();
        }
    }

    private void RainOfFire() 
    {
        GameObject rainOfFire = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skills/Fire/RainOfFireBox"));
        ResetForNewSkill();
        cooldownPeriod = 60.0f;
    }

    private void LightningStrike()
    { 
        // Then the user touched the screen somewhere
        if (touchOrigin != Vector2.zero)
        {
            if (LightningStrikeSpawner.Instance.SpawnCloud(touchOrigin, true))
            {
                ResetForNewSkill();
            }
        }
    }

    private void MultiStrike()
    {
        // Then the user touched the screen somewhere
        if (touchOrigin != Vector2.zero)
        {
            if (LightningStrikeSpawner.Instance.SpawnCloud(touchOrigin, false))
            {
                ResetForNewSkill();
            }
        }
    }

    private void LightningStorm()
    {
        // Then the user touched the screen somewhere
        if (touchOrigin != Vector2.zero)
        {
            GroundLightningSpawner.Instance.CreateLightningStorm(touchOrigin);
            ResetForNewSkill();
        }
    }

    private void LightningWall()
    {
        // Then the user touched the screen somewhere
        if (touchOrigin != Vector2.zero && touchEnd != Vector2.zero)
        {
            GroundLightningSpawner.Instance.CreateLightningwall(touchOrigin, touchEnd);
            ResetForNewSkill();
        }
    }

    private void ThunderStorm()
    {
        // Then the user touched the screen somewhere
        if (touchOrigin != Vector2.zero)
        {
            GroundLightningSpawner.Instance.CreateThunderStorm();
            ResetForNewSkill();
        }
    }

    private void SnowCloud()
    {
        // Then the user touched the screen somewhere
        if (touchOrigin != Vector2.zero)
        {
            if (SnowCloudSpawner.Instance.SpawnCloud(touchOrigin, true))
            {
                ResetForNewSkill();
            }
        }
    }

    private void FreezeCloud()
    {
        // Then the user touched the screen somewhere
        if (touchOrigin != Vector2.zero)
        {
            if (SnowCloudSpawner.Instance.SpawnCloud(touchOrigin, false))
            {
                ResetForNewSkill();
            }
        }
    }

    private void SnowStorm()
    {
        // Then the user touched the screen somewhere
        if (touchOrigin != Vector2.zero)
        {
            GroundIceSpawner.Instance.CreateSnowStorm(touchOrigin);
            ResetForNewSkill();
        }
    }

    private void IceWall()
    {
        // Then the user touched the screen somewhere
        if (touchOrigin != Vector2.zero && touchEnd != Vector2.zero)
        {
            GroundIceSpawner.Instance.CreateIceWall(touchOrigin, touchEnd);
            ResetForNewSkill();
        }
    }

    private void Blizzard()
    {
        // Then the user touched the screen somewhere
        if (touchOrigin != Vector2.zero)
        {
            GroundIceSpawner.Instance.CreateBlizzard();
            ResetForNewSkill();
        }
    }

    internal void ResetForNewSkill()
    {
        pastSkillSpawn = timer;
        ChangeSkill();
    }

    internal void ChangeSkill()
    {
        touchOrigin = touchEnd = Vector2.zero;
        clicked = false;
        currentSkill = Skills.NONE;
    }

}
