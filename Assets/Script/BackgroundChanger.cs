using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundChanger : MonoBehaviour
{
    public Image backgroundImage; // Arka plan için Image bileşeni
    public Sprite[] backgrounds; // Arka plan resimleri
    public float transitionTime = 2f; // Geçiş süresi
    public float waitTime = 5f; // Geçişler arasındaki bekleme süresi

    private int currentBackgroundIndex = 0;

    void Start()
    {
        StartCoroutine(BackgroundTransitionRoutine());
    }

    IEnumerator BackgroundTransitionRoutine()
    {
        while (true)
        {
            // Geçerli arka planı karart
            yield return StartCoroutine(FadeOut());

            // Yeni arka planı yükle
            currentBackgroundIndex = (currentBackgroundIndex + 1) % backgrounds.Length;
            backgroundImage.sprite = backgrounds[currentBackgroundIndex];

            // Yeni arka planı aydınlat
            yield return StartCoroutine(FadeIn());

            // Bir sonraki geçişten önce bekle
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator FadeOut()
    {
        Color color = backgroundImage.color;
        for (float t = 0; t < transitionTime; t += Time.deltaTime)
        {
            color.a = Mathf.Lerp(1, 0, t / transitionTime);
            backgroundImage.color = color;
            yield return null;
        }
        color.a = 0;
        backgroundImage.color = color;
    }

    IEnumerator FadeIn()
    {
        Color color = backgroundImage.color;
        for (float t = 0; t < transitionTime; t += Time.deltaTime)
        {
            color.a = Mathf.Lerp(0, 1, t / transitionTime);
            backgroundImage.color = color;
            yield return null;
        }
        color.a = 1;
        backgroundImage.color = color;
    }
}
