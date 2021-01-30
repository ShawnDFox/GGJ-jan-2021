using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TrampaAtrapar : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]private GameObject interfazBoton;
    [SerializeField]private Image circuloRecarga;
    [Header("")]
    [SerializeField]private int maximoOprimirVecesLiberarse;
    [SerializeField]private int tiempo;
    [SerializeField]private int cantidadVidaAQuitar;
    private Action<int> CambiarVelocidad;
    private Action<int> QuitarVida;
    private Action<float> EfectoDañoPocoAPoco;
    private CharacterInputs characterInputs;
    private bool jugadorEntro;
    private int counter=0;
    private int originalSpeed;
    void Start()
    {
        //EfectoDañoPocoAPoco= Camera.main.GetComponent<>().
        EfectoDañoPocoAPoco=FindObjectOfType<EfectosVisualesVolume>().EfectoViñeta;

        BotEngine botEngine= FindObjectOfType<BotEngine>();
        CambiarVelocidad=botEngine.ChangeSpeed;
        originalSpeed=botEngine.Velocidad;

        QuitarVida = botEngine.GetComponent<BotHealth>().Heal;

        characterInputs=botEngine.GetComponent<CharacterInputs>();

        DetectorPlayer detectorPlayer=GetComponent<DetectorPlayer>();
        detectorPlayer.JugadorEntro= ()=> {
            CambiarVelocidad(originalSpeed/2);
            Debug.Log("jugador dentro");
            Invoke("JugadorDentro",0.5f);
        };
        detectorPlayer.JugadorSalio= ()=> {
            jugadorEntro=false;
        };
    }
    
    private void JugadorDentro(){
        EfectoDañoPocoAPoco(tiempo);
        interfazBoton.SetActive(true);
        characterInputs.OnInteract += AtrapandoJugador;
        CambiarVelocidad(0);
        jugadorEntro = true;
        StartCoroutine(SacarAlJugadorTrampa());
    }
    IEnumerator SacarAlJugadorTrampa(){
        yield return new WaitForSeconds(tiempo);
        //QuitarVida(cantidadVidaAQuitar);
        SacarJugador();
    }
    private void AtrapandoJugador(){
        if(jugadorEntro && Time.timeScale>0){
            counter++;
            float porcentajeVecesOprimidas=(counter*maximoOprimirVecesLiberarse);
            porcentajeVecesOprimidas/=100;
            circuloRecarga.fillAmount= porcentajeVecesOprimidas;
            //Liberar
            if(counter>=maximoOprimirVecesLiberarse){
                StopCoroutine(SacarAlJugadorTrampa());
                //Retorna todo a la normalidad
                EfectoDañoPocoAPoco(0);
                Invoke("SacarJugador",0.5f);
            }
        }
    }
    private void SacarJugador()
    {
        interfazBoton.SetActive(false);
        circuloRecarga.fillAmount=0;
        CambiarVelocidad(originalSpeed);
        characterInputs.OnInteract -= AtrapandoJugador;
        gameObject.SetActive(false);
    }
}
