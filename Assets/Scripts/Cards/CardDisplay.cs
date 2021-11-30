using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    public TextMeshPro textArea;

    public void DisplayText()
    {
        textArea.text = string.Format(card.description, card.damage);
    }
}
