using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkill : SkillBase
{
    [SerializeField] private int bladeCountPerFire;
    [SerializeField] private float radius = 0.1f;
    [SerializeField] private float damage = 10f;

    private ObjectPooler pooler;

    public override void Init()
    {
        base.Init();
        pooler = ObjectPooler.Instance;
    }

    protected override void OnSkillActivated(SkillType refType)
    {
        base.OnSkillActivated(refType);
    }

    protected override void Fire()
    {
        for (int i = 0; i < bladeCountPerFire; i++)
        {
            // Distance around the circle
            var radians = 2 * Mathf.PI / bladeCountPerFire * i;

            // direction
            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);

            var spawnDir = new Vector3(horizontal, vertical, 0);
            var spawnPos = transform.position + spawnDir * radius; // Radius is just the distance away from the point

            PoolableObjectBase throwable = pooler.GetPooledObjectWithType(PoolObjectType.PlayerGravityThrowable);
            throwable.transform.position = spawnPos;

            Vector3 targetPos = (transform.position - throwable.transform.position).normalized;
            float angle = Vector3.Angle(new Vector3(targetPos.x, 0, targetPos.y), Vector3.forward);
            if (targetPos.x >= 0) angle *= -1f;
            throwable.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            throwable.gameObject.SetActive(true);
            throwable.GetComponent<ThrowableBase>().Damage = damage;
            throwable.Init();
        }
    }
}
