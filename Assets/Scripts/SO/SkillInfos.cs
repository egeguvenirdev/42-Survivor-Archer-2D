using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfos : MonoBehaviour
{
    [SerializeField] private SkillPref skillPrefs;

    public SkillPref GetSkillPrefs { get => skillPrefs; set => skillPrefs = value; }

    [System.Serializable]
    public class SkillPref
    {
        [Header("Stats")]
        public float coolDown;
        public string skillName;
        public Sprite image;
    }
}
