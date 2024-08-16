using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PauseGame : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
}
