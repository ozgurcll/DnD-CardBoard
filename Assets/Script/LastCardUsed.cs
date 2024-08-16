using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastCardUsed : MonoBehaviour
{
    public static LastCardUsed instance;
    public CardData lastUsedCardData;

    [Header("Card Info")]
    [SerializeField] private CardRarely cardRarely;
    [SerializeField] private CardType cardType;
    [SerializeField] private DamageType damageType;
    [SerializeField] private int damage;
    [SerializeField] private int shield;
    [SerializeField] private int heal;
    [Header("Status")]
    [SerializeField] private bool haveStatus;
    [SerializeField] private int statusTurn;
    [SerializeField] private int statusDamage;

    [Header("Card Effects")]
    [SerializeField] private GameObject cardEffect;
    [SerializeField] private GameObject statusEffect;



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
    }

    public void SetLastCardUsed(CardData cardData)
    {
        lastUsedCardData = cardData;

        cardRarely = cardData.cardRarely;
        cardType = cardData.cardType;
        damageType = cardData.damageType;
        damage = cardData.damage;
        shield = cardData.shield;
        heal = cardData.heal;
        haveStatus = cardData.haveStatus;
        statusTurn = cardData.statusTurn;
        statusDamage = cardData.statusDamage;

        cardEffect = cardData.cardEffect;
        statusEffect = cardData.statusEffect;
    }


}
