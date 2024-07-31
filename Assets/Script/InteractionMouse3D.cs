using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionMouse3D : MonoBehaviour
{
    private void OnMouseEnter()
    {
        Color color = new Color(1f, .7f, 0.3f, 1);
        MeshRenderer rend = GetComponentInChildren<MeshRenderer>();
        rend.material.color = color;
    }

    private void OnMouseExit()
    {
        MeshRenderer rend = GetComponentInChildren<MeshRenderer>();
        rend.material.color = Color.white;
    }
}
