using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackActions : MonoBehaviour
{
    //Düşmana Vurduğumuzda Kırmızı Beyaz Material arasında geçiş yapılacak ve ufak bir titreşim olucak
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HitTarget();
        }
    }

    public RaycastHit HitTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {

            if (IsEnemyInGrid(hit) && player.stats.isAttack)
            {
                UpdateEnemyStatus(hit);

                ApplyDamageEnemy(hit);
            }

            if (player.cardManager.selectedCard != null && player.cardManager.selectedCard.cardData.cardType == CardType.Defense)
                if (hit.collider.gameObject.CompareTag("Player") && player.stats.isAttack)
                {
                    UpdatePlayerStatus(hit);
                    ApplyBuffPlayer(hit);
                }
        }
        return hit;
    }

    #region Attack Visuals
    public IEnumerator ApplyHitEffect(RaycastHit hit)
    {
        Vector3 originalPosition = hit.transform.position;
        Vector3 targetPosition = originalPosition + new Vector3(0.2f, 0.2f, 0.2f);
        Color originalColor = hit.collider.gameObject.GetComponentInChildren<SpriteRenderer>().material.color;
        Color targetColor = Color.red;

        float duration = 0.1f; // toplam süre
        float elapsed = 0f;

        // Hit etkisi
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            hit.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
            hit.collider.gameObject.GetComponentInChildren<SpriteRenderer>().material.color = Color.Lerp(originalColor, targetColor, t);
            yield return null;
        }

        elapsed = 0f;

        // Geri dönüş etkisi
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            hit.transform.position = Vector3.Lerp(targetPosition, originalPosition, t);
            hit.collider.gameObject.GetComponentInChildren<SpriteRenderer>().material.color = Color.Lerp(targetColor, originalColor, t);
            yield return null;
        }

        // Son olarak pozisyon ve rengi orijinal haline getir
        hit.transform.position = originalPosition;
        hit.collider.gameObject.GetComponentInChildren<SpriteRenderer>().material.color = originalColor;
    }
    #endregion

    private void ApplyDamageEnemy(RaycastHit hit)
    {
        player.stats.isAttack = false;
        player.uI.attackImage.gameObject.SetActive(false);

        player.cardManager.selectedCard.ApplyCardEffect(hit); // Düşmana Tıkladığımız Zaman Kart Efekti

        StartCoroutine(ApplyHitEffect(hit)); // Düşmana Tıkladığımız Zaman Kırmızı Beyaz Titreşim Efekti
        int slotIndex = Array.IndexOf(player.cardManager.cardSlots, player.cardManager.selectedCard.transform.parent);
        player.cardManager.UseCard(slotIndex);
    }

    private void ApplyBuffPlayer(RaycastHit hit)
    {
        player.stats.isAttack = false;
        player.uI.attackImage.gameObject.SetActive(false);

        player.cardManager.selectedCard.ApplyCardEffect(hit); // Düşmana Tıkladığımız Zaman Kart Efekti

        StartCoroutine(ApplyHitEffect(hit)); // Düşmana Tıkladığımız Zaman Kırmızı Beyaz Titreşim Efekti
        int slotIndex = Array.IndexOf(player.cardManager.cardSlots, player.cardManager.selectedCard.transform.parent);
        player.cardManager.UseCard(slotIndex);
    }

    private void UpdateEnemyStatus(RaycastHit hit)
    {
        EnemyStats enemy = hit.collider.gameObject.GetComponentInChildren<EnemyStats>();
        Status_UI[] enemyStatusUI = hit.collider.gameObject.GetComponentsInChildren<Status_UI>();

        // İlk boş slotu bul
        if (player.cardManager.selectedCard.cardData.haveStatus)
            for (int i = 0; i < enemyStatusUI.Length; i++)
            {
                if (!enemyStatusUI[i].IsSlotFilled())
                {
                    enemyStatusUI[i].UpdateSlot();
                    break;
                }
            }

        enemy.TakeDamage(player.cardManager.selectedCard.cardData.damage);
    }
    private void UpdatePlayerStatus(RaycastHit hit)
    {
        PlayerStats playerStats = hit.collider.gameObject.GetComponentInChildren<PlayerStats>();
        Status_UI[] enemyStatusUI = hit.collider.gameObject.GetComponentsInChildren<Status_UI>();

        // İlk boş slotu bul
        if (player.cardManager.selectedCard.cardData.haveStatus)
            for (int i = 0; i < enemyStatusUI.Length; i++)
            {
                if (!enemyStatusUI[i].IsSlotFilled())
                {
                    enemyStatusUI[i].UpdateSlot();
                    break;
                }
            }

        playerStats.TakeDamage(player.cardManager.selectedCard.cardData.damage);
    }

    private bool IsEnemyInGrid(RaycastHit hit) => player.gridCalculation.highlightedObjects.Contains(hit.collider.gameObject);
}
