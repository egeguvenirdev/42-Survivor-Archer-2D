using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : CollectableBase
{
    [SerializeField] private float health = 10f;

    public override void Collect()
    {
        base.Collect();
        ActionManager.PlayerDamage?.Invoke(health);
    }
}
