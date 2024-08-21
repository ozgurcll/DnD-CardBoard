using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb { get; private set; }

    public PlayerMoveActions playerMove { get; private set; }
    public PlayerAttackActions attackActions { get; private set; }
    public PlayerUI uI { get; private set; }
    public PlayerGridCalculation gridCalculation { get; private set; }
    public CharacterStats stats { get; private set; }

    public GridBehavior gridBehavior;
    public CardManager cardManager;
    public CardActions cardActions;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMove = GetComponent<PlayerMoveActions>();
        attackActions = GetComponent<PlayerAttackActions>();
        uI = GetComponent<PlayerUI>();
        gridCalculation = GetComponent<PlayerGridCalculation>();
        stats = GetComponent<CharacterStats>();

        cardManager = CardManager.instance;
        cardActions = CardManager.instance.cardActions;
    }

    public void CardRange(int range)
    {
        gridCalculation.currentRange = range;
        gridCalculation.HighlightAreaAroundPlayer();
    }



    public void Die()
    {
        Debug.Log("Player died");
    }



    private void OnMouseEnter()
    {
        Color color = new Color(.8f, .8f, .8f, 1);
        uI.statusUI.SetActive(true);
        Renderer rend = GetComponentInChildren<Renderer>();

        rend.material.color = color;
    }

    private void OnMouseExit()
    {
        Renderer rend = GetComponentInChildren<Renderer>();
        uI.statusUI.SetActive(false);
        rend.material.color = Color.white;
    }
}
