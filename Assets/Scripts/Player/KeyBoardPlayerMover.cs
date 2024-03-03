using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardPlayerMover : MonoBehaviour
{
    [Header("Movement Properties")]
    [SerializeField] private float bodySpeed = 5;
    [SerializeField] private float clampValueXpos = 4.8f;
    [SerializeField] private float clampValueYpos = 4.8f;
    [SerializeField] private Transform character;
    [SerializeField] private SimpleAnimancer animancer;

    [Header("Anim Clips")]
    [SerializeField] private AnimationClip run;
    [SerializeField] private AnimationClip idle;

    public void Init()
    {
        ActionManager.Updater += OnUpdate;
    }

    public void DeInit()
    {
        ActionManager.Updater -= OnUpdate;
    }

    private void OnUpdate(float deltaTime)
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 tempVect = new Vector3(h, v, 0);
        if (tempVect != Vector3.zero) OnMoveInput(true);
        else OnMoveInput(false);

        tempVect = tempVect.normalized * bodySpeed * deltaTime;
        character.transform.localPosition += tempVect;

        Vector3 pos = character.localPosition;
        pos.x = Mathf.Clamp(pos.x, -clampValueXpos, clampValueXpos);
        pos.z = 0;
        pos.y = Mathf.Clamp(pos.y, -clampValueYpos, clampValueYpos);

        if (tempVect.x < 0) character.localScale = new Vector3(-1, 1, 1);
        else character.localScale = Vector3.one;

        character.localPosition = pos;
        return;

    }

    private void OnMoveInput(bool check)
    {
        if (check) PlayAnimation(run);
        else PlayAnimation(idle);
    }


    public void PlayAnimation(AnimationClip anim)
    {
        animancer.PlayAnimation(anim);
    }
}
