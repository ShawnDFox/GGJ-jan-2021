using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BotHealth : MonoBehaviour
{
    [SerializeField]
    private int Salud_max;
    [SerializeField]
    private int Carga_max;
    [SerializeField]
    private int Defensa_max;

    public float Carga;
    public int Salud;
    public int Defensa;
    
    public bool Infrarojo;
    

    public event Action OnPlayerLose;
    public event Action OnCharge;

    private void OnEnable()
    {
        Salud = Salud_max;
        Carga = Carga_max;
        //OnplayerSlow += SlowCalc; subscripcion para metodos que alenticen al jugador
    }

    
    private void TakeDamage(int damage)
    {
        Salud -= damage; 
    }

    private void Charge(int amount)
    {
        if (Carga <= Carga_max)
            Carga += amount;
    }

    private void DisCharge(int amount)
    {
        Carga -= amount;
    }

    private void Update()
    {
        if (Carga <= 0 || Salud <= 0)
        {
            OnPlayerLose?.Invoke();
        }

    }


}
