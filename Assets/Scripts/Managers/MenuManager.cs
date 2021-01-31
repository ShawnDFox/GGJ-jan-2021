using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public GameObject Menu;
    public GameObject Pause;
    public GameObject Win;
    public GameObject Lose;
    public GameObject Creditos;
    public GameObject GameUI;

    private void Awake()
    {
        
        
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
