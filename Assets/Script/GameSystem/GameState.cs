using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GameMode
{
    PlayerTurn,
    EnemyTurn
}

public class GameState : GameManager
{
    private void Update()
    {
        switch (currentMode)
        {
            case GameMode.PlayerTurn:
                StartCoroutine(PlayerTurnState());
                break;
            case GameMode.EnemyTurn:
                StartCoroutine(EnemyTurnState());
                break;
        }
    }

    private IEnumerator PlayerTurnState()
    {
        if (!isPlayerTurn)
        {
            isPlayerTurn = true;
            StartPlayerTurn();
        }

        if (!IsAction())
        {
            MovementDirectionImage().SetActive(IsAction());
        }
        if (!IsAction() && !IsAttack())
        {
            EndPlayerTurn();
            yield return new WaitForSeconds(1f);
            isPlayerTurn = false;
        }
    }

    private IEnumerator EnemyTurnState()
    {
        MovementDirectionImage().SetActive(IsAction());
        yield return StartCoroutine(EnemyTurnRoutine());
    }

    private GameObject MovementDirectionImage() => player.uI.movementDirectionUI;
}