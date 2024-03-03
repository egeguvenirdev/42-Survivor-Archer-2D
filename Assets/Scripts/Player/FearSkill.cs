using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearSkill : SkillBase
{
    [SerializeField] private float fearDuration;

    protected override void OnSkillActivated(SkillType refType)
    {
        base.OnSkillActivated(refType);
    }

    protected override void Fire()
    {
        ActionManager.FearSkill?.Invoke(fearDuration);
    }
}
