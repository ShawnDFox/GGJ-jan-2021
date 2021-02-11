using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaBajarBateria : MonoBehaviour
{
    [SerializeField]private int cantidadABajar=2;
    [SerializeField]private float tiempoEspera=0.5f;
    [SerializeField] private ParticleSystem Particle;
    //private event Action<float> BajarBateria;
    float ct = 0;
    private void Awake()
    {
        GetComponent<DetectorPlayer>().MientrasJugadorEsta += Activate;
        GetComponent<DetectorPlayer>().JugadorEntro += Activate;
        if (Particle.isPlaying)
        {
            Particle.Stop();
        }
    }

    private void Activate(GameObject obj)
    {
        
        var hp = obj.GetComponent<BotHealth>();
        if (ct >= tiempoEspera)
        {

            hp.DisCharge(cantidadABajar);
            if (!Particle.isStopped) {
                Particle.Play();
            }
            ct = 0;
        }
        else {
            if (Particle.isPlaying)
            {
                Particle.Stop();
            }
            ct += Time.deltaTime;
        }


    }

    private void Start() {
        //BajarBateria=FindObjectOfType<BotHealth>().Charge;
        // DetectorPlayer detectorPlayer=GetComponent<DetectorPlayer>();
        //detectorPlayer.MientrasJugadorEsta=()=> {BajarBateria(0.1f);};
    }
}
