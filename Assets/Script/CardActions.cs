using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardActions : MonoBehaviour
{

    private Player player;
    public CardData cardData;

    public float speed = 8f;

    private void Start()
    {
        player = GameManager.instance.player;
    }

    public void ApplyCardEffects(RaycastHit hit)
    {
        cardData = player.cardManager.selectedCard.cardData;
        // Kartın türüne ve hasar türüne göre efektleri uygula
        switch (cardData.cardType)
        {
            case CardType.Attack:
                ApplyAttackEffect(hit);
                break;
            case CardType.Defense:
                ApplyDefenseEffect();
                break;
            case CardType.Magic:
                ApplyMagicEffect(hit);
                break;
        }
    }

    private void ApplyAttackEffect(RaycastHit hit)
    {
        GameObject cardEffect = Instantiate(cardData.cardEffect, player.transform.position, Quaternion.identity);
        Debug.Log("Card Effect: " + cardEffect);
        if (cardEffect != null)
        {
            Effects throwableItem = cardEffect.GetComponent<Effects>();

            if (throwableItem == null)
            {
                Debug.LogError("Throwable item is null");
                throwableItem = cardEffect.AddComponent<Effects>();
            }
            throwableItem.SetTarget(hit.collider.transform.GetComponent<Enemy>());
        }


        // Saldırı efektleri burada uygulanacak
        // Örnek olarak, bir düşmana hasar vermek
        // Düşman hedefini belirleyin ve hasarı uygulayın
    }

    private void ApplyDefenseEffect()
    {
        GameObject cardEffect = Instantiate(cardData.cardEffect, player.transform.position, Quaternion.identity);
        cardEffect.transform.SetParent(player.transform);
        cardEffect.transform.position = new Vector3(player.transform.position.x, -.5f, player.transform.position.z);


        player.stats.maxHealth.AddModifier(cardData.heal);

        Destroy(cardEffect, 2f);


        // Savunma efektleri burada uygulanacak
        // Örnek olarak, oyuncuya kalkan eklemek
        // Oyuncuya kalkanı ekleyin
    }



    private void ApplyMagicEffect(RaycastHit hit)
    {
        GameObject cardEffect = Instantiate(cardData.cardEffect, player.transform.position, Quaternion.identity);
        Debug.Log("Card Effect: " + cardEffect);
        if (cardEffect != null)
        {
            Effects effects = cardEffect.GetComponent<Effects>();

            if (effects == null)
            {
                Debug.LogError("Throwable item is null");
                effects = cardEffect.AddComponent<Effects>();
            }
            effects.SetTarget(hit.collider.transform.GetComponent<Enemy>());
        }
        // Büyü efektleri burada uygulanacak
        // Örnek olarak, bir iyileştirme veya özel büyü efekti uygulamak
        // Oyuncuya veya düşmana büyüyü uygulayın
    }
}
