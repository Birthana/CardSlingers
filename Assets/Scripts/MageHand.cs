using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageHand : MonoBehaviour
{
    public Action<Card> OnAddCard;
    public Action<int> OnRemoveCard;
    public Action<List<Card>> OnHandChange;
    public Action<int> OnCurrentIndexChange;

    public List<Card> deck = new List<Card>();
    public List<Card> hand = new List<Card>();

    [SerializeField]
    private int currentIndex = 0;

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Draw();
        }
    }

    private void Update()
    {
        CheckCastCard();
        ChangeCurrentIndex();
    }

    private void CheckCastCard()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hand.Count == 0)
                return;
            hand[currentIndex].Cast();
            RemoveHandCard(currentIndex);
        }
    }

    private void ChangeCurrentIndex()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            currentIndex =  Mathf.Min(currentIndex + 1, Mathf.Max(0, hand.Count - 1));
            OnCurrentIndexChange(currentIndex);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            currentIndex = Mathf.Max(0, currentIndex - 1);
            OnCurrentIndexChange(currentIndex);
        }
    }

    public void Draw()
    {
        if (deck.Count == 0)
            return;
        Card topOfDeck = deck[0];
        deck.RemoveAt(0);
        hand.Add(topOfDeck);
        OnAddCard(topOfDeck);
        OnHandChange(hand);
    }

    public void RemoveHandCard(int index)
    {
        hand.RemoveAt(index);
        OnRemoveCard(index);
        OnHandChange(hand);
        currentIndex = Mathf.Max(0, currentIndex - 1);
        OnCurrentIndexChange(currentIndex);
    }

    public Trigger TriggerCheck()
    {
        Trigger trigger = deck[0].trigger;   
        Draw();
        return trigger;
    }

    public void EmptyHand()
    {
        hand = new List<Card>();
        int count = hand.Count;
        for (int i = 0; i < count; i++)
        {
            OnRemoveCard(0);
        }
    }
}
