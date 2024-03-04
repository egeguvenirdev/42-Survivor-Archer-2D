using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableBase : PoolableObjectBase
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float speed = 5f;
    [SerializeField] private CircleCollider2D col;
    private float remainingTime;
    private bool canMove;
    private float damage;

    protected VibrationManager vibration;

    public float Damage { get => damage; set => damage = value; }

    private void Start()
    {
        vibration = VibrationManager.Instance;
    }

    public override void Init()
    {
        col.enabled = true;
        canMove = true;
        remainingTime = lifeTime;
        ActionManager.Updater += OnUpdate;
    }

    public void DeInit()
    {
        col.enabled = false;
        canMove = false;
        remainingTime = 0;
        ActionManager.Updater -= OnUpdate;
        gameObject.SetActive(false);
    }

    private void OnUpdate(float deltaTime)
    {
        if (!canMove) return;

        transform.localPosition += deltaTime * speed * transform.up;
        remainingTime -= deltaTime;
        if (remainingTime <= 0) DeInit();
    }
}
