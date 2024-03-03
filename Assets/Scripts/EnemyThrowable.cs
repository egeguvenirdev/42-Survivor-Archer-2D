using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrowable : ThrowableBase
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable damagedObj))
        {
            damagedObj.TakeDamage(Damage);
        }
    }
}
