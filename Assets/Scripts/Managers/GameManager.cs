using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public QuestController Quests;

    public GameObject player;
    Transform PlayerstartPos;
    
    
    //public TrapManager ;
    //Public questGiver;
    [SerializeField]
    int Currentlevel=1;
    bool CanMove;
    bool IsPlaying;

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
        questmanager.TerminarNivel += nextlevel;
        player.GetComponent<BotHealth>().OnPlayerLose += PlayerNoControl;
        PlayerstartPos = player.transform;
        //player.GetComponent<CharacterInputs>().OnInteract += restart;//necesito boton o control para reiniciar

    }

    private void restart()
    {
        if (!CanMove)
        {
            StartCoroutine(RestartIEnum());
        }

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
        startlevel();
        yield return null;
        yield break;
    }


    void startlevel()
    {
        CanMove = true;
        player.GetComponent<BotEngine>().CanMove = CanMove;
        player.GetComponent<BotHealth>().CanMove = CanMove;
        
    }

    private void nextlevel(GameObject obj)
    {
        PlayerNoControl();

        Currentlevel++;

        restart();

    }

   
    //posiblemente activado desde el menu
    
    public void QuestAnim()
    {

    }

    public void FirstLevel()
    {
        QuestAnim();//cambiar por corutina para evitar activar el personaje? o buscar la forma que el personaje no pierda energia cuando no puede moverse
        /*
              play the timeline possibly....
              relocate object in scene randomizer code posiblemente parte de questController

              relocate player to the new spawn point and give a charge level
         */
        //Quests.GenerateQuest(Currentlevel);//Give first quest
        player.SetActive(true);
        
        questmanager.GenerateQuest(Currentlevel);//posiblemente dentro de coorutina

        startlevel();

    }

}
