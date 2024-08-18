using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardPlace : MonoBehaviour
{
    public Ease animationEase = Ease.OutBack;
    public float cardHeightDifference;
    public float firstCardHeight;
    public List<Card> Cards = new List<Card>();

    public Card TopCard => Cards.Count > 0 ? Cards[Cards.Count - 1] : null;

    public void AddCard(Card newCard)
    {
        Cards.Add(newCard);
        newCard.Place = this;
    }

    public void CorrectCardPlaces()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            var card = Cards[i];
            card.transform.position = GetCardPosition(i);
        }
    }

    public Vector3 GetCardPosition(int index)
    {
        return transform.position
            + firstCardHeight * Vector3.up
            + index * Vector3.up * cardHeightDifference;
    }

    public List<Card> IndicateSelectedCards()
    {
        if (Cards.Count == 0)
            return null;

        var selectedCards = FindSelectedCards();
        AnimateSelectedCards(selectedCards);

        return selectedCards;
    }

    List<Card> FindSelectedCards()
    {
        var topDeck = TopCard;
        var selectedCards = new List<Card>();

        for (int i = Cards.Count - 1; i > -1; i--)
        {
            var card = Cards[i];
            if (card.Color == topDeck.Color)
                selectedCards.Add(card);
            else
                break;
        }
        return selectedCards;
    }

    void AnimateSelectedCards(List<Card> selectedCards)
    {
        for (int i = 0; i < selectedCards.Count; i++)
        {
            var card = selectedCards[i];
            Vector3 startPosition = card.transform.position;
            Vector3 endPosition = startPosition + Vector3.up * 2f;

            card.transform.DOMove(endPosition, .5f)
                .SetDelay(i * .1f)
                .SetEase(Ease.OutElastic);
        }
    }

    public void RemoveTopCards()
    {
        if (Cards.Count == 0)
            return;

        var selectedCards = FindSelectedCards();
        foreach (var card in selectedCards)
            Cards.Remove(card);
    }

    public void SetNewCards(List<Card> newCards)
    {
        foreach (var card in newCards)
        {
            card.Place = this;
            Cards.Add(card);
        }

        for (int i = 0; i < newCards.Count; i++)
        {
            var index = Cards.Count - 1 - newCards.Count + 1 + i;
            var cardPosition = GetCardPosition(index);

            var tweenCard = Cards[index];

            Vector3[] path = new Vector3[8];
            float radius = 1f;

            for (int j = 0; j < path.Length; j++)
            {
                float angle = j * Mathf.PI * 2 / path.Length;
                path[j] = cardPosition + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
            }

            tweenCard.transform.DOPath(path, 1f, PathType.CatmullRom)
                .SetDelay(i * .1f)
                .SetEase(animationEase)
                .OnComplete(() =>
                {
                    tweenCard.transform.DOMove(cardPosition, 0.5f)
                        .SetEase(Ease.OutElastic);
                });
        }
    }
}
