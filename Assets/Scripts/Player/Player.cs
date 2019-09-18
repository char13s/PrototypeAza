﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
#pragma warning disable 0649
public class Player : MonoBehaviour
{
    //private bool usingController;
    [Header("Movement")]
    private bool moving;
    //public float speed;
    [SerializeField] private float moveSpeed = 3;

    [Space]
    [Header("Attacking")]
    [SerializeField] private GameObject hitBox;
    private bool attacking;
    private bool skillButton;
    private bool lockedOn;
    [SerializeField] private GameObject swordSpawn;
    [SerializeField] private GameObject swordDSpawn;
    private bool swordIN;
    private bool skillIsActive;
    [Space]
    [Header("rotations")]
    [SerializeField] private GameObject body;
    //public bool right;

    [Space]
    [Header("Items")]
    //public List<Items> inventory;
    [SerializeField] private GameObject demonSword;
    [SerializeField] private GameObject demonSwordBack;
    [SerializeField] private GameObject guitar;

    [Space]
    [Header("Animation States")]
    private bool rockOut;
    private bool pickUp;
    private bool wall;
    private bool climbing;
    private bool chopping;
    private bool grounded;
    private bool gliding;
    private bool wallMoving;
    private bool leftDash;
    private bool rightDash;
    private bool guard;
    private bool hit;
    private bool dead;
    private int direction;
    private bool stop;
    private int skillId;
    private bool attack;
    private int animations;
    [Space]
    [Header("OtherFunctions")]
    private Rigidbody rBody;
    private bool pause;
    private int guitarTimer = 150;
    private byte timer;
    private bool loaded;
    [SerializeField] private GameObject aza;
    [SerializeField] private GameObject zend;

    [SerializeField] private RuntimeAnimatorController azaAnimatorController;

    [SerializeField] private Material fader;
    [SerializeField] private Material normal;
    [SerializeField] private Material handle;
    [SerializeField] private Material blade;


    [SerializeField] private SkillButton triangle;
    [SerializeField] private SkillButton circle;
    [SerializeField] private SkillButton square;
    [SerializeField] private SkillButton x;

    [SerializeField] private GameObject trail;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject axe;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject Cam;
    [SerializeField] private GameObject abilitiesUi;

    [SerializeField] private GameObject AoeHitbox;
    [SerializeField] private GameObject forwardHitbox;
    [SerializeField] private GameObject fireTrail;
    [SerializeField] private GameObject fireCaster;
    private static Player instance;


    private Coroutine guardCoroutine;
    private Coroutine hitDefuse;
    private Coroutine dodgeCoroutine;

    private int hitCounter;
    //private Vector3 delta;
    internal Inventory items = new Inventory();
    internal Stats stats = new Stats();
    private AxisButton dPadUp = new AxisButton("DPad Up");
    private AxisButton R2 = new AxisButton("R2");

    private bool perfectGuard;
    private NavMeshAgent nav;
    private PlayerBattleSceneMovement battleMode;
    private Animator anim;
    private Vector3 displacement;

    public static event UnityAction onPlayerDeath;
    public static event UnityAction onPlayerEnabled;
    public static event UnityAction playerIsLockedOn;
    //Optimize these to use only one Animation parameter in 9/14
    public bool RockOut { get => rockOut; set { rockOut = value; anim.SetBool("RockOut", rockOut); } }
    public bool PickUp1 { get => pickUp; set { pickUp = value; anim.SetBool("PickUp", pickUp); } }
    public bool Wall { get => wall; set => wall = value; }
    public bool Climbing1 { get => climbing; set { climbing = value; anim.SetBool("Climbing", climbing); } }
    public bool Grounded { get => grounded; set { Debug.Log(value); grounded = value; anim.SetBool("Grounded", grounded); } }

    public bool WallMoving { get => wallMoving; set { wallMoving = value; anim.SetBool("WallMoving", wallMoving); } }
    public bool LeftDash { get => leftDash; set { leftDash = value; anim.SetBool("LeftDash", leftDash); } }
    public bool RightDash { get => rightDash; set { rightDash = value; anim.SetBool("RightDash", rightDash); } }
    public bool Guard { get => guard; set { guard = value; if (value) Moving = false; shield.SetActive(value); anim.SetBool("Guard", guard); } }
    public bool Attacking { get => attacking; set { attacking = value; anim.SetBool("AttackStance", attacking); } }
    public bool Moving { get => moving; set { moving = value; anim.SetBool("Moving", moving); } }
    public int HitCounter { get => hitCounter; set { hitCounter = value; anim.SetInteger("counter", hitCounter); } }
    public byte Timer { get => timer; set => timer = value; }
    public GameObject Body { get => body; set => body = value; }
    public bool Hit { get => hit; set { hit = value; anim.SetBool("Hurt", hit); if (hit) { hitDefuse = StartCoroutine(HitDefuse()); } } }
    public bool Dead { get => dead; set { dead = value; anim.SetBool("Dead", dead); if (dead && onPlayerDeath != null) { onPlayerDeath(); } } }
    public int Direction { get => direction; set { direction = value; anim.SetInteger("Direction", direction); } }
    //public Vector3 Delta { get => delta; set => delta = value; }
    public GameObject AbilitiesUi { get => abilitiesUi; set => abilitiesUi = value; }
    public bool Stop { get => stop; set => stop = value; }
    public bool Pause { get => pause; set { pause = value; if (pause) { Time.timeScale = 0; } else { Time.timeScale = 1; } } }
    public bool Loaded { get => loaded; set { loaded = value; Nav.enabled = value; } }

    public GameObject FireCaster { get => fireCaster; set => fireCaster = value; }

    public PlayerBattleSceneMovement BattleMode { get => battleMode; set => battleMode = value; }
    public GameObject DemonSword { get => demonSword; set => demonSword = value; }
    public GameObject HitBox { get => hitBox; set => hitBox = value; }
    public bool Attack { get => attack; set { attack = value; anim.SetBool("Attack", attack); } }
    public int SkillId { get => skillId; set { skillId = value; anim.SetInteger("Skill ID", skillId); } }
    public Rigidbody RBody { get => rBody; set => rBody = value; }
    public NavMeshAgent Nav { get => nav; set { nav = value; } }
    public Animator Anim { get => anim; set => anim = value; }
    public bool Chopping { get => chopping; set { chopping = value; anim.SetBool("Chopping", chopping); } }

    public GameObject Axe { get => axe; set => axe = value; }
    public bool PerfectGuard { get => perfectGuard; set => perfectGuard = value; }
    public GameObject ForwardHitbox { get => forwardHitbox; set => forwardHitbox = value; }
    public GameObject FireTrail { get => fireTrail; set => fireTrail = value; }
    public GameObject AoeHitbox1 { get => AoeHitbox; set => AoeHitbox = value; }
    public int Animations { get => animations; set { animations = value; anim.SetInteger("Animations", animations); } }

    public bool LockedOn { get => lockedOn; set { lockedOn = value; if (LockedOn) { if (playerIsLockedOn != null) playerIsLockedOn(); } if (!LockedOn) Direction = 0; } }

    public bool SkillIsActive { get => skillIsActive; set => skillIsActive = value; }

    public static Player GetPlayer() => instance.GetComponent<Player>();
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        Anim = GetComponent<Animator>();
    }

    void Start()
    {
        
        //Stats.onStaminaChange+=StartCoroutine(StaminaRec());
        onPlayerDeath += OnDead;
        GameController.onNewGame += SetDefault;
        stats.Start();
        items.Start();
        rBody = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        battleMode = GetComponent<PlayerBattleSceneMovement>();
        grounded = anim.GetBool("Grounded");
    }
    private void OnEnable()
    {
        if (onPlayerEnabled != null)
        {
            onPlayerEnabled();
        }
        StartCoroutine(StaminaRec());
    }
    // Update is called once per frame
    void Update()
    {

        if (grounded && hitCounter <= 0 && !guard && !lockedOn)
        {
            GetInput();
        }
        Sword();
        //Inventory();
        //Guitar();
        OnPause();
        Skills();

        if (attacking && R2.GetButton())
        {
            skillButton = true;
        }
        else
            skillButton = false;
        //if (Input.GetKey(KeyCode.P)) { stats.Level += 10; }
    }
    void OnDead()
    {
        //GetComponentInChildren<SkinnedMeshRenderer>().material = fader;
        //GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("Boolean_B8FD8DD", 1);

    }
    void SetDefault()
    {
        Attacking = false;
        HitCounter = 0;
        stats.Start();
        //GetComponentInChildren<SkinnedMeshRenderer>().material = normal;
        battleMode.Enemies.Clear();
        Dead = false;
    }
    void SwitchCharacter()
    {
        zend.SetActive(false);
        Instantiate(aza, transform);
        transform.localScale = new Vector3(1, 1, 1);
    }
    private void GetInput()
    {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * Time.deltaTime;
        displacement = Vector3.Normalize(new Vector3(x, 0, y));
        if (ThreeDCamera.IsActive && !lockedOn)
        {
            displacement = ThreeDCamera.XZOrientation.TransformDirection(displacement);
        }
        if (Input.GetButtonDown("R3"))
        {
            Grounded = true;
            //SwitchCharacter();
        }
        MoveIt(x, y);
    }
    private void MoveIt(float x, float y)
    {
        if (x != 0 || y != 0)
        {
            //Moving = true;
            Animations = 1;
            transform.position += displacement * moveSpeed * Time.deltaTime;
            if (Vector3.SqrMagnitude(displacement) > 0.01f)
            {
                transform.forward = displacement;
            }
            if (attacking && Input.GetButtonDown("X"))
            {

            }
        }
        else
        {
            Animations = 0;
            //Moving = false;
        }
    }
    private void Inventory()
    {
        if (items.PocketActive)
        {
            if (Input.GetKeyDown(KeyCode.E) && items.Page < 3)
            {

                items.Page++; Debug.Log(items.Page);
                items.DisplayInventory();
            }
            if (Input.GetButtonDown("L1") && items.Page > 0)
            {

                items.Page--; Debug.Log(items.Page);
                items.DisplayInventory();
            }
        }
    }
    private void Skills()
    {
        if (skillButton && Input.GetButtonDown("Triangle") && !skillIsActive)
        {
            Debug.Log("Check 1");
            
            if (triangle.SkillAssigned != null && stats.MPLeft >= triangle.MpRequired)
            {
                stats.MPLeft -= triangle.MpRequired;
                triangle.UseSkill();
                skillIsActive = true;
            }

        }

        if (skillButton && Input.GetButtonDown("Square") && !skillIsActive)
        {
            if (square.SkillAssigned != null && stats.MPLeft >= square.MpRequired)
            {
                stats.MPLeft -= square.MpRequired;
                square.UseSkill();
                skillIsActive = true;
            }

        }
        if (skillButton && Input.GetButtonDown("Circle") && !skillIsActive)
        {
            if (circle.SkillAssigned != null && stats.MPLeft >= circle.MpRequired)
            {
                stats.MPLeft -= circle.MpRequired;
                circle.UseSkill();
                skillIsActive = true;
            }

        }
        if (skillButton && Input.GetButtonDown("X") && !skillIsActive)
        {
            if (x.SkillAssigned != null && stats.MPLeft >= x.MpRequired)
            {
                stats.MPLeft -= x.MpRequired;
                x.UseSkill();
                skillIsActive = true;
            }

        }
        if (stats.MPLeft >= 2 && !skillIsActive && Input.GetButtonDown("Circle") && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            SkillId = 10;
            stats.MPLeft -= 2;
            skillIsActive = true;
        }
    }
    void Guitar()
    {
        if (Input.GetKey(KeyCode.R) && items.HasItem(2))
        {

            RockOut = true;
        }
    }
    private IEnumerator GuardCoroutine()
    {

        yield return new WaitForSeconds(1f);
        PerfectGuard = false;
        StopCoroutine(guardCoroutine);

    }
    private IEnumerator HitDefuse()
    {
        yield return new WaitForSeconds(0.3f);
        Hit = false;
        StopCoroutine(hitDefuse);
    }
    private IEnumerator StaminaRec()
    {

        while (isActiveAndEnabled)
        {
            yield return new WaitForSeconds(5);
            if (stats.MPLeft < stats.MP)
            {
                stats.MPLeft += 5;
            }
        }

    }
    private void Sword()
    {

        if (Input.GetButtonDown("L1") && !Attacking)
        {
            Attacking = true;
            demonSwordBack.SetActive(false);

            return;
        }
        if (Attacking)
        {
            if (Input.GetButtonDown("R1") && BattleMode.Enemies.Count > 0)
            {
                Animations = 0;
            }
            if (Input.GetButton("R1") && BattleMode.Enemies.Count > 0)
            {
                LockedOn = true;
            }
            else
            {
                LockedOn = false;
            }

            if (Input.GetButtonDown("Square"))
            {
                PerfectGuard = true;
                guardCoroutine = StartCoroutine(GuardCoroutine());
            }
            if (Input.GetButton("Square"))
            {
                Guard = true;
            }
            else
            {
                Guard = false;
            }

            if (Input.GetButtonDown("L1"))
            {
                Debug.Log("attacking is false");
                Attacking = false;
                LockedOn = false;
                return;
            }

            DemonSword.SetActive(true);
            trail.SetActive(true);

            if (Input.GetButtonDown("X"))
            {
                Attack = true;
            }
        }
        else
        {

            trail.SetActive(false);
            HitBox.SetActive(false);
            SkillId = 0;
            DemonSword.SetActive(false);
            demonSwordBack.SetActive(true);
            HitCounter = 0;
        }
    }
    private void OnPause()
    {
        if (Input.GetButtonDown("Pause") && !Pause)
        {
            pauseMenu.SetActive(true);
            Pause = true;
            return;
        }
        if (Input.GetButtonDown("Pause") && Pause)
        {
            pauseMenu.SetActive(false);
            Pause = false;
            return;
        }
    }
    public void PickUp(Items other)
    {
        PickUp1 = true;
        Wall = false;

        Timer = 5;
        items.AddItem(other.GetComponent<Items>().data);


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            items.AddItem(other.GetComponent<Items>().data);
            other.gameObject.SetActive(false);
            Destroy(other);
        }
    }
}
