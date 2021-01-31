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
        player.GetComponent<BotHealth>().OnPlayerLose += RestartLevel;
        PlayerstartPos = player.transform;
    }

    private void RestartLevel()
    {
        //reiniciar nivel
        //codigo de transicion del nivel
        //reuvicar al jugador
        player.transform.position = new Vector3( PlayerstartPos.position.x,PlayerstartPos.position.y,PlayerstartPos.position.z);

        questmanager.GenerateQuest(Currentlevel);
    }

    private void nextlevel(GameObject obj)
    {
        Currentlevel++;
        player.transform.position = new Vector3(PlayerstartPos.position.x, PlayerstartPos.position.y, PlayerstartPos.position.z);
        questmanager.GenerateQuest(Currentlevel);


        /* 
               

               //codigo de transicion del nivel

               if (currentlevel % 5=0)//cada 5 niveles
               {
                 Dar mejora al jugador
               }
         */
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
        
    }

}
