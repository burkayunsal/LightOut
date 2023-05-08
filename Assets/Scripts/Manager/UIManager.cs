using System;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Panels")]
    [SerializeField] Panels pnl;

    private CanvasGroup activePanel = null;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        GameManager.OnStartGame();
        FadeInAndOutPanels(pnl.gameIn);
    }
    public void OnFail()
    {
        FadeInAndOutPanels(pnl.fail);
    }

    public void OnSuccess()
    {
        FadeInAndOutPanels(pnl.success);
    }

    public void ReloadScene(bool isSuccess)
    {
        GameManager.ReloadScene(isSuccess);
    }

    void FadeInAndOutPanels(CanvasGroup _in)
    {
        CanvasGroup _out = activePanel;
        activePanel = _in;

        if(_out != null)
        {
            _out.interactable = false;
            _out.blocksRaycasts = false;

            _out.DOFade(0f, 0.25f).OnComplete(() =>
            {
                _in.DOFade(1f, 0.25f).OnComplete(() =>
                {
                    _in.interactable = true;
                    _in.blocksRaycasts = true;
                });
            });
        }
        else
        {
            _in.DOFade(1f, 0.25f).OnComplete(() =>
            {
                _in.interactable = true;
                _in.blocksRaycasts = true;
            });
        }
    }

   

    [Serializable]
    public class Panels
    {
        public CanvasGroup gameIn, fail,success;
    }
    

}