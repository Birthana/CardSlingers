using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUI : MonoBehaviour
{
    public CardDisplay cardPrefab;
    private MageHand playerHand;
    private List<CardDisplay> hand = new List<CardDisplay>();
    private int previousIndex = 0;
    
    [SerializeField]
    private float CARD_SPACING = 5f;
    [SerializeField]
    private int verticalOffset = 1;

    private void Awake()
    {
        playerHand = GetComponent<MageHand>();
        playerHand.OnAddCard += CreateCard;
        playerHand.OnRemoveCard += RemoveCard;
        playerHand.OnHandChange += DisplayHand;
        playerHand.OnCurrentIndexChange += DisplayCurrentCard;
    }

    public void CreateCard(Card cardInfo)
    {
        CardDisplay newCard = Instantiate(cardPrefab, transform);
        newCard.card = cardInfo;
        newCard.DisplayText();
        hand.Add(newCard);
    }

    public void DisplayHand(List<Card> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            float transformAmount = ((float)i) - ((float)hand.Count - 1) / 2;
            float angle = transformAmount * 3.0f;
            Vector3 position = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, 0) * CARD_SPACING;
            hand[i].transform.localPosition = position;
        }
    }

    public void DisplayCurrentCard(int currentIndex)
    {
        if (hand.Count == 0)
            return;
        hand[previousIndex].transform.localPosition = new Vector3(hand[previousIndex].transform.localPosition.x, 0, 0);
        hand[currentIndex].transform.localPosition += new Vector3(0, verticalOffset, 0);
        previousIndex = currentIndex;
    }

    public void RemoveCard(int index)
    {
        CardDisplay cardToRemove = hand[index];
        hand.RemoveAt(index);
        DestroyImmediate(cardToRemove.gameObject);
        previousIndex = 0;
    }
}
