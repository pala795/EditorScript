using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EditorScripting/Deck", fileName = "DECK_name")]
public class Deck : ScriptableObject
{
    public string Name;
    public int MaxCards = 30;
    public List<Card> Cards;

    public Dictionary<Card, int> GetGroupedCards()
    {
        var groupedCards = new Dictionary<Card, int>();

        foreach (var card in Cards)
        {
            if (groupedCards.ContainsKey(card))
            {
                groupedCards[card]++;
            }
            else
            {
                groupedCards.Add(card, 1);   
            }
        }

        return groupedCards;
    }
}
