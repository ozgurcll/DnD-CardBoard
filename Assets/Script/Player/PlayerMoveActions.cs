using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveActions : MonoBehaviour
{
    private Player player;


    public GameObject[] playerDirectionIndicators;
    private float checkDistance = 1f;
    private Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
    public LayerMask obstacleLayer;
    public LayerMask enemyLayer;
    private bool canMove;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        CheckMoveDirection();
    }

    public void CheckMoveDirection() // Yön Butonlarının Çevresinde Engel Varmı Kontrol Ediyoruz Varsa O Butonu Kapatıyoruz
    {
        Vector3 currentPosition = transform.position;

        for (int i = 0; i < directions.Length; i++)
        {
            // Perform raycast from the player's position in the direction
            RaycastHit hit;
            canMove = !Physics.Raycast(currentPosition, transform.TransformDirection(directions[i]), out hit, checkDistance, obstacleLayer);

            // Enable or disable the direction indicators based on the raycast result
            playerDirectionIndicators[i].SetActive(canMove);
        }
    }

    public void GoForward()
    {
        MovePlayer(Vector3.forward);
    }

    public void GoBackward()
    {
        MovePlayer(Vector3.back);
    }

    public void GoLeft()
    {
        MovePlayer(Vector3.left);
    }

    public void GoRight()
    {
        MovePlayer(Vector3.right);
    }

    private void MovePlayer(Vector3 direction)
    {
        player.uI.DownActionPoint();
        Vector3 newPosition = transform.position + direction * player.gridBehavior.cellSize;
        player.transform.position = newPosition;

        player.gridCalculation.HighlightAreaAroundPlayer();

        CheckMoveDirection();
    }
}
