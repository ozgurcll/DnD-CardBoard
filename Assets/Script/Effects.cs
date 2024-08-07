using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    public Enemy target;
    public float speed = 8f;

    private void Update()
    {
        FindEnemy();
    }

    private void FindEnemy()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);


            if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
            {
                GameObject damageVFX = Instantiate(LastCardUsed.instance.lastUsedCardData.cardDamageEffect, target.transform.position, Quaternion.identity);

                Destroy(damageVFX, 2f);
                Destroy(gameObject);
            }
        }
    }
    public void SetTarget(Enemy newTarget)
    {
        target = newTarget;
    }
}
