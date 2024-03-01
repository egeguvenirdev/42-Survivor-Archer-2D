//using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] protected int level = 1;
    [SerializeField] protected EnemyInfos enemyInfos;
    [SerializeField] public EnemyType enemyType;
    //[SerializeField] [EnumFlags] private DropType dropType;

    protected float maxHealth;
    protected float range;
    protected float speed;
    protected float attackDamage;
    protected float currentHealth;

    [Header("Animation")]
    [SerializeField] protected SimpleAnimancer _animancer;
    [SerializeField] private AnimationClip[] dieClips;

    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected PlayerManager playerManager;

    protected bool canMove = true;
    protected bool isRunning = false;
    protected bool isAlive = false;

    public float setHealth
    {
        get => currentHealth;
        set
        {
            value = Mathf.Clamp(value, 0, float.MaxValue);
            currentHealth -= value;
            if (currentHealth <= 0) Die();
        }
    }

    protected void Initialized()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        //SetProperties(playerManager.PlayerLevel);

        ActionManager.AiUpdater += MoveTowardsPlayer;
        ActionManager.GameEnd += OnGameEnd;

        ResHealth();
        //
    }

    protected virtual void MoveTowardsPlayer(Vector3 player)
    {

    }

    public virtual void TakeDamage(float hitAmount)
    {
        setHealth = hitAmount;
        PlayDamageText(hitAmount);
    }

    public void Freeze(float time)
    {
        StartCoroutine(Frozen(time));
    }

    private IEnumerator Frozen(float time)
    {
        canMove = false;
        agent.isStopped = true;
        yield return new WaitForSeconds(time);
        agent.isStopped = false;
        canMove = true;
    }

    protected void PlayDamageText(float hitAmount)
    {
        if (hitAmount <= 0) return;
    }

    private void OnGameEnd(bool playerWin)
    {
        if (playerWin)
        {
            OnPlayerWin();
            return;
        }
        OnPlayerLose();
    }

    private void OnPlayerWin()
    {
        if (isAlive)
        {
            _animancer.PlayAnimation("Die");
            agent.isStopped = true;
        }
    }

    private void OnPlayerLose()
    {
        if (isAlive)
        {
            _animancer.PlayAnimation("Idle");
            agent.isStopped = true;
        }
    }

    protected virtual void Die()
    {
        ActionManager.AiUpdater -= MoveTowardsPlayer;
        ActionManager.GameEnd -= OnGameEnd;

        ResHealth();
        isAlive = false;

        //if (dropType == DropType.Nothing) return;
    }

    private void SetProperties(int playerLevel)
    {
        /*if (playerLevel > enemyInfos.GetCharacterPrefs.Length) playerLevel = enemyInfos.GetCharacterPrefs.Length;

        EnemyInfos.CharacterPref currentLevel = enemyInfos.GetCharacterPrefs[playerLevel - 1];

        maxHealth = currentLevel.maxHealth;
        speed = currentLevel.speed;
        range = currentLevel.range;
        attackDamage = currentLevel.attackDamge;*/


        var enemyConfig = EnemyConfigUtility.GetEnemyConfigByLevel((byte)level);
        maxHealth = enemyConfig.Health;
        attackDamage = enemyConfig.Power;
    }

    private void ResHealth()
    {
        isAlive = true;
        currentHealth = maxHealth;
    }
}