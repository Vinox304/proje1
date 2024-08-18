using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private List<Card> selectedCards;

    private bool HasCardsAlready => selectedCards.Count > 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                HandleRaycastHit(hit.transform);
            }
        }
    }

    void HandleRaycastHit(Transform target)
    {
        var cardPlace = GetCardPlace(target);
        if (cardPlace == null)
            return;

        if (HasCardsAlready)
        {
            if (cardPlace.TopCard != null && cardPlace.TopCard.Color != selectedCards[0].Color)
            {
                Debug.Log("Only cards of the same color can be moved.");
                return;
            }

            var oldPlace = selectedCards[0].Place;
            oldPlace.RemoveTopCards();

            cardPlace.SetNewCards(selectedCards);
            selectedCards.Clear();
        }
        else
        {
            selectedCards = cardPlace.IndicateSelectedCards();
        }
    }

    CardPlace GetCardPlace(Transform target)
    {
        var card = target.GetComponent<Card>();
        if (card != null)
            return card.Place;

        var cardPlace = target.GetComponent<CardPlace>();
        if (cardPlace != null)
            return cardPlace;

        return null;
    }
}
