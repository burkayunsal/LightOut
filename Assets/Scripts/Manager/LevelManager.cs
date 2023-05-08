using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{

    private void Start()
    {
        CreateLevel();
    }
    
    public void CreateLevel()
    {

        BoardManager.I.OnGameStart(); 
        GameManager.canStart = true;
    }
    
}
