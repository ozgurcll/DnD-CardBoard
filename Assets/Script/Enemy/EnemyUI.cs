using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    private Enemy enemy;

    private Canvas canvas;
    private Image healthFill;
    private TextMeshProUGUI health;
    private void Start()
    {
        enemy = GetComponent<Enemy>();
        canvas = GetComponentInChildren<Canvas>();
        healthFill = GetComponentInChildren<Canvas>().transform.Find("HealthUI").Find("HealthFill").GetComponent<Image>();
        health = GetComponentInChildren<Canvas>().transform.Find("HealthUI").GetComponentInChildren<TextMeshProUGUI>();
        canvas.enabled = false;
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        healthFill.fillAmount = (float)enemy.stats.currentHealth / enemy.stats.GetMaxHealthValue();
        health.text = $"{enemy.stats.currentHealth}/{enemy.stats.GetMaxHealthValue()}";
    }

    public void ShowHealthUI()
    {
        UpdateHealthUI();
        canvas.enabled = true;
    }

    public void HideHealthUI()
    {
        UpdateHealthUI();
        canvas.enabled = false;
    }

}
