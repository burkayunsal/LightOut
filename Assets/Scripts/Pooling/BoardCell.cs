using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardCell : PoolObject
{
    public int i, j;
    private List<BoardCell> lsAdjCells = new List<BoardCell>();

    private bool _isOn;

    public bool isOn
    {
        get => _isOn;

        set
        {
            _isOn = value;
            img.color = isOn ? new Color(1f, 1f, 1f, 1f) : new Color(0.5f, 0.5f, 0.5f, 1f);
        }
    }

    
    [SerializeField] private Image img;

    public override void OnDeactivate()
    {
        gameObject.SetActive(false);
    }

    public override void OnSpawn()
    {
        img.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        gameObject.SetActive(true);
    }

    public override void OnCreated()
    {
        OnDeactivate();
    }

    public void SetAdjCells()
    {
        lsAdjCells.AddRange(BoardManager.I.GetAdjCells(this));
    }
    
    public void Clicked()
    {
        foreach (var cell in lsAdjCells)
        {
            cell.ToggleOnOff();
        }
        ToggleOnOff();

        if ( BoardManager.I.AreAllLightsOff())
        {
           GameManager.OnLevelSuccess(); 
        }

        MovementCounter.I.DecreaseCounter();

    }

    private void ToggleOnOff()
    {
        isOn = !isOn;
    }
}
