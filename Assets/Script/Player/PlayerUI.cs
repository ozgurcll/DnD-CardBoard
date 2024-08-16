using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private Player player;
    [Header("Profile UI")]

    [Header("Fill Image")]
    public Image healthFillImage;
    public Image manaFillImage;
    public Image actionFillImage;

    [Header("Texts")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI actionText;

    [Header("Bool Image")]
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
        actionText.text = player.stats.currentActionPoint.ToString() + "/" + player.stats.GetActionPoints().ToString();
        actionFillImage.fillAmount = (float)player.stats.currentActionPoint / player.stats.GetActionPoints();
        if (player.stats.currentActionPoint <= 0)
        {
            player.stats.isAction = false;
            actionImage.gameObject.SetActive(false);
        }
    }

    public void DownManaPoint()
    {
        player.stats.currentManaPoint -= player.cardManager.selectedCard.cardData.mana;
        manaText.text = player.stats.currentManaPoint.ToString() + "/" + player.stats.GetManaPoints().ToString();
        manaFillImage.fillAmount = (float)player.stats.currentManaPoint / player.stats.GetManaPoints();
    }

    public void ResetActionsUI()
    {
        Debug.Log("Reset Actions UI");
        player.stats.isAction = true;
        player.stats.isAttack = true;

        movementDirectionUI.SetActive(true);
        actionImage.gameObject.SetActive(player.stats.isAction);
        attackImage.gameObject.SetActive(player.stats.isAttack);

        player.stats.currentActionPoint = player.stats.GetActionPoints();
        if (player.stats.currentManaPoint < player.stats.GetManaPoints())
            player.stats.currentManaPoint += 1;

        manaText.text = player.stats.currentManaPoint.ToString() + "/" + player.stats.GetManaPoints().ToString();
        actionText.text = player.stats.currentActionPoint.ToString() + "/" + player.stats.GetActionPoints().ToString();

        actionFillImage.fillAmount = (float)player.stats.currentActionPoint / player.stats.GetActionPoints();
        manaFillImage.fillAmount = (float)player.stats.currentManaPoint / player.stats.GetManaPoints();
    }


    public void UpdateManaUI()
    {
        manaText.text = player.stats.currentManaPoint.ToString() + "/" + player.stats.GetManaPoints().ToString();
        manaFillImage.fillAmount = (float)player.stats.currentManaPoint / player.stats.GetManaPoints();
    }
}
