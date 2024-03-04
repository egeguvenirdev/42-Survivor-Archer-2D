using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowable : ThrowableBase
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggered");
        if (collision.transform.parent.TryGetComponent(out IDamageable damagedObj))
        {
            damagedObj.TakeDamage(Damage);
            vibration.MediumVibration();
            DeInit();
        }
    }
}
