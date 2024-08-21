using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CardInventory : MonoBehaviour
{
    public Transform selectedCardsContainer; // AllCards UI için container
    public Transform allCardsContainer; // AllCards UI için container

    private void Start() {
        DisplaySelectedCards();
        DisplayAllCards();
    }

    public void DisplaySelectedCards()
    {
        foreach (CardData cardData in CardManager.instance.selectedCards)
        {
            GameObject cardObject = Instantiate(CardManager.instance.cardPrefab, selectedCardsContainer);
            Card card = cardObject.GetComponent<Card>();

            //cardObject.transform.localScale = new Vector3(0.5f);

            card.enabled = false;
            cardObject.GetComponent<CardVisuals>().enabled = false;
            card.GetComponent<Button>().enabled = false;

            card.Setup(cardData);
        }
    }

    public void DisplayAllCards()
    {
        foreach (CardData cardData in CardManager.instance.allCards)
        {
            GameObject cardObject = Instantiate(CardManager.instance.cardPrefab, allCardsContainer);
            Card card = cardObject.GetComponent<Card>();
            
            card.enabled = false;
            cardObject.GetComponent<CardVisuals>().enabled = false;
            card.GetComponent<Button>().enabled = false;
            
            
            card.Setup(cardData);
        }
    }
}
