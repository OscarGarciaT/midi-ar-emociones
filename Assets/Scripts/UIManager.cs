using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // GameUI
    [SerializeField] GameObject GameUI;
    [SerializeField] Button ThrowBallButton;

    // ItemsMenuUI
    [SerializeField] GameObject ItemsMenuUI;

    // StartGameUI
    [SerializeField] GameObject StartGameUI;

    // MainMenuUI
    [SerializeField] GameObject MainMenuUI;

    private void OnEnable()
    {
        GameManager.Instance.OnGameUI += EnableGameUI;
        GameManager.Instance.OnItemsMenu += EnableItemsMenuUI;
        GameManager.Instance.OnGameMenu += EnableMainMenuUI;
        GameManager.Instance.OnGameInitialized += EnableStartGameUI;

        GameManager.Instance.OnThrowObjectChange += UpdateThrowBallButton;
    }

    public void UpdateThrowBallButton(EmpathyObjectSO empathyObjectSO)
    {
        ThrowBallButton.transform.GetChild(0).GetComponent<Image>().sprite = empathyObjectSO.objectSprite;
    }


    public void EnableGameUI()
    {
        GameUI.SetActive(true);
        ItemsMenuUI.SetActive(false);
        StartGameUI.SetActive(false);
        MainMenuUI.SetActive(false);
    }

    public void EnableItemsMenuUI()
    {
        GameUI.SetActive(false);
        ItemsMenuUI.SetActive(true);
        StartGameUI.SetActive(false);
        MainMenuUI.SetActive(false);
    }

    public void EnableMainMenuUI()
    {
        GameUI.SetActive(false);
        ItemsMenuUI.SetActive(false);
        StartGameUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }

    public void EnableStartGameUI()
    {
        GameUI.SetActive(false);
        ItemsMenuUI.SetActive(false);
        StartGameUI.SetActive(true);
        MainMenuUI.SetActive(false);
    }
}
