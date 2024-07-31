using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPosition : MonoBehaviour
{
    public GridBehavior gridBehavior { get; private set;}

    public int gridX;
    public int gridY;

    void Start()
    {
        gridBehavior = FindObjectOfType<GridBehavior>();
        if (gridBehavior != null)
        {
            AlignToGridPosition();
        }
        else
        {
            Debug.LogError("GridBehavior not found in the scene.");
        }
    }
    

    void AlignToGridPosition()
    {
        if (gridX >= 0 && gridX < gridBehavior.columns && gridY >= 0 && gridY < gridBehavior.rows)
        {
            Vector3 gridPosition = new Vector3(
                gridBehavior.origin.x + gridX * gridBehavior.cellSize,
                gridBehavior.origin.y,
                gridBehavior.origin.z + gridY * gridBehavior.cellSize
            );

            transform.position = gridPosition;
        }
        else
        {
            Debug.LogError("Grid coordinates out of range.");
        }
    }

}
