using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGridCalculation : MonoBehaviour
{
    private Player player;

    public int currentRange;
    public HashSet<GameObject> highlightedObjects = new HashSet<GameObject>();
    private void Start()
    {
        player = GetComponent<Player>();
    }

    public void HighlightAreaAroundPlayer()
    {
        int playerX = Mathf.FloorToInt((transform.position.x - player.gridBehavior.origin.x) / player.gridBehavior.cellSize);
        int playerY = Mathf.FloorToInt((transform.position.z - player.gridBehavior.origin.z) / player.gridBehavior.cellSize);

        player.gridBehavior.HighlightArea(playerX, playerY, currentRange);
        UpdateHighlightedObjects(playerX, playerY, currentRange);
    }

    private void UpdateHighlightedObjects(int playerX, int playerY, int range)
    {
        highlightedObjects.Clear();

        for (int i = 0; i < player.gridBehavior.columns; i++)
        {
            for (int j = 0; j < player.gridBehavior.rows; j++)
            {
                int distance = Mathf.Abs(playerX - i) + Mathf.Abs(playerY - j);

                if (distance <= range)
                {
                    GridStat gridStat = player.gridBehavior.gridArray[i, j].GetComponent<GridStat>();
                    if (gridStat != null)
                    {
                        Collider[] colliders = Physics.OverlapBox(gridStat.transform.position, new Vector3(player.gridBehavior.cellSize / 2, 1, player.gridBehavior.cellSize / 2));
                        foreach (Collider collider in colliders)
                        {
                            if (collider.CompareTag("Enemy"))
                            {
                                highlightedObjects.Add(collider.gameObject);
                            }
                        }
                    }
                }
            }
        }
    }
}
