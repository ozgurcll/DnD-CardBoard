using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject pauseGameUI;

    private UI_PauseGame pauseGame;

    private void Awake()
    {
        pauseGame = GetComponentInChildren<UI_PauseGame>();
    }
    private void Start()
    {
        SwitchTo(inGameUI);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchWithKeyTo(pauseGameUI);
        }
    }
    public void SwitchTo(GameObject _menu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if (_menu != null)
        {

            _menu.SetActive(true);
        }
    }

    public void SwitchWithKeyTo(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            Animator animator = _menu.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("Close"); // Kapanış animasyonunu başlatır
                StartCoroutine(DelayedSwitchTo());
            }
            return;
        }

        _menu.SetActive(true); // Menü açılır
        Animator openAnimator = _menu.GetComponentInChildren<Animator>();
        if (openAnimator != null)
        {
            openAnimator.SetTrigger("Open"); // Açılış animasyonunu başlatır
        }

    }


    IEnumerator DelayedSwitchTo()
    {
        yield return new WaitForSeconds(.5f);
        CheckForInGameUI();
    }

    private void CheckForInGameUI()
    {
        SwitchTo(inGameUI);
    }
}
