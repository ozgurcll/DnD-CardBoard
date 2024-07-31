using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private Player player;
    [Header("Profile UI")]
    public Image actionFillImage;
    public Image actionImage;
    public Image attackImage;

    public GameObject movementDirectionUI;
    public GameObject statusUI;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    public void DownActionPoint()
    {
        player.stats.currentActionPoint--;
        actionFillImage.fillAmount = (float)player.stats.currentActionPoint / 3;
        if (player.stats.currentActionPoint <= 0)
        {
            player.stats.isAction = false;
            actionImage.gameObject.SetActive(false);
        }
    }

    public void ResetActionsUI()
    {
        Debug.Log("ActivePlayerActionsUI");
        player.stats.isAction = true;
        player.stats.isAttack = true;
        movementDirectionUI.SetActive(true);
        actionImage.gameObject.SetActive(player.stats.isAction);
        attackImage.gameObject.SetActive(player.stats.isAttack);

        player.stats.currentActionPoint = player.stats.GetActionPoints();
        actionFillImage.fillAmount = 1;
    }

}
