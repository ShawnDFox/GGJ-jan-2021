using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private SoundManager Sound;

    public GameObject Menu;
    public GameObject Pause;
    public GameObject Win;
    public GameObject Lose;
    public GameObject Creditos;
    public GameObject GameUI;

    private void Awake()
    {
        Sound = GetComponent<SoundManager>();
        
    }

    private void Start()
    {
        Menu.SetActive(true);
        Pause.SetActive(false);
        Win.SetActive(false);
        Lose.SetActive(false);
        GameUI.SetActive(false);
    }

    private void LooseHandler()
    {
        Lose.SetActive(true);
    }
    private void WinHandler()
    {
        Win.SetActive(true);
    }

    public void GameStart()
    {
        Menu.SetActive(false);
        Pause.SetActive(false);
        Win.SetActive(false);
        Lose.SetActive(false);
        GameUI.SetActive(true);
        Sound.Level.TransitionTo(0.5f);
        GameManager.Instance.FirstLevel();
        FindObjectOfType<BotHealth>().OnPlayerLose += LooseHandler;
    }

    public void GameStop()
    {
        Menu.SetActive(true);
        Pause.SetActive(false);
        Win.SetActive(false);
        Lose.SetActive(false);
        GameUI.SetActive(false);
        Sound.Menu.TransitionTo(1.5f);
    }

    public void ToogleCredits()
    {
        if (Creditos.activeSelf == true)
        {
            Creditos.SetActive(false);
        }
        else {
            Creditos.SetActive(true);
        }
    }
}
