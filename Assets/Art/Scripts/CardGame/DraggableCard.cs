using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private GameObject placeholder;
    private GameObject CardGame;
    private int originalIndex;

    public static List<int> cardOrder; // 存储卡牌的正确顺序
    public int cardId; // 当前卡牌的标识符或编号

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        if (cardOrder == null)
        {
            InitializeCardOrder(); // 初始化卡牌顺序
        }
    }

    private void InitializeCardOrder()
    {
        // 初始化卡牌顺序，比如 {2, 5, 3, 7, ...} 表示正确的顺序
        cardOrder = new List<int> {14,13,12,11,10,9,8,7,6,5,4,3,2};
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalIndex = transform.GetSiblingIndex();
        placeholder = new GameObject("Placeholder");
        placeholder.transform.SetParent(transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = rectTransform.rect.width;
        le.preferredHeight = rectTransform.rect.height;
        le.flexibleWidth = 100;
        le.flexibleHeight = 150;
        placeholder.transform.SetSiblingIndex(originalIndex);

        transform.SetParent(transform.parent.parent);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
        SortPlaceholder();
    }

    private void SortPlaceholder()
    {
        int newSiblingIndex = placeholder.transform.parent.childCount;
        for (int i = 0; i < placeholder.transform.parent.childCount; i++)
        {
            if (transform.position.x < placeholder.transform.parent.GetChild(i).position.x)
            {
                newSiblingIndex = i;
                break;
            }
        }
        placeholder.transform.SetSiblingIndex(newSiblingIndex);
    }

    private bool CheckIfCompleted()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            DraggableCard card = transform.parent.GetChild(i).GetComponent<DraggableCard>();
            if (card != null && DraggableCard.cardOrder[i] != card.cardId)
            {
                return false;  // 如果任何卡牌不在正确位置，返回false
            }
        }
        return true;  // 所有卡牌正确排序
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        int newIndex = placeholder.transform.GetSiblingIndex();
        transform.SetParent(placeholder.transform.parent);
        transform.SetSiblingIndex(newIndex);
        canvasGroup.blocksRaycasts = true;
        Destroy(placeholder);

        CheckCardOrder();
        if (CheckIfCompleted())
        {
            Destroy(CardGame);
        }
    }
   

    private void CheckCardOrder()
    {
        if (transform.GetSiblingIndex() == cardOrder.IndexOf(cardId))
        {
            Debug.Log("Correct placement");
        }
        else
        {
            Debug.Log("Incorrect placement");
            // Optional: Move the card back to its original position or show a hint
        }
    }
}
