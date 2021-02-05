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
    public GameObject Discharge;
    public GameObject Lose;
    public GameObject Creditos;
    public GameObject GameUI;

    bool canpause = false;
    private void Awake()
    {
        Sound = GetComponent<SoundManager>();
        GetComponent<QuestController>().TerminarNivel += Complete;
        
    }

    private void Complete(GameObject obj)
    {
        Win.SetActive(true);
        
    }

    private void Start()
    {
        Menu.SetActive(true);
        Pause.SetActive(false);
        Win.SetActive(false);
        Discharge.SetActive(false);
        GameUI.SetActive(false);
    }

    private void DischargeHandler()
    {
        Discharge.SetActive(true);
        canpause = false;
    }
    private void WinHandler()
    {
        Win.SetActive(true);
        canpause = false;
    }

    public void GameStart()
    {
        Menu.SetActive(false);
        Pause.SetActive(false);
        Win.SetActive(false);
        Discharge.SetActive(false);
        GameUI.SetActive(true);
        Sound.Level.TransitionTo(0.5f);
        GameManager.Instance.FirstLevel();
        FindObjectOfType<BotHealth>().OnEnablePlayer += HideExtraWindows;
        FindObjectOfType<BotHealth>().OnPlayerDischarge += DischargeHandler;
        FindObjectOfType<BotHealth>().OnPlayerTotalDamage += LoseHandler;
        FindObjectOfType<CharacterInputs>().PauseGame += TooglePause;
        canpause = true;

    }

    private void HideExtraWindows()
    {
        Lose.SetActive(false);
        Discharge.SetActive(false);
        Win.SetActive(false);
        canpause = true;
    }

    private void LoseHandler()
    {
        Lose.SetActive(true);
        canpause = false;
    }

    public void GameStop()
    {
        Menu.SetActive(true);
        Pause.SetActive(false);
        Win.SetActive(false);
        Discharge.SetActive(false);
        GameUI.SetActive(false);
        Sound.Menu.TransitionTo(1.5f);

        FindObjectOfType<BotHealth>().OnEnablePlayer -= HideExtraWindows;
        FindObjectOfType<BotHealth>().OnPlayerDischarge -= DischargeHandler;
        FindObjectOfType<BotHealth>().OnPlayerTotalDamage -= LoseHandler;
        FindObjectOfType<CharacterInputs>().PauseGame -= TooglePause;
        canpause = false;
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

    public void TooglePause()
    {
        if (canpause)
        {
            if (Pause.activeSelf == true)
            {
                Pause.SetActive(false);
            }
            else
            {
                Pause.SetActive(true);
            }
        }
    }

    
}
