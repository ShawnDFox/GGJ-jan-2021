using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public QuestController Quests;
    public DialogoManager dialogos;

    public GameObject player;
    [SerializeField]
    Transform PlayerstartPos;
    
    
    //public TrapManager ;
    //Public questGiver;
    [SerializeField]
    int Currentlevel=1;
    bool CanMove;
    bool CanPause;
    bool IsPlaying;

    public event Action playerEnabled;
    public event Action LevelRestarted = delegate { };
    private QuestController questmanager;

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else {
            Destroy(this);
        }
        #endregion

        
        questmanager = GetComponent<QuestController>();
        questmanager.TerminarNivel += Complete;
        player.GetComponent<BotHealth>().OnPlayerDischarge += PlayerLose;
        player.GetComponent<BotHealth>().OnPlayerTotalDamage += PlayerLose;
        //PlayerstartPos = player.transform.position;
        player.GetComponent<CharacterInputs>().PauseGame += Pause;//necesito boton o control para reiniciar

    }

    private void PlayerLose()
    {
        PlayerNoControl();
        CanPause = false;
    }

    private void Complete(GameObject obj)
    {
        PlayerNoControl();
        CanPause = false;
    }

    private void Pause()
    {
        if (CanPause)
        {
            if (Time.timeScale == 1)
            {
                PlayerNoControl();
                Time.timeScale = 0;

            }
            else
            {
                startlevel();
                Time.timeScale = 1;

            }
        }
    }

    public void restart()
    {
        if (!CanMove)
        {
            StartCoroutine(RestartIEnum());
        }

    }
    void startlevel()
    {
        CanMove = true;
        
        player.GetComponent<BotEngine>().CanMove = CanMove;
        player.GetComponent<BotHealth>().CanMove = CanMove;

    }
    private void PlayerNoControl()
    {
        CanMove = false;

        player.GetComponent<BotEngine>().CanMove = CanMove;
        player.GetComponent<BotHealth>().CanMove = CanMove;
        
        //reiniciar nivel
        //codigo de transicion del nivel
        //reuvicar al jugador
    }
    public IEnumerator RestartIEnum()
    {
        player.SetActive(false);
        
        yield return new WaitForSeconds(1);
        player.transform.position = new Vector2(PlayerstartPos.position.x, PlayerstartPos.position.y);

        player.GetComponent<BotHealth>().Carga = player.GetComponent<BotHealth>().Carga_max;
        player.SetActive(true);
        questmanager.GenerateQuest(Currentlevel);
        LevelRestarted?.Invoke();
        startlevel();
        CanPause = true;
        yield return null;
        yield break;
    }

    public void nextlevel()
    {
        Currentlevel++;
        restart();
    }

   
    //posiblemente activado desde el menu
    

    public void FirstLevel()
    {
        //cambiar por corutina para evitar activar el personaje? o buscar la forma que el personaje no pierda energia cuando no puede moverse
        /*
              play the timeline possibly....
              relocate object in scene randomizer code posiblemente parte de questController

              relocate player to the new spawn point and give a charge level
         */
        //Quests.GenerateQuest(Currentlevel);//Give first quest
        player.transform.position = new Vector2(PlayerstartPos.position.x, PlayerstartPos.position.y);
        player.SetActive(true);
        playerEnabled();
        
        questmanager.GenerateQuest(Currentlevel);//posiblemente dentro de coorutina

        startlevel();
        CanPause = true;

    }

    public void quit()
    {
        PlayerNoControl();
        player.SetActive(false);
        Currentlevel = 1;
    }
}
