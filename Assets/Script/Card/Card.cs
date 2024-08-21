using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum CardDamageType
{
    CloseRange,
    LongRange
}

public class Card : MonoBehaviour
{
    //public CardDamageType cardDamageType;
    private Player player;
    public CardData cardData;

    #region Card Visuals Setup
    [Header("Create Card")]
    public Image cardBackground;
    [Header("Upside")]
    public Image[] cardTypeFrame;
    public Image[] cardTypeBackground;
    public Image cardTypeIcon;
    public Image cardUpsideFrame;
    public Image cardiconImage;
    public TextMeshProUGUI actionCost;
    public int range;
    [Header("Downside")]
    public Image[] evoSlots;
    public Image cardDescFrame;
    public TextMeshProUGUI descriptionText;
    #endregion

    #region Card Features Setup
    public int cardDamage;
    public int cardHeal;
    public int cardShield;
    public GameObject cardEffect;
    public bool haveStatus;
    public int statusTurn;
    public int statusDamage;
    public GameObject statusEffect;
    public GameObject cardDamageEffect;
    #endregion
    public int manaValue;

    public int currentRange;


    private void Start()
    {
        player = GameManager.instance.player;
    }

    public void Setup(CardData _cardData)
    {
        if (_cardData == null)
        {
            Debug.LogError("Card data is null");
            return;
        }
        cardData = _cardData;
        ConfigureCardData(_cardData);
    }

    #region Card Setup
    private void ConfigureCardData(CardData cardData)
    {
        if (cardData == null)
        {
            Debug.LogError("Card data is null in ConfigureCardData");
            return;
        }
        switch (cardData.cardRarely)
        {
            case CardRarely.Bronze:
                ConfigureBronzeCardData(cardData);
                break;
            case CardRarely.Silver:
                ConfigureSilverCardData(cardData);
                break;
            case CardRarely.Gold:
                ConfigureGoldCardData(cardData);
                break;
        }

        switch (cardData.damageType)
        {
            case DamageType.Heavy:
                cardTypeIcon.sprite = cardData.cardTypeIcon[0];
                break;
            case DamageType.Light:
                cardTypeIcon.sprite = cardData.cardTypeIcon[1];
                break;
            case DamageType.Spear:
                cardTypeIcon.sprite = cardData.cardTypeIcon[2];
                break;
            case DamageType.Arrow:
                cardTypeIcon.sprite = cardData.cardTypeIcon[3];
                break;
            case DamageType.Armor:
                cardTypeIcon.sprite = cardData.cardTypeIcon[4];
                break;
            case DamageType.Classic:
                cardTypeIcon.sprite = cardData.cardTypeIcon[5];
                break;
            case DamageType.Posion:
                cardTypeIcon.sprite = cardData.cardTypeIcon[6];
                break;
            case DamageType.Zap:
                cardTypeIcon.sprite = cardData.cardTypeIcon[7];
                break;
        }

        ConfigureCardVisuals(cardData);
    }

    private void ConfigureCardVisuals(CardData cardData)
    {
        cardiconImage.sprite = cardData.cardIcon;
        range = cardData.range;
        currentRange = range;
        actionCost.text = cardData.mana.ToString();
        manaValue = cardData.mana;

        cardDamage = cardData.damage;
        cardHeal = cardData.heal;
        cardShield = cardData.shield;
        //cardEffect = cardData.cardEffect;
        statusTurn = cardData.statusTurn;
        statusDamage = cardData.statusDamage;
    }
    #endregion

    #region Card Rarely Configurations
    private void ConfigureCardAppearance(CardData cardData, int index)
    {
        cardBackground.sprite = cardData.cardBackground[index];
        for (int i = 0; i < cardTypeBackground.Length; i++)
        {
            cardTypeBackground[i].sprite = cardData.evoSlots[index];
            cardTypeFrame[i].sprite = cardData.cardTypeFrame[index];
        }
        cardUpsideFrame.sprite = cardData.cardFrame[index];
        for (int i = 0; i < evoSlots.Length; i++)
        {
            evoSlots[i].sprite = cardData.evoSlots[index];
        }
        cardDescFrame.sprite = cardData.cardDescFrame[index];
        descriptionText.text = cardData.cardDescription;
    }

    private void ConfigureBronzeCardData(CardData cardData)
    {
        ConfigureCardAppearance(cardData, 0);
    }

    private void ConfigureSilverCardData(CardData cardData)
    {
        ConfigureCardAppearance(cardData, 1);
    }

    private void ConfigureGoldCardData(CardData cardData)
    {
        ConfigureCardAppearance(cardData, 2);
    }
    #endregion

    public void OnCardClick()
    {
        if (!player.stats.isAttack)
        {
            player.CardRange(-1);
            return;
        }

        player?.CardRange(currentRange);
    }

    public void ResetRange() => player.CardRange(-1);

    public static implicit operator Card(CardData v)
    {
        throw new NotImplementedException();
    }
}
