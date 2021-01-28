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
    public event Action<float> OnCharge;
    public event Action<float> OnDisCharge;
    public event Action<float> OnTakeDamage;
    public event Action<float> OnHeal;
    public event Action<int> SetHP;
    public event Action<int> SetPower;

    private void OnEnable()
    {
        Salud = Salud_max;
        Carga = Carga_max;
        SetHP(Salud_max);
        SetPower(Carga_max);
        OnHeal(Salud);
        OnCharge(Carga);
        //OnplayerSlow += SlowCalc; subscripcion para metodos que alenticen al jugador
    }

    
    private void TakeDamage(int damage)
    {
        Salud -= damage;
        OnTakeDamage(Salud);
    }

    private void Heal(int damage)
    {
        Salud += damage;
        OnHeal(Salud);
    }

    public void Charge(int amount)
    {
        if (Carga <= Carga_max)
            Carga += amount;
        OnCharge(Carga);
    }

    private void DisCharge(int amount)
    {
        Carga -= amount;
        OnDisCharge(Carga);
    }

    private void Update()
    {
        if (Carga <= 0 || Salud <= 0)
        {
            OnPlayerLose?.Invoke();
        }
        if (Carga > 0)
        {
            Carga -= 0.005f;
            OnDisCharge(Carga);
        }

    }


}
