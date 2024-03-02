using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyBase
{
    private Vector3 playerPos;

    protected override void MoveTowardsPlayer(Vector3 player)
    {
        if (agent == null) return;

        if (player != null && canMove) // check the player if its dead or what
        {
            agent.SetDestination(player);
        }

        playerPos = player;
        if (agent.remainingDistance > 0 && agent.remainingDistance < range)
        {
            if (canMove)
            {
                isRunning = false;
                animancer.PlayAnimation(idleAnim);
                StartCoroutine(HitRoutine());
            }
        }
        else
        {
            if (canMove && !isRunning)
            {
                animancer.PlayAnimation(runAnim);
                StopAllCoroutines();
                isRunning = true;
            }
        }
    }

    private IEnumerator HitRoutine()
    {
        canMove = false;
        yield return new WaitForSeconds(idleAnim.length / 2);
        Fire();
        yield return new WaitForSeconds(idleAnim.length / 2);
        //if (animancer.gameObject.activeInHierarchy) animancer.Stop();
        canMove = true;
    }

    private void Fire()
    {
        PoolableObjectBase throwable = pooler.GetPooledObjectWithType(PoolObjectType.EnemyThrowable);
        throwable.transform.position = transform.position;
        throwable.gameObject.SetActive(true);
        throwable.Init(Vector3.Angle(transform.position,playerPos));
    }
}
