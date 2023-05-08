using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static bool canStart = false, isRunning = false;
    
    public static void OnStartGame()
    {
        if (isRunning || !canStart) return;

        canStart = false;
        isRunning = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnLevelSuccess();
        }
    }

    public static void OnLevelSuccess()
    {
        isRunning = false;
        canStart = false;
        UIManager.I.OnSuccess();
        
        int nextLevelID = SaveLoadManager.GetLevel() + 1;
        Debug.Log("Next Level ID :" + nextLevelID );
        
        if (nextLevelID == 1)
        {
            SaveLoadManager.IncreaseColumnNumber();
            SaveLoadManager.IncreaseRowNumber();
        } 
        else if (nextLevelID == 3)
        {
            SaveLoadManager.IncreaseColumnNumber();
            SaveLoadManager.IncreaseRowNumber();
        } else if (nextLevelID % 7 == 1)
        {
            SaveLoadManager.IncreaseColumnNumber();
            SaveLoadManager.IncreaseRowNumber();
        }

        if (SaveLoadManager.GetLightPercentage() != 40)
        {
            SaveLoadManager.IncreaseLightPercentage();
        }
    }
    
    public static void OnLevelFailed()
    {
        isRunning = false;
        canStart = false;
        UIManager.I.OnFail();
    }
    
    public static void ReloadScene(bool isSuccess)
    {
        if (isSuccess)
        {
            SaveLoadManager.IncreaseLevel();
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("GameLightOut");
    }
}