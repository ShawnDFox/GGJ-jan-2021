using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterVars : MonoBehaviour
{
    public int Carga;
    public int Salud;
    public int Velocidad;
    public bool Infrarojo;
    public int Defensa;

    public event Action OnPlayerLose;

    private void OnEnable()
    {
        //OnplayerSlow += SlowCalc; subscripcion para metodos que alenticen al jugador
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        var charger = collision.GetComponent<Charger>();
        if (charger != null)
        {
            Charge(charger.amount);
        }
    }*/

    private void Charge(int amount)
    {
        Carga -= amount;
    }

    private void TakeDamage(int damage)
    {
        Salud -= damage; 
    }

    private void Update()
    {
        if (Carga <= 0 || Salud <= 0)
        {
            OnPlayerLose?.Invoke();
        }
        
    }

   
}
