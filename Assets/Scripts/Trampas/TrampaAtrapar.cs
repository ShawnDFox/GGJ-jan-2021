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

    private BotEngine CambiarVelocidad;
    private BotHealth QuitarVida;

    private Action<float> EfectoDañoPocoAPoco;
    private CharacterInputs characterInputs;
    private bool jugadorEntro;
    private int counter=0;
    private int originalSpeed;

    private void OnEnable()
    {
        try
        {
            /*interfazBoton = transform.FindChild("Trampa Atrapar").gameObject;
            interfazBoton = GameObject.Find("Trampa Atrapar").gameObject;
            circuloRecarga = interfazBoton.transform.Find("Linea Recarga").GetComponent<Image>();
            */
            interfazBoton.SetActive(false);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
        

    }

    void Start()
    {
        EfectoDañoPocoAPoco = FindObjectOfType<EfectosVisualesVolume>().EfectoViñeta;
        GetComponent<DetectorPlayer>().JugadorEntro += trapped;
        GetComponent<DetectorPlayer>().JugadorSalio += Released;


        //EfectoDañoPocoAPoco= Camera.main.GetComponent<>().
        /*EfectoDañoPocoAPoco=FindObjectOfType<EfectosVisualesVolume>().EfectoViñeta;

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
        */

    }

    private void Released(GameObject obj)
    {
        jugadorEntro = false;
    }

    private void trapped(GameObject obj)
    {
        //obtiene los compoenentes del bot que vienen desde el delegado de detector players
        CambiarVelocidad = obj.GetComponent<BotEngine>();
        QuitarVida = obj.GetComponent<BotHealth>();
        characterInputs = obj.GetComponent<CharacterInputs>();

        originalSpeed = CambiarVelocidad.Velocidad;//obtiene la velocidad el bot
        CambiarVelocidad.ChangeSpeed(originalSpeed / 2);//cambia la velocidad del bot

        Invoke("JugadorDentro", 0.5f);
    }

    private void JugadorDentro(){
        EfectoDañoPocoAPoco(tiempo);
        interfazBoton.SetActive(true);
        characterInputs.OnInteract += AtrapandoJugador;
        CambiarVelocidad.ChangeSpeed(0);//acceso al metodo que tiene botEngine
        jugadorEntro = true;
        StartCoroutine(SacarAlJugadorTrampa());
    }
    IEnumerator SacarAlJugadorTrampa(){
        yield return new WaitForSeconds(tiempo);
        QuitarVida.TakeDamage(cantidadVidaAQuitar);
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
        CambiarVelocidad.ChangeSpeed(originalSpeed);//acceso al metodo que tiene botEngine reestablecer velocidad
        characterInputs.OnInteract -= AtrapandoJugador;
        gameObject.SetActive(false);
    }
}
