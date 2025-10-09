using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    public GridPosition gridPosition;

    private void Start()
    {
        gridPosition = GetComponent<GridPosition>();
    }
}
