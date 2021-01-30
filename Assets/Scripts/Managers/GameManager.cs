using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public QuestController Quests;

    public GameObject player;
    
    //public TrapManager ;
    //Public questGiver;

    int Currentlevel=1;
    bool CanMove;
    bool IsPlaying;

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


    }

    // Start is called before the first frame update
    void Start()
    {
        player.SetActive(true);
    }
    //posiblemente activado desde el menu
    public void FirstLevel()
    {
        /*
              relocate object in scene randomizer code posiblemente parte de questController

              relocate player to the new spawn point and give a charge level
              

         */
        Quests.GenerateQuest(Currentlevel);//Give first quest
    }

    // Update is called once per frame
    void Update()//tal vez cambiar por un metodo el cual lea de quest manager
    {
        /* if(Questgiver.completed == True && Player can move)
           {
               currentlevel ++;

               //codigo de transicion del nivel

               relocate object in scene randomizer code
               relocate player to the new spawn point and give a charge level

               if (currentlevel % 5=0)//cada 5 niveles
               {
                 Dar mejora al jugador
               }

               Quests.GenerateQuest(Currentlevel)
           }else
           {
             //reiniciar nivel
             reuvicar el jugador a la salida ya existente y darle un nivel de carga

           }
         */
    }
}
