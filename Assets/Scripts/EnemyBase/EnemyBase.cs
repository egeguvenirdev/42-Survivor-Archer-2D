using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] protected EnemyInfos enemyInfos;
    //[SerializeField] [EnumFlags] private DropType dropType;

    private float maxHealth;
    protected float range;
    private float speed;
    private float attackDamage;
    private float currentHealth;

    [Header("Animation")]
    [SerializeField] protected SimpleAnimancer animancer;
    [SerializeField] protected AnimationClip idleAnim;
    [SerializeField] protected AnimationClip runAnim;
    [SerializeField] protected NavMeshAgent agent;

    protected bool canMove = true;
    protected bool isRunning = false;

    protected ObjectPooler pooler;
    protected VibrationManager vibration;

    public float setHealth
    {
        get => currentHealth;
        set
        {
            value = Mathf.Clamp(value, 0, float.MaxValue);
            //hitParticle.Play();
            currentHealth -= value;
            SlideText hitText = pooler.GetPooledText();
            hitText.SetTheText("", (int)value, Color.red, null, transform.position);
            vibration.SoftVibration();
            if (currentHealth <= 0) DeInit();
        }
    }

    private void Start()
    {
        pooler = ObjectPooler.Instance;
        vibration = VibrationManager.Instance;
    }

    public void Init()
    {
        agent.isStopped = false;
        canMove = true;
        SetProperties();

        ActionManager.AiUpdater += MoveTowardsPlayer;
        ActionManager.GameEnd += OnGameEnd;
    }

    public void DeInit()
    {
        agent.isStopped = true;
        canMove = false;
        gameObject.SetActive(false);

        ActionManager.AiUpdater -= MoveTowardsPlayer;
        ActionManager.GameEnd -= OnGameEnd;
    }

    protected abstract void MoveTowardsPlayer(Vector3 player);

    private void OnGameEnd(bool playerWin)
    {
        StopAllCoroutines();
        animancer.PlayAnimation(idleAnim);
        agent.isStopped = true;
    }

    private void SetProperties()
    {
        EnemyInfos.CharacterPref currentLevel = enemyInfos.GetCharacterPrefs;

        maxHealth = currentLevel.maxHealth;
        speed = currentLevel.speed;
        range = currentLevel.range;
        attackDamage = currentLevel.attackDamge;
    }
}