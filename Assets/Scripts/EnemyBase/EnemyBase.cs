using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : PoolableObjectBase, IDamageable
{
    [Header("Properties")]
    [SerializeField] protected EnemyInfos enemyInfos;
    [SerializeField] protected Transform model;
    //[SerializeField] [EnumFlags] private DropType dropType;

    private float maxHealth;
    protected float range;
    protected float attackDamage;
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

    private void Start()
    {
        pooler = ObjectPooler.Instance;
        vibration = VibrationManager.Instance;
    }

    public override void Init()
    {
        agent.isStopped = false;
        agent.updateRotation = false;
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

    protected abstract void CheckDirection();

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
        agent.speed = currentLevel.speed;
        range = currentLevel.range;
        attackDamage = currentLevel.attackDamge;
    }

    public void TakeDamage(float damage)
    {
        damage = Mathf.Clamp(damage, 0, float.MaxValue);
        //hitParticle.Play();
        currentHealth -= damage;
        SlideText hitText = pooler.GetPooledText();
        hitText.SetTheText("", (int)damage, Color.red, null, transform.position);
        vibration.SoftVibration();
        if (currentHealth <= 0) DeInit();
    }
}