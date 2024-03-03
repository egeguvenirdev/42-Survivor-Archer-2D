using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class PlayerSkillBase : MonoBehaviour
{
    [SerializeField] private SkillInfos skillInfos;
    [SerializeField] private Button button;
    [SerializeField] protected TMP_Text coolDown;
    [SerializeField] protected TMP_Text skillName;
    [SerializeField] protected Image buttonImage;
    private float currentCooldown;

    public void Init()
    {
        SetProperties();
        ActionManager.Updater += OnUpdate;
        currentCooldown = 0;
    }

    public void DeInit()
    {

        SetProperties();
        ActionManager.Updater -= OnUpdate;
    }

    protected virtual void OnUpdate(float deltaTime)
    {
        if(currentCooldown > 0)
        {
            currentCooldown -= deltaTime;
            coolDown.text = "" + (int)currentCooldown;
            return;
        }
        else
        {
            coolDown.text = "Ready";
        }
    }

    private void SetProperties()
    {
        SkillInfos.SkillPref skillStats = skillInfos.GetSkillPrefs;

        coolDown.text = "" + skillStats.coolDown;
        skillName.text = skillStats.skillName;
        buttonImage.sprite = skillStats.image;
    }

    public abstract void OnButtonClick();
}
