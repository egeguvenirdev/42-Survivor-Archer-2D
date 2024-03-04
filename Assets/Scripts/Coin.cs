using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : CollectableBase
{
    [SerializeField] private float money = 100f;

    public override void Collect()
    {
        base.Collect();
        ActionManager.UpdateMoney?.Invoke(money);
        ActionManager.PlaySound?.Invoke(clip);
    }
}
