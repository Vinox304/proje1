using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    public int cardCount;
    public List<Card> CardPrefabs;
    public List<CardPlace> CardPlaces;

    void Start()
    {
        GenerateCards();
    }

    void GenerateCards()
    {
        for (int i = 0; i < cardCount; i++)
        {
            var cardPlaceIndex = Random.Range(0, CardPlaces.Count);
            var cardPlace = CardPlaces[cardPlaceIndex];

            var cardIndex = Random.Range(0, CardPrefabs.Count);
            var card = Instantiate(CardPrefabs[cardIndex]);
            card.transform.SetParent(cardPlace.transform);

            cardPlace.AddCard(card);
        }

        foreach (var cardPlace in CardPlaces)
        {
            cardPlace.CorrectCardPlaces();
        }
    }
}
