using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyBase
{
    private Vector3 playerPos;

    protected override void MoveTowardsPlayer(Vector3 player)
    {
        playerPos = player;
        CheckDirection();

        if (!canMove) return;

        agent.SetDestination(player);
        if (agent.remainingDistance > 0 && agent.remainingDistance < range)
        {
            agent.isStopped = true;
            isRunning = false;
            animancer.PlayAnimation(idleAnim);
            StartCoroutine(HitRoutine());
        }
        else
        {
            if (!isRunning)
            {
                agent.isStopped = false;
                animancer.PlayAnimation(runAnim);
                StopAllCoroutines();
                isRunning = true;
            }
        }
    }

    private IEnumerator HitRoutine()
    {
        canMove = false;
        yield return new WaitForSeconds(1);
        Fire();
        yield return new WaitForSeconds(1);
        canMove = true;
    }

    private void Fire()
    {
        PoolableObjectBase throwable = pooler.GetPooledObjectWithType(PoolObjectType.EnemyThrowable);
        throwable.transform.position = transform.position;
        throwable.transform.LookAt(playerPos);

        Vector3 targetPos = (playerPos - throwable.transform.position).normalized;
        float angle = Vector3.Angle(new Vector3(targetPos.x, 0, targetPos.y), Vector3.forward);
        if (targetPos.x >= 0) angle *= -1f;
        throwable.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        throwable.gameObject.SetActive(true);
        throwable.GetComponent<ThrowableBase>().Damage = attackDamage;
        throwable.Init();
    }

    protected override void CheckDirection()
    {
        if(playerPos.x <= transform.localScale.x) model.localScale = new Vector3(-1, 1, 1);
        else model.localScale = Vector3.one;
    }
}
