using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{

    [SerializeField] private int cantidadAcargar = 2;
    [SerializeField] private float tiempoEspera = 0.5f;
    //private event Action<float> BajarBateria;
    float ct = 0;
    [SerializeField] bool canCharge;

    private void Awake()
    {
        GetComponent<DetectorPlayer>().MientrasJugadorEsta += Activate;
        //GetComponent<DetectorPlayer>().JugadorSalio += Disable;
    }
    private void OnEnable()
    {
        canCharge = true;
    }

    private void Disable(GameObject obj)
    {
        canCharge = false;
        
    }

    private void Activate(GameObject obj)
    {

        if (canCharge)
        {

            var hp = obj.GetComponent<BotHealth>();
            if (ct >= tiempoEspera)
            {
                hp.Charge(cantidadAcargar);
                ct = 0;
                if (hp.Carga >= hp.Carga_max)
                {
                    canCharge = false;
                }
            }
            else
            {
                ct += Time.deltaTime;
            }
            
        }
    }
}
