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

    private VariableJoystick variableJoystick;

    public void Init()
    {
        variableJoystick = FindObjectOfType<VariableJoystick>();
        ActionManager.Updater += OnUpdate;
    }

    public void DeInit()
    {
        ActionManager.Updater -= OnUpdate;
    }

    private void OnUpdate(float deltaTime)
    {
        if (Input.GetMouseButton(0) && variableJoystick.Direction != Vector2.zero)
        {
            character.localPosition += new Vector3(variableJoystick.Horizontal, variableJoystick.Vertical, 0) * bodySpeed * deltaTime;
            Debug.Log("" + bodySpeed + " " + variableJoystick.Vertical);

            Vector3 pos = character.localPosition;

            pos.x = Mathf.Clamp(pos.x, -clampValueXpos, clampValueXpos);
            pos.z = 0;
            pos.y = Mathf.Clamp(pos.y, -clampValueYpos, clampValueYpos);

            character.localPosition = pos;
            return;
        }
        //animController.PlayIdleAnimation(1);
    }
}