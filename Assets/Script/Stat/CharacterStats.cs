using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    strength,
    agility,
    intelligence,
    vitality,
    damage,
    critChance,
    critPower,
    health,
    armor,
    evasion,
    magicRes,
    fireDamage,
    iceDamage,
    lightingDamage,
}
public class CharacterStats : MonoBehaviour
{
    [Header("Action Control")]
    public Stat actionPoint;
    public bool isAction;
    public bool isAttack;


    [Header("Major stats")]
    public Stat strength; // 1 point increase damage by 1 and crit.power by 1%
    public Stat agility;  // 1 point increase evasion by 1% and crit.chance by 1%
    public Stat intelligence; // 1 point increase magic damage by 1 and magic resistance by 3
    public Stat vitality; // 1 point incredase health by 3 or 5 points

    [Header("Offensive stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critPower;              // default value 150%

    [Header("Defensive stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;
    public Stat magicResistance;

    [Header("Magic stats")]
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightingDamage;


    public bool isIgnited;   // does damage over time
    public bool isChilled;   // reduce armor by 20%
    public bool isShocked;   // reduce accuracy by 20%

    //[SerializeField] private float ailmentsDuration = 4;
    // private float ignitedTimer;
    // private float chilledTimer;
    // private float shockedTimer;


    // private float igniteDamageCoodlown = .3f;
    // private float igniteDamageTimer;
    // private int igniteDamage;
    //[SerializeField] private GameObject shockStrikePrefab;
    // private int shockDamage;
    public int currentHealth;
    public int currentActionPoint;

    public System.Action onHealthChanged;
    public bool isDead { get; private set; }
    public bool isInvincible { get; private set; }
    private bool isVolnurable;

    protected virtual void Start()
    {
        critPower.SetDefaultValue(150);
        currentHealth = maxHealth.GetValue();
        currentActionPoint = actionPoint.GetValue();
    }
    protected virtual void Update()
    {

    }

    public virtual void TakeDamage(int _damage)
    {
        if (isInvincible)
            return;

        DecreaseHealthBy(_damage);

        // GetComponent<Entity>().DamageImpact();
        // fx.StartCoroutine("FlashFX");

        if (currentHealth <= 0 && !isDead)
            Die();


    }
    public virtual void DoDamage(CharacterStats _targetStats)
    {
        // bool criticalStrike = false;

        if (_targetStats.isInvincible)
            return;

        //  _targetStats.GetComponent<Entity>().SetupKnockbackDir(transform);

        int totalDamage = damage.GetValue();

        if (CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
            // criticalStrike = true;
        }

        // fx.CreateHitFX(_targetStats.transform, criticalStrike);

        _targetStats.TakeDamage(totalDamage);
    }

    public int GetMaxHealthValue() => maxHealth.GetValue() + vitality.GetValue() * 5;
    public int GetActionPoints() => actionPoint.GetValue();


    public virtual void IncreaseHealthBy(int _Amount)
    {
        currentHealth += _Amount;

        if (currentHealth > GetMaxHealthValue())
            currentHealth = GetMaxHealthValue();

        if (onHealthChanged != null)
            onHealthChanged();
    }

    protected virtual void DecreaseHealthBy(int _damage)
    {
        if (isVolnurable)
            _damage = Mathf.RoundToInt(_damage * 1.1f);

        currentHealth -= _damage;

        // if (_damage > 0)
        //     fx.CreatePopUpText(_damage.ToString());

        if (onHealthChanged != null)
            onHealthChanged();
    }

    protected bool CanCrit()
    {
        int totalCriticalChance = critChance.GetValue();

        if (Random.Range(0, 100) <= totalCriticalChance)
        {
            return true;
        }


        return false;
    }
    public void KillEntity()
    {
        if (!isDead)
            Die();
    }


    protected int CalculateCriticalDamage(int _damage)
    {
        float totalCritPower = critPower.GetValue() * .01f;
        float critDamage = _damage * totalCritPower;

        return Mathf.RoundToInt(critDamage);
    }
    protected virtual void Die()
    {
        isDead = true;
    }

    public Stat GetStat(StatType _statType)
    {
        if (_statType == StatType.strength) return strength;
        else if (_statType == StatType.agility) return agility;
        else if (_statType == StatType.intelligence) return intelligence;
        else if (_statType == StatType.vitality) return vitality;
        else if (_statType == StatType.damage) return damage;
        else if (_statType == StatType.critChance) return critChance;
        else if (_statType == StatType.critPower) return critPower;
        else if (_statType == StatType.health) return maxHealth;
        else if (_statType == StatType.armor) return armor;
        else if (_statType == StatType.evasion) return evasion;
        else if (_statType == StatType.magicRes) return magicResistance;
        else if (_statType == StatType.fireDamage) return fireDamage;
        else if (_statType == StatType.iceDamage) return iceDamage;
        else if (_statType == StatType.lightingDamage) return lightingDamage;

        return null;
    }
}
