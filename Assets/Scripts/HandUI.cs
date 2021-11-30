using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUI : MonoBehaviour
{
    public GameObject handUI;
    public CardDisplay cardPrefab;
    public float slowDuration = 10f;

    private MageHand playerHand;
    private List<CardDisplay> hand = new List<CardDisplay>();
    private int previousIndex = 0;
    
    [SerializeField]
    private float CARD_SPACING = 5f;
    [SerializeField]
    private int verticalOffset = 1;

    private Coroutine fadeCoroutine;
    private Coroutine slowCoroutine;

    private void Awake()
    {
        playerHand = GetComponent<MageHand>();
        playerHand.OnAddCard += CreateCard;
        playerHand.OnRemoveCard += RemoveCard;
        playerHand.OnHandChange += DisplayHand;
        playerHand.OnCurrentIndexChange += DisplayCurrentCard;
        playerHand.OnStartScrolling += StartSlowMode;
        playerHand.OnStopScrolling += StopSlowMode;
    }

    public void CreateCard(Card cardInfo)
    {
        CardDisplay newCard = Instantiate(cardPrefab, handUI.transform);
        newCard.card = cardInfo;
        newCard.DisplayText();
        hand.Add(newCard);
    }

    public void DisplayHand(List<Card> cards)
    {
        StartFade(); 
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
        StartFade();
        CardDisplay cardToRemove = hand[index];
        hand.RemoveAt(index);
        DestroyImmediate(cardToRemove.gameObject);
        previousIndex = 0;
    }

    private void StartFade()
    {
        if (slowCoroutine != null)
            return;
        handUI.SetActive(true);
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(Leaving());
    }

    IEnumerator Leaving()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        handUI.SetActive(false);
    }

    private void StartSlowMode()
    {
        if (slowCoroutine != null)
            StopCoroutine(slowCoroutine);
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        handUI.SetActive(true);
        slowCoroutine = StartCoroutine(Slowing());
    }

    IEnumerator Slowing()
    {
        Time.timeScale = 0.4f;
        yield return new WaitForSecondsRealtime(slowDuration);
        StopSlowMode();
    }

    public bool StopSlowMode()
    {
        if (slowCoroutine != null)
            StopCoroutine(slowCoroutine);
        handUI.SetActive(false);
        Time.timeScale = 1.0f;
        return false;
    }
}
