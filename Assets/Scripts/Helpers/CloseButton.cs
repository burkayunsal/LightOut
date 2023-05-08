using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    [SerializeField] private Button btn;

    private void Start()
    {
        btn.onClick.AddListener(HandleButtonClick);
    }

    void HandleButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
