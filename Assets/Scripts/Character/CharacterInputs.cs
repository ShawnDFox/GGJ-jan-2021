using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputs : MonoBehaviour
{
    public float Horizontal;
    public float Vertical;
    public bool Interact;

    public event Action OnInteract=delegate { };
    public event Action<float,float> OnMove;
    public event Action SalirTrampa;
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

        Interact = Input.GetButtonDown("Interact");
        if (Interact)
        {
            OnInteract();
        }

        //Pausar o despausar juego
        if(Input.GetButtonDown("Pause")){
            PauseGame?.Invoke();
        }
    }
}
