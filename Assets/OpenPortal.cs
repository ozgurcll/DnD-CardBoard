using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPortal : MonoBehaviour
{
    [SerializeField] private GameObject portal;

    public void PortalActivated()
    {
        portal.SetActive(true);
    }
}
