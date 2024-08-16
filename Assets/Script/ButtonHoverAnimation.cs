using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalPosition;
    public float hoverHeight = 10f;
    public float hoverSpeed = 5f;
    public float clickZoomFactor = 1.2f;
    public float clickSpeed = 5f;

    private bool isHovering;

    private void Start()
    {
        originalPosition = transform.position;
    }
    private void Update()
    {
        HandleHoverAnimation();
    }
    public void HandleHoverAnimation()
    {
        Vector3 targetPosition = isHovering ? originalPosition + Vector3.up * hoverHeight : originalPosition;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * hoverSpeed);

    }

   public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

}
