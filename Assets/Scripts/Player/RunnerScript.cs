using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using DG.Tweening;

public class RunnerScript : MonoBehaviour
{
    [Header("Scripts and Transforms")]
    [SerializeField] private Transform model;
    [SerializeField] private Transform localMoverTarget;
    [SerializeField] private SimpleAnimancer animancer;
    [SerializeField] private PlayerSwerve playerSwerve;

    [Header("Path Settings")]
    [SerializeField] private float clampLocalX = 2f;

    [Header("Run Settings")]
    [SerializeField] private float runSpeed = 2;
    [SerializeField] private float localTargetSwipeSpeed = 2f;
    [SerializeField] private float characterSwipeLerpSpeed = 2f;
    [SerializeField] private float characterRotateLerpSpeed = 2f;
    [SerializeField] private bool canFollow = true;

    private Vector3 oldPosition;
    private bool canSwerve = false;

    public float RunSpeed
    {
        set => runSpeed = value;
    }

    public void Init()
    {
        ActionManager.SwerveValue += PlayerSwipe_OnSwerve;
        ActionManager.Updater += OnUpdate;

        StartToRun(true);
    }

    public void DeInit()
    {
        ActionManager.SwerveValue -= PlayerSwipe_OnSwerve;
        ActionManager.Updater -= OnUpdate;
        StartToRun(false);
    }

    private void OnUpdate(float deltaTime)
    {
        //UpdatePath();
        FollowLocalMoverTarget(deltaTime);
        oldPosition = model.localPosition;
    }

    private void StartToRun(bool checkRun)
    {
        if (checkRun)
        {
            playerSwerve.Init();
            canSwerve = true;
        }
        else
        {
            playerSwerve.DeInit();
            canSwerve = false;
        }
    }

    private void PlayerSwipe_OnSwerve(Vector2 direction)
    {
        if (!canSwerve) return;
        Vector2 swipeValue = direction * localTargetSwipeSpeed * Time.deltaTime;
        localMoverTarget.localPosition += new Vector3(swipeValue.x, swipeValue.y, 0);
        ClampLocalPosition();
    }

    void ClampLocalPosition()
    {
        Vector3 pos = localMoverTarget.localPosition;
        pos.x = Mathf.Clamp(pos.x, -clampLocalX, clampLocalX);
        localMoverTarget.localPosition = pos;
    }

    void FollowLocalMoverTarget(float deltaTime)
    {
        //follower character
        if (canFollow)
        {
            Vector3 direction = localMoverTarget.localPosition - oldPosition;
            model.transform.forward = Vector3.Lerp(model.transform.forward, direction, characterRotateLerpSpeed * deltaTime);
        }
    }

    public void PlayAnimation(AnimationClip anim)
    {
        animancer.PlayAnimation(anim);
    }

    public void PlayAnimationWithSpeed(AnimationClip anim, float speed)
    {
        animancer.PlayAnimation(anim);
        animancer.SetStateSpeed(speed);
    }
}
