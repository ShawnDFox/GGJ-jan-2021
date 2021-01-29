using System;
using System.Collections;
using UnityEngine;

public class TrampaElectricidad : MonoBehaviour
{
    [SerializeField]private int cantidadABajar=-2;
    [SerializeField]private float tiempoEsperaElectricidad=1.5f;
    [SerializeField]private ParticleSystem particle;
    private event Action<int> BajarBateria;
    private bool jugadorDentro;
    private void Start() {
        BajarBateria=FindObjectOfType<BotHealth>().Charge;
        DetectorPlayer detectorPlayer=GetComponent<DetectorPlayer>();
        detectorPlayer.JugadorEntro= ()=> {if(particle.isPlaying && !particle.isStopped)BajarBateria(cantidadABajar);};
        particle.Stop();
        StartCoroutine(ActivarElectricidad());
    }
    private IEnumerator ActivarElectricidad()
    {
        yield return new WaitForSeconds(tiempoEsperaElectricidad);
        particle.Play();
        yield return new WaitForSeconds(particle.main.duration);
        particle.Stop();
        StartCoroutine(ActivarElectricidad());
    }
}
