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
    bool canCharge = true;
    private void Awake()
    {
        GetComponent<DetectorPlayer>().MientrasJugadorEsta += Activate;
        GetComponent<DetectorPlayer>().JugadorSalio += Disable;
    }

    private void Disable(GameObject obj)
    {
        
        
    }

    private void Activate(GameObject obj)
    {
        if (canCharge)
        {
            var hp = obj.GetComponent<BotHealth>();
            if (ct >= tiempoEspera)
            {
                hp.DisCharge(cantidadAcargar);
                ct = 0;
            }
            else
            {
                ct += Time.deltaTime;
            }
            canCharge = false;
        }


    }
}
