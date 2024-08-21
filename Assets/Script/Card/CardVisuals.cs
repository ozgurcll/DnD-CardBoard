using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardVisuals : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Vector3 originalPosition;
    private Vector3 originalScale;
    public float hoverHeight = 10f;
    public float hoverSpeed = 5f;
    public float clickZoomFactor = 1.2f;
    public float clickSpeed = 5f;

    private bool isHovering;
    private bool isClicked;

    // Static variable to keep track of the currently clicked card
    private static CardVisuals currentlyClickedCard;

    private void Start()
    {
        originalPosition = transform.localPosition;
        originalScale = transform.localScale;
    }

    private void Update()
    {
        HandleCardAnimation();
    }

    private void HandleCardAnimation()
    {
        HandleHoverAnimation();
        HandleClickAnimation();
    }

    private void HandleHoverAnimation()
    {
        Vector3 targetPosition = isHovering ? originalPosition + Vector3.up * hoverHeight : originalPosition;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * hoverSpeed);
    }

    private void HandleClickAnimation()
    {
        Vector3 targetScale = isClicked ? originalScale * clickZoomFactor : originalScale;
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * clickSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentlyClickedCard != null && currentlyClickedCard != this)
        {
            // Reset the previously clicked card
            currentlyClickedCard.ResetCard();
        }

        isClicked = !isClicked;
        if (isClicked)
        {
            currentlyClickedCard = this;
        }
        else
        {
            GameManager.instance.player.CardRange(-1);
            CardManager.instance.selectedCard = null;
            currentlyClickedCard = null;
        }
    }

    private void ResetCard()
    {
        isClicked = false;
        HandleCardAnimation();
    }
}
