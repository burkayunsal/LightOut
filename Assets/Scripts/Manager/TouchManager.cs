using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TouchManager : Singleton<TouchManager>
{
    public bool isDragging = false;
    bool canPlay = false;
    public void SetPlayable(bool isPlayable) => canPlay = isPlayable;
    
    private void Start()
    {
        SetPlayable(true);
    }

    private void Update()
    {
        if(canPlay)
            HandleTouch();
    }
    
    private void HandleTouch()
    {
        if(!isDragging)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                OnDown();
            }
        }
        else
        {
            OnDrag();            
            if(Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                OnUp();
            }
        }
        
    }
    
    void OnDown()
    {
        
    }

    void OnDrag()
    {
        
    }

    void OnUp()
    {
        
    }
}
