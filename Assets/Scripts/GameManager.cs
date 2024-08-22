using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Action OnGameMenu;
    public Action OnGameUI;
    public Action OnGameInitialized;
    public Action OnGameStarted;
    public Action OnItemsMenu;
    public Action<EmpathyObjectSO> OnThrowObjectChange;
    public Action OnEndGameUI;

    [SerializeField] private int goalCount = 1; // Customizable goal, default is 1
    private int correctEmpathyObjectCount = 0;

    public bool GameEnded = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        GameInitialized();

        AudioManager.Instance.PlayMusic("main-theme");
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

    public void IncrementCorrectEmpathyObjectCount()
    {
        correctEmpathyObjectCount++;
        if (correctEmpathyObjectCount >= goalCount)
        {
            TriggerEndGameUI();
        }
    }

    private void TriggerEndGameUI()
    {
        OnEndGameUI?.Invoke();

        GameEnded = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
