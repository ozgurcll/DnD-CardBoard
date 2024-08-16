using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
   public CardData card;
   public GameObject statusEffect;
   private int x;

   public int turn;

   public void ApplyStatusEffect()
   {
      if (x <= 0)
         if (card.statusEffect != null)
         {
            x++;
            Enemy enemy = GetComponentInParent<Enemy>();
            statusEffect = Instantiate(card.statusEffect, enemy.transform.position, Quaternion.identity);
            statusEffect.transform.SetParent(transform);
         }

      if (turn <= 1)
      {
         Destroy(statusEffect);
      }
   }


}
