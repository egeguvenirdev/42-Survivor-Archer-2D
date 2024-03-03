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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ActionManager.InputType?.Invoke(KeyCode.Q);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            ActionManager.InputType?.Invoke(KeyCode.W);
        }
    }
}
