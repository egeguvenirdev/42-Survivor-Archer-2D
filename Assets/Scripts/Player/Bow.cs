using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private float shootingCooldown = 1f;
    [SerializeField] private float damage = 20f;
    [SerializeField] private Transform aimHolder;
    [SerializeField] private GameObject aim;
    private float cooldownTimer = 0f;

    private VariableJoystick bowJoystick;
    private ObjectPooler pooler;

    public void Init()
    {
        bowJoystick = UIManager.Instance.GetBowJoystick;
        pooler = ObjectPooler.Instance;
        ActionManager.Updater += OnUpdate;
        ActionManager.FireInput += OnFireInput;
        cooldownTimer = 0f;
    }

    public void DeInit()
    {
        ActionManager.Updater -= OnUpdate;
        ActionManager.FireInput -= OnFireInput;
    }

    private void OnUpdate(float deltaTime)
    {
        if (!Input.GetMouseButton(0)) return;

        if (bowJoystick.Direction != Vector2.zero)
        {
            float angle = Vector3.Angle(new Vector3(bowJoystick.Horizontal, 0, bowJoystick.Vertical), Vector3.forward);

            if (bowJoystick.Horizontal >= 0) angle *= -1f;
            aimHolder.rotation = Quaternion.Euler(0f, 0f, angle);

            if (cooldownTimer <= 0)
            {
                PoolableObjectBase throwable = pooler.GetPooledObjectWithType(PoolObjectType.PlayerThrowable);
                throwable.transform.position = transform.position;
                throwable.transform.rotation = Quaternion.Euler(0, 0, angle);
                throwable.gameObject.SetActive(true);
                throwable.GetComponent<ThrowableBase>().Damage = damage;
                throwable.Init();
                cooldownTimer = shootingCooldown;
            }
            else
            {
                cooldownTimer -= deltaTime;
                //Debug.Log(cooldownTimer);
            }
        }
        else
        {
            cooldownTimer = 0f;
        }
    }

    private void OnFireInput(bool check)
    {
        if (check) aim.SetActive(true);
        else aim.SetActive(false);
    }
}
