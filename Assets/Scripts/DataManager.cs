using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private List<EmpathyObjectSO> items = new();
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private ItemButtonManager itemButtonManager;

    private void Start()
    {
        GameManager.Instance.OnItemsMenu += CreateButtons;
    }

    private void CreateButtons()
    {
        foreach (var item in items)
        {
            ItemButtonManager button;
            button = Instantiate(itemButtonManager, buttonContainer.transform);
            button.Item = item;
            button.ItemName = item.objectName;
            button.ItemImage = item.objectSprite;
            button.ItemDescription = item.objectDescription;
            button.ItemSentimiento = item.relatedBehaviorType;
            button.Ball3DPrefab = item.ball3DPrefab;
            button.name = item.objectName;


            GameManager.Instance.OnItemsMenu -= CreateButtons;
        }
    }
}
