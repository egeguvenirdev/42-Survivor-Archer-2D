using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class PlayerSkillCardBase : MonoBehaviour
{
    [SerializeField] private SkillInfos skillInfos;
    [SerializeField] protected Button button;
    [SerializeField] protected TMP_Text coolDownText;
    [SerializeField] protected TMP_Text skillName;
    [SerializeField] protected Image buttonImage;
    protected float currentCooldown;
    protected float cooldown;
    protected KeyCode keyCode;

    protected bool mobileCheck;

    public virtual void Init(bool isMobileDevice)
    {
        SetProperties();
        ActionManager.Updater += OnUpdate;
        currentCooldown = 0;
        mobileCheck = isMobileDevice;
    }

    public virtual void DeInit()
    {

        SetProperties();
        ActionManager.Updater -= OnUpdate;
    }

    protected abstract void OnUpdate(float deltaTime);

    private void SetProperties()
    {
        SkillInfos.SkillPref skillStats = skillInfos.GetSkillPrefs;

        if (mobileCheck) skillName.text = "" + skillStats.skillName;
        else skillName.text = "" + skillStats.skillName + " " + skillStats.skillButtonName;
        cooldown = skillStats.coolDown;
        buttonImage.sprite = skillStats.image;
        keyCode = skillStats.keyCode;
    }
}
