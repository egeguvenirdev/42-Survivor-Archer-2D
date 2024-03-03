using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MeeleEnemy : EnemyBase
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
        ActionManager.PlayerDamage?.Invoke(attackDamage);
        yield return new WaitForSeconds(1);
        canMove = true;
    }

    protected override void CheckDirection()
    {
        if (playerPos.x <= transform.localScale.x) model.localScale = new Vector3(-1, 1, 1);
        else model.localScale = Vector3.one;
    }
}