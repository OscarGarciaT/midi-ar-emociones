using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonManager : MonoBehaviour
{
    private EmpathyObjectSO item;
    private string itemName;
    private Sprite itemImage;
    private string itemDescription;
    private EmotionBehaviorType itemBehavior;
    private GameObject ballPrefab;

    public EmpathyObjectSO Item { set => item = value; }
    public string ItemName { set => itemName = value; }
    public Sprite ItemImage { set => itemImage = value; }
    public string ItemDescription { set => itemDescription = value; }
    public EmotionBehaviorType ItemSentimiento { set => itemBehavior = value; }
    public GameObject Ball3DPrefab { set => ballPrefab = value; }

    [SerializeField] private Text itemNameText;
    [SerializeField] private Image itemImageSprite;
    [SerializeField] private Text itemDescriptionText;

    // Start is called before the first frame update
    private void Start()
    {
        itemNameText.text = itemName;
        itemImageSprite.sprite = itemImage;
        itemDescriptionText.text = itemDescription;

        var button = GetComponent<Button>();
        button.onClick.AddListener(GameManager.Instance.GameUI);
        button.onClick.AddListener(SetCurrentThrowObject);
    }

    private void SetCurrentThrowObject()
    {
        Debug.Log("Setting current throw object");
        GameManager.Instance.ThrowObjectChange(item);
    }
}