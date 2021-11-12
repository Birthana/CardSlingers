using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageHand : MonoBehaviour
{
    public CardDisplay cardPrefab;

    public List<Card> deck = new List<Card>();
    public List<CardDisplay> hand = new List<CardDisplay>();

    public float CARD_SPACING = 5f;
    private int currentIndex = 0;

    private void Update()
    {
        CheckCastCard();
    }

    private void CheckCastCard()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hand.Count == 0)
                return;
            hand[currentIndex].card.Cast();
            RemoveHandCard(currentIndex);
        }
    }

    public void RemoveHandCard(int index)
    {
        CardDisplay selectedCard = hand[index];
        hand.RemoveAt(index);
        Destroy(selectedCard.gameObject);
        DisplayHand();
    }

    public void Draw()
    {
        Card topOfDeck = deck[0];
        deck.RemoveAt(0);
        CardDisplay newCard = Instantiate(cardPrefab, transform);
        newCard.card = topOfDeck;
        newCard.DisplayText();
        hand.Add(newCard);
        DisplayHand();
    }

    public void DisplayHand()
    {
        for (int i = 0; i < hand.Count; i++)
        {
            float transformAmount = ((float)i) - ((float)hand.Count - 1) / 2;
            float angle = transformAmount * 3.0f;
            Vector3 position = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, 0) * CARD_SPACING;
            hand[i].transform.localPosition = position;
        }
    }

    public void EmptyHand()
    {
        int count = hand.Count;
        for (int i = 0; i < count; i++)
        {
            CardDisplay card = hand[0];
            hand.RemoveAt(0);
            DestroyImmediate(card.gameObject);
        }
    }
}
