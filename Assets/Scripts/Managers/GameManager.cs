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
    private SoundManager soundManager;

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
        soundManager = FindObjectOfType<SoundManager>();

        SetInits();
    }

    private void SetInits()
    {
        levelManager.Init();
        uIManager.Init(inputManager.IsMobileDevice());
        moneyManager.Init();
        updateManager.Init();
        inputManager.Init();
        soundManager.Init();
    }

    private void DeInits()
    {
        levelManager.DeInit();
        uIManager.DeInit();
        moneyManager.DeInit();
        updateManager.DeInit();
        camManager.DeInit();
        pooler.DeInit();
        inputManager.DeInit();
        soundManager.DeInit();
    }

    public void OnStartTheGame()
    {
        ActionManager.GameStart?.Invoke();

        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.Init(inputManager.IsMobileDevice());

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
}
