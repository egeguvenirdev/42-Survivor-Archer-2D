using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerMover : MonoBehaviour
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

    private VariableJoystick moveJoystick;

    public void Init()
    {
        moveJoystick = UIManager.Instance.GetMoveJoystick;
        ActionManager.Updater += OnUpdate;
        ActionManager.MoveInput += OnMoveInput;
    }

    public void DeInit()
    {
        ActionManager.Updater -= OnUpdate;
        ActionManager.MoveInput -= OnMoveInput;
    }

    private void OnUpdate(float deltaTime)
    {
        if (Input.GetMouseButton(0) && moveJoystick.Direction != Vector2.zero)
        {
            character.localPosition += new Vector3(moveJoystick.Horizontal, moveJoystick.Vertical, 0) * bodySpeed * deltaTime;

            Vector3 pos = character.localPosition;

            pos.x = Mathf.Clamp(pos.x, -clampValueXpos, clampValueXpos);
            pos.z = 0;
            pos.y = Mathf.Clamp(pos.y, -clampValueYpos, clampValueYpos);

            if (moveJoystick.Horizontal < 0) character.localScale = new Vector3(-1, 1, 1);
            else character.localScale = Vector3.one;

            character.localPosition = pos;
            return;
        }
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