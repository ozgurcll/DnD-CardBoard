using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    // public GameObject[] arena2Variants; // 2. arenanın 10 farklı varyasyonunu içeren dizi
    // public GameObject[] arena3Variants; // 3. arenanın 10 farklı varyasyonunu içeren dizi
    // // Diğer arenalar için de aynı şekilde diziler eklenebilir.

    // public GameObject currentArenaInstance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadNextArena(int arenaIndex)
    {
        // Mevcut arenayı temizle
        // if (currentArenaInstance != null)
        // {
        //     Destroy(currentArenaInstance);
        // }

        // Arena varyasyonlarını seç
        switch (arenaIndex)
        {
            case 2:
                SceneManager.LoadScene("Arena2");
                break;
            case 3:
                SceneManager.LoadScene("Arena3");
                break;
            // Diğer arenalar için case'ler ekleyin
            default:
                Debug.LogError("Geçersiz arena indeksi!");
                return;
        }
    }

    // public void LoadNextArena(int arenaIndex)
    // {
    //     // Mevcut arenayı temizle
    //     if (currentArenaInstance != null)
    //     {
    //         Destroy(currentArenaInstance);
    //     }

    //     GameObject[] selectedArenaVariants = null;

    //     // Arena varyasyonlarını seç
    //     switch (arenaIndex)
    //     {
    //         case 2:
    //             selectedArenaVariants = arena2Variants;
    //             break;
    //         case 3:
    //             selectedArenaVariants = arena3Variants;
    //             break;
    //         // Diğer arenalar için case'ler ekleyin
    //         default:
    //             Debug.LogError("Geçersiz arena indeksi!");
    //             return;
    //     }

    //     // Rastgele bir varyasyonu seç
    //     int variationIndex = Random.Range(0, selectedArenaVariants.Length);
    //     currentArenaInstance = Instantiate(selectedArenaVariants[variationIndex]);

    //     // Yeni arenayı sahneye ekle (konum, rotasyon, ölçek ayarları gerekebilir)
    //     currentArenaInstance.transform.position = currentArenaInstance.transform.position;
    // }
}
