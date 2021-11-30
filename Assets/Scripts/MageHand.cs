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
    public Action OnStartScrolling;
    public Func<bool> OnStopScrolling;

    public List<Card> deck = new List<Card>();
    public List<Card> hand = new List<Card>();
    public List<Card> drop = new List<Card>();

    [SerializeField]
    private int currentIndex = 0;
    private bool isScrolling = false;

    private void Start()
    {
        Shuffle();
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
            if (!hand[currentIndex].CanCast())
                return;
            hand[currentIndex].Cast();
            RemoveHandCard(currentIndex);
        }
    }

    private void ChangeCurrentIndex()
    {
        if (hand.Count == 0 & isScrolling)
        {
            isScrolling = OnStopScrolling();
        }
        if (Input.GetKeyDown(KeyCode.Q) & !isScrolling)
        {
            isScrolling = true;
            OnStartScrolling();
        }
        if (!isScrolling)
            return;
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
        if (NoCardsLeft())
            return;
        ReshuffleDropToDeck();
        Card topOfDeck = deck[0];
        deck.RemoveAt(0);
        hand.Add(topOfDeck);
        OnAddCard(topOfDeck);
        OnHandChange(hand);
    }

    public void RemoveHandCard(int index)
    {
        Card card = hand[index];
        drop.Add(card);
        hand.RemoveAt(index);
        OnRemoveCard(index);
        OnHandChange(hand);
        currentIndex = Mathf.Max(0, currentIndex - 1);
        OnCurrentIndexChange(currentIndex);
    }

    public Trigger TriggerCheck()
    {
        ReshuffleDropToDeck();
        Trigger trigger = deck[0].trigger;   
        Draw();
        return trigger;
    }

    public bool NoCardsLeft()
    {
        return deck.Count == 0 && drop.Count == 0;
    }
    public void ReshuffleDropToDeck()
    {
        if (deck.Count == 0)
        {
            deck = drop;
            //Shuffle.
            drop = new List<Card>();
        }
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

    public void Shuffle()
    {
        System.Random r = new System.Random();
        for (int n = deck.Count - 1; n > 0; n--)
        {
            int k = r.Next(n + 1);
            Card temp = deck[n];
            deck[n] = deck[k];
            deck[k] = temp;
        }
    }
}
