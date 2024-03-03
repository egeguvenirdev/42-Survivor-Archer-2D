using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public void Init()
    {
        ActionManager.Updater += Thick;
    }

    public void DeInit()
    {
        ActionManager.Updater -= Thick;
    }

    private static void Thick(float deltaTime)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("e key");
            ActionManager.InputType?.Invoke(KeyCode.E);
        }
    }

    public bool IsMobileDevice()
    {
        //Check if the device running this is a handheld
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            return true;
        }

        //Check if the device running this is a desktop
        else if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            return false;
        }

        //Check if the device running this is unknown
        else
        {
            return false;
        }
    }
}
