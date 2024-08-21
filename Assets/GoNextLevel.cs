using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoNextLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            Debug.Log("Next Level");
        }
    }
}
