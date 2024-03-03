using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearSkill : PlayerSkillBase
{
    public override void Init(bool isMobileDevice)
    {
        base.Init(isMobileDevice);
        if (!isMobileDevice) ActionManager.InputType += OnButtonClick;
    }

    public override void DeInit()
    {
        base.DeInit();
        if (!mobileCheck) ActionManager.InputType -= OnButtonClick;
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= deltaTime;
            coolDownText.text = "CD:" + (int)currentCooldown;
            return;
        }
        else
        {
            button.interactable = true;
            coolDownText.text = "Ready";
        }
    }

    public void OnButtonClick(KeyCode key)
    {
        Debug.Log("ref key: " + key + " keykode: " + keyCode);
        if (keyCode == key)
        {
            currentCooldown = cooldown;
            coolDownText.text = "Activated";
            button.interactable = false;
        }
    }

    public void OnButtonClick()
    {
        currentCooldown = cooldown;
        coolDownText.text = "Activated";
        button.interactable = false;
    }
}
