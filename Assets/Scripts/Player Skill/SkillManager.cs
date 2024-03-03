using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private SkillBase[] skillBase;

    public void Init()
    {
        for (int i = 0; i < skillBase.Length; i++)
        {
            skillBase[i].Init();
        }
    }

    public void DeInit()
    {
        for (int i = 0; i < skillBase.Length; i++)
        {
            skillBase[i].DeInit();
        }
    }
}
