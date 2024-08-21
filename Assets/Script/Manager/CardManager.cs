using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    public CardActions cardActions;

    public CardData[] allCards;
    public GameObject cardPrefab;
    public Transform[] cardSlots;
    public Transform nextCardSlot;

    public List<CardData> selectedCards = new List<CardData>();

    [SerializeField] private List<CardData> currentCards = new List<CardData>();
    [SerializeField] private CardData nextCard;
    [SerializeField] private List<CardData> waitingCards = new List<CardData>();

    public Card selectedCard; // Currently selected card

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }

        cardActions = GetComponent<CardActions>();
    }

    private void Start()
    {
        InitializeCards();
        PlaceSelectedCardsInSlots();
    }
   

    private void InitializeCards()
    {
        if (selectedCards.Count < cardSlots.Length + 1)
        {
            Debug.LogError("Not enough selected cards to fill the slots.");
            return;
        }

        // Fisher-Yates algoritması ile karıştırma
        for (int i = selectedCards.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            CardData temp = selectedCards[i];
            selectedCards[i] = selectedCards[j];
            selectedCards[j] = temp;
        }

        // Karıştırılmış kartları yerleştirme
        for (int i = 0; i < cardSlots.Length; i++)
        {
            currentCards.Add(selectedCards[i]);
        }

        nextCard = selectedCards[cardSlots.Length];

        for (int i = cardSlots.Length + 1; i < selectedCards.Count; i++)
        {
            waitingCards.Add(selectedCards[i]);
        }
    }

    private void PlaceSelectedCardsInSlots()
    {
        for (int i = 0; i < cardSlots.Length; i++)
        {
            PlaceCardInSlot(cardSlots[i], currentCards[i]);
        }

        if (nextCardSlot != null)
        {
            PlaceCardInSlot(nextCardSlot, nextCard);
        }
    }

    private void PlaceCardInSlot(Transform slot, CardData cardData)
    {
        GameObject cardObject = Instantiate(cardPrefab, slot);
        Card card = cardObject.GetComponent<Card>();

        SetupCardButton(cardData, cardObject, card);
    }



    public void OnCardClick(Card card)
    {
        selectedCard = card;
        card.OnCardClick();

        if (card == null)
        {
            Debug.LogError("Card is null.");
        }
    }

    public void UseCard(int slotIndex) // Düşmana Tıkladığımız zaman çalışıyor yani bu fonksiyonun içinde Attack ve Effekt işlemleri gerçekleştirebiliriz
    {
        if (slotIndex < 0 || slotIndex >= cardSlots.Length)
        {
            Debug.LogError("Invalid slot index.");
            return;
        }

        currentCards[slotIndex] = nextCard;


        if (waitingCards.Count > 0)
        {
            nextCard = waitingCards[0];
            waitingCards.RemoveAt(0);
        }
        else
        {
            nextCard = null; // All cards have been used
        }

        waitingCards.Add(selectedCard.cardData);
        LastCardUsed.instance.SetLastCardUsed(selectedCard.cardData);
        selectedCard.ResetRange();
        selectedCard = null;


        UpdateCardVisuals();
    }

    private void UpdateCardVisuals()
    {
        for (int i = 0; i < cardSlots.Length; i++)
        {
            UpdateCardVisual(cardSlots[i], currentCards[i]);
        }

        if (nextCardSlot != null && nextCard != null)
        {
            UpdateCardVisual(nextCardSlot, nextCard);
        }
    }

    private void UpdateCardVisual(Transform slot, CardData cardData)
    {
        Card card = slot.GetComponentInChildren<Card>();
        UpdateCardButton(cardData, card);
    }

    private void UpdateCardButton(CardData cardData, Card card)
    {
        if (card != null)
        {
            card.Setup(cardData);
            Button button = card.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnCardClick(card));
            }
        }
    }

    private void SetupCardButton(CardData cardData, GameObject cardObject, Card card)
    {
        if (card != null)
        {
            card.Setup(cardData);
            Button button = cardObject.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnCardClick(card));
            }
        }
    }
}
