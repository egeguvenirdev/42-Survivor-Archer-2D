using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThroable : PoolableObjectBase
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float speed = 5f;
    [SerializeField] private CircleCollider2D col;
    private float remainingTime;
    private bool canMove;

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
    }

    private void OnUpdate(float deltaTime)
    {
        if (!canMove) return;

        transform.localPosition += deltaTime * speed * transform.up;
    }
}
