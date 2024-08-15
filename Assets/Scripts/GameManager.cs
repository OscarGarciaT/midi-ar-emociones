using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Action OnGameMenu;
    public Action OnGameUI;
    public Action OnGameInitialized;
    public Action OnGameStarted;
    public Action OnItemsMenu;
    public Action<EmpathyObjectSO> OnThrowObjectChange;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        GameUI();
    }

    public void GameInitialized()
    {
        OnGameInitialized?.Invoke();
    }

    public void GameStarted()
    {
        OnGameStarted?.Invoke();
    }

    public void GameMenu()
    {
        OnGameMenu?.Invoke();
    }

    public void GameUI()
    {
        OnGameUI?.Invoke();
    }

    public void ItemsMenu()
    {
        OnItemsMenu?.Invoke();
    }

    public void ThrowObjectChange(EmpathyObjectSO newObject)
    {
        OnThrowObjectChange?.Invoke(newObject);
    }
}
