using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovementCounter : Singleton<MovementCounter>
{
    private int movesLeft;

    [SerializeField] private TextMeshProUGUI txtMoveLeft;

    private void Start()
    { 
        int moveNumber = Configs.MovementNumber.MoveCount;
        movesLeft = moveNumber + (SaveLoadManager.GetLevel() * 5);

        txtMoveLeft.text = movesLeft.ToString("00");
    }

    public void DecreaseCounter()
    {
        movesLeft--;
        txtMoveLeft.text = movesLeft.ToString("00");
        
        if (movesLeft == 0)
        {
            GameManager.OnLevelFailed();
        }
    }
}
