using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GridPosition gridPosition;
    public CharacterStats stats;
    public EnemyUI enemyUI;

    public int movePoints = 3; // Her turda belirli hareket hakkı
    public int currentMovePoints;
    private void Start()
    {
        gridPosition = GetComponent<GridPosition>();
        stats = GetComponent<CharacterStats>();
        enemyUI = GetComponent<EnemyUI>();
        currentMovePoints = movePoints;
    }
    public void TakeAction()
    {
        // Belirli hareket sayısını gerçekleştir
        if (currentMovePoints > 0)
        {
            MoveEnemy();
            Debug.Log($"{gameObject.name} takes action.");
            currentMovePoints--;
        }
    }
    public void ResetMovePoints()
    {
        currentMovePoints = movePoints;
    }
    private void MoveEnemy()
    {
        // Düşmanın hareket edeceği yönü belirleme ve hareket ettirme
        // Bu örnek için rastgele hareket ediyoruz
        Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
        Vector3 direction = directions[Random.Range(0, directions.Length)];
        Vector3 newPosition = transform.position + direction * gridPosition.gridBehavior.cellSize;

        // Hareket et
        transform.position = newPosition;
    }

    public void Die()
    {
        GameManager.instance.RemoveToDieEnemy();
    }


    private void OnMouseEnter()
    {
        enemyUI.ShowHealthUI();
        Color color = new Color(1f, .7f, 0.3f, 1);

        Renderer rend = GetComponentInChildren<Renderer>();

        rend.material.color = color;
    }

    private void OnMouseExit()
    {
        enemyUI.HideHealthUI();
        Renderer rend = GetComponentInChildren<Renderer>();

        rend.material.color = Color.white;
    }
}
