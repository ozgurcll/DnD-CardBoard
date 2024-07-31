using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardRarely
{
    Bronze,
    Silver,
    Gold
}
public enum CardType
{
    Attack,
    Defense,
    Magic
}

public enum DamageType
{
    Heavy,
    Light,
    Spear,
    Arrow,
    Armor,
    Classic,
    Posion,
    Zap
}
[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
public class CardData : ScriptableObject
{
    [Header("Card Info")]
    public CardRarely cardRarely;
    public CardType cardType;
    public DamageType damageType;
    #region Card Visuals
    [Header("Create Cart")]
    public Sprite[] cardBackground;
    [Header("Upside")]
    public Sprite[] cardTypeFrame;
    public Sprite[] cardTypeIcon;
    public Sprite[] cardFrame;
    public Sprite cardIcon;
    public int actionCost;
    public int range;
    [Header("Downside")]
    public Sprite[] evoSlots;
    public Sprite[] cardDescFrame;
    [TextArea]
    public string cardDescription;
    #endregion

    [Header("Card Features")]
    #region Card Features
    public int damage;
    public int shield;
    public int heal;
    [Header("Status")]
    public bool haveStatus;
    public int statusTurn;
    public int statusDamage;
    public GameObject statusEffect;
    #endregion

    [Header("Card Effects")]
    #region Card Effects
    public GameObject cardEffect;

    #endregion
}