using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaManager : MonoBehaviour
{
    public event Action FadeAnimation;
    [SerializeField] private bool escenaSinPausa=false;
    [SerializeField]private float retraso;
    [Header("Pause")]
    [SerializeField]private GameObject menuPause;
    private bool pauseON=false;
    private int level;

    private void Start() { 
        if(!escenaSinPausa) FindObjectOfType<CharacterInputs>().PauseGame+=Pause;
    }
    /*private void Update() {
        //Checks if the scene requires to pause
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }

    }*/
    //Pause
    public void Pause(){
        pauseON = !pauseON;
        if (pauseON)
        {
            Time.timeScale=0f;
            menuPause.SetActive(true);
        }
        else
        {
            Time.timeScale=1f;
            menuPause.SetActive(false);
        }
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
        if(pauseON) Pause();
        level=levelToChoose;
        FadeAnimation();
        Invoke("Level",retraso);
    }
    void Level(){
        SceneManager.LoadScene(level);
    }
    public void RestartLevel(){
        if(pauseON) Pause();
        FadeAnimation();
        Invoke("Restart",retraso);
    }
    void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
#endregion
}
