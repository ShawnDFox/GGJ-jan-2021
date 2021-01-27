using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaManager : MonoBehaviour
{
    public Action FadeAnimation;
    [SerializeField]private float retraso;
    [Header("Pause")]
    [SerializeField]private GameObject menuPause;
    private bool sceneNoPause;
    private bool pauseON;
    private int level;

    void Start()
    {
        
        pauseON=false;
        //Checa si es la primera escena, por lo cuál no requiere pausa
        /*if(SceneManager.GetActiveScene().buildIndex==0){
            sceneNoPause=true;
        }*/
    }
    private void Update() {
        //Checks if the scene requires to pause
        if(!sceneNoPause){
            if(Input.GetKeyDown(KeyCode.P)){
                pauseON=!pauseON;
                if(pauseON){
                    Pause(0);
                    menuPause.SetActive(true);
                } else {
                    Pause(1);
                    menuPause.SetActive(false);
                }
            }    
        }
        
    }
    //Pause
    public void Pause(int time){
        Time.timeScale=time;
        //Se pone este if en caso de que un boton de pausa desactive la pausa
        if(time==1) pauseON=false;
    } 
    
#region Music
    private void PlayOrPauseMusic(int play){
        //Play
        if(play==1){
            AudioListener.pause=false;
        } 
        //Pause
        else {
            AudioListener.pause=true;
        }
        
    }
#endregion 

#region Scenes
    public void NextLevel(){
        ChooseLevel((SceneManager.GetActiveScene().buildIndex)+1);
    }
    public void ChooseLevel(int levelToChoose){
        Pause(1);
        level=levelToChoose;
        FadeAnimation();
        Invoke("Level",retraso);
    }
    void Level(){
        SceneManager.LoadScene(level);
    }
    public void RestartLevel(){
        Pause(1);
        FadeAnimation();
        Invoke("Restart",retraso);
    }
    void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
#endregion
}
