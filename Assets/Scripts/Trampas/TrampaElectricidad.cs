using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TrampaElectricidad : MonoBehaviour
{
    [SerializeField]private int cantidadABajar=2;
    [SerializeField]private float tiempoEsperaElectricidad=1.5f;
    [SerializeField]private ParticleSystem particle;
    [SerializeField]private Light2D light;
    //private event Action<float> BajarBateria;
    private bool jugadorDentro;

    private void Start() {
        GetComponent<DetectorPlayer>().JugadorEntro += activateTrap;
        particle.Stop();
        StartCoroutine(ActivarElectricidad());
    }

    void activateTrap(GameObject obj)
    {
        if (particle.isPlaying && !particle.isStopped)
        {
            obj.GetComponent<BotHealth>().DisCharge(cantidadABajar);
        }
    }

    private IEnumerator ActivarElectricidad()
    {
        yield return new WaitForSeconds(tiempoEsperaElectricidad);
        light.enabled=true;
        particle.Play();
        yield return new WaitForSeconds(particle.main.duration);
        light.enabled=false;
        particle.Stop();
        StartCoroutine(ActivarElectricidad());
    }
}
