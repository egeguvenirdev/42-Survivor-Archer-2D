using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Settings", menuName = "New Skill")]

public class SkillInfos : ScriptableObject
{
    [SerializeField] private SkillPref skillPrefs;

    public SkillPref GetSkillPrefs { get => skillPrefs; set => skillPrefs = value; }

    [System.Serializable]
    public class SkillPref
    {
        [Header("Stats")]
        public float coolDown;
        public string skillName;
        public string skillButtonName;
        public KeyCode keyCode;
        public Sprite image;
        public SkillType skillType;
    }
}
