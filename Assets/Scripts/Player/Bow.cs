using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private float shootingCooldown = 1f;
    private float cooldownTimer = 0f;

    private VariableJoystick bowJoystick;

    public void Init()
    {
        bowJoystick = UIManager.Instance.GetBowJoystick;
        ActionManager.Updater += OnUpdate;
        cooldownTimer = 0f;
    }

    public void DeInit()
    {
        ActionManager.Updater -= OnUpdate;
    }

    private void OnUpdate(float deltaTime)
    {
        if (!Input.GetMouseButton(0)) return;

        if (bowJoystick.Direction != Vector2.zero)
        {
            if (cooldownTimer <= 0)
            {
                cooldownTimer = shootingCooldown;
            }
            else
            {
                cooldownTimer -= deltaTime;
                Debug.Log(cooldownTimer);
            }
        }
        else
        {
            cooldownTimer = 0f;
        }
    }
}
