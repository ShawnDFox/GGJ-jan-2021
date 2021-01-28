using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotEngine : MonoBehaviour
{
    
    [SerializeField]
    private int Velocidad_max;
    public int Velocidad;

    private BotHealth Health;
    private void Awake()
    {
        Health = GetComponent<BotHealth>();
        GetComponent<CharacterInputs>().OnMove += Move;
        //GetComponent<CharacterInputs>().OnInteract += Interact;
    }


    private void Move(float Hor,float vert)
    {
        //
        Debug.Log("Moving");
        if (Hor != 0)
        {
            vert = 0;
        }
        Health.Carga -= 0.05f;

        Vector3 movement = new Vector3(Hor, vert, 0);
        transform.Translate(movement * Time.deltaTime * Velocidad);
    }

    public void ChangeSpeed(int amount){
        Velocidad=amount;
    }
   
    /*private void OnTriggerStay2D(Collider2D collision)
    {
        var charger = collision.GetComponent<Charger>();
        var Discharger = collision.GetComponent<DisCharger>();
        if (charger != null)
        {
            Charge(charger.amount);
        }
        if (Discharger != null)
        {
            DisCharge(charger.amount);
        }
    }*/
}
