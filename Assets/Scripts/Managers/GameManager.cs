using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("PlayerPrefs")]
    [SerializeField] private bool clearPlayerPrefs;

    private PlayerManager playerManager;
    private LevelManager levelManager;
    private UpdateManager updateManager;
    private CamManager camManager;
    private MoneyManager moneyManager;
    private UIManager uIManager;
    private ObjectPooler pooler;
    private InputManager inputManager;
    private EnemySpawner enemySpawner;

    void Start()
    {
        if (clearPlayerPrefs) PlayerPrefs.DeleteAll();

        levelManager = LevelManager.Instance;
        moneyManager = MoneyManager.Instance;
        uIManager = UIManager.Instance;
        pooler = ObjectPooler.Instance;
        inputManager = new InputManager();
        updateManager = FindObjectOfType<UpdateManager>();
        camManager = FindObjectOfType<CamManager>();

        SetInits();
    }

    private void SetInits()
    {
        levelManager.Init();
        uIManager.Init(IsMobileDevice());
        moneyManager.Init();
        updateManager.Init();
        inputManager.Init();
    }

    private void DeInits()
    {
        levelManager.DeInit();
        uIManager.DeInit();
        moneyManager.DeInit();
        updateManager.DeInit();
        camManager.DeInit();
        pooler.DeInit();
        inputManager.Init();
    }

    public void OnStartTheGame()
    {
        ActionManager.GameStart?.Invoke();

        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.Init(IsMobileDevice());

        enemySpawner = FindObjectOfType<EnemySpawner>();
        enemySpawner.Init();

        camManager.Init();
    }

    public void OnLevelSucceed()
    {
        levelManager.LevelUp();
        DeInits();
        SetInits();
    }

    public void OnLevelFailed()
    {
        DeInits();
        SetInits();
    }

    public void FinishTheGame(bool check)
    {
        playerManager.DeInit();
        enemySpawner.DeInit();

        ActionManager.UpdateMoney(0f);
        ActionManager.GameEnd?.Invoke(check);

    }

    private bool IsMobileDevice()
    {
        //Check if the device running this is a handheld
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            return true;
        }

        //Check if the device running this is a desktop
        else if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            return false;
        }

        //Check if the device running this is unknown
        else
        {
            return false;
        }
    }
}
