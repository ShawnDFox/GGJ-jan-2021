using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{

    [SerializeField] private int cantidadAcargar = 2;
    [SerializeField] private float tiempoEspera = 0.5f;
    [SerializeField] private ParticleSystem Particle;
    //private event Action<float> BajarBateria;
    float ct = 0;
    [SerializeField] bool canCharge;

    private void Awake()
    {
        GetComponent<DetectorPlayer>().MientrasJugadorEsta += Activate;
        GetComponent<DetectorPlayer>().JugadorSalio += Inactivate;
    }

    private void Inactivate(GameObject obj)
    {
        if (Particle.isPlaying)
        {
            Particle.Stop();
            Debug.Log($"Particles stopped");
        }
    }

    private void OnEnable()
    {
        if (Particle.isPlaying)
        {
            Particle.Stop();
            Debug.Log($"Particles stopped");
        }
        canCharge = true;
        
    }

    private void Disable(GameObject obj)
    {
        //canCharge = false;
        
    }

    private void Activate(GameObject obj)
    {
        
        if (canCharge)
        {
            
            var hp = obj.GetComponent<BotHealth>();
            if (ct >= tiempoEspera)
            {
                hp.Charge(cantidadAcargar);

                if (Particle.isStopped)
                {
                    Debug.Log($"Particles trying to play");
                    
                    Particle.Play();
                    //Debug.Log($"IsParticlePlaying?: {Particle.isPlaying}");
                }
                
               
                ct = 0;
                if (hp.Carga >= hp.Carga_max)
                {
                    if (Particle.isPlaying)
                    {
                        Particle.Stop();
                        Debug.Log($"Particles stopped");
                    }
                    //canCharge = false;
                }
            }
            else
            {
                
                ct += Time.deltaTime;
            }
            
        }
    }
}
