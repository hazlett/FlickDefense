using UnityEngine;
using System.Collections;

public class SkillHandler : MonoBehaviour {

    public GameplayGUI gameplayGUI;

    private static SkillHandler instance;
    public static SkillHandler Instance { get { return instance; } set { instance = value; } }

    internal float cooldownPeriod = 0.0f;

    private Touch userTouch;
    private int level;
    private float timer = 0.0f, pastSkillSpawn = 0.0f;
    private bool usingSkill;
    private Vector2 touchOrigin, touchEnd;

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
        CHAINLIGHTNING,
        LIGHTNINGSTORM,
        LIGHTNINGWALL,
        THUNDERSTORM,
        NONE
    }

    internal Skills currentSkill = Skills.FIRESTORM;

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

        Debug.Log(currentSkill.ToString());

        if (timer - pastSkillSpawn > cooldownPeriod)
        {
            if (!GUIClicked())
            {
                UseSkill();
            }
        }
        if (currentSkill != Skills.NONE)
        {
            GetCoordinates();
        }
    }

    private void GetCoordinates()
    {
        if (Input.touchCount > 0)
        {
            userTouch = Input.GetTouch(0);

            switch (userTouch.phase)
            {
                case TouchPhase.Began: touchOrigin = userTouch.position;
                    break;
                case TouchPhase.Ended: touchEnd = userTouch.position;
                    break;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            touchOrigin = Input.mousePosition;
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
            case Skills.RAINOFFIRE:
                break;
            case Skills.ICEBALL:
                break;
            case Skills.ICEBLAST:
                break;
            case Skills.ICESTORM:
                break;
            case Skills.ICEWALL:
                break;
            case Skills.BLIZZARD:
                break;
            case Skills.LIGHTNINGSTRIKE:
                break;
            case Skills.CHAINLIGHTNING:
                break;
            case Skills.LIGHTNINGSTORM:
                break;
            case Skills.LIGHTNINGWALL:
                break;
            case Skills.THUNDERSTORM:
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
            currentSkill = Skills.NONE;
        }
    }

    private void FireBlast()
    {
        // Then the user touched the screen somewhere
        if (touchOrigin != Vector2.zero)
        {
            FireballSpawner.Instance.LaunchFireball(touchOrigin, false);
            ResetForNewSkill();
            currentSkill = Skills.NONE;
        }
    }

    private void FireStorm()
    {
        // Then the user touched the screen somewhere
        if (touchOrigin != Vector2.zero)
        {
            GroundFireSpawner.Instance.CreateFireStorm(touchOrigin);
            ResetForNewSkill();
            currentSkill = Skills.NONE;
        }
    }

    private void FireWall()
    {
        // Then the user touched the screen somewhere
        if (touchOrigin != Vector2.zero && touchEnd != Vector2.zero)
        {
            GroundFireSpawner.Instance.CreateFirewall(touchOrigin, touchEnd);
            ResetForNewSkill();
            currentSkill = Skills.NONE;
        }
    }

    private void RainOfFire() 
    {
        GameObject rainOfFire = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skills/Fire/RainOfFireBox"));
        ResetForNewSkill();
        cooldownPeriod = 60.0f;
    }

    private void ResetForNewSkill()
    {
        pastSkillSpawn = timer;
        touchOrigin = touchEnd = Vector2.zero;
        currentSkill = Skills.NONE;
    }
}
