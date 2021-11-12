using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeckEditor : EditorWindow
{
    public MageHand deck;
    public Card[] cards;
    public int[] cardAmount;

    [MenuItem("Tools/DeckEditor")]
    public static void OpenWindow()
    {
        GetWindow<DeckEditor>("Deck");
    }

    private void OnGUI()
    {
        minSize = new Vector2(500, 200);
        EditorGUILayout.LabelField("Set Cards to Deck", EditorStyles.boldLabel);

        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty deckProperty = so.FindProperty("deck");
        SerializedProperty cardsProperty = so.FindProperty("cards");
        SerializedProperty cardAmountProperty = so.FindProperty("cardAmount");

        EditorGUILayout.PropertyField(deckProperty, true);

        EditorGUI.BeginDisabledGroup(deck == null);
        using (new GUILayout.HorizontalScope())
        {
            EditorGUILayout.PropertyField(cardsProperty, true);
            if(cards != null)
            {
                EditorGUI.BeginDisabledGroup(cards.Length <= 0);
                EditorGUILayout.PropertyField(cardAmountProperty, true);
                EditorGUI.EndDisabledGroup();
            }
        }

        if (GUILayout.Button("Populate Deck"))
        {
            if (cards.Length == 0 || cardAmount.Length == 0 ||
                cards.Length != cardAmount.Length)
            {
                Debug.Log("Error.");
                return;
            }
            deck.deck = new List<Card>();
            for (int i = 0; i < cards.Length; i++)
            {
                for (int j = 0; j < cardAmount[i]; j++)
                    deck.deck.Add(cards[i]);
            }
        }

        if (GUILayout.Button("Display Start Hand"))
        {
            for (int i = 0; i < 5; i++)
                deck.Draw();
        }

        if(GUILayout.Button("Empty Hand"))
        {
            deck.EmptyHand();
        }
        EditorGUI.EndDisabledGroup();

        so.ApplyModifiedProperties();
    }
}
