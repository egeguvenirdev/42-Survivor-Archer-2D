using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    [SerializeField] protected SkillType skillType;
    
    public virtual void Init()
    {
        ActionManager.PlayerSkillActivated += OnSkillActivated;
    }

    public virtual void DeInit()
    {
        ActionManager.PlayerSkillActivated -= OnSkillActivated;
    }

    protected virtual void OnSkillActivated(SkillType refType)
    {
        if (refType != skillType) return;
        Fire();
    }

    protected abstract void Fire();
}
