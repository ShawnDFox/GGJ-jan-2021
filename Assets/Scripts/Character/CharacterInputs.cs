using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputs : MonoBehaviour
{
    [SerializeField]
    private float Horizontal;
    [SerializeField]
    private float Vertical;
    public bool trapped;

    public event Action<string> Liberarse;
    public event Action<float,float> OnMove;
    //public event Action SalirTrampa;
    public event Action PauseGame;

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        if (Horizontal != 0 || Vertical != 0)
        {
            OnMove(Horizontal,Vertical);
        }

        //Liberarse Trampa
        if (trapped)
        {
            //Izquierda
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Liberarse?.Invoke("Left");
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Liberarse?.Invoke("Right");
            }
        }
        
        //Pausar o despausar juego
        if(Input.GetButtonDown("Pause")){
            PauseGame?.Invoke();
        }
    }
}
