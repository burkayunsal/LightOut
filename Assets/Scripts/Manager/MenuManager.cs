using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{

    public void OpenGame(int gameNumber)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameNumber);
    }
}
