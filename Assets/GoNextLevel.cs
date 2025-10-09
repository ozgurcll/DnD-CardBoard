using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoNextLevel : MonoBehaviour
{
    public int nextLevel;
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.GetComponent<Player>())
        {
            LevelManager.Instance.LoadNextArena(nextLevel);
        }
    }
}
