using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MeleeEnemy : EnemyBase
{ 
    [SerializeField] private string[] gemNames;
    private Vector3 destination;
    private ObjectPooler objectPooler;

    protected override void MoveTowardsPlayer(Vector3 player)
    {
        if (agent == null) return;

        if (player != null && canMove) // check the player if its dead or what
        {
            destination = player;
            agent.SetDestination(destination);
            //Debug.Log(agent.remainingDistance);
        }

        if (agent.remainingDistance > 0 && agent.remainingDistance < range)
        {
            if (canMove) transform.LookAt(destination);
            if (canMove)
            {
                isRunning = false;
                animancer.PlayAnimation("Attack");
                StartCoroutine(HitRoutine());
            }
        }
        else
        {
            if (canMove && !isRunning)
            {
                animancer.PlayAnimation("Run");
                StopAllCoroutines();
                isRunning = true;
            }
        }
    }

    private IEnumerator HitRoutine()
    {
        canMove = false;
        yield return CoroutineManager.GetTime(0.833f * 0.54f, 30f);
        if (objectPooler == null) objectPooler = ObjectPooler.Instance;
        Fire();
        yield return CoroutineManager.GetTime(0.833f - (0.833f * 0.54f), 30f);
        animancer.Stop();
        canMove = true;
    }

    private void PlayParticle(string particleName)
    {
    }

    private void DropExpDiamond()
    {
        /*GameObject diamond = ObjectPooler.Instance.GetPooledObjectWithTag(gemNames[Random.Range(0, gemNames.Length)]);
        diamond.transform.position = transform.position;
        diamond.transform.rotation = Quaternion.identity;
        diamond.SetActive(true);

        diamond.transform.DOJump(new Vector3(diamond.transform.position.x, 0, diamond.transform.position.z), 2, 1, 0.3f).OnComplete(() =>
        {
            diamond.GetComponent<Collider>().enabled = true;
        });*/
    }

    private void Fire()
    {
        //
    }

    private void ResEnemy()
    {
        canMove = true;
        isRunning = false;
    }
}