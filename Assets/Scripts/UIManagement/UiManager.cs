﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
#pragma warning disable 0649
public class UiManager : MonoBehaviour
{
    [Header("Tutorial Stuff")]
    [SerializeField] private GameObject movementTutorial;
    [SerializeField] private GameObject miniMapTutorial;
    [SerializeField] private GameObject pauseTutorial;
    [SerializeField] private GameObject combatTutorial;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject tutorMenu;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject fireBallTutorial;
    [SerializeField] private GameObject flameTornadoTutorial;
    [SerializeField] private GameObject HeavySwingTutorial;
    [Space]
    [Header("PlayerUI")]
    [SerializeField] private GameObject playerUi;
    [SerializeField] private Image black;
    [SerializeField] private GameObject miniMap;
    [SerializeField] private Text exp;
    [SerializeField] private Text level;
    [SerializeField] private Text health;
    [SerializeField] private Text money;
    [SerializeField] private GameObject abilities;
    [SerializeField] private Text stamina;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider staminaBar;
    [SerializeField] private Slider expBar;
    [SerializeField] private GameObject abilityClose;
    [Space]
    [Header("AzaUI")]
    [SerializeField] private Text azaMP;
    [SerializeField] private Slider azaMPBar;
    [Space]
    [Header("Abilities")]
    [SerializeField] private Text attack;
    [SerializeField] private Text defense;
    [SerializeField] private Text intelligence;
    [SerializeField] private Text healthAb;
    [SerializeField] private Text staminaAb;
    [SerializeField] private Text mpBoost;
    [SerializeField] private Text attackBoost;
    [SerializeField] private Text defenseBoost;
    [SerializeField] private Text healthBoost;
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject skillMenu;
    [Space]
    [Header("EventSystems")]
    [SerializeField] private GameObject mainEventSystem;
    [SerializeField] private GameObject mainMenuEventSystem;
    [Space]
    [Header("Pocket")]
    [SerializeField] private GameObject pocket;
    [SerializeField] private GameObject pageTitle;
    [SerializeField] private GameObject pageNum;
    [SerializeField] private GameObject itemInvent;
    [Space]
    [Header("CraftingMenu")]

    [SerializeField] private GameObject craftMenuPrefab;
    private static GameObject craftMenu;
    [SerializeField] private Image itemListPrefab;
    private static Image itemList;
    [Space]

    [Header("StoreMenu")]
    [SerializeField] private GameObject StoreMenuPrefab;
    private static GameObject storeMenu;
    [SerializeField] private GameObject storeMenuDefaultButton;
    [Space]
    [Header("UseMenu")]
    [SerializeField] private GameObject useMenuPrefab;
    [SerializeField] private Button useButtonPrefab;
    [SerializeField] private Button itemDescriptionButtonPrefab;
    [SerializeField] private Button giveButtonPrefab;
    [SerializeField] private Button dropButtonPrefab;
    [SerializeField] private GameObject useMenuDefaultButton;
    private static GameObject useMenu;
    private static Button useButton;
    private static Button itemDescriptionButton;
    private static Button giveButton;
    private static Button dropButton;
    [Space]


    [Header("SkillAssignMenu")]
    [SerializeField] private GameObject skillAssignMenuPrefab;
    private static GameObject skillAssignMenu;
    [SerializeField] private GameObject skillAssignDefaultButton;
    [Space]
    [Header("Dialogue Management")]
    private static GameObject dialogueMenu;
    [SerializeField] private GameObject dialogueMenuPrefab;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Text whoseTalking;
    [Space]
    [Header("StatBuildMenu")]
    [SerializeField] private GameObject levelMenuPrefab;
    [SerializeField] private Text baseAttack;
    [SerializeField] private Text baseDefense;
    [SerializeField] private Text baseMp;
    [SerializeField] private Text baseHealth;
    [SerializeField] private Text expRequired;
    [SerializeField] private Text lvMenuExp;
    [SerializeField] private Text abilityPoints;
    [SerializeField] private Text abilityPointsCost;
    [SerializeField] private Text itemAbilityPointsCost;
    [SerializeField] private GameObject levelMenuDefaultButton;
    [Space]
    [Header("EquipmentWindow")]
    [SerializeField] private ItemSlot weaponSlot;
    [SerializeField] private ItemSlot shieldSlot;
    [SerializeField] private ItemSlot maskSlot;
    [SerializeField] private GameObject weaponInvent;
    [SerializeField] private GameObject shieldInvent;
    [SerializeField] private GameObject maskInvent;
    [SerializeField] private GameObject equipWindowDefaultButton;
    [Space]
    [Header("PauseMenu")]
    [SerializeField] private GameObject invent;
    [SerializeField] private GameObject comboMenu;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject objectiveMenu;
    [SerializeField] private GameObject statusWindow;

    [Space]
    [Header("Videos")]
    [SerializeField] private VideoClip combo1;
    [SerializeField] private VideoClip combo2;
    [SerializeField] private VideoClip combo3;
    [SerializeField] private VideoClip combo4;
    private int page;
    
    [Space]
    [Header("Objective menu")]
    [SerializeField] private Text descriptionBox;
    [SerializeField] private GameObject missionListing;
    [SerializeField] private Button activeQuest;
    [SerializeField] private Button completedQuest;
    private List<Objective> objectives = new List<Objective>();

    [Header("Options")]
    [SerializeField] private GameObject optDefaultButton;
    [Header("Pop Up Windows")]
    [SerializeField] private GameObject newObjectiveWindow;
    [SerializeField] private GameObject objectiveClear;
    [SerializeField] private GameObject objectiveUpdated;
    [Header("Fonts")]
    [SerializeField] private Font luckiestGuy;
    private static UiManager instance;

    StoreManager store = new StoreManager();

    //Events
    public static UnityAction notCrafting;
    [SerializeField] private GameObject defaultObject;
    [SerializeField] private GameObject inventDefaultButton;

    public static GameObject UseMenu { get => useMenu; set => useMenu = value; }
    public static Button UseButton { get => useButton; set => useButton = value; }
    public static Button ItemDescriptionButton { get => itemDescriptionButton; set => itemDescriptionButton = value; }
    public static Button GiveButton { get => giveButton; set => giveButton = value; }
    public static Button DropButton { get => dropButton; set => dropButton = value; }
    public static GameObject CraftMenu { get => craftMenu; set => craftMenu = value; }
    public static Image ItemList { get => itemList; set => itemList = value; }
    public static GameObject StoreMenu { get => storeMenu; set => storeMenu = value; }
    public static GameObject SkillAssignMenu { get => skillAssignMenu; set => skillAssignMenu = value; }
    public Image Black { get => black; set => black = value; }
    public Text DialogueText { get => dialogueText; set => dialogueText = value; }
    public static GameObject DialogueMenu { get => dialogueMenu; set => dialogueMenu = value; }
    public GameObject DefaultObject { get => defaultObject; set => defaultObject = value; }
    public int Page { get => page; set { page = Mathf.Clamp(value, 0, 4); PageControl(); GetSelected(); } }

    public GameObject Invent { get => invent; set => invent = value; }
    public GameObject ShieldInvent { get => shieldInvent; set => shieldInvent = value; }
    public GameObject WeaponInvent { get => weaponInvent; set => weaponInvent = value; }
    public GameObject MaskInvent { get => maskInvent; set => maskInvent = value; }
    public Text DescriptionBox { get => descriptionBox; set => descriptionBox = value; }
    public Font LuckiestGuy { get => luckiestGuy; set => luckiestGuy = value; }
    public GameObject ItemInvent { get => itemInvent; set => itemInvent = value; }



    //public static event UnityAction movementTutorialActive;
    //public static event UnityAction miniMapTutorialActive;
    //public static event UnityAction pauseTutorialActive;
    //public static event UnityAction combatTutorialActive;
    //public static GameObject GetUseMenu() => useMenu;
    public static UiManager GetUiManager() => instance;
    public void Awake()
    {
        if (instance != null && instance != this)
        {
            GameObject.DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        UseMenu = useMenuPrefab;
        UseButton = useButtonPrefab;
        GiveButton = giveButtonPrefab;
        itemDescriptionButton = itemDescriptionButtonPrefab;
        dropButton = dropButtonPrefab;
        CraftMenu = craftMenuPrefab;
        ItemList = itemListPrefab;
        storeMenu = StoreMenuPrefab;
        SkillAssignMenu = skillAssignMenuPrefab;
        dialogueMenu = dialogueMenuPrefab;
        StoreManager.itemWasBought += UpdateMoney;
        GameController.onGameWasStarted += GameScreen;
        Npc.dialogueUp += DialogueManagerUp;
        Npc.dialogueDown += DialogueManagerDown;
        ExpConverter.levelMenuUp += LevelUpMenuUp;
        Stats.onBaseStatsUpdate += UpdateBoost;
        Items.onItemClick += UseMenuHandling;
        Objective.onObjectiveClick += ObjectiveDescription;
    }
    void Start()
    {

        Stats.onLevelUp += StatsUpdate;
        Stats.onShowingStats += ViewStats;
        Stats.onMPLeft += MPChange;
        Stats.onHealthChange += HealthChange;
        Enemy.onAnyEnemyDead += EnemyDeath;
        weaponSlot.GetComponent<Button>().onClick.AddListener(WeaponInventUp);
        shieldSlot.GetComponent<Button>().onClick.AddListener(ShieldInventUp);
        maskSlot.GetComponent<Button>().onClick.AddListener(MaskInventUp);
        //Cursor.lockState = CursorLockMode.Locked;
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;

    }

    void OnDisable()
    {

        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            GetSelected();
        }
        //SetCanvas();
    }
    private void PauseGame() {
        Player.GetPlayer().Pause = true;
    }
    public void FuckU() {

        Debug.Log("fucks i give :0");

    }
    private void GameScreen() {
        StartCoroutine(WaitCoroutine());

    }
    private void ObjectiveMenuHandling() {
        if (missionListing.transform.childCount>0)
        {
            defaultObject = missionListing.transform.GetChild(0).gameObject;
            missionListing.transform.GetChild(0).GetComponent<Objective>().IconClick();
            Debug.Log("this should work???");
        }
        else {

            defaultObject = null;
        }
    }
    private void InventWindowHandling()
    {
        if (itemInvent.transform.childCount>0)
        {
            defaultObject = itemInvent.transform.GetChild(0).gameObject;

            
        }
        else {
            defaultObject = null;
        }
    }
    private void ObjectiveDescription(string objective) {
        DescriptionBox.text = objective;

    }
    private void UseMenuHandling() {

        defaultObject = useButton.gameObject;
        GetSelected();
    }
    private void GetSelected() { EventSystem.current.SetSelectedGameObject(DefaultObject); }
    private void PageControl() {
        ClearMenus();
        switch (page) {
            case 0:
                
                Invent.SetActive(true);

                InventWindowHandling();
                
                break;

            case 1:
                
                comboMenu.SetActive(true);
                break;
            case 2:

                
                objectiveMenu.SetActive(true);
                ObjectiveMenuHandling();
                break;
            
            case 3:
                
                statusWindow.SetActive(true);
                defaultObject = equipWindowDefaultButton;
                break;
            case 4:
                
                options.SetActive(true);
                defaultObject = optDefaultButton;
                break;

        }
        GetSelected();
    }
    private void ClearMenus() {
        Invent.SetActive(false);
        statusWindow.SetActive(false);
        comboMenu.SetActive(false);
        options.SetActive(false);
        objectiveMenu.SetActive(false);
    }
    private void LevelUpMenuUp()
    {
        if (!levelMenuPrefab.activeSelf)
        {
            levelMenuPrefab.SetActive(true);
            PauseGame();
        }
        ViewStatsUpWindow();
        
        DefaultObject = levelMenuDefaultButton;
        //EventSystem.current.SetSelectedGameObject(DefaultObject);
    }
    public void MenusDown() {
        pauseMenu.SetActive(false);
        levelMenuPrefab.SetActive(false);
        storeMenu.SetActive(false);
    }

    public void SkillMenuUp()
    {
        abilities.SetActive(false);
        abilityClose.SetActive(false);
        skillMenu.SetActive(true);
        playerUi.SetActive(false);
        miniMap.SetActive(false);
    }
    public void SkillMenuOff()
    {
        skillMenu.SetActive(false);
        Player.GetPlayer().Pause = false;
        playerUi.SetActive(true);
        miniMap.SetActive(true);
        //abilities.SetActive(true);
    }
    
    private void SetCanvas() {
        if (SceneManager.GetSceneByBuildIndex(1).isLoaded)
        {
            mainMenuCanvas.SetActive(true);
            mainMenuEventSystem.SetActive(true);
            mainEventSystem.SetActive(false);
            mainCanvas.SetActive(false);
        }
        else
        {
            mainCanvas.SetActive(true);
            mainMenuCanvas.SetActive(false);
            mainMenuEventSystem.SetActive(false);
            mainEventSystem.SetActive(true);
        }

    }


    #region Tutorial Logic
    public void MoveTutor()
    {
        Clear();
        movementTutorial.SetActive(true);
    }
    public void MiniMapTutor()
    {
        Clear();
        miniMapTutorial.SetActive(true);
    }
    public void PauseTutor()
    {
        Clear();
        pauseTutorial.SetActive(true);
    }
    public void CombatTutor()
    {
        Clear();
        combatTutorial.SetActive(true);
    }
    public void TutorMenu()
    {
        Clear();
        backButton.SetActive(false);
        tutorMenu.SetActive(true);
    }
    
    public void MainMenu()
    {
        Clear();
        backButton.SetActive(false);
        background.SetActive(false);
    }
    private void Clear()
    {
        backButton.SetActive(true);
        background.SetActive(true);
        tutorMenu.SetActive(false);
        movementTutorial.SetActive(false);
        miniMapTutorial.SetActive(false);
        pauseTutorial.SetActive(false);
        combatTutorial.SetActive(false);
    }
    public void CloseTheStore()
    {

        storeMenu.SetActive(false);
        Player.GetPlayer().MoveSpeed = 6;

    }
    public void CloseCraftMenu()
    {

        craftMenu.SetActive(false);
        if (notCrafting != null)
        {
            notCrafting();

        }

    }
    public void ClearSkillTutorials()
    {

        SkillMenuUp();
        fireBallTutorial.SetActive(false);
        flameTornadoTutorial.SetActive(false);
        HeavySwingTutorial.SetActive(false);
    }
    public void FireBallTutorialUp()
    {
        fireBallTutorial.SetActive(true);
        skillMenu.SetActive(false);
    }
    public void FlameTornadoTutorialUp()
    {
        flameTornadoTutorial.SetActive(true);
        skillMenu.SetActive(false);
    }
    public void HeavySwingTutorialUp()
    {
        HeavySwingTutorial.SetActive(true);
        skillMenu.SetActive(false);
    }
    #endregion
    #region UI Updates
    private void DialogueManagerUp()
    {
        if (!dialogueMenu.activeSelf)
        {
            dialogueMenu.SetActive(true);

        }

    }
    private void DialogueManagerDown()
    {
        dialogueMenu.SetActive(false);
    }
    private void StatsUpdate()
    {
        health.text = "Hp: " + Player.GetPlayer().stats.HealthLeft + "/" + Player.GetPlayer().stats.Health;
        stamina.text = "Mp: " + Player.GetPlayer().stats.MP;
        exp.text = "Spirits: " + Player.GetPlayer().stats.Exp;
        expBar.value = Player.GetPlayer().stats.Exp;
        money.text = "Munn: " + Player.GetPlayer().Money.ToString();
        healthBar.value = Player.GetPlayer().stats.HealthLeft;
        healthBar.maxValue = Player.GetPlayer().stats.Health;
        staminaBar.maxValue = Player.GetPlayer().stats.MP;
        staminaBar.value = Player.GetPlayer().stats.MPLeft;
        expBar.maxValue = Player.GetPlayer().stats.CalculateExpNeed();

        level.text = "LV. " + Player.GetPlayer().stats.Level;
    }
    private void UpdateMoney()
    {
        money.text = "Munn: " + Player.GetPlayer().Money.ToString();


    }
    private void ViewStats()
    {
        attack.text = "Attack = " + Player.GetPlayer().stats.Attack.ToString();
        defense.text = "Defense = " + Player.GetPlayer().stats.Defense.ToString();
        healthAb.text = "Health = " + Player.GetPlayer().stats.Health.ToString();
        staminaAb.text = "Mp = " + Player.GetPlayer().stats.MPLeft.ToString();
        intelligence.text = "Intellect = " + Player.GetPlayer().stats.Intellect.ToString();
    }

    private void HealthChange()
    {
        health.text = "Hp: " + Player.GetPlayer().stats.HealthLeft + "/" + Player.GetPlayer().stats.Health;
        healthBar.value = Player.GetPlayer().stats.HealthLeft;
        healthBar.maxValue = Player.GetPlayer().stats.Health;


    }
    private void MPChange()
    {
        staminaBar.maxValue = Player.GetPlayer().stats.MP;
        staminaBar.value = Player.GetPlayer().stats.MPLeft;
        stamina.text = "Mp: " + Player.GetPlayer().stats.MPLeft;
        /*azaMP.text="Mp: " + AzaAi.GetAza().stats.MPLeft;
        azaMPBar.maxValue = AzaAi.GetAza().stats.MP;
        azaMPBar.value = AzaAi.GetAza().stats.MPLeft;*/
    }
    private void EnemyDeath()
    {
        exp.text = "Spirits: " + Player.GetPlayer().stats.Exp;
        expBar.value = Player.GetPlayer().stats.Exp;
    }
    
    
    public void SoulsToAbility()
    {
        if (Player.GetPlayer().stats.Exp > Player.GetPlayer().stats.RequiredExp)
        {
            Player.GetPlayer().stats.Abilitypoints++;
            Player.GetPlayer().stats.Exp -= Player.GetPlayer().stats.RequiredExp;
            Player.GetPlayer().stats.RequiredExp = (int)(Player.GetPlayer().stats.RequiredExp*1.2f);
            lvMenuExp.text = "Spirits :" + Player.GetPlayer().stats.Exp;
            abilityPointsCost.text = "Cost :" + Player.GetPlayer().stats.RequiredExp;
        }
        
    }

    public void AddAttack()
    {
        if (Player.GetPlayer().stats.Abilitypoints > 0) {
            Player.GetPlayer().stats.AttackBoost++;
            Player.GetPlayer().stats.Abilitypoints--;


        }
        
    }

    public void AddDefense()
    {
        if (Player.GetPlayer().stats.Abilitypoints > 0)
        {
            Player.GetPlayer().stats.DefenseBoost++;
            Player.GetPlayer().stats.Abilitypoints--;


        }
    }

    public void AddMp()
    {
        if (Player.GetPlayer().stats.Abilitypoints > 0)
        {
            Player.GetPlayer().stats.MpBoost++;
            Player.GetPlayer().stats.Abilitypoints--;


        }
    }

    public void AddHealth()
    {
        if (Player.GetPlayer().stats.Abilitypoints > 0)
        {
            Player.GetPlayer().stats.HealthBoost++;
            Player.GetPlayer().stats.Abilitypoints--;


        }
    }

    public void MinusAttack()
    {
        if (Player.GetPlayer().stats.AttackBoost > 0) {
            Player.GetPlayer().stats.Abilitypoints++;
        }
        Player.GetPlayer().stats.AttackBoost--;
    }

    public void MinusDefense()
    {
        if (Player.GetPlayer().stats.DefenseBoost > 0)
        {
            Player.GetPlayer().stats.Abilitypoints++;
        }
        Player.GetPlayer().stats.DefenseBoost--;
    }

    public void MinusMp()
    {
        if (Player.GetPlayer().stats.MpBoost > 0)
        {
            Player.GetPlayer().stats.Abilitypoints++;
        }
        Player.GetPlayer().stats.MpBoost--;
    }

    public void MinusHealth()
    {
        if (Player.GetPlayer().stats.HealthBoost > 0)
        {
            Player.GetPlayer().stats.Abilitypoints++;
        }
        Player.GetPlayer().stats.HealthBoost--;
    }

    private void UpdateBoost()
    {
        attackBoost.text = Player.GetPlayer().stats.AttackBoost.ToString();
        defenseBoost.text = Player.GetPlayer().stats.DefenseBoost.ToString();
        mpBoost.text = Player.GetPlayer().stats.MpBoost.ToString();
        healthBoost.text = Player.GetPlayer().stats.HealthBoost.ToString();
        abilityPoints.text = "Ability Points: "+Player.GetPlayer().stats.Abilitypoints.ToString();
    }
    private void ViewStatsUpWindow()
    {
        baseAttack.text = "Attack = " + Player.GetPlayer().stats.BaseAttack.ToString();
        baseDefense.text = "Defense = " + Player.GetPlayer().stats.BaseDefense.ToString();
        baseHealth.text = "Health = " + Player.GetPlayer().stats.BaseHealth.ToString();
        baseMp.text = "Mp = " + Player.GetPlayer().stats.BaseMp.ToString();
        lvMenuExp.text = "Souls: " + Player.GetPlayer().stats.Exp;


    }
    #endregion
    private void EquipmentInventUp(ItemSlot.ItemSlotType type) {

        switch (type)
        {
            case ItemSlot.ItemSlotType.Weapon:

                break;
            case ItemSlot.ItemSlotType.Shield:

                break;
            case ItemSlot.ItemSlotType.Mask:

                break;
        }
    }
    public void WeaponInventUp() {

        WeaponInvent.SetActive(true);
    }
    public void ShieldInventUp()
    {
        ShieldInvent.SetActive(true);
    }
    public void MaskInventUp()
    {
        MaskInvent.SetActive(true);
    }
    private void ClearInvents() {
        WeaponInvent.SetActive(false);
        ShieldInvent.SetActive(false);
        MaskInvent.SetActive(false);
    }
    private IEnumerator WaitCoroutine() {
        YieldInstruction wait = new WaitForSeconds(0.4f);
        yield return wait;
        

    }
    private IEnumerator WindowFade(GameObject window)
    {
        YieldInstruction wait = new WaitForSeconds(1.4f);
        yield return wait;
        window.SetActive(false);

    }
    public void ObjectUpdate() {
        
        objectiveUpdated.SetActive(true);
        StartCoroutine(WindowFade(objectiveUpdated));
        Debug.Log("object update");
    }
    public void ObjectiveClear() {
        
        objectiveClear.SetActive(true);
        StartCoroutine(WindowFade(objectiveClear));
        Debug.Log("object cleared");
    }
    private void AreYouWorking(GameObject b) {

        //Debug.Log(b == null ? "null" : b.name + ": activeSelf = " + b.activeSelf + ", activeInHierarchy" + b.activeInHierarchy);

    }
    public void AddObjective(Objective o) {

        objectives.Add(o);
        Instantiate(o,missionListing.transform);
        newObjectiveWindow.SetActive(true);
        StartCoroutine(WindowFade(newObjectiveWindow));
    }

    
    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        SetCanvas();
        AreYouWorking(mainMenuCanvas);
        AreYouWorking(mainCanvas);
        AreYouWorking(pauseMenu);
    }
}
