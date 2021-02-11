using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BotHealth : MonoBehaviour
{
    [SerializeField]
    public int Salud_max =100;
    [SerializeField]
    public int Carga_max;
    [SerializeField]
    public int Defensa_max;

    public float Carga;
    public int Salud;
    public int Defensa;
    
    public bool Infrarojo;
    public bool CanMove;


    public event Action<int> SetHP;
    public event Action<int> SetPower;
    public event Action<float> OnCharge;
    public event Action<float> OnDisCharge;
    public event Action<float> OnTakeDamage;
    public event Action<float> OnHeal;

    public event Action OnPlayerDischarge;
    public event Action OnPlayerTotalDamage;
    public event Action OnEnablePlayer = delegate { };


    private void OnEnable()
    {
        SetHP(Salud_max);
        SetPower(Carga_max);
        Salud = Salud_max;
        Carga = Carga_max;
        
        CanMove = false;
        OnHeal(Salud);
        OnCharge(Carga);
        OnEnablePlayer();
        //OnplayerSlow += SlowCalc; subscripcion para metodos que alenticen al jugador

    }

    
    public void TakeDamage(int damage)
    {
        Salud -= damage;
        OnTakeDamage(Salud);
    }

    public void Heal(int damage)
    {
        Salud += damage;
        OnHeal(Salud);
    }

    public void Charge(float amount)
    {
        if (Carga < Carga_max )

            Carga += amount;

        OnCharge(Carga);
    }

    public void DisCharge(float amount)
    {
        Carga -= amount;
        OnDisCharge(Carga);
    }

    private void Update()
    {
        if (Carga <= 0)
        {
            OnPlayerDischarge?.Invoke();
        }
        if (Salud <= 0)
        {
            OnPlayerTotalDamage?.Invoke();
        }
        if (Carga > 0 && CanMove)
        {
            DisCharge(0.005f);
        }

    }


}
