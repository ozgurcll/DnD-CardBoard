using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Status_UI : MonoBehaviour
{
    private CardManager cardManager;
    public Status status;
    [Header("UI")]
    [SerializeField] private Image statusImage;
    [SerializeField] private TextMeshProUGUI disappearTimeText;
    public bool isSlotFilled = false;



    private void Start()
    {
        cardManager = CardManager.instance;
        status = GetComponent<Status>();
    }

    public bool IsSlotFilled() => isSlotFilled;
    public Image GetStatusImage() => statusImage;
    public TextMeshProUGUI GetDisappearTimeText() => disappearTimeText;


    public void UpdateSlot()
    {
        status.card = cardManager.selectedCard.cardData;
        status.turn = cardManager.selectedCard.cardData.statusTurn;
        isSlotFilled = true;
        statusImage.color = Color.white;
        statusImage.sprite = cardManager.selectedCard.cardiconImage.sprite;
        disappearTimeText.text = cardManager.selectedCard.statusTurn.ToString();
    }
    public void DecreaseTurn()
    {
        Debug.Log("DecreaseTurn");
        status.turn--;
        disappearTimeText.text = status.turn.ToString();
        if (status.turn <= 0)
        {
            CleanUpSlot();
        }
    }

    public void CleanUpSlot()
    {
        isSlotFilled = false;
        statusImage.sprite = null;
        statusImage.color = Color.clear;
        disappearTimeText.text = "";
    }

    public int GetDamage()
    {
        return status.card.statusDamage;
    }
    public int GetTurn()
    {
        return status.turn;
    }
}
