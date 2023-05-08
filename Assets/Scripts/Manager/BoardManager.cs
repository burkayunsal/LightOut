using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BoardManager : Singleton<BoardManager>
{
    
    private int rowCount, columnCount;
    [SerializeField] private Transform cellParent;
    [SerializeField] private GridLayoutGroup grid;
    
    BoardCell[,] cells;
    private List<BoardCell> lsCells = new List<BoardCell>();

    public void OnGameStart()
    {
        rowCount = SaveLoadManager.GetRowCount();
        columnCount = SaveLoadManager.GetColumnCount();
        cells = new BoardCell[rowCount,columnCount];
        
        int cellSize = (Screen.width / (columnCount + 1));
        grid.cellSize = new Vector2(cellSize, cellSize);
        grid.constraintCount = rowCount;

        StartCoroutine(WaitForInitialize());
    }
    
    
    IEnumerator WaitForInitialize()
    {
        yield return new WaitUntil(() => PoolManager.I.IsInitialized());
        CreateBoardCells();
        SetAdjCells();
        RandomLightOn();
    }
    
    private void CreateBoardCells()
    {
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                BoardCell cell = PoolManager.I.GetObject<BoardCell>();
                cell.transform.SetParent(cellParent);
                cells[i, j] = cell;
                cell.i = i;
                cell.j = j;
                
#if UNITY_EDITOR
                cell.gameObject.name = cell.i + "," + cell.j;
#endif
                cell.isOn = false;
                lsCells.Add(cell);

            }
        }
    }
    
    private void RandomLightOn()
    {
        int percentage = SaveLoadManager.GetLightPercentage();
        int lightOnCount = (int)(lsCells.Count * (percentage / 100f));

        if (lightOnCount == 0) // for the first level
            lightOnCount++;
        
        for (int i = 0; i < lightOnCount; i++)
        {
            int rndm = Random.Range(0, lsCells.Count);
            
            if (!lsCells[rndm].isOn)
            {
                lsCells[rndm].isOn = true;
            }
            else
            {
                i--;
            }
        }
    }

    private void SetAdjCells()
    {
        for (int i = 0; i < lsCells.Count; i++)
        {
            lsCells[i].SetAdjCells();
        }
    }

    public List<BoardCell> GetAdjCells(BoardCell cell)
    {
        List<BoardCell> _lsAdjCells = new List<BoardCell>();

        int[] rowOffsets = { 0, 0, -1, 1 };
        int[] colOffsets = { -1, 1, 0, 0 };

        for (int i = 0; i < 4; i++)
        {
            int row = cell.i + rowOffsets[i];
            int col = cell.j + colOffsets[i];

            if (row >= 0 && row < rowCount && col >= 0 && col < columnCount)
            {
                _lsAdjCells.Add(cells[row, col]);
            }
        }
        return _lsAdjCells;
    }

    public bool AreAllLightsOff() 
    {
        foreach (BoardCell cell in lsCells) {
            if (cell.isOn) {
                return false;
            }
        }
        return true;
    }
}
