using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkillCard : PlayerSkillCardBase
{
    private float instantiateDelay = 2f;
    private float currentInstantiateDelay;

    public override void Init(bool isMobileDevice)
    {
        base.Init(isMobileDevice);
    }

    public override void DeInit()
    {
        base.DeInit();
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (currentInstantiateDelay > 0)
        {
            currentInstantiateDelay -= deltaTime;
            return;
        }

        if (currentCooldown > 0)
        {
            currentCooldown -= deltaTime;
            coolDownText.text = "CD:" + (int)currentCooldown;
            return;
        }
        else
        {
            currentInstantiateDelay = instantiateDelay;
            button.interactable = true;
            coolDownText.text = "Ready";
            OnButtonClick();
        }
    }

    public void OnButtonClick()
    {
        currentCooldown = cooldown;
        coolDownText.text = "Activated";
        button.interactable = false;
    }
}
