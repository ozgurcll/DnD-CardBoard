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
    #endregion
    public int actionCostValue;

    public int currentRange;


    private void Start()
    {
        player = FindObjectOfType<Player>();
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
        actionCost.text = cardData.actionCost.ToString();
        actionCostValue = cardData.actionCost;

        cardDamage = cardData.damage;
        cardHeal = cardData.heal;
        cardShield = cardData.shield;
        cardEffect = cardData.cardEffect;
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

    public void ApplyCardEffect(RaycastHit hit)
    {
        GameObject cardEffect = Instantiate(cardData.cardEffect, player.transform.position, Quaternion.identity);

        if (cardEffect != null)
        {
            ThrowableEffects throwableItem = cardEffect.GetComponent<ThrowableEffects>();

            if (throwableItem == null)
            {
                Debug.LogError("Throwable item is null");
                throwableItem = cardEffect.AddComponent<ThrowableEffects>();
            }
            throwableItem.SetTarget(hit.collider.transform.GetComponent<Enemy>());
        }
    }

    public void ApplyStatusEffect(RaycastHit hit)
    {
        Instantiate(cardData.statusEffect, hit.transform.position, Quaternion.identity);
        hit.transform.SetParent(hit.transform);
    }

    private void ApplyCardEffects(RaycastHit hit)
    {
        // Kartın türüne ve hasar türüne göre efektleri uygula
        switch (cardData.cardType)
        {
            case CardType.Attack:
                ApplyAttackEffect(cardData);
                break;
            case CardType.Defense:
                ApplyDefenseEffect(cardData);
                break;
            case CardType.Magic:
                ApplyMagicEffect(cardData);
                break;
        }
    }

    private void ApplyAttackEffect(CardData card)
    {
        GameObject cardEffect = Instantiate(cardData.cardEffect, player.transform.position, Quaternion.identity);


        // Saldırı efektleri burada uygulanacak
        Debug.Log($"Applying {card.damageType} attack with {card.damage} damage.");
        // Örnek olarak, bir düşmana hasar vermek
        // Düşman hedefini belirleyin ve hasarı uygulayın
    }

    private void ApplyDefenseEffect(CardData card)
    {
        GameObject cardEffect = Instantiate(cardData.cardEffect, player.transform.position, Quaternion.identity);

        // Savunma efektleri burada uygulanacak
        Debug.Log($"Applying defense with {card.shield} shield.");
        // Örnek olarak, oyuncuya kalkan eklemek
        // Oyuncuya kalkanı ekleyin
    }

    private void ApplyMagicEffect(CardData card)
    {
        GameObject cardEffect = Instantiate(cardData.cardEffect, player.transform.position, Quaternion.identity);

        // Büyü efektleri burada uygulanacak
        Debug.Log($"Applying magic with {card.heal} heal and effect {card.cardEffect.name}.");
        // Örnek olarak, bir iyileştirme veya özel büyü efekti uygulamak
        // Oyuncuya veya düşmana büyüyü uygulayın
    }

    public void ResetRange() => player.CardRange(-1);
}
