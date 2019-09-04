﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using UnityEngine.SceneManagement;
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
    [Header("Abilities")]
    [SerializeField] private Text attack;
    [SerializeField] private Text defense;
    [SerializeField] private Text intelligence;
    [SerializeField] private Text healthAb;
    [SerializeField] private Text staminaAb;
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
    [Space]
    [Header("UseMenu")]
    [SerializeField] private GameObject useMenuPrefab;
    [SerializeField] private Button useButtonPrefab;
    [SerializeField] private Button itemDescriptionButtonPrefab;
    [SerializeField] private Button giveButtonPrefab;
    [SerializeField] private Button dropButtonPrefab;
    
    private static GameObject useMenu;
    private static Button useButton;
    private static Button itemDescriptionButton;
    private static Button giveButton;
    private static Button dropButton;
    private static UiManager instance;

    StoreManager store = new StoreManager();
    //Events
    public static UnityAction notCrafting;

    public static GameObject UseMenu { get => useMenu; set => useMenu = value; }
    public static Button UseButton { get => useButton; set => useButton = value; }
    public static Button ItemDescriptionButton { get => itemDescriptionButton; set => itemDescriptionButton = value; }
    public static Button GiveButton { get => giveButton; set => giveButton = value; }
    public static Button DropButton { get => dropButton; set => dropButton = value; }
    public static GameObject CraftMenu { get => craftMenu; set => craftMenu = value; }
    public static Image ItemList { get => itemList; set => itemList = value; }
    public static GameObject StoreMenu { get => storeMenu; set => storeMenu = value; }


    //public static event UnityAction movementTutorialActive;
    //public static event UnityAction miniMapTutorialActive;
    //public static event UnityAction pauseTutorialActive;
    //public static event UnityAction combatTutorialActive;
    //public static GameObject GetUseMenu() => useMenu;
    public static UiManager GetUiManager() => instance.GetComponent<UiManager>();
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
    }
    void Start()
    {
        Player.GetPlayer().items.Pocket = pocket;
        Player.GetPlayer().items.PageTitle = pageTitle;
        Player.GetPlayer().items.PageNum = pageNum;
        Stats.onLevelUp += StatsUpdate;
        Stats.onShowingStats += ViewStats;
        Stats.onStaminaChange += StaminaChange;
        Stats.onHealthChange += HealthChange;
        Enemy.onAnyEnemyDead += EnemyDeath;

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

    }
    public void SkillMenuUp() {
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
    public void CloseCraftMenu()
    {

        craftMenu.SetActive(false);
        if (notCrafting != null)
        {
            notCrafting();

        }

    }
    public void ClearSkillTutorials() {

        SkillMenuUp();
        fireBallTutorial.SetActive(false);
        flameTornadoTutorial.SetActive(false);
        HeavySwingTutorial.SetActive(false);
    }
    public void FireBallTutorialUp() {
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

    void StatsUpdate()
    {
        health.text = "Hp: " + Player.GetPlayer().stats.HealthLeft + "/" + Player.GetPlayer().stats.Health;
        stamina.text = "Sta: " + Player.GetPlayer().stats.Stamina;
        exp.text = "Exp: " + Player.GetPlayer().stats.Exp;
        expBar.value = Player.GetPlayer().stats.Exp;
        money.text = store.Money.ToString();
        healthBar.value = Player.GetPlayer().stats.HealthLeft;
        healthBar.maxValue = Player.GetPlayer().stats.Health;
        staminaBar.maxValue = Player.GetPlayer().stats.Stamina;
        staminaBar.value = Player.GetPlayer().stats.StaminaLeft;
        expBar.maxValue = Player.GetPlayer().stats.CalculateExpNeed();

        level.text = "LV. " + Player.GetPlayer().stats.Level;
    }
    void ViewStats()
    {
        attack.text = "Attack = " + Player.GetPlayer().stats.Attack.ToString();
        defense.text = "Defense = " + Player.GetPlayer().stats.Defense.ToString();
        healthAb.text = "Health = " + Player.GetPlayer().stats.Health.ToString();
        staminaAb.text = "Stamina = " + Player.GetPlayer().stats.StaminaLeft.ToString();
        intelligence.text = "Intellect = " + Player.GetPlayer().stats.Intellect.ToString();
    }
    void HealthChange()
    {
        health.text = "Hp: " + Player.GetPlayer().stats.HealthLeft + "/" + Player.GetPlayer().stats.Health;
        healthBar.value = Player.GetPlayer().stats.HealthLeft;
        healthBar.maxValue = Player.GetPlayer().stats.Health;


    }
    void StaminaChange()
    {
        staminaBar.maxValue = Player.GetPlayer().stats.Stamina;
        staminaBar.value = Player.GetPlayer().stats.StaminaLeft;
        stamina.text = "Sta: " + Player.GetPlayer().stats.StaminaLeft;
    }
    void EnemyDeath()
    {

        exp.text = "Exp: " + Player.GetPlayer().stats.Exp;
        expBar.value = Player.GetPlayer().stats.Exp;
    }
    void SetCanvas()
    {
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
    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        SetCanvas();
    }
}
