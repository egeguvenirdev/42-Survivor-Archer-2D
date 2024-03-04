using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : ButtonBase
{
    [SerializeField] private GameObject panelElements;
    private GameManager gameManager;

    public override void Init()
    {
        base.Init();

        gameManager = GameManager.Instance;
        panelElements.SetActive(true);
    }

    public override void DeInit()
    {
        panelElements.SetActive(false);
    }

    public override void OnButtonClick()
    {
        base.OnButtonClick();

        gameManager.OnStartTheGame();
        panelElements.SetActive(false);
    }
}
