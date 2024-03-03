using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour, IDamageable
{
    [Header("Components")]
    [SerializeField] private JoystickPlayerMover runnerScript;
    [SerializeField] private Bow bow;
    [SerializeField] private Transform characterTransform;

    [Header("Props")]
    [SerializeField] private float maxHp;
    private float currentHealth;

    private GameManager gameManager;
    private VibrationManager vibration;
    private ObjectPooler pooler;

    public Transform GetCharacterTransform
    {
        get => characterTransform;
    }

    public void Init()
    {
        gameManager = GameManager.Instance;
        vibration = VibrationManager.Instance;
        pooler = ObjectPooler.Instance;
        runnerScript.Init();
        bow.Init();

        ActionManager.PlayerDamage += TakeDamage;
    }

    public void DeInit()
    {
        runnerScript.DeInit();
        bow.DeInit();

        ActionManager.PlayerDamage -= TakeDamage;
    }

    public void TakeDamage(float damage)
    {
        damage = Mathf.Clamp(damage, 0, float.MaxValue);
        //hitParticle.Play();
        currentHealth -= damage;
        SlideText hitText = pooler.GetPooledText();
        hitText.gameObject.SetActive(true);
        hitText.SetTheText("", (int)damage, Color.red, null, transform.position);
        vibration.SoftVibration();
        if (currentHealth <= 0) gameManager.FinishTheGame(false);
    }

    #region Upgrade
    public void OnUpgrade(UpgradeType type, float value)
    {
        switch (type)
        {
            case UpgradeType.Income:
                IncomeUpgrade(value);
                break;
            case UpgradeType.FireRange:
                FireRangeUpgrade(value);
                break;
            case UpgradeType.FireRate:
                FireRateUpgrade(value);
                break;
            default:
                Debug.Log("NOTHING");
                break;
        }
    }

    private void IncomeUpgrade(float value)
    {
        if (value < 1)
        {
            ActionManager.UpdateMoneyMultiplier?.Invoke(1);
            return;
        }
        ActionManager.UpdateMoneyMultiplier?.Invoke(value);
    }

    private void FireRangeUpgrade(float value)
    {

    }

    private void FireRateUpgrade(float value)
    {

    }
    #endregion
}
