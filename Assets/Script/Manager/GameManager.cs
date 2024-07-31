using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player;
    public List<Enemy> enemies;

    public GameMode currentMode = GameMode.PlayerTurn;
    public bool isPlayerTurn;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        AddEnemiesToList();
    }

    private void AddEnemiesToList()
    {
        enemies.AddRange(FindObjectsOfType<Enemy>());
    }

    public void StartPlayerTurn()
    {
        DecreaseAndDamagePerTurn();
        player.playerMove.CheckMoveDirection();
        player.uI.ResetActionsUI();
    }

    public void EndPlayerTurn()
    {
        ResetEnemyMovePoints();
        currentMode = GameMode.EnemyTurn;
        StartCoroutine(EnemyTurnRoutine());
    }

    public IEnumerator EnemyTurnRoutine()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].TakeAction();
                yield return new WaitForSeconds(1f);
            }
        }
        currentMode = GameMode.PlayerTurn;
    }
    public void ResetEnemyMovePoints()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.ResetMovePoints();
        }
    }

    public void DecreaseAndDamagePerTurn()
    {
        List<Enemy> enemiesToRemove = new List<Enemy>();

        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            Enemy enemy = enemies[i];
            int totalDamage = 0;
            Status_UI[] enemyStatus = enemy.GetComponentsInChildren<Status_UI>();

            foreach (Status_UI status in enemyStatus)
            {
                if (status.IsSlotFilled())
                {
                    StartCoroutine(ApplyHitEffect(enemies[i]));
                    status.status.ApplyStatusEffect();
                    status.DecreaseTurn();

                    totalDamage += status.GetDamage();
                    Debug.Log(totalDamage);
                }
            }

            if (totalDamage > 0)
            {
                enemy.stats.TakeDamage(totalDamage);
                if (enemy.stats.currentHealth <= 0)
                {
                    enemiesToRemove.Add(enemy);
                }
            }
        }

        // Ölen düşmanları listeden ve sahneden sil
        foreach (Enemy enemy in enemiesToRemove)
        {
            enemies.Remove(enemy);
            Destroy(enemy.gameObject);
        }
    }

    public void DecreaseTurn()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            Enemy enemy = enemies[i];
            int damage;

            Status_UI[] enemyStatus = enemy.GetComponentsInChildren<Status_UI>();

            foreach (Status_UI status in enemyStatus)
            {
                if (status.IsSlotFilled())
                {
                    damage = status.GetDamage();
                    enemy.stats.TakeDamage(damage);

                    status.status.ApplyStatusEffect();
                    StartCoroutine(ApplyHitEffect(enemies[i]));

                    status.DecreaseTurn();
                }
            }
        }
    }

    public void RemoveToDieEnemy()
    {
        enemies.RemoveAll(e => e.stats.isDead);
    }

    public bool IsAction() => player.stats.isAction;
    public bool IsAttack() => player.stats.isAttack;
    public int ActionPoint() => player.stats.actionPoint.GetValue();

    public void FinishTurn()
    {
        player.stats.isAction = false;
        player.stats.isAttack = false;
    }




    public void DestroyStatusEffect(GameObject statusEffect)
    {
        Destroy(statusEffect);
    }





    public IEnumerator ApplyHitEffect(Enemy enemy)
    {

        Vector3 originalPosition = enemy.transform.position;
        Vector3 targetPosition = originalPosition + new Vector3(0.07f, 0.07f, 0.07f);
        Color originalColor = enemy.GetComponentInChildren<SpriteRenderer>().material.color;
        Color targetColor = Color.red;

        float duration = 0.05f; // toplam süre
        float elapsed = 0f;

        // Hit etkisi
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            enemy.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
            enemy.GetComponentInChildren<SpriteRenderer>().material.color = Color.Lerp(originalColor, targetColor, t);
            yield return null;
        }

        elapsed = 0f;

        // Geri dönüş etkisi
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            enemy.transform.position = Vector3.Lerp(targetPosition, originalPosition, t);
            enemy.GetComponentInChildren<SpriteRenderer>().material.color = Color.Lerp(targetColor, originalColor, t);
            yield return null;
        }

        // Son olarak pozisyon ve rengi orijinal haline getir
        enemy.transform.position = originalPosition;
        enemy.GetComponentInChildren<SpriteRenderer>().material.color = originalColor;
    }

}
